using System.Collections.Generic;

namespace FRC_HoloClient
{
	public class WidgetGrid
	{
		public List<Widget> widgets;

		public WidgetGrid(bool TestingMode) {
			widgets = new List<Widget>();

			if (TestingMode)
			{
				#region TextWidgets
				Widget stringTextWidget = new Widget(WidgetType.Text, -2f, 0)
				{
					NetworkKey = "TestingString",
					Label = "String"
				};

				widgets.Add(stringTextWidget);

				Widget numberTextWidget = new Widget(WidgetType.Text, 0, 0)
				{
					NetworkKey = "TestingDouble",
					Label = "Double"
				};

				widgets.Add(numberTextWidget);

				Widget boolTextWidget = new Widget(WidgetType.Text, 2f, 0)
				{
					NetworkKey = "TestingBoolean",
					Label = "Boolean"
				};

				widgets.Add(boolTextWidget);
				#endregion TextWidgets

				#region CameraWidgets
				Widget cameraWidget = new Widget(WidgetType.Camera, 0f, 2f)
				{
					//NetworkKey = "http://roborio-4089-frc.local:1181/?action=stream",
					NetworkKey = "http://infinitepc:1181/?action=stream",
					Label = "Image.exe"
				};

				widgets.Add(cameraWidget);
				#endregion

				#region StatusWidgets
				Widget statusWidget = new Widget(WidgetType.Status, 0f, -2f)
				{
					NetworkKey = "Status",
					Label = "Robot Status"
				};

				widgets.Add(statusWidget);
				#endregion
			}
		}
	}
}
