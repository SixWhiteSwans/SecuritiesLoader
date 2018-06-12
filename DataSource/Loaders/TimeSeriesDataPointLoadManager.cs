using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Model;

namespace DataSource.Loaders
{
	public static class TimeSeriesDataPointLoadManager
	{
		static readonly Dictionary<FileTypes, IDataLoaderTimeSeries> Loaders = new Dictionary<FileTypes, IDataLoaderTimeSeries>();
		static TimeSeriesDataPointLoadManager()
		{
			Loaders.Add(FileTypes.Xml,new XmlLoader());
			Loaders.Add(FileTypes.Csv, new CsvLoader());
			Loaders.Add(FileTypes.Excel, new ExcelLoader());

		}

		public static ValidFileInfo GetValidationInfo(LoaderConfig loaderConfig, IProgress<ProgressManager> progress)
		{
			var validFileInfo = new ValidFileInfo(loaderConfig.FileLocation, loaderConfig.Source, loaderConfig.FileType);

			if(progress!=null)
				validFileInfo.ProgressLogger = progress;


			return validFileInfo;
		}

		public static async Task<TimeSeries> GetTimeSeries(ValidFileInfo validFileInfo)
		{
			if(!Loaders.ContainsKey(validFileInfo.FileType))
				throw new ArgumentException("Json file type has not be setup in the loaders configuration layer.");


			var loader = Loaders[validFileInfo.FileType];
			return await loader.LoadData(validFileInfo);

		}
	}
}
