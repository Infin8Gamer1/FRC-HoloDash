using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using FRC_Holo.API;

namespace FRC_HoloServer
{
	class Program
	{
		static void Main(string[] args)
		{
			NetworkUtil.InitNetworkTable(4089);

			WebServer ws = new WebServer(SendNetworkTableJSON, "http://*:4089/GetNetworkTablesJSON/");
			ws.Run();
			Console.WriteLine("A simple webserver running on port 4089.");
			//Console.ReadKey();
			//Test();
			Console.WriteLine("Press any key to quit.exe");
			Console.ReadKey();
			ws.Stop();
		}

		public static string SendNetworkTableJSON(HttpListenerRequest request)
		{
			return NetworkUtil.ConvertTableToJSON();
		}

		public static async void Test()
		{

			HttpClient client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync("http://infinitepc:4089/GetNetworkTablesJSON");
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine("\n");

			NetworkElement tree = NetworkUtil.ConvertJSONToNetworkElement(responseBody);

			tree.PrintTable();

			object result = NetworkUtil.GetKey("TestRoot", tree);

			Console.WriteLine("Get Key Result: " + result.ToString());
		}
	}
}
