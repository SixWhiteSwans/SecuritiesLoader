using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Services;
using Core.Model;
using DataSource.Providers;
using DataSource.Service;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataSource.Tests
{
	[TestFixture()]
	public class SecurityServiceINtergrationTest
	{
		private ISecuritiesService SecuritiesService;

		private readonly DateTime startDate = DateTime.Parse("01/01/2018");
		private readonly DateTime endDate = DateTime.Parse("01/06/2018");

		[SetUp]
		public void Setup()
		{
			var ctx = new ApplicationDbContext();

			SecuritiesService = new SecuritiesService(ctx, "BOB,ALICE,CHARLIE");

		}


		[Test]
		public void SecurityQuerySource()
		{

			var securityQuery = new SecurityQuery
			{
				Source = "CHARLIE",
				Sector = "Health Care",
				SubIndustry = string.Empty,
				OrderBy = string.Empty,
				OrderByField = string.Empty
			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var count = dataPoints.Result.Count();

			Assert.IsFalse(dataPoints.Result.Any(x => !x.Source.Equals(securityQuery.Source))); 

			Assert.AreEqual(count, 594);

		}

		[Test]
		public void SecurityQuerySector()
		{

			var securityQuery = new SecurityQuery
			{
				Source = string.Empty,
				Sector = "Health Care",
				SubIndustry = string.Empty,
				OrderBy = string.Empty,
				OrderByField = string.Empty
			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			Assert.AreEqual(dataPoints.Result.Count(),1401);

		}

		[Test]
		public void SecurityQuerySubIndustry()
		{

			var securityQuery = new SecurityQuery
			{
				Source = string.Empty,
				Sector = string.Empty,
				SubIndustry = "Health Care Equipment",
				OrderBy = string.Empty,
				OrderByField = string.Empty
			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var count = dataPoints.Result.Count();

			Assert.AreEqual(count, 404);

		}



		[Test]
		public void SecurityQueryOrderBYOnlyHealthCareReturned()
		{

			var securityQuery = new SecurityQuery
			{
				Source = string.Empty,
				Sector = string.Empty,
				SubIndustry = "Health Care Equipment",
				OrderByField = "Sector",
				OrderBy = "ASC"
				
			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var count = dataPoints.Result.Count();

			Assert.AreEqual(count, 404);

		}


		[Test]
		public void SecurityQueryOrderBySector_Source_CHARLIE()
		{

			var securityQuery = new SecurityQuery
			{
				Source = "CHARLIE",
				Sector = string.Empty,
				SubIndustry = "",
				OrderByField = "Sector",
				OrderBy = "ASC"

			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var sectors = dataPoints.Result.ToList().Select(x => x.Sector).Distinct();

			var ordered = sectors.OrderBy(x => x);

			Assert.IsTrue(ordered.SequenceEqual(sectors)); 

			var count = dataPoints.Result.Count();

			Assert.AreEqual(4810,count);

		}

		[Test]
		public void SecurityQueryOrderBySubIndustry_Source_CHARLIE()
		{

			var securityQuery = new SecurityQuery
			{
				Source = "CHARLIE",
				Sector = string.Empty,
				SubIndustry = "",
				OrderByField = "SubIndustry",
				OrderBy = "ASC"

			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var subIndustry = dataPoints.Result.ToList().Select(x => x.SubIndustry).Distinct();

			var ordered = subIndustry.OrderBy(x => x);

			Assert.IsTrue(ordered.SequenceEqual(subIndustry));

			var count = dataPoints.Result.Count();

			Assert.AreEqual(4810, count);

		}

		[Test]
		public void SecurityQueryOrderBySource_Source_ALICE_BOB_CHARLIE()
		{

			var securityQuery = new SecurityQuery
			{
				Source = string.Empty,
				Sector = string.Empty,
				SubIndustry = "",
				OrderByField = "Source",
				OrderBy = "ASC"

			};

			var dataPoints = SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			var source = dataPoints.Result.ToList().Select(x => x.Source).Distinct();

			var ordered = source.OrderBy(x => x);

			Assert.IsTrue(ordered.SequenceEqual(source));

			var count = dataPoints.Result.Count();
			Assert.AreEqual(11828, count);

		}
	}
}
