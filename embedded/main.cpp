#include "mbed.h"
#include "DebounceIn.h"
#include "PrintHead.h"
#include "SerialCommander.h"
#include "Ticker.h"

DigitalOut left_led(LED1);
DigitalOut right_led(LED4);
DigitalOut test_led2(LED2);
DigitalOut test_led3(LED3);

LocalFileSystem local("local");

// DebounceIn left_pb(p27);
// DebounceIn right_pb(p28);

PrintHead y_head(p13, p14, p26, p15, p16);
PrintHead x_head(p11, p12, p25, p17, p18);

enum positioning_mode {ABS, REL};
enum positioning_mode current_positioning_mode = ABS;

int feedrate = 500;
int update_rate = 500; //Hz
int x_goal = 0;
int y_goal = 0;
float d_x_tick = 0;
float d_y_tick = 0;
int traverse_timer_total_ticks = 0;
int traverse_timer_current_tick = 0;
bool should_move = false;
bool traversing = false;

Ticker move_ticker;

void move_tick() {
	if(should_move) {
		if(traverse_timer_current_tick++ == traverse_timer_total_ticks) {
			should_move = false;
			SerialCommander::instance()->send_ack();
		} else {
			x_head.goal += d_x_tick;
			y_head.goal += d_y_tick;
		}
	}
}

void initiate_move(int end_x, int end_y) {
	int d_x = end_x - x_head.count;
	int d_y = end_y - y_head.count;
	float dist = sqrt((float)(d_x * d_x + d_y * d_y));
	float traverse_time = dist / feedrate;
	traverse_timer_total_ticks = update_rate * traverse_time;
	traverse_timer_current_tick = 0;
	d_x_tick = d_x / (float)traverse_timer_total_ticks;
	d_y_tick = d_y / (float)traverse_timer_total_ticks;
	//char buff[200];
	//sprintf(buff, "dx: %i\r\n dy: %i\r\n; dist: %f\r\n time: %f\r\n, total ticks: %i\r\n d_x_tick: %f\r\n d_y_tick: %f\r\n", d_x, d_y, dist, traverse_time, traverse_timer_total_ticks, d_x_tick, d_y_tick);
	//SerialCommander::instance()->send_response(buff);
	should_move = true;
}

void serial_heartbeat_callback(char argv[10][100], int argc) {
	SerialCommander::instance()->send_ack();
}

void serial_name_callback(char argv[10][100], int argc) {
	if(argc == 1) {
		FILE *fp = fopen("/local/name.txt", "w");
		if(fp != NULL) {
			fprintf(fp, "%s", argv[0]);
			fclose(fp);
			SerialCommander::instance()->send_ack();
		} else {
			SerialCommander::instance()->send_nack();
		}
	} else if(argc == 0) {
		FILE *fp = fopen("/local/name.txt", "r");
		if(fp != NULL) {
			char buff[100];
			fgets(buff, 100, fp);
			SerialCommander::instance()->send_response(buff);
			fclose(fp);
		} else {
			SerialCommander::instance()->send_response((char *)"unnamed");
		}
	} else {
		SerialCommander::instance()->send_nack();
	}
}

void serial_position_callback(char argv[10][100], int argc) {
	if(argc == 0) {
		// just return the position
		char res[10][100];
		sprintf(res[0], "%i", x_head.count);
		sprintf(res[1], "%i", y_head.count);
		SerialCommander::instance()->send_return(res, 2);
	} else if(argc == 2) {
		int x = atoi(argv[0]);
		int y = atoi(argv[1]);
		if(current_positioning_mode == ABS) {
			x_head.count = x;
			y_head.count = y;
		} else if(current_positioning_mode == REL) {
			x_head.count += x;
			y_head.count += y;
		}
		SerialCommander::instance()->send_ack();
	} else {
		SerialCommander::instance()->send_nack();
	}
}

void serial_positioning_mode_callback(char argv[10][100], int argc) {
	if(argc == 1) {
		char mode_char = argv[0][0];
		if(mode_char == 'a') {
			current_positioning_mode = ABS;
			SerialCommander::instance()->send_ack();
		} else if(mode_char == 'r') {
			current_positioning_mode = REL;
			SerialCommander::instance()->send_ack();
		} else {
			SerialCommander::instance()->send_nack();
		}
	} else {
		if(current_positioning_mode == ABS) {
			SerialCommander::instance()->send_response((char *)"a"); 
		} else if(current_positioning_mode == REL) {
			SerialCommander::instance()->send_response((char *)"r");
		} else {
			SerialCommander::instance()->send_nack();
		}
	}
}

// sets the feedrate in ticks/second, or returns the current feedrate
void serial_feedrate_callback(char argv[10][100], int argc) {
	if(argc == 1) {
		feedrate = atoi(argv[0]);
		SerialCommander::instance()->send_ack();
	} else if(argc == 0) {
		char res[20];
		sprintf(res, "%i", feedrate);
		SerialCommander::instance()->send_response(res);
	} else {
		SerialCommander::instance()->send_nack();
	}
}

void serial_move_callback(char argv[10][100], int argc) {
	if(argc == 2 && !traversing) {
		int x = atoi(argv[0]);
		int y = atoi(argv[1]);
		if(current_positioning_mode == ABS) {
			initiate_move(x, y);
		} else if(current_positioning_mode == REL) {
			initiate_move(x_head.count + x, y_head.count + y);
		} else {
			SerialCommander::instance()->send_nack();
		}
	} else {
		SerialCommander::instance()->send_nack();
	}
}

void register_commands() {

	// heartbeat
	SerialCommander::instance()->register_command((char*)"h", 0, serial_heartbeat_callback);
	
	// get/set name
	SerialCommander::instance()->register_command((char*)"n", 1, serial_name_callback);
	
	// get/set position (does not move, just sets ref position)
	SerialCommander::instance()->register_command((char*)"p", 2, serial_position_callback);
	
	// get/set feedrate
	SerialCommander::instance()->register_command((char*)"f", 1, serial_feedrate_callback);
	
	// get/set positioning mode
	SerialCommander::instance()->register_command((char*)"pm", 1, serial_positioning_mode_callback);
	
	// move
	SerialCommander::instance()->register_command((char *)"m", 2, serial_move_callback);
}

int main() {
	y_head.kp = 0.08;
	y_head.ki = 0.0005;
	y_head.kd = 0;

	x_head.kp = 0.08; //was 0.1 before ki added
	x_head.ki = 0.005;
	x_head.kd = 0;

	SerialCommander::instance()->setup();
	register_commands();

	move_ticker.attach(&move_tick, 1/(float)update_rate);

	while(1) {

	}
}
