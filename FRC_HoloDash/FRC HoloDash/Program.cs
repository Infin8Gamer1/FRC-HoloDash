using System;
using Windows.ApplicationModel.Core;
using Urho;
using Urho.Actions;
using Urho.SharpReality;
using Urho.Shapes;
using Urho.Gui;
using System.Collections.Generic;
using FRC_Holo.API;

namespace FRC_HoloServer
{
	internal class Program
	{
		[MTAThread]
		static void Main() => CoreApplication.Run(
			new UrhoAppViewSource<HoloDashApp>(
				new ApplicationOptions("Data")));
	}

	public class HoloDashApp : StereoApplication
	{
		Node HUDNode;

		// For HL optical stabilization (optional)
		public override Vector3 FocusWorldPoint => HUDNode.WorldPosition;

		public HoloDashApp(ApplicationOptions opts) : base(opts) { }

		protected override async void Start()
		{
			// base.Start() creates a basic scene
			base.Start();

			//register cortana commands
			await RegisterCortanaCommands(new Dictionary<string, Action> {
					{"Help", Help },
					{"Hud Up", HUDUp },
					{"Hud Down", HUDDown },
					{"Hud Push", HUDPush },
					{"Hud Pull", HUDPull },
					{"Toggle Tag Along", ToggleTagAlong },
					{"Enable Tag Along", EnableTagAlong },
					{"Disable Tag Along", DisableTagAlong }
				});

			// Create a node for the Hud and add components to it
			HUDNode = Scene.CreateChild();

			HUDNode.CreateComponent<WidgetManager>();

			HUDNode.CreateComponent<NetworkManager>();

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

		async void HUDPush()
		{
			bool enabled = HUDNode.GetComponent<TagAlong>().TagAlongEnabled;

			if (enabled)
			{
				HUDNode.GetComponent<TagAlong>().PositionOffset += 0.2f;
			} else {
				await TextToSpeech("Only available when HUD Tag Along is enabled");
			}
			
		}

		async void HUDPull()
		{
			bool enabled = HUDNode.GetComponent<TagAlong>().TagAlongEnabled;

			if (enabled)
			{
				HUDNode.GetComponent<TagAlong>().PositionOffset -= 0.2f;
			} else {
				await TextToSpeech("Only available when HUD Tag Along is enabled");
			}
		}

		async void ToggleTagAlong()
		{
			bool enabled = HUDNode.GetComponent<TagAlong>().TagAlongEnabled;

			if(enabled == true)
			{
				HUDNode.GetComponent<TagAlong>().TagAlongEnabled = false;
				await TextToSpeech("Tag Along Disabled");
			} else {
				HUDNode.GetComponent<TagAlong>().TagAlongEnabled = true;
				await TextToSpeech("Tag Along Enabled");
			}
		}

		async void EnableTagAlong()
		{
			HUDNode.GetComponent<TagAlong>().TagAlongEnabled = true;
			await TextToSpeech("Tag Along Enabled");
		}

		async void DisableTagAlong()
		{
			HUDNode.GetComponent<TagAlong>().TagAlongEnabled = false;
			await TextToSpeech("Tag Along Disabled");
		}

		#endregion Cortana Commands

		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);

		}

		protected override void Stop()
		{
			base.Stop();
			NetworkUtil.GetInstance().Shutdown();
		}
	}
}