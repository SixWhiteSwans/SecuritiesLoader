using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using DataSource.Configuration;


namespace DataSource.Loaders
{
	public static class TimeSeriesBatchJob
	{

		public static async Task<ContextLoaderDataValidation> RunTickerLoader()
		{
			var list = new List<ContextLoaderDataValidation>();
			var progress = new Progress<ProgressManager>(ContextManager.ProcessDataLogMessage);

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var configData = ConfigDataHandler.GetTickerConfig();


			IEnumerable<ValidFileInfo> validatedData;
			try
			{	//check configs setup correctly. If not throw error as this is a programming error.
				validatedData = configData.Select(x => TickerDataPointLoadManager.GetValidationInfo(x, progress));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return new ContextLoaderDataValidation(false, e.Message); ;
			}
			
			var tasks = validatedData.Select(async (validFileInfo) => await TickerDataPointLoadManager.GetTickers(validFileInfo));

			var results = await Task.WhenAll(tasks);

			foreach (var tickers in results)
			{
					if(!tickers.Any())
						continue;

				var source = tickers.ToList().ElementAt(0).Source;

			var loadDataValidation = await ContextManager.PopulateData(source, tickers, progress);
				list.Add(loadDataValidation);
			}

			stopWatch.Stop();

			Console.WriteLine("Total seconds:" + stopWatch.Elapsed.TotalSeconds);
			return new ContextLoaderDataValidation(true,string.Empty);

		}

		public static async Task<IEnumerable<ContextLoaderDataValidation>> RunTimeSeriesDataPointsLoader()
		{
			var list = new List<ContextLoaderDataValidation>();

			//IProgress<ProgressManager> progress
			var progress = new Progress<ProgressManager>(ContextManager.ProcessDataLogMessage);

			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var configData = ConfigDataHandler.GetTimeSeriesTickDataConfig();
			IEnumerable<ValidFileInfo> validatedData;
			try
			{   //check configs setup correctly. If not throw error as this is a programming error.
				validatedData = configData.Select(x => TimeSeriesDataPointLoadManager.GetValidationInfo(x, progress));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				list.Add(new ContextLoaderDataValidation(false, e.Message));
				return list;
			}

			var tasks = validatedData.Select(async (validFileInfo) => await TimeSeriesDataPointLoadManager.GetTimeSeries(validFileInfo));
			

			var timeSeriesDataPoints = await Task.WhenAll(tasks);


			foreach (var timeSeriesDataPoint in timeSeriesDataPoints)
			{
				if (!timeSeriesDataPoint.Any())
					continue;

				var source = timeSeriesDataPoint.ToList().ElementAt(0).Source;

				var loadDataValidation = await ContextManager.PopulateData(source, timeSeriesDataPoint, progress);
				list.Add(loadDataValidation);
			}
			
			//var populatedResults = completed.Select(async (timeSeries) => await  PopulateTimeSeriesDataPoint(timeSeries, progress));

			stopWatch.Stop();

			Console.WriteLine("Total seconds:" + stopWatch.Elapsed.TotalSeconds);

			return list;

		}



		

	}
}
