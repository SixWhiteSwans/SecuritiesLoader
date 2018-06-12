using System;
using Core.Model;
using Core.Utils;
using NUnit.Framework;

namespace Core.Tests
{
	[TestFixture]
	public class TickerTest
	{
		private Ticker _ticker;
		[SetUp]
		public void Setup()
		{
			_ticker = new Ticker()
			{
				Symbol = "MMM",
				Security = "3M Company",
				Sector = "Industrials",
				SubIndustry = "Industrial Conglomerates",
				LastUpdated = "test",
				Source = "A",
				UpdateDate = DateTime.UtcNow
			};

		}

		[Test]
		public void SetTickerProperties()
		{

			StringAssert.AreEqualIgnoringCase(_ticker.Symbol,"MMM");
			StringAssert.AreEqualIgnoringCase(_ticker.Security, "3M Company");
			StringAssert.AreEqualIgnoringCase(_ticker.Sector, "Industrials");
			StringAssert.AreEqualIgnoringCase(_ticker.SubIndustry, "Industrial Conglomerates");

		}

		[Test]
		public void Serialized()
		{

			var serializedMemory = _ticker.Serialize();
			var deserialized = serializedMemory.Deserialize<Ticker>();
			
			StringAssert.AreEqualIgnoringCase(deserialized.Symbol, "MMM");
			StringAssert.AreEqualIgnoringCase(deserialized.Security, "3M Company");
			StringAssert.AreEqualIgnoringCase(deserialized.Sector, "Industrials");
			StringAssert.AreEqualIgnoringCase(deserialized.SubIndustry, "Industrial Conglomerates");

		}



	}
}
