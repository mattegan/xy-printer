/*
    Copyright (c) 2010 Andy Kirkham
 
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
 
    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.
 
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/
 
#ifndef DEBOUNCEIN_H
#define DEBOUNCEIN_H
 
#include "mbed.h"

/** DebounceIn adds mechanical switch debouncing to DigitialIn.
 *
 * Example:
 * @code
 * #include "mbed.h"
 * #include "DebounceIn.h"
 *
 * DebounceIn  d(p5);
 * DigitialOut led1(LED1);
 * DigitialOut led2(LED2);
 *
 * int main() {
 *     while(1) {
 *         led1 = d;
 *         led2 = d.read();
 *     }
 * }
 * @endcode
 *
 * @see set_debounce_us() To change the sampling frequency.
 * @see set_samples() To alter the number of samples.
 *
 * Users of this library may also be interested in PinDetect library:-
 * @see http://mbed.org/users/AjK/libraries/PinDetect/latest
 *
 * This example shows one input displayed by two outputs. The input
 * is debounced by the default 10ms.
 */
 
class DebounceIn : public DigitalIn {
    public:
    
        /** set_debounce_us
         *
         * Sets the debounce sample period time in microseconds, default is 1000 (1ms)
         *
         * @param int i The debounce sample period time to set.
         */        
        void set_debounce_us(int i) { _ticker.attach_us(this, &DebounceIn::_callback, i); }
        
        /** set_samples
         *
         * Defines the number of samples before switching the shadow 
         * definition of the pin. 
         *
         * @param int i The number of samples.
         */        
        void set_samples(int i) { _samples = i; }
        
        /** read
         *
         * Read the value of the debounced pin.
         */
        int read(void) { return _shadow; }
        
#ifdef MBED_OPERATORS
        /** operator int()
         *
         * Read the value of the debounced pin.
         */
        operator int() { return read(); }
#endif  

        /** Constructor
         * 
         * @param PinName pin The pin to assign as an input.
         */
        DebounceIn(PinName pin) : DigitalIn(pin) { _counter = 0; _samples = 10; set_debounce_us(1000); };
        
    protected:
        void _callback(void) { 
            if (DigitalIn::read()) { 
                if (_counter < _samples) _counter++; 
                if (_counter == _samples) _shadow = 1; 
            }
            else { 
                if (_counter > 0) _counter--; 
                if (_counter == 0) _shadow = 0; 
            }
        }
        
        Ticker _ticker;
        int    _shadow;
        int    _counter;
        int    _samples;
};
 
#endif
