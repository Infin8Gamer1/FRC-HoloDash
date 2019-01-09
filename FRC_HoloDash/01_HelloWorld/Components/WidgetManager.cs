using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;

namespace FRC_HoloDash
{
	class WidgetManager : Component
	{
		// Constructor needed for deserialization 
		public WidgetManager(IntPtr handle) : base(handle) {
			
		}

		public WidgetManager() {
			
		}

		// user defined properties (managed state): 
		public WidgetGrid Grid { get; set; }

		public override void OnSerialize(IComponentSerializer ser) {
			// register our properties with their names as keys using nameof()
			ser.Serialize(nameof(Grid), Grid);
		}

		public override void OnDeserialize(IComponentDeserializer des) {
			Grid = des.Deserialize<WidgetGrid>(nameof(Grid));
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node) {
			Init();
		}

		//init method
		public void Init()
		{
			ReceiveSceneUpdates = true;
			Grid = new WidgetGrid(true);

			//add nodes for each widget in the grid as children from this node
			foreach (Widget widget in Grid.widgets)
			{
				AddWidget(widget);
			}
			
		}

		public void AddWidget(Widget widget)
		{
			Node widgetNode = new Node();
			widgetNode.SetScale(0.1f);
			widgetNode.Position += new Vector3(widget.Position[0] * 0.1f, widget.Position[1] * 0.1f, widget.Position[2] * 0.1f);

			//Background Plane
			Node backgroundPlaneNode = widgetNode.CreateChild();
			backgroundPlaneNode.Scale = new Vector3(2f, 1f, 1f);
			backgroundPlaneNode.Rotation = new Quaternion(-90, 0, 0);
			backgroundPlaneNode.Position += new Vector3(0, 0, 0.05f);

			Urho.Shapes.Plane plane = backgroundPlaneNode.CreateComponent<Urho.Shapes.Plane>();
			plane.SetMaterial(Material.FromColor(new Color(0.6f, 0.6f, 0.6f, 0.7f)));

			switch (widget.type)
			{
				case WidgetType.Text:
					//Text Component
					Text3D text = widgetNode.CreateComponent<Text3D>();
					text.HorizontalAlignment = HorizontalAlignment.Center;
					text.VerticalAlignment = VerticalAlignment.Center;
					text.SetFont(CoreAssets.Fonts.AnonymousPro, 12);
					text.SetColor(Color.Red);
					text.Text = "Testing 123";

					//Text Widget Display
					TextWidget textWidget = widgetNode.CreateComponent<TextWidget>();
					textWidget.Text = text;
					textWidget.ValueType = widget.NtType;
					textWidget.Table = widget.NetworkTable;
					textWidget.Key = widget.NetworkKey;
					textWidget.Label = widget.Label;

					break;
				case WidgetType.Camera:
					break;
			}

			Node.AddChild(widgetNode);
		}

		//update method
		protected override void OnUpdate(float timeStep)
		{
			base.OnUpdate(timeStep);
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
