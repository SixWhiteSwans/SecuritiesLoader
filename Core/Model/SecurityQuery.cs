using System;
using System.Text;

namespace Core.Model
{
	public class SecurityQuery
	{
		public string Sector { get; set; }
		
		public string SubIndustry { get; set; }

		public string Source { get; set; }

		public string OrderBy { get; set; }

		public string OrderByField { get; set; }

		public string GetTickerSql()
		{
			var sb = new StringBuilder();

			var query = "SELECT * FROM Tickers";

			if (!String.IsNullOrEmpty(Sector))
				sb.Append(string.Concat("Sector=\'", Sector, "\'"));

			if (!String.IsNullOrEmpty(SubIndustry))
				sb.Append(string.Concat(" AND SubIndustry=\'", SubIndustry, "\'"));

			//if (!String.IsNullOrEmpty(Source))
			//	sb.Append(string.Concat(" AND Source=\'", Source, "\'"));

			//if (!String.IsNullOrEmpty(OrderBy) && !String.IsNullOrEmpty(OrderByField) &&
			//	(OrderByField.Equals("Sector") || OrderByField.Equals("SubIndustry") || OrderByField.Equals("Source")))
			//	sb.Append(string.Concat(" Order By ", OrderByField, " ", OrderBy));


			var sql = sb.ToString().TrimStart().StartsWith("AND") ? sb.ToString().Substring(4) : sb.ToString();

			if (String.IsNullOrEmpty(Sector) && String.IsNullOrEmpty(SubIndustry))
				return query;


				return query + " WHERE " + sql;

		}

		
	}
}
