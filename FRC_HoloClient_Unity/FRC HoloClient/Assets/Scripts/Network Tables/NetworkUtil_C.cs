using System;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;

namespace FRC_Holo.API
{
	public class NetworkUtil
	{
		private static NetworkUtil instance = null;
		private static readonly object instanceLock = new object();

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
				// Use the () operator to raise the event.
				handler(this, e);
			}
		}

		private NetworkElement tree;

		public NetworkUtil()
		{
			tree = null;
		}

		public IEnumerator UpdateNtTable()
		{
			UnityWebRequest www = UnityWebRequest.Get("http://infinitepc:4089/GetNetworkTablesJSON/");
			yield return www.Send();

			if (www.isError)
			{
				Debug.Log(www.error);
			} else {
				// Show results as text
				//Debug.Log(www.downloadHandler.text);
				string json = www.downloadHandler.text;

				if (json != null && json != "")
				{
					tree = JsonConvert.DeserializeObject<NetworkElement>(json);
				}

				OnRaiseNetworkUpdatedEvent(new NetworkUpdatedEvent(tree));
			}
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
							throw new Exception("Key " + tokens[x] + "Not Found!");
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
	}

	public class NetworkUpdatedEvent : EventArgs
	{
		public NetworkUpdatedEvent(NetworkElement tree)
		{
			this.tree = tree;
		}

		public NetworkElement tree;

	}
}