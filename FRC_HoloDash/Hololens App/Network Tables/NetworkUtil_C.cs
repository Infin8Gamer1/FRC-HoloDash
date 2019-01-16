using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FRC_Holo.API
{
	public class NetworkUtil
	{
		private NetworkElement tree;

		public void LoadNetworkFromJSON(string json)
		{
			tree = JsonConvert.DeserializeObject<NetworkElement>(json);
		}

		public object GetKey(string inputKey)
		{
			string[] tokens = inputKey.Split('/');

			NetworkElement myElement = tree;
			int x = 0;

			while (myElement.Key != tokens.Last())
			{
				var matches = myElement.Children.Where(ntItem => ntItem.Key == tokens[x]);
				if(matches.Count() > 0 && matches.First() != null)
				{
					myElement = matches.First();
				} else {
					throw new Exception($"Key {tokens[x]} Not Found!");
				}

				x++;
				
			}

			return myElement.Value;
		}
	}
}
