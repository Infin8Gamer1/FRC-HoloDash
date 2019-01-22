using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace FRC_Holo.API
{
	public class NetworkUtil
	{
		private static NetworkUtil instance = null;
		private static readonly Object instanceLock = new object();

		public static NetworkUtil GetInstance() {
			lock (instanceLock)
			{
				if (instance == null)
				{
					instance = new NetworkUtil();
				}

				return instance;
			}
		}

		public EventHandler<NetworkUpdatedEvent> networkUpdatedHandler;

		protected virtual void OnRaiseNetworkUpdatedEvent(NetworkUpdatedEvent e)
		{
			// Make a temporary copy of the event to avoid possibility of
			// a race condition if the last subscriber unsubscribes
			// immediately after the null check and before the event is raised.
			EventHandler<NetworkUpdatedEvent> handler = networkUpdatedHandler;

			// Event will be null if there are no subscribers
			if (handler != null)
			{
				// Format the string to send inside the CustomEventArgs parameter
				e.message += $" at {DateTime.Now}";

				// Use the () operator to raise the event.
				handler(this, e);
			}
		}

		private NetworkElement tree;

		private HttpClient client;

		public NetworkUtil()
		{
			HttpBaseProtocolFilter RootFilter = new HttpBaseProtocolFilter();

			RootFilter.CacheControl.ReadBehavior = HttpCacheReadBehavior.NoCache;
			RootFilter.CacheControl.WriteBehavior = HttpCacheWriteBehavior.NoCache;

			client = new HttpClient(RootFilter);

			tree = null;
		}

		public async void UpdateNtTable()
		{
			LoadNetworkFromJSON(await GetTableJSON());

			OnRaiseNetworkUpdatedEvent(new NetworkUpdatedEvent(""));
		} 

		private void LoadNetworkFromJSON(string json)
		{
			if(json != null && json != "")
			{
				tree = JsonConvert.DeserializeObject<NetworkElement>(json);
			}
		}

		private async Task<string> GetTableJSON()
		{
			try
			{
				UriBuilder uri = new UriBuilder("http://infinitepc:4089/GetNetworkTablesJSON/");

				HttpResponseMessage response = await client.GetAsync(uri.Uri);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();

				return responseBody;
			} catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return null;
			
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

					try
					{
						if (matches.Count() > 0 && matches.First() != null)
						{
							myElement = matches.First();
						}
						else
						{
							throw new Exception($"Key {tokens[x]} Not Found!");
						}
					} catch (Exception e) {
						Console.WriteLine(e.Message);
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

	public class NetworkUpdatedEvent : EventArgs
	{
		public NetworkUpdatedEvent(string s)
		{
			message = s;
		}

		public string message;
	}
}
