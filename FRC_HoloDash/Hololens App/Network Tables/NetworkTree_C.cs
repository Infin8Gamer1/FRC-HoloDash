﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				Console.WriteLine(element.ToString());

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