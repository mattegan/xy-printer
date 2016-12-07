#include "mbed.h"

// debug is on
// #define DEBUG 1;

class SerialCommander {	
	
public:

    Serial _term;

	static SerialCommander *shared_instance;

	struct command {
		char *name;
		int param_count;
		void (*callback)(char argv[10][100], int);
	};

	int num_commands;
	command commands[100];

	int serial_buffer_size;
	char serial_buffer[100+(10*100)];
	
	//----- static member functions -----//
	SerialCommander();
	static SerialCommander *instance();
	static void static_serial_irq();

	//----- member functions -----//
	void register_command(char *name, int param_count, void (*callback)(char[10][100],int));
	void setup();
	void handle_serial_character();
	void handle_serial_command();
	void delegate_command(char *command_name, char argv[10][100], int argc);
	void send_response(char *response);
	void send_ack();
	void send_nack();
	void send_command(char *command_name, char argv[10][100], int argc);
	void send_return(char argv[10][100], int argc);

};

