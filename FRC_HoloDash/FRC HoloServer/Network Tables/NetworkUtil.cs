using NetworkTables;
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
		/// <summary>
		/// Used to Initilize the network tables
		/// </summary>
		/// <param name="team">pass 0 to use localhost</param>
		/// <param name="useDriverStation"></param>
		public static void InitNetworkTable(int team, bool useDriverStation = true)
		{
			NetworkTable.SetClientMode();
			if (team == 0)
				NetworkTable.SetIPAddress("localhost");
			else
				NetworkTable.SetTeam(team);
			NetworkTable.SetUpdateRate(0.1);
			NetworkTable.SetDSClientEnabled(useDriverStation);
			NetworkTable.SetNetworkIdentity("HoloDash");
			NetworkTable.Initialize();
		}

		public static object GetKey(string inputKey, NetworkElement tree)
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
		}

		public static string ConvertTableToJSON()
		{
			NetworkElement tree = new NetworkElement();
			
			//filter out camera publisher as it is heckin big
			for (int i = 0; i < tree.Children.Count; i++)
			{
				if(tree.Children.ElementAt(i).Key == "CameraPublisher")
				{
					tree.Children.RemoveAt(i);
				}
			}

			string myJson = JsonConvert.SerializeObject(tree);

			return myJson;
		}

		public static NetworkElement ConvertJSONToNetworkElement(string json)
		{
			return JsonConvert.DeserializeObject<NetworkElement>(json);
		}

		/// <summary>
		/// The SmartDashboard network table path
		/// </summary>
		public static NetworkTable SmartDashboard {
			get {
				return NetworkTable.GetTable("/SmartDashboard");
			}
		}

		/// <summary>
		/// Gets the .NET type of a NetworkTable simple value
		/// </summary>
		/// <param name="v">The value to check</param>
		public static Type TypeOf(Value v)
		{
			switch (v?.Type)
			{
				case NtType.Boolean:
					return typeof(bool);
				case NtType.BooleanArray:
					return typeof(bool[]);
				case NtType.Double:
					return typeof(double);
				case NtType.DoubleArray:
					return typeof(double[]);
				case NtType.String:
					return typeof(string);
				case NtType.StringArray:
					return typeof(string[]);
				case NtType.Raw:
					return typeof(byte[]);
				case NtType.Rpc:
					//remote procedure call, should not be trying to handle these
					throw new InvalidCastException("Can't handle bindings to remote procedure calls");
				default:
					return null;
			}
		}

		/// <summary>
		/// Converts the NetworkTable simple value into an actual .NET data value
		/// </summary>
		/// <param name="v">The value to convert</param>
		public static object ReadValue(Value v)
		{
			switch (v?.Type)
			{
				case NtType.Boolean:
					return v.GetBoolean();
				case NtType.BooleanArray:
					return v.GetBooleanArray();
				case NtType.Double:
					return v.GetDouble();
				case NtType.DoubleArray:
					return v.GetDoubleArray();
				case NtType.String:
					return v.GetString();
				case NtType.StringArray:
					return v.GetStringArray();
				default:
					return null;
			}
		}

		/// <summary>
		/// Gets the URL of the MJPEG stream for the specified camera resource
		/// </summary>
		/// <param name="cameraName">The name of the camera, usually from GetCameras()</param>
		/// <returns>The URL of the stream, or null if a suitable stream doesn't exist</returns>
		public static string GetCameraStreamURL(string cameraName)
		{
			NetworkTable cam = NetworkTable.GetTable($"/CameraPublisher/{cameraName}");
			string[] streams = cam.GetStringArray("streams", new string[] { });
			//get the first stream that's a normal IP address
			foreach (string stream in streams)
			{
				string matchIPv4WithPort = @"mjpg:(http:\/\/(\d{1,3}\.){3}\d{1,3}:\d+.*)";
				if (Regex.IsMatch(stream, matchIPv4WithPort))
				{
					return Regex.Replace(stream, matchIPv4WithPort, @"$1");
				}
			}
			return null;
		}

		/*/// <summary>
		/// Gets the names of all cameras attached to the robot
		/// </summary>
		/// <returns>A collection of camera names, or an empty collection if the robot has no cameras or is disconnected</returns>
		public static IEnumerable<string> GetCameras()
		{
			NetworkTable network = GetTableOutline("CameraPublisher");
			foreach (NetworkElement elm in network.Children)
			{
				if (elm.Type == typeof(NetworkTree))
				{
					yield return elm.Key;
				}
			}
		}*/
	}
}
