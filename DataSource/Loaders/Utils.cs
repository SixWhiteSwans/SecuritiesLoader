using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataSource.Loaders
{
	public static class Utils
	{
		public static double SafeCastToDouble(string value)
		{

			var val = double.NaN;
			double.TryParse(value, out val);

			return val;
		}

		public static double SafeCastToDoubleParse(string value)
		{
			if (value.Equals("NA") || value.Equals("N/A")|| string.IsNullOrEmpty(value))
				return double.NaN;


			return double.Parse(value);
		}

		public static DateTime SafeCastToDate(string date)
		{
			var val = DateTime.MinValue;
			DateTime.TryParse(date, out val);
			return val;

		}

		public static DateTime SafeCastToOADate(string date)
		{
			double d = double.Parse(date);
			return DateTime.FromOADate(d);
		}

		

		public static DateTime ConvertValueToDate(XElement el, string name)
		{
			DateTime val = DateTime.MinValue;

			if (el.Element(name) == null)
				return val;



			DateTime.TryParse(el.Element(name).Value, out val);
			return val;
		}


		public static string ConvertValueToString(XElement el, string name)
		{
			if (el.Element(name) == null)
				return String.Empty;

			var val = (string)el.Element(name).Value;
			return val;
		}

		public static double ConvertValueToDouble(XElement el, string name)
		{
			
			if (el.Element(name) == null || el.Element(name).Value.Equals("NA") || el.Element(name).Value.Equals("N/A"))
				return double.NaN;

			var val = Double.NaN;
			double.TryParse(el.Element(name).Value, out val);

			return val;
		}
	}
}
