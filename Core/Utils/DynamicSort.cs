using System.Linq;
using System.Linq.Dynamic;

namespace Core.Utils
{
	public static class DynamicSort
	{
		public static IQueryable Sort(this IQueryable collection, string sortBy, bool reverse = false)
		{
			return collection.OrderBy(sortBy + (reverse ? " descending" : ""));
		}
	}
}
