using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Physics;
using Urho.Resources;
using NetworkTables;
using FRC_HoloDash.USB_Comms;

namespace FRC_HoloDash
{
	class TextWidget : Component
	{
		public Text3D Text;

		public NtType ValueType;
		public string Table;
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

			string value = "Error Type Not Implemented";

			switch (ValueType)
			{
				case NtType.Unassigned:
					break;
				case NtType.Boolean:
					value = UsbCOMS.Instance.GetBoolean(Key).ToString();
					break;
				case NtType.Double:
					value = UsbCOMS.Instance.GetNumber(Key).ToString();
					break;
				case NtType.String:
					value = UsbCOMS.Instance.GetString(Key);
					break;
				case NtType.Raw:
					break;
				case NtType.BooleanArray:
					break;
				case NtType.DoubleArray:
					break;
				case NtType.StringArray:
					break;
				case NtType.Rpc:
					break;
				default:
					break;
			}

			Text.Text = Label + ": " + value;
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
