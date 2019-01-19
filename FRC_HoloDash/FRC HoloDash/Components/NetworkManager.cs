using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;
using FRC_Holo.API;

namespace FRC_HoloServer
{
	class NetworkManager : Component
	{
		// Constructor needed for deserialization 
		public NetworkManager(IntPtr handle) : base(handle) {
			
		}

		public NetworkManager() {
			ReceiveSceneUpdates = true;
		}

		public const float timeBetweenUpdates = 0.3f;
		private float countdown;

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node) {
			Init();
		}

		//init method
		public void Init()
		{
			ReceiveSceneUpdates = true;

			countdown = timeBetweenUpdates;
		}

		//update method
		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);

			countdown -= timeStep;

			if (countdown <= 0)
			{
				countdown = timeBetweenUpdates;
				NetworkUtil.Instance.UpdateNtTable();
			}
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
