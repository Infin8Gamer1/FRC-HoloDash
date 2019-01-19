using FRC_Holo.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Physics;
using Urho.Resources;

namespace FRC_HoloServer
{
	class TextWidget : Component
	{
		public Text3D Text;

		public string Key;
		public string Label;

		// Constructor needed for deserialization 
		public TextWidget(IntPtr handle) : base(handle) {
			
		}

		public TextWidget() {
			ReceiveSceneUpdates = true;
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node)
		{
		}

		//update method
		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);

			string value = NetworkUtil.GetInstance().GetKey(Key)?.ToString();
			
			Text.Text = Label + ": " + value;
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
