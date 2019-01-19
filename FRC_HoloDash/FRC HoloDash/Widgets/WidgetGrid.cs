using System.Collections.Generic;

namespace FRC_HoloServer
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
			}
		}
	}
}
