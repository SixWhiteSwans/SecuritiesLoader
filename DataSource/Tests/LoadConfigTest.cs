using System.Collections.Generic;
using System.Linq;
using Core.Model;
using NUnit.Framework;

namespace DataSource.Tests
{
	[TestFixture()]
	public class LoadConfigTest
	{
		
		[Test]
		public void LoadConfig()
		{
			var loader = new List<LoaderConfig>
			{
				new LoaderConfig() {FileName = "ALICE", FileType = FileTypes.Csv},
				new LoaderConfig() {FileName = "BOB", FileType = FileTypes.Excel},
				new LoaderConfig() {FileName = "CHARLIE", FileType = FileTypes.Xml}
			};

			var updated = loader.Select(x => {
				x.FileLocation = "MyData";
				return x;
			});

			foreach (var loaderConfig in updated)
			{
				StringAssert.Contains("MyData",loaderConfig.FileLocation);
			}

		}

	}
}
