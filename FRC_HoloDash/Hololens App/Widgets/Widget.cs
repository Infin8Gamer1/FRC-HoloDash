

namespace FRC_HoloServer
{

	public struct Widget
	{
		public Widget(WidgetType _type, float x, float y, float z = 0)
		{
			type = _type;
			Position = new float[3] {Utils.Clamp(x, -2.15f, 2.15f), y, Utils.Clamp(z, -0.5f, 15.0f) };

			NetworkKey = "Default";

			Label = "SET THE LABEL";
		}

		public WidgetType type;
		public float[] Position;

		public string NetworkKey;

		public string Label;
	}

	public enum WidgetType
	{
		Text,
		Camera
	}
}
