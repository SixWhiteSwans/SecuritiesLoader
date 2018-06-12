using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model;

namespace DataSource.Loaders
{
	public static class TickerDataPointLoadManager
	{
		private static readonly JsonLoader<Ticker> Loader = new JsonLoader<Ticker>();
		static TickerDataPointLoadManager()
		{
		
		}
		public static ValidFileInfo GetValidationInfo(LoaderConfig loaderConfig, IProgress<ProgressManager> progress)
		{
			var validFileInfo = new ValidFileInfo(loaderConfig.FileLocation, loaderConfig.Source, FileTypes.Json);

			if (progress != null)
				validFileInfo.ProgressLogger = progress;
			

			return validFileInfo;
		}

		public static async Task<IEnumerable<Ticker>> GetTickers(ValidFileInfo validFileInfo)
		{			
			return await Loader.LoadData(validFileInfo);

		}
	}
}
