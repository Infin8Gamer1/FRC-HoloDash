using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Resources;

namespace FRC_HoloClient
{
	class WidgetManager : Component
	{

		public WidgetManager() {
			
		}

		// user defined properties (managed state): 
		public WidgetGrid Grid { get; set; }

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
			backgroundPlaneNode.Scale = new Vector3(1.9f, 1f, 1f);
			backgroundPlaneNode.Rotation = new Quaternion(-90, 0, 0);
			backgroundPlaneNode.Position += new Vector3(0, 0, 0.05f);

			Urho.Shapes.Plane plane = backgroundPlaneNode.CreateComponent<Urho.Shapes.Plane>();
			plane.SetMaterial(Material.FromColor(new Color(0.6f, 0.6f, 0.6f, 0.5f)));

			//Text Component
			Text3D text = widgetNode.CreateComponent<Text3D>();
			text.HorizontalAlignment = HorizontalAlignment.Center;
			text.VerticalAlignment = VerticalAlignment.Center;
			text.SetFont(CoreAssets.Fonts.AnonymousPro, 12);
			text.SetColor(Color.Red);
			text.Text = "LOADING...";

			switch (widget.type)
			{
				case WidgetType.Text:
					//Text Widget Display
					TextWidget textWidget = widgetNode.CreateComponent<TextWidget>();
					textWidget.Text = text;
					textWidget.Key = widget.NetworkKey;
					textWidget.Label = widget.Label;
					break;
				case WidgetType.Camera:
					//remove text
					text.Remove();

					//Camera Widget
					CameraWidget cameraWidget = widgetNode.CreateComponent<CameraWidget>();
					cameraWidget.plane = plane;
					cameraWidget.URL = widget.NetworkKey;
					break;
				case WidgetType.Status:
					//adjust the size of the background plane
					backgroundPlaneNode.Scale = new Vector3(0.5f, 0.25f, 1f);

					//Status Widget
					StatusWidget statusWidget = widgetNode.CreateComponent<StatusWidget>();
					statusWidget.plane = plane;
					statusWidget.Text = text;
					statusWidget.Label = widget.Label;
					statusWidget.Key = widget.NetworkKey;
					break;
			}

			Node.AddChild(widgetNode);
		}

		//delete method
		protected override void OnDeleted()
		{
			base.OnDeleted();
		}
	}
}
