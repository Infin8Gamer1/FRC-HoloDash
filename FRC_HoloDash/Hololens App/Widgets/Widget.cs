

namespace FRC_HoloDash
{

	public struct Widget
	{
		public Widget(WidgetType _type, float x, float y, float z = 0)
		{
			type = _type;
			Position = new float[3] {Utils.Clamp(x, -2.15f, 2.15f), y, Utils.Clamp(z, -0.5f, 15.0f) };

			ntType = NtType.String;
			NetworkTable = "";
			NetworkKey = "Default";

			Label = "SET THE LABEL";
		}

		public WidgetType type;
		public float[] Position;

		public NtType ntType;
		public string NetworkTable;
		public string NetworkKey;

		public string Label;
	}

	public enum WidgetType
	{
		Text,
		Camera
	}

	public enum NtType
	{
		/// <summary>
		/// No type assigned
		/// </summary>
		Unassigned = 0,
		/// <summary>
		/// Boolean type
		/// </summary>
		Boolean = 1,
		/// <summary>
		/// Double type
		/// </summary>
		Double = 2,
		/// <summary>
		/// String type
		/// </summary>
		String = 3,
		/// <summary>
		/// Raw type
		/// </summary>
		Raw = 4,
		/// <summary>
		/// Boolean Array type
		/// </summary>
		BooleanArray = 5,
		/// <summary>
		/// Double Array type
		/// </summary>
		DoubleArray = 6,
		/// <summary>
		/// String Array type
		/// </summary>
		StringArray = 7,
		/// <summary>
		/// Rpc type
		/// </summary>
		Rpc = 8
	}
}
