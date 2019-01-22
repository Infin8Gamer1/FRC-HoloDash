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
	class StatusWidget : Component
	{
		public Text3D Text;
		public Urho.Shapes.Plane plane;

		public string Key;
		public string Label;

		public StatusWidget(Text3D text, Urho.Shapes.Plane plane, string key, string label) {
			this.Text = text;
			this.plane = plane;
			this.Key = key;
			this.Label = label;
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node)
		{
			plane.SetMaterial(Material.FromColor(Color.Red));
			Text.Text = Label;

			//subscribe to the network updated ring of trust
			NetworkUtil.GetInstance().networkUpdatedHandler += OnNetworkUpdate;
		}

		//update method
		private void OnNetworkUpdate(object sender, NetworkUpdatedEvent e)
		{
			string value = NetworkUtil.GetInstance().GetKey(Key)?.ToString();
			
			if(value == "BAD" || value == "" || value == null)
			{
				plane.SetMaterial(Material.FromColor(Color.Red));
			} else if (value == "OK")
			{
				plane.SetMaterial(Material.FromColor(Color.Yellow));
			} else if (value == "GOOD")
			{
				plane.SetMaterial(Material.FromColor(Color.Green));
			}
		}
	}
}
