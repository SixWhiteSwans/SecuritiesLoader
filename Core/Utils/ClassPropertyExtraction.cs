using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Utils
{
	public static class ClassPropertyExtraction
	{

		public static IEnumerable<string> PropertyList<T>()
		{

			var list = new List<string>();
			var obj = typeof(T);

			PropertyInfo[] propertyInfos;
			propertyInfos = typeof(T).GetProperties(BindingFlags.Public |
			                                        BindingFlags.Static);


			var opPropertyInfo = obj.GetType().GetProperties(BindingFlags.Public |
			                                                 BindingFlags.Static);
			foreach (var item in opPropertyInfo)
			{
				Console.WriteLine(item);
			}

			return list;


		}

	}
}
