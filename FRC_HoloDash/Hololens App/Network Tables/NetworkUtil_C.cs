using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FRC_Holo.API
{
	public static class NetworkUtil
	{

		public static NetworkElement ConvertJSONToNetworkElement(string json)
		{
			return JsonConvert.DeserializeObject<NetworkElement>(json);
		}
	}
}
