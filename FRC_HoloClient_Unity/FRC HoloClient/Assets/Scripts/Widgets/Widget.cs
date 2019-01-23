using System;

namespace FRC_HoloClient
{
	[Serializable]
	public struct Widget
	{
		public Widget(WidgetType _type, float x, float y, float z = 0)
		{
			type = _type;
			Position = new float[3] {x, y, z};

			NetworkKey = "Default";

			Label = "SET THE LABEL";
		}

		public WidgetType type;
		public float[] Position;

		public string NetworkKey;
		public string Label;
	}

	[Serializable]
	public enum WidgetType
	{
		Text,
		Camera,
		Status
	}
}
