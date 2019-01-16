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

		public object GetKey(string key)
		{
			string[] tokens = key.Split('/');

			return null;
		}
	}
}
