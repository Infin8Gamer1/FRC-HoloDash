using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace FRC_HoloServer.USB_Coms
{
	class USBComs
	{
		public SerialPort port;

		public USBComs()
		{
			Connect("COM4", 9600);
		}

		public void Connect(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
		{
			port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

			port.DataReceived += Port_DataReceived;

			port.Open();
		}

		private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort sp = (SerialPort)sender;
			string indata = sp.ReadExisting();
			Console.WriteLine("Data Recived:");
			//Console.Write(indata);
			string[] separatingChars = { "<<", "|" };

			string[] data = indata.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

			string device = data[0];

			Console.WriteLine("Device: " + device);
			for (int i = 1; i < data.Length; i++)
			{
				Console.WriteLine("Command: " + data[i]);
			}
		}

		public void Disconnect()
		{
			port.Close();
		}
	}
}
