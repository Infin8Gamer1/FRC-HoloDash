using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace FRC_Holo.API
{
	public class NetworkUtil
	{
		private static NetworkUtil instance = null;

		public static NetworkUtil GetInstance() {
			if (instance == null)
			{
				instance = new NetworkUtil();
			}
			return instance;
		}

		private NetworkElement tree;

		private HttpClient client;

		public NetworkUtil()
		{
			client = new HttpClient();

			tree = null;
		}

		public async void UpdateNtTable()
		{
			LoadNetworkFromJSON(await GetTableJSON());
		} 

		private void LoadNetworkFromJSON(string json)
		{
			this.tree = JsonConvert.DeserializeObject<NetworkElement>(json);
		}

		private async Task<string> GetTableJSON()
		{
			UriBuilder uri = new UriBuilder("http://infinitepc:4089/GetNetworkTablesJSON");

			HttpResponseMessage response = await client.GetAsync(uri.Uri);
			response.EnsureSuccessStatusCode();
			string responseBody = await response.Content.ReadAsStringAsync();

			return responseBody;
		}

		public object GetKey(string inputKey)
		{
			if(tree != null)
			{
				string[] tokens = inputKey.Split('/');

				NetworkElement myElement = tree;
				int x = 0;

				while (myElement.Key != tokens.Last())
				{
					var matches = myElement.Children.Where(ntItem => ntItem.Key == tokens[x]);
					if (matches.Count() > 0 && matches.First() != null)
					{
						myElement = matches.First();
					}
					else
					{
						throw new Exception($"Key {tokens[x]} Not Found!");
					}

					x++;

				}

				return myElement.Value;
			} else {
				return null;
			}
			
		}

		public void Shutdown()
		{
			client.Dispose();
		}
	}
}
