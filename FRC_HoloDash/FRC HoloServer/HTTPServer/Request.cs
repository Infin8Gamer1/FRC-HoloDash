using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRC_HoloServer.Server
{
	public class Request
	{
		public string Type { get; set; }
		public string URL { get; set; }
		public string Host { get; set; }
		public string Referer { get; set; }


		private Request(string type, string url, string host, string referer)
		{
			Type = type;
			URL = url;
			Host = host;
			Referer = referer;
		}

		public static Request GetRequest(string request)
		{
			if (string.IsNullOrEmpty(request))
			{
				return null;
			}
			//to get the first two tokens we must split on ' '
			string[] tokens = request.Split(' ');
			string type = tokens[0];
			string url = tokens[1];

			//to get the rest of the tokens we must split on '\n' and ": "
			string[] sep = { "\n", ": " };
			tokens = request.Split(sep, StringSplitOptions.None);

			string host = "";
			string referer = "";
			

			for (int i = 0; i < tokens.Length; i++)
			{
				if (tokens[i] == "Host")
				{
					host = tokens[i + 1];
				}

				if (tokens[i] == "Referer")
				{
					referer = tokens[i + 1];
				}
				
			}

			return new Request(type, url, host, referer);
		}
	}
}
