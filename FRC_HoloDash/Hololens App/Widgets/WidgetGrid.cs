using System.Collections.Generic;

namespace FRC_HoloDash
{
	public class WidgetGrid
	{
		public List<Widget> widgets;

		public WidgetGrid(bool TestingMode) {
			widgets = new List<Widget>();

			if (TestingMode)
			{
				Widget widget = new Widget(WidgetType.Text, -2f, 0)
				{
					ntType = NtType.String,
					NetworkKey = "Testing",
					NetworkTable = "",
					Label = "Testing"
				};

				widgets.Add(widget);
			}
		}
	}
}
