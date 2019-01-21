using System;
using System.Threading.Tasks;
using Urho;
using FRC_Holo.API;

namespace FRC_HoloClient
{
	class NetworkManager : Component
	{
		// Constructor needed for deserialization 
		public NetworkManager(IntPtr handle) : base(handle) {
			
		}

		public NetworkManager() {
		}

		public bool active;

		public const int timeBetweenUpdatesMS = 200;

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node) {
			Init();
		}

		//init method
		public void Init()
		{
			active = true;

			Action action = () =>
			{
				while (active)
				{
					Task.Delay(timeBetweenUpdatesMS);
					

					NetworkUtil.GetInstance().UpdateNtTable();
				}
			};

			Task t = new Task(action);
			t.Start();
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();

			active = false;
		}
	}
}
