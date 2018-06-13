using System;
using System.Linq;
using DataSource.Providers;
using DataSource.Queries;
using NUnit.Framework;

namespace DataSource.Tests
{
	[Ignore("Not required")]
	[TestFixture]
	public class TickerDataContextTest:IDisposable
	{

		private ApplicationDbContext _context;
		[SetUp]
		public void Setup()
		{
			_context = new ApplicationDbContext();
		}

		[Test]
		public void QueryTicker()
		{
			var query = new TickerQuery(_context);
			var result = query.Execute();
			Assert.Equals(result.Count(), 1);

		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
