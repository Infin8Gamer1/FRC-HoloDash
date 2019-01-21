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

namespace FRC_HoloClient
{
	class CameraWidget : Component
	{
		public Urho.Shapes.Plane plane;

		public string URL;

		public CameraWidget()
		{
			ReceiveSceneUpdates = true;
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node)
		{
			plane.SetMaterial(Material.FromImage("Earth.jpg"));
		}

		//update method
		protected override void OnUpdate(float timeStep)
		{
			
			base.OnUpdate(timeStep);
		}
	}
}
