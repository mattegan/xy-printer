#include "SerialCommander.h"

//----- Initalizers -----//

// by default, we're using the USB serial connection
SerialCommander::SerialCommander() : _term(USBTX, USBRX){}
	

//----- static members -----//
	
void SerialCommander::static_serial_irq() {
	shared_instance->handle_serial_character();
}

// returns the singular instance
SerialCommander *SerialCommander::instance() {
	if(!shared_instance) {
			shared_instance = new SerialCommander;
	}
	return shared_instance;
}

//----- member functions -----//

void SerialCommander::register_command(char *name, int param_count, void (*callback)(char argv[10][100], int)) {
	
	command new_command = {};
	new_command.name = name;
	new_command.param_count = param_count;
	new_command.callback = callback;
	
	commands[num_commands++] = new_command;
}

void SerialCommander::handle_serial_character() {
	while(_term.readable()) {
		char c = _term.getc();

#ifdef DEBUG
		_term.putc(c);
#endif	

		serial_buffer[serial_buffer_size++] = c;
		if(c == ';') {

#ifdef DEBUG
			_term.printf("\r\nhandling command now!\r\n");
#endif

			this->handle_serial_command();
		}
	}
}

void SerialCommander::handle_serial_command() {
	// wait until the first ! or ?, then the characters until a space
	// are the command name, next should be comma delimited parameters
	
	bool found_command_start = false;
	bool is_query = false;
	bool found_command_name = false;

	int command_name_size = 0;
	char command_name[100];

	int current_param = 0;
	int current_param_size = 0;
	char command_params[10][100];

	for(int i = 0; i < serial_buffer_size; i++) {
		char c = serial_buffer[i];
		if(found_command_start) {
			if(found_command_name) {
				// if this is a command, not a query, then we need to parse
				// all of the params, if it's a query, go ahead through the
				// list to exhaust the buffer (inefficient, but works for now)
				if(c != ';') {
					if(!is_query) {
						if(c == ',') {
							command_params[current_param][current_param_size++] = '\0';
							current_param++;
							current_param_size = 0;
						} else {
							command_params[current_param][current_param_size++] = c;
						}
					} else {
						continue;
					}
				} else {
					if(!is_query) {
						command_params[current_param++][current_param_size++] = '\0';
					}
					break;
				}
			} else {
				if(c == ' ' || c == ';') {
					found_command_name = true;
					command_name[command_name_size++] = '\0';
				} else {
					command_name[command_name_size++] = c;
				}
			}
		} else {
			if(c == '?' || c == '!') {
				found_command_start = true;
				is_query = c == '?';
			}
		}
	}

#ifdef DEBUG	
	_term.printf("command name: \"%s\"\r\n", command_name);
	for(int i = 0; i < current_param; i++) {
		_term.printf("param %i : \"%s\"\r\n", i, command_params[i]);
	}
#endif

	this->delegate_command(command_name, command_params, current_param);

	serial_buffer_size = 0;
	command_name[0] = '\0';
}

// looks through the command list and calls the right callback
void SerialCommander::delegate_command(char *command_name, char argv[10][100], int argc) {

	for(int i = 0; i < num_commands; i++) {
		command current_command = commands[i];

#ifdef DEBUG
		_term.printf("looking at command: \"%s\"\r\n", current_command.name);
#endif		

		if(strcmp(current_command.name, command_name) == 0) {
			current_command.callback(argv, argc);
		}
	}
}

void SerialCommander::send_command(char *command_name, char argv[10][100], int argc) {
	_term.printf("!%s ", command_name);
	for(int i = 0; i < argc; i++) {
		_term.printf("%s", argv[i]);
		if(i < argc - 1) {
			_term.putc(',');
		}
	}
	_term.putc(';');

#ifdef DEBUG
	_term.printf("\r\n");
#endif

}

void SerialCommander::send_return(char argv[10][100], int argc) {
	_term.putc('!');
	for(int i = 0; i < argc; i++) {
		_term.printf("%s", argv[i]);
		if(i < argc - 1) {
			_term.putc(',');
		}
	}
	_term.putc(';');
}

// response must not have ! or ; or ?
void SerialCommander::send_response(char *response) {
	_term.printf("!%s;", response);

#ifdef DEBUG
	_term.printf("\r\n");
#endif

}

void SerialCommander::send_ack() {
	this->send_response((char*)"ack");
}

void SerialCommander::send_nack() {
	this->send_response((char*)"nack");
}

void SerialCommander::setup() {
	_term.baud(57600);
	_term.attach(static_serial_irq, Serial::RxIrq);

	num_commands = 0;
	serial_buffer_size = 0;
}

SerialCommander *SerialCommander::shared_instance = 0;
