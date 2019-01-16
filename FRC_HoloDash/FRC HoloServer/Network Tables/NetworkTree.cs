using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkTables;
using NetworkTables.Tables;
using Newtonsoft.Json;

namespace FRC_Holo.API
{
	
	public class NetworkElement
	{
		/// <summary>
		/// The name of the entry
		/// </summary>
		public string Key;
		/// <summary>
		/// The Key of the parent
		/// </summary>
		public string ParentKey;
		/// <summary>
		/// The type of the entry. Can be a primitive type or NetworkTable
		/// </summary>
		public Type Type;
		/// <summary>
		/// The Value of the entry
		/// </summary>
		public object Value;
		/// <summary>
		/// The Children of this node
		/// </summary>
		public List<NetworkElement> Children;

		[JsonConstructor]
		public NetworkElement(string Key, string ParentKey, Type Type, object Value, List<NetworkElement> Children)
		{
			this.Key = Key;
			this.ParentKey = ParentKey;
			this.Type = Type;
			this.Value = Value;
			this.Children = Children;
		}

		/// <summary>
		/// used to make a complete tree of the tables
		/// </summary>
		public NetworkElement()
		{
			ConstructChildren();
		}

		private NetworkElement(string Key, string ParentKey, Type Type, object Value)
		{
			this.Key = Key;
			this.ParentKey = ParentKey;
			this.Type = Type;
			this.Value = Value;
			this.Children = new List<NetworkElement>();
		}

		private NetworkElement(string root, string ParentKey, ITable baseTree)
		{
			this.Key = root;
			this.ParentKey = ParentKey;

			ConstructChildren(root, baseTree);
		}

		private void ConstructChildren(string root = "", ITable baseTree = null)
		{
			Children = new List<NetworkElement>();
			ITable table = baseTree?.GetSubTable(root) ?? NetworkTable.GetTable(root);

			foreach (string key in table.GetKeys())
			{
				Children.Add(new NetworkElement(key, root, NetworkUtil.TypeOf(table.GetValue(key, null)), NetworkUtil.ReadValue(table.GetValue(key, null))));
			}

			foreach (string key in table.GetSubTables())
			{
				//ok being recursive, network tables usually stay fairly small
				Children.Add(new NetworkElement(key, root, table));
			}
		}

		public override string ToString()
		{
			return $"{Key} (Type: {Type?.Name}) (Value: {Value?.ToString()}) (Children: {Children?.Count}) (Parent: {ParentKey?.ToString()})";
		}

		public void PrintTable(int level = 0)
		{
			Console.WriteLine($"Level = {level}");
			Console.WriteLine("----------------------------");

			foreach (NetworkElement element in this.Children)
			{
				for (int i = 0; i < level; i++)
				{
					Console.Write("\t");
				}

				Console.Write(element.ToString() + "\n");

				if(element.Children.Count > 0)
				{
					int nextLevel = level + 1;
					element.PrintTable(nextLevel);
				}
			}

			Console.WriteLine("----------------------------");
		}
	}
}
