#include "mbed.h"
#include "DebounceIn.h"
#include "PrintHead.h"
#include "SerialCommander.h"

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
int feedrate = 0;

void serial_heartbeat_callback(char argv[10][100], int argc) {
	SerialCommander::instance()->send_ack();
}

void serial_name_callback(char argv[10][100], int argc) {
	// SerialCommander::instance()->_term.printf("argument count: %i\r\n", argc);
	// SerialCommander::instance()->_term.printf("argument 0: \"%s\"\r\n", argv[0]);
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
	if(argc == 2) {
		
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
	SerialCommander::instance()->register_command((char *)"m", 2, serial_move_callback)
}

int main() {
	SerialCommander::instance()->setup();
	register_commands();

	while(1) {

	}
}
