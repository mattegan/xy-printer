#include "mbed.h"

/** PrintHead allows you to control a inkjet printer carriage.
 * This library expects the DC motor is controlled using three outputs,
 * one PWM output should set the speed, and the other two should turn the motor
 * on in a the left and right direction when brought high. The implementation
 * circuitry is an implementation detail of which the library has no concern.
 *
 * The library also expects two interrupt pins that are used as inputs from the
 * two photo interrupter outputs from the carriage. If the carriage is moving
 * to the right the "right encoder" input should rise while the "left encoder" 
 * or fall while the "left encoder" is low. The opposite should be true for the
 * "left encoder". If in doubt, try one, output the count continuously to a
 * serial terminal. Positive numbers are to the right on the count. If it's
 * backwards, just flip the writing or the declaration of your instance.
 */

class PrintHead {
public:

    // positive is to the right
    // negative is to the left
    int count;
    float goal;
    int last_error;
    float error_integral, output, iterm, dterm;
    float dt;
		float ki, kp, kd;

    /** Constructor
     * 
     * @param PinName left_motor_pin when this pin is high, the carriage should move left
     * @param PinName right_motor_pin when this pin is high, the carriage should move right
     * @param PinName motor_pwm_pin this should control the speed of the motor
     * @param PinName left_encoder_pin the encoder output on the left side of the carriage
     * @param PinName right_encoder_pin the encoder output on the right side of the carriage
     *
     * The initial goal and count position is 0. These are public so change
     * or read them at will. The class will attempt to servo at whatever goal
     * position is set.
     */ 
    PrintHead(PinName left_motor_pin, 
              PinName right_motor_pin, 
              PinName motor_pwm_pin,
              PinName left_encoder_pin,
              PinName right_encoder_pin) :
              _left_motor(left_motor_pin),
              _right_motor(right_motor_pin),
              _motor_speed(motor_pwm_pin),
              _left_encoder(left_encoder_pin),
              _right_encoder(right_encoder_pin) {
        
        // change the motor pwm speed for smooth motor operation
        // if the pwm frequency is too low the motor jerks the
        // print head along the track as the PWM toggles on and off
        _motor_speed.period(0.00001);
        _motor_speed = 0.6;
        
        _left_encoder.rise(this, &PrintHead::left_encoder_rising);
        _left_encoder.fall(this, &PrintHead::left_encoder_falling);
        _right_encoder.rise(this, &PrintHead::right_encoder_rising);
        _right_encoder.fall(this, &PrintHead::right_encoder_falling);
        
        dt = 0.001;
        _update_motor_ticker.attach_us(this, &PrintHead::update_motor, 1000);
        
        count = 0;
        goal = 0;
        error_integral = 0;
								
				kp = kd = ki = 0;
    }
        
    void left_encoder_rising() {
        if(_right_encoder) count--;
        else count++;
    }
    
    void left_encoder_falling() {
        if(_right_encoder) count++;
        else count--;
    }
    
    void right_encoder_rising() {
        if(_left_encoder) count++;
        else count--;
    }
    
    void right_encoder_falling() {
        if(_left_encoder) count--;
        else count++;
    }
    
    void update_motor() {
        _left_motor = 0;
        _right_motor = 0;
        
        // gate between 0.4 and 1;
        // it should be 1 if we're more than 200 away from the goal
        // between 200 and 0 from the goal, that should be 1 -> 0.4
        int error = goal - count;
        //
//        float kp = 0.005;
//        float ki = 0.01;
//        float kd = 0.0002;

        //float kp = 0.045;
        //float kp = 0.0225;
        //float ki = 0.015;
        //float kd = 0.0001;fd
        
				//float kp = 0.01; //was 0.1 before ki added
				//float ki = 0.005;
			  //float kd = 0;
			
        error_integral += error * dt;
        iterm = ki * error_integral;
        dterm = kd * ((error - last_error) / dt);
        
        output = ((kp * error) + iterm + dterm);
        
        if(abs(output) > 1) _motor_speed = 1;
        else _motor_speed = abs(output);
        
        _left_motor = (output < 0);
        _right_motor = (output > 0);
        
        last_error = error;
    }
    
private:
    DigitalOut _left_motor;
    DigitalOut _right_motor;
    PwmOut _motor_speed;
    InterruptIn _left_encoder;
    InterruptIn _right_encoder;
    Ticker _update_motor_ticker;
    
};
