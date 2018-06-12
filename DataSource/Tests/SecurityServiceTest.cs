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
	public class SecurityServiceTest
	{
		private ISecuritiesService SecuritiesService;

		[SetUp]
		public void Setup()
		{


			SecuritiesService = new SecuritiesService(null, "BOB,ALICE,CHARLIE");
		}

		[Test]
		public void HierarchyNoValues()
		{
			var s = new []{ "BOB", "ALICE", "CHARLIE" };
		
			var sourceHierarchyQueue = new Queue<string>(s);
			var timeSeriesDataPoints = new List<DataPoint>();
			var data = SecuritiesService.ProcessSourceHierarchy(timeSeriesDataPoints, sourceHierarchyQueue);
		
			Assert.Greater(0,data.Result.Count());

		}

		[Test]
		public void Hierarchy()
		{
			var s = new[] { "BOB", "ALICE", "CHARLIE" };

			var sourceHierarchyQueue = new Queue<string>(s);
			var timeSeriesDataPoints = new List<DataPoint>
			{
				new DataPoint() {Source = "BOB", Symbol = "ABC", Close = 100, Date = DateTime.Today.AddDays(-2),Sector = "A",SubIndustry = "aa"},
				new DataPoint() {Source = "CHARLIE", Symbol = "ABC", Close = 100, Date = DateTime.Today.AddDays(-2),Sector = "A",SubIndustry = "aa"},
				new DataPoint() {Source = "ALICE", Symbol = "ABC", Close = 100, Date = DateTime.Today.AddDays(-1),Sector = "A",SubIndustry = "aa"},
				new DataPoint() {Source = "CHARLIE", Symbol = "ABC", Close = 100, Date = DateTime.Today,Sector = "A",SubIndustry = "aa"}
			};

			var data = SecuritiesService.ProcessSourceHierarchy(timeSeriesDataPoints, sourceHierarchyQueue);

			Assert.Greater(0, data.Result.Count());

		}




	}
}
