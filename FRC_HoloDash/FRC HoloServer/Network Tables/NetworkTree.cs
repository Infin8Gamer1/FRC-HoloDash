using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables;
using NetworkTables.Tables;
using Newtonsoft.Json;

namespace FRC_HoloServer
{
	/// <summary>
	/// Represents an entry in the network table without needing its value
	/// </summary>
	public class NetworkElement
	{
		/// <summary>
		/// The name of the entry
		/// </summary>
		public string Name;
		/// <summary>
		/// The type of the entry. Can be a primitive type or NetworkTable
		/// </summary>
		public Type Type;
		/// <summary>
		/// The Value of the entry
		/// </summary>
		public object Value;

		internal NetworkElement(string name, Type type, object value)
		{
			Name = name;
			Type = type;
			Value = value;
		}

		public override string ToString()
		{
			return $"{Name} (Type: {Type.Name}) (Value: {Value.ToString()})";
		}
	}


	/// <summary>
	/// Represents a network table's contents as a tree structure
	/// </summary>
	public class NetworkTree : NetworkElement
	{
		/// <summary>
		/// All values and subtrees belonging to this subtable
		/// </summary>
		public List<NetworkElement> Children;

		internal NetworkTree(string root, ITable baseTree = null) : base(root, typeof(NetworkTree), null)
		{
			ConstructChildren(root, baseTree);
		}

		private void ConstructChildren(string root, ITable baseTree)
		{
			Children = new List<NetworkElement>();
			ITable table = baseTree?.GetSubTable(root) ?? NetworkTable.GetTable(root);
			foreach (string key in table.GetKeys())
			{
				Children.Add(new NetworkElement(key, NetworkUtil.TypeOf(table.GetValue(key, null)), NetworkUtil.ReadValue(table.GetValue(key, null))));
			}
			foreach (string key in table.GetSubTables())
			{
				//ok being recursive, network tables usually stay fairly small
				Children.Add(new NetworkTree(key, table));
			}
		}
	}
}
