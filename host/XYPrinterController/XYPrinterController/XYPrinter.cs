using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// holds a serial port which should be connected to a XYPrinter device
// deals with sending commands, and delegating responses
namespace XYPrinterController
{
    public class XYPrinter
    {
        public SerialPort port;
        public readonly bool awaitingResponse;
        public delegate void ResponseDelegate(List<string> responses);

        Object lockingObj = new Object();

        private struct Command
        {
            public string command;
            public List<dynamic> args;
            public ResponseDelegate callback;
        }

        private List<Command> commandQueue = new List<Command>();

        private StringBuilder responseBuffer = new StringBuilder();

        public XYPrinter(string serialPortName)
        {
            port = new SerialPort(serialPortName);

            port.BaudRate = 57600;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            port.DataBits = 8;
            port.Handshake = Handshake.None;
            port.RtsEnable = false;
            port.Encoding = Encoding.ASCII;

            port.DataReceived += new SerialDataReceivedEventHandler(DataHandler);
            port.Open();
        }

        ~XYPrinter()
        {
            Debug.WriteLine("CLOSING SERIAL PORT");
            if(port.IsOpen)
            {
                port.Close();
            }
        }

        public bool SendQuery(string command, ResponseDelegate callback)
        {
            if (port.IsOpen)
            {
                this.InsertIntoCommandBuffer(command, new List<dynamic>(), callback);
                return true;
            } else
            {
                return false;
            }
        }

        public bool SendCommand(string command, List<dynamic> args, ResponseDelegate callback)
        {
            if(port.IsOpen) {
                this.InsertIntoCommandBuffer(command, args, callback);
                return true;
            } else
            {
                return false;
            }
        }

        private void InsertIntoCommandBuffer(string command, List<dynamic> args, ResponseDelegate callback)
        {
            Command commandRep = new XYPrinterController.XYPrinter.Command();
            commandRep.command = command;
            commandRep.args = args;
            commandRep.callback = callback;
            commandQueue.Add(commandRep);

            // if this was the most recent thing on the command buffer, then go ahead
            // and send it out over the serial port, if the count is more than one
            // then that means we're waiting for a serial command response;
            if (commandQueue.Count() == 1)
            {
                this.SendNextCommand();
            }
        }

        //sends the command at the top of the command buffer
        private void SendNextCommand()
        {
            Command commandRep = commandQueue.First();
            if(commandRep.args.Count() > 0)
            {
                port.Write('!' + commandRep.command + ' ');
            } else
            {
                port.Write('?' + commandRep.command + ' ');
            }

            for (int i = 0; i < commandRep.args.Count(); i++)
            {
                port.Write(Convert.ToString(commandRep.args[i]));
                if (i < commandRep.args.Count() - 1)
                {
                    port.Write(",");
                }
            }
            port.Write(";");
        }

        private void DataHandler(object sender, SerialDataReceivedEventArgs e)
        {
            lock(lockingObj)
            {
                while (port.BytesToRead > 0)
                {
                    // add the characters to the buffer string
                    char c = (char)port.ReadChar();
                    responseBuffer.Append(c);
                    if (c == ';')
                    {
                        this.ParseResponseBuffer();
                    }
                }
            }

        }

        private void ParseResponseBuffer()
        {
            // responses will be of the following format
            // !result1, result2, result3...;

            bool foundResponseStart = false;
            List<StringBuilder> responses = new List<StringBuilder>();
            for(int i = 0; i < responseBuffer.Length; i++)
            {
                char c = responseBuffer[i];
                if(foundResponseStart)
                {
                    if(c != ',' && c != ';')
                    {
                        responses.Last().Append(c);
                    } else
                    {
                        if(c == ',')
                        {
                            responses.Add(new StringBuilder());
                        }
                        if(c == ';')
                        {
                            //found the end of the command, need to call it's delegate
                            this.CallDelegate(responses);
                        }
                    }
                } else
                {
                    if(c == '!')
                    {
                        //now just need to parse the args
                        foundResponseStart = true;
                        responses.Add(new StringBuilder());
                    }
                }
            }
            responseBuffer.Clear();
        }

        private void CallDelegate(List<StringBuilder> responses)
        {
            List<string> stringResponses = new List<string>();
            foreach(StringBuilder response in responses)
            {
                stringResponses.Add(response.ToString());
            }
            
            // call the command and then remove it from the queue
            commandQueue.First().callback(stringResponses);
            commandQueue.RemoveAt(0);

            if(commandQueue.Count > 0)
            {
                this.SendNextCommand();
            }
        }


    }
}
