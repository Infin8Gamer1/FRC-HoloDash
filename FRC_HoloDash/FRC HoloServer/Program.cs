using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FRC_HoloServer.Server;
using System.Net;
using System.IO;

namespace FRC_HoloServer
{
	class Program
	{
		static void Main(string[] args)
		{
			NetworkUtil.InitNetworkTable(0);

			WebServer ws = new WebServer(SendNetworkTableJSON, "http://localhost:4089/GetNetworkTablesJSON/");
			ws.Run();
			Console.WriteLine("A simple webserver running on port 4089.");
			Console.ReadKey();
			Test();
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
			HttpResponseMessage response = await client.GetAsync("http://localhost:4089/GetNetworkTablesJSON");
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			Console.WriteLine(responseBody);
		}
	}
}
