using System.Collections.Generic;

namespace FRC_HoloDash
{
	public class WidgetGrid
	{
		public int Columns;
		public int Rows;

		public List<Widget> widgets;

		public WidgetGrid(bool TestingMode) {
			Columns = 2;
			Rows = 3;

			widgets = new List<Widget>();

			if (TestingMode)
			{
				Widget widget = new Widget(WidgetType.Text, -2f, 0)
				{
					NtType = NetworkTables.NtType.String,
					NetworkKey = "Testing",
					NetworkTable = "",
					Label = "Testing"
				};

				widgets.Add(widget);
			}
		}

		
	}
}
