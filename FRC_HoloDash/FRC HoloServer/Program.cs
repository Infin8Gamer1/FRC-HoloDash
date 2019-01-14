using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FRC_HoloServer.Server;

namespace FRC_HoloServer
{
	class Program
	{
		static void Main(string[] args)
		{
			NetworkUtil.InitNetworkTable(0);

			Console.WriteLine("Starting Server on port 4089");
			HTTPServer server = new HTTPServer(4089);
			server.Start();

			Console.ReadKey();

			Test();
		}

		public static async void Test()
		{
			NetworkTree tree = new NetworkTree("");

			string myJson = JsonConvert.SerializeObject(tree);

			//NetworkTree myTree = JsonConvert.DeserializeObject<NetworkTree>(myJson);

			using (var client = new HttpClient())
			{
				var response = await client.PostAsync(
					"http://localhost:4089/",
					 new StringContent(myJson, Encoding.UTF8, "application/json"));
			}
		}
	}
}
