using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables;
using NetworkTables.Tables;

namespace FRC_HoloDash
{
	/// <summary>
	/// Represents an entry in the network table without needing its value
	/// </summary>
	public class NetworkElement
	{
		/// <summary>
		/// The name of the entry
		/// </summary>
		public readonly string Name;
		/// <summary>
		/// The type of the entry. Can be a primitive type or NetworkTable
		/// </summary>
		public readonly Type Type;
		internal NetworkElement(string name, Type type)
		{
			Name = name;
			Type = type;
		}

		public override string ToString()
		{
			return $"{Name} ({Type.Name})";
		}
	}


	/// <summary>
	/// Represents a network table's contents as a tree structure
	/// </summary>
	public class NetworkTree : NetworkElement
	{
		private List<NetworkElement> children;
		/// <summary>
		/// All values and subtrees belonging to this subtable
		/// </summary>
		public IList<NetworkElement> Children {
			get {
				return children.AsReadOnly();
			}
		}
		internal NetworkTree(string root, ITable baseTree = null) : base(root, typeof(NetworkTree))
		{
			ConstructChildren(root, baseTree);
		}

		private void ConstructChildren(string root, ITable baseTree)
		{
			children = new List<NetworkElement>();
			ITable table = baseTree?.GetSubTable(root) ?? NetworkTable.GetTable(root);
			foreach (string key in table.GetKeys())
			{
				children.Add(new NetworkElement(key, NetworkUtil.TypeOf(table.GetValue(key, null))));
			}
			foreach (string key in table.GetSubTables())
			{
				//ok being recursive, network tables usually stay fairly small
				children.Add(new NetworkTree(key, table));
			}
		}
	}
}
