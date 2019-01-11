using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables;

namespace FRC_HoloServer
{
	class Program
	{
		public const int TeamNumber = 4089;

		USB_Coms.USBComs coms;

		public void Start()
		{
			Console.WriteLine("Starting...");

			//set up Network Tables
			NetworkTable.SetClientMode();

			NetworkTable.SetTeam(TeamNumber);
			//NetworkTable.SetIPAddress("10.40.89.2");
			//NetworkTable.SetPort(NetworkTable.DefaultPort);

			NetworkTable.SetUpdateRate(0.1);
			//NetworkTable.SetDSClientEnabled(true);
			NetworkTable.SetNetworkIdentity(TeamNumber.ToString() + " Hololens");

			//NetworkTable.Initialize();

			coms = new USB_Coms.USBComs();
		}

		public void Shutdown()
		{
			Console.WriteLine("Shuting Down...");

			coms.Disconnect();

			NetworkTable.Shutdown();
		}

		static int Main(string[] args)
		{

			Program program = new Program();

			program.Start();
			
			Console.WriteLine("Press any key to shutdown.");
			Console.ReadKey();

			program.Shutdown();
			return 0;
		}
	}
}
