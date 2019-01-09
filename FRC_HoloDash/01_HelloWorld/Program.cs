using System;
using Windows.ApplicationModel.Core;
using Urho;
using Urho.Actions;
using Urho.SharpReality;
using Urho.Shapes;
using Urho.Gui;
using System.Collections.Generic;
using NetworkTables;
using System.Net;
using Windows.Networking;

namespace FRC_HoloDash
{
	internal class Program
	{
		[MTAThread]
		static void Main() => CoreApplication.Run(
			new UrhoAppViewSource<HoloDashApp>(new ApplicationOptions("Data"))
		);
	}

	public class HoloDashApp : StereoApplication
	{
		public int TeamNumber = 4089;

		Node HUDNode;

		// For HL optical stabilization (optional)
		public override Vector3 FocusWorldPoint => HUDNode.WorldPosition;

		public HoloDashApp(ApplicationOptions opts) : base(opts) { }

		protected override async void Start()
		{
			//base.Start() creates a basic scene
			base.Start();

			

			//set up Network Tables
			NetworkTable.SetClientMode();

			//NetworkTable.SetTeam(TeamNumber);
			NetworkTable.SetIPAddress("10.40.89.2");
			NetworkTable.SetPort(NetworkTable.DefaultPort);

			NetworkTable.SetUpdateRate(0.1);
			//NetworkTable.SetDSClientEnabled(true);
			NetworkTable.SetNetworkIdentity(TeamNumber.ToString() + " Hololens");

			NetworkTable.Initialize();

			bool result = NetworkTable.GetTable("").PutString("PutTest", "this is a test to see if we can put data");

			NetworkTree tree = new NetworkTree("");

			//register cortana commands
			await RegisterCortanaCommands(new Dictionary<string, Action> {
					{"Help", Help },
					{"Hud Up", HUDUp },
					{"Hud Down", HUDDown }
				});

			// Create a node for the Hud
			HUDNode = Scene.CreateChild();
			HUDNode.Position = new Vector3(0, 0, 1); // One meter away from the center

			HUDNode.CreateComponent<WidgetManager>();

			TagAlong tagAlong = HUDNode.CreateComponent<TagAlong>();
			tagAlong.LeftCamera = LeftCamera.Node;

			
		}

		#region Cortana Commands
		async void Help()
		{
			await TextToSpeech("Available commands are:");
			foreach (var cortanaCommand in CortanaCommands.Keys)
				await TextToSpeech(cortanaCommand);
		}

		void HUDUp()
		{
			HUDNode.GetComponent<TagAlong>().HeightPosition += 0.1f;
		}

		void HUDDown()
		{
			HUDNode.GetComponent<TagAlong>().HeightPosition -= 0.1f;
		}

		#endregion Cortana Commands

		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);

		}

		protected override void Stop()
		{
			base.Stop();

			//stop the network tables
			NetworkTable.Shutdown();
		}
	}
}