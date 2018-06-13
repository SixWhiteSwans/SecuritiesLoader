using Core.Model;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Core.Tests
{
	[TestFixture()]
	public class SecurityQueryTest
	{
		[Test]
		public void SecurityQuery()
		{
			var sq = new SecurityQuery
			{
				Sector = "Health Care",
				SubIndustry = "Health Care Equipment",
				Source = "ALICE",
				OrderBy = "ASC",
				OrderByField = "Sector"
			};
			 
			var sql = sq.GetTickerSql();
			StringAssert.AreEqualIgnoringCase("SELECT * FROM Tickers WHERE Sector=\'Health Care\' AND SubIndustry=\'Health Care Equipment\'", sql);

		}


		[Test]
		public void SecurityQuerySubIndustry()
		{
			var sq = new SecurityQuery
			{
				Sector = string.Empty,
				SubIndustry = "Health Care Equipment",
				Source = string.Empty,
				OrderBy = string.Empty,
				OrderByField = string.Empty
			};

			var sql = sq.GetTickerSql();
			StringAssert.AreEqualIgnoringCase("SELECT * FROM Tickers WHERE  SubIndustry='Health Care Equipment'", sql);

		}

		[Test]
		public void SecurityQuerySubIndustryOrderBy()
		{
			var sq = new SecurityQuery
			{
				Sector = string.Empty,
				SubIndustry = "Health Care Equipment",
				Source = string.Empty,
				OrderBy = "ASC",
				OrderByField = "Sector"
			};

			var sql = sq.GetTickerSql();
			StringAssert.AreEqualIgnoringCase("SELECT * FROM Tickers WHERE  SubIndustry='Health Care Equipment'", sql);

		}

		

	}
}
