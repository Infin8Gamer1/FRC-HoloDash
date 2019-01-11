using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;

namespace FRC_HoloDash.USB_Comms
{
	class UsbCOMS
	{
		#region Singleton
		private static UsbCOMS instance = null;
		private static readonly object padlock = new object();

		public static UsbCOMS Instance {
			get {
				lock (padlock)
				{
					if (instance == null)
					{
						instance = new UsbCOMS();
					}
					return instance;
				}
			}
		}
		#endregion Singleton

		UsbCOMS()
		{

		}

		public string GetString(string Key)
		{
			return "Need To Implement Get String";
		}

		public bool GetBoolean(string Key)
		{
			return false;
		}

		public double GetNumber(string Key)
		{
			return -999999.9999;
		}
	}
}
