using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Physics;
using Urho.Resources;

namespace FRC_HoloDash
{
	class TagAlong : Component
	{
		public float PositionOffset = 1.25f;
		public float HeightPosition = 0;
		public Node LeftCamera;

		// Constructor needed for deserialization 
		public TagAlong(IntPtr handle) : base(handle) {
			
		}

		public TagAlong() {
			ReceiveSceneUpdates = true;
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node) {}

		//update method
		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);

			double angle = LeftCamera.WorldRotation.YawAngle * (Math.PI/180);

			Vector3 newPos = LeftCamera.WorldPosition + new Vector3((PositionOffset * (float)Math.Sin(angle)), 0,(PositionOffset * (float)Math.Cos(angle)));
			newPos.Y = HeightPosition;

			Node.Rotation = new Quaternion(0, LeftCamera.WorldRotation.YawAngle, 0);
			Node.Position = newPos;
		}


		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
