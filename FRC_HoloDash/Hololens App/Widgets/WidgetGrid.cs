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
					ntType = NtType.String,
					NetworkKey = "TestingString",
					NetworkTable = "",
					Label = "String :"
				};

				widgets.Add(stringTextWidget);

				Widget numberTextWidget = new Widget(WidgetType.Text, 0, 0)
				{
					ntType = NtType.Double,
					NetworkKey = "TestingDouble",
					NetworkTable = "",
					Label = "Double :"
				};

				widgets.Add(numberTextWidget);

				Widget boolTextWidget = new Widget(WidgetType.Text, 2f, 0)
				{
					ntType = NtType.Boolean,
					NetworkKey = "TestingBoolean",
					NetworkTable = "",
					Label = "Boolean :"
				};

				widgets.Add(boolTextWidget);
				#endregion TextWidgets
			}
		}
	}
}
