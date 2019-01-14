using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetworkTables;

namespace FRC_HoloServer
{
	class Program
	{
		

		static void Main(string[] args)
		{
			InitNetworkTable(0);

			Console.ReadKey();

			Test();
		}

		public static void InitNetworkTable(int team, bool useDriverStation = true)
		{
			NetworkTable.SetClientMode();
			if (team == 0)
				NetworkTable.SetIPAddress("localhost");
			else
				NetworkTable.SetTeam(team);
			NetworkTable.SetUpdateRate(0.1);
			NetworkTable.SetDSClientEnabled(useDriverStation);
			NetworkTable.SetNetworkIdentity("HoloDash");
			NetworkTable.Initialize();
		}

		public static async void Test()
		{
			NetworkTree tree = new NetworkTree("");

			string myJson = JsonConvert.SerializeObject(tree);

			//NetworkTree myTree = JsonConvert.DeserializeObject<NetworkTree>(myJson);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(
					"http://localhost:80/",
					 new StringContent(myJson, Encoding.UTF8, "application/json"));
			}
		}
	}
}
