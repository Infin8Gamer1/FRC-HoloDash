

namespace FRC_HoloDash
{
	public struct Widget
	{
		public Widget(WidgetType _type, float x, float y, float z = 0)
		{
			type = _type;
			Position = new float[3] {Utils.Clamp(x, -2.15f, 2.15f), y, Utils.Clamp(z, -0.5f, 15.0f) };

			NtType = NetworkTables.NtType.String;
			NetworkTable = "";
			NetworkKey = "Default";

			Label = "SET THE LABEL";

			CameraIP = "https://192.168.0.1/";
		}

		public WidgetType type;
		public float[] Position;

		public NetworkTables.NtType NtType;
		public string NetworkTable;
		public string NetworkKey;

		public string Label;

		public string CameraIP;
	}

	public enum WidgetType
	{
		Text,
		Camera
	}
}
