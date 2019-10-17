﻿using System.IO.Ports;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;

namespace Serial_communication
{
    public class Program
    {
        static SerialPort serial;
        
        public static void Main()
        {
            // initialize the serial port for COM1 (using D0 & D1)
            serial = new SerialPort(SerialPorts.COM1, 9600, Parity.None, 8, StopBits.One);
            // open the serial-port, so we can send & receive data
            serial.Open();
            // add an event-handler for handling incoming data
            serial.DataReceived += new SerialDataReceivedEventHandler(serial_DataReceived);
            

            // wait forever...
            Thread.Sleep(Timeout.Infinite);
        }

        static void serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // create a single byte array
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            byte[] bytes = new byte[1];

            // as long as there is data waiting to be read
            while (serial.BytesToRead > 0)
            {
                // read a single byte
                serial.Read(bytes, 0, bytes.Length);
                led.Write(true);
                // send the same byte back
                serial.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
