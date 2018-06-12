using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Core.Model;

namespace DataSource.Configuration
{
	public static class ConfigDataHandler
	{

		public static string SystemName = "AutoLoad";

		public static IEnumerable<LoaderConfig> GetTimeSeriesTickDataConfig()
		{
			
			var loader = new List<LoaderConfig>
			{
				new LoaderConfig() { FileName = "ALICE", FileType = FileTypes.Csv, Source = "ALICE" },
				new LoaderConfig() { FileName = "BOB", FileType = FileTypes.Excel, Source = "BOB" },
				new LoaderConfig() { FileName = "CHARLIE", FileType = FileTypes.Xml, Source = "CHARLIE" }
			};

			var updated = loader.Select(x => {
				x.FileLocation = ConfigurationManager.AppSettings[x.FileName];
				
				return x;
			});

			return updated;
		}

		public static IEnumerable<LoaderConfig> GetTickerConfig()
		{

			var loader = new List<LoaderConfig>
			{
				new LoaderConfig() { FileName = "Securities", FileType = FileTypes.Json, Source = "Securities" },			
			};

			var updated = loader.Select(x => {
				x.FileLocation = ConfigurationManager.AppSettings[x.FileName];

				return x;
			});

			return updated;
		}

	}
}
