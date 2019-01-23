﻿using System;
using System.Collections.Generic;

namespace FRC_Holo.API
{
	[Serializable]
	public class NetworkElement
	{
		/// <summary>
		/// The name of the entry
		/// </summary>
		public string Key;
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

		public NetworkElement(string Key, Type Type, object Value, List<NetworkElement> Children)
		{
			this.Key = Key;
			this.Type = Type;
			this.Value = Value;
			this.Children = Children;
		}

		public override string ToString()
		{
			return Key + " (Type: " + Type.Name + ") (Value: " + Value.ToString() + ") (Children: " + Children.Count +")";
		}

		public void PrintTable(int level = 0)
		{
			Console.WriteLine("Level = " + level);
			Console.WriteLine("----------------------------");

			foreach (NetworkElement element in this.Children)
			{
				for (int i = 0; i < level; i++)
				{
					Console.Write("\t");
				}

				Console.Write(element.ToString() + "\n");

				if (element.Children.Count > 0)
				{
					int nextLevel = level + 1;
					element.PrintTable(nextLevel);
				}
			}

			Console.WriteLine("----------------------------");
		}
	}
}
