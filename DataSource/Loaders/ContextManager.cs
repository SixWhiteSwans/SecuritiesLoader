using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Model;
using DataSource.Configuration;
using DataSource.Extensions;
using DataSource.Providers;

namespace DataSource.Loaders
{
	public static class ContextManager
	{
		public static void ProcessDataLogMessage(ProgressManager progress)
		{
			using (var ctx = new ApplicationDbContext())
			{
				var pmld = new ProcessDataLog
				{
					UpdateDate = DateTime.UtcNow,
					Action = progress.Action,
					Message = progress.Message
				};


				ctx.ProcessDataLog.Add(pmld);
				ctx.SaveChanges();

			}


			Console.WriteLine(progress.Message);
		}

		



		public static async Task<ContextLoaderDataValidation> PopulateData(string source, IEnumerable<TimeSeriesDataPoint> timeSeriesDataPoints, IProgress<ProgressManager> progress)
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				if(string.IsNullOrEmpty(source))
					source = timeSeriesDataPoints.ToList().ElementAt(0).Source;


				progress?.Report(GetProgressManager($"Populate TimeSeries started total number of timeseries datapoints  to process: {timeSeriesDataPoints.Count()}", source));

				using (var ctx = new ApplicationDbContext())
				{
					//steps 1
					//delete all timeseries from source - assuming a bcp operation
					//todo: implement a sql bcp program from git to evaluate speed increase.
					//https://github.com/ErikEJ/SqlCeBulkCopy
					
					//step 2:
					//exlude all tsdp where ticker is not setup.

					//step 3
					//validate tsdp for validate data points na, outliers

					//step 4
					//upload tsdp
					
					progress?.Report(GetProgressManager($"Bulk update: Removing all timeseries from source  {source}", source));
					//remove all data from the source
					if (timeSeriesDataPoints.Any() && ctx.TimeSeries.Any())
					{
						ctx.TimeSeries.RemoveRange(ctx.TimeSeries.Where(x => x.Source.Equals(source)));
						ctx.SaveChanges();
					}

					//we want to make the index on the db source,symbol,date
					//handling duplicate data: source,symbol,date
					//distinct timeseries data set only: remove any items which have dups.
					List<TimeSeriesDataPoint> distinctTimeSeriesDataPoints = timeSeriesDataPoints
						.GroupBy(t => new {t.Source, t.Symbol, t.Date})
						.Select(g => g.First())
						.ToList();


					//tickers not setup in goldern source
					var tickersWhereTickerIsNotSetup = distinctTimeSeriesDataPoints.Where(x => !ctx.Ticker.Any(y => y.Symbol.Equals(x.Symbol)))
						.Select(x => new Ticker() {Symbol = x.Symbol, Source = x.Source}).DistinctBy(x => x.Symbol).ToList();


					//process invalid tickers
					progress?.Report(GetProgressManager($"Processing invalid tickers started  {source}", source));
					var invalidTickerDataPointErrorLog = tickersWhereTickerIsNotSetup.Select(ticker =>{return DataValidationRules.IsTimeSeriesDataValid(ticker);});
					var dataPointErrorLogs = GetTickerDataPointErrorLog(invalidTickerDataPointErrorLog).ToList();

					if (dataPointErrorLogs.Any())
					{
						ctx.DataErrorLog.AddRange(dataPointErrorLogs);
						await ctx.SaveChangesAsync();
					}
					
					progress?.Report(GetProgressManager($"Processing invalid tickers completed {source}", source));


					//only process data where tickers setup

					progress?.Report(GetProgressManager($"Processing invalid timeseries data points started  {source}", source));

					var timeSeriesDataPointsWhereTickerIsSetup =
						distinctTimeSeriesDataPoints.Where(x => !tickersWhereTickerIsNotSetup.Any(y => y.Symbol.Equals(x.Symbol))).ToList();
					//validate tickers - close and outliers etc.

					var validatedTimeSeriesDataPoint = timeSeriesDataPointsWhereTickerIsSetup.Select(timeSeries => { return DataValidationRules.IsTimeSeriesDataValid(timeSeries); });


					//invalid timeseries data points
					var inValidatedTimeSeriesDataPoint = validatedTimeSeriesDataPoint.Where(x => !x.Value);

					var timeSeriesDataPointErrorLogs = GetTimeSeriesDataDataPointErrorLog(inValidatedTimeSeriesDataPoint);

					if (timeSeriesDataPointErrorLogs.Any())
					{
						//foreach (var timeSeriesDataPointErrorLog in timeSeriesDataPointErrorLogs)
						ctx.DataErrorLog.AddRange(timeSeriesDataPointErrorLogs);
						await ctx.SaveChangesAsync();
					}
					
					progress?.Report(GetProgressManager($"Processing invalid time series data point completed  {source}", source));


					//process valid timeseriesdata points
					progress?.Report(GetProgressManager($"Processing valid timeseries started  {source}", source));
					var validatedTimeSeries = validatedTimeSeriesDataPoint.Where(x => x.Value).Select(x => x.Item).ToList();

					if (validatedTimeSeries.Any())
					{
						ctx.TimeSeries.AddRange(validatedTimeSeries);
						await ctx.SaveChangesAsync();
					}

					stopWatch.Stop();

					progress?.Report(GetProgressManager($"Processing valid timeseries completed  {source}: Elapsed Seconds {stopWatch.Elapsed.TotalSeconds}", source));

				}


				//	var count = 0;
				//	//todo make parallel
				//	foreach (var timeSeriesDataPoint in distinctTimeSeriesDataPoints)
				//	{

				//		var ticker = ctx.Ticker.Find(timeSeriesDataPoint.Symbol);

				//		var tickerValidation = DataValidationRules.IsTimeSeriesDataValid(ticker);
				//		if (!tickerValidation.Value)
				//		{
				//			var dpel = GetDataPointErrorLog(tickerValidation, timeSeriesDataPoint);
				//			ctx.DataErrorLog.Add(dpel);

				//			continue;

				//		}


				//		var tsdpValidation = DataValidationRules.IsTimeSeriesDataValid(timeSeriesDataPoint);
				//		if (!tsdpValidation.Value)
				//		{

				//			var dpel = GetDataPointErrorLog(tsdpValidation, timeSeriesDataPoint);
				//			ctx.DataErrorLog.Add(dpel);

				//			continue;
				//		}






				//		ctx.TimeSeries.Add(timeSeriesDataPoint);



				//		count++;

				//		if (count % 1000 != 0) continue;
				//		progressItem = new ProgressManager
				//		{
				//			TimeStamp = DateTime.UtcNow,
				//			Progress = 1,
				//			Action = source,
				//			Message = "Still populating record count" + count
				//		};
				//		progress?.Report(progressItem);
				//		await ctx.SaveChangesAsync();
				//	}
				//}

				//progressItem = new ProgressManager
				//{
				//	TimeStamp = DateTime.UtcNow,
				//	Progress = 1,
				//	Action = source,
				//	Message = "PopulateTimeSeriesDataPoint Completed"
				//};

				//progress?.Report(progressItem);

				}
				catch (Exception e)
			{
				return new ContextLoaderDataValidation(false, e.InnerException.ToString());

			}
			return new ContextLoaderDataValidation(true, "TimeSeriesDataPoint completed");
		}




		private static ProgressManager GetProgressManager(string message,string source)
		{
			var progressItem = new ProgressManager
			{
				TimeStamp = DateTime.UtcNow,
				Progress = 1,
				Action = source,
				Message = message
			};

			return progressItem;

		}



		public static async Task<ContextLoaderDataValidation> PopulateData(string source, IEnumerable<Ticker> tickers, IProgress<ProgressManager> progress)
		{
			try
			{
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				progress?.Report(GetProgressManager($"PopulateTickers started total number of securities to process: {tickers.Count()} source {source}", source));

				using (var ctx = new ApplicationDbContext())
				{
					IEnumerable<Ticker> unpopulatedTickers;
					progress?.Report(GetProgressManager($"Bulk update: All securities from goldern source: {source}", source));
					//remove items which are not in goldern source
					if (tickers.Any() && ctx.Ticker.Any())
					{
						progress?.Report(GetProgressManager($"Processing updating tickers started: {source}", source));
						var existingTickers = tickers.Where(x => ctx.Ticker.Any(y => y.Symbol.Equals(x.Symbol)));

						foreach (var existingTicker in existingTickers)
						{
							ctx.Ticker.AddOrUpdate(existingTicker);
						}

						await ctx.SaveChangesAsync();
						progress?.Report(GetProgressManager($"Processing updating tickers completed: {source}", source));
					}

					unpopulatedTickers = tickers.Where(x => !ctx.Ticker.Any(y => y.Symbol.Equals(x.Symbol))).ToList();

					if (!unpopulatedTickers.Any())
						return new ContextLoaderDataValidation(true, "Tickers data population completed. no new tickers to add.");

					//validate tickers
					var validatedData = unpopulatedTickers.Select(ticker => { return DataValidationRules.IsTickerDataValid(ticker); });

					progress?.Report(GetProgressManager($"Processing invalid tickers started: {source}", source));
					var inValidTickers = validatedData.Where(x => !x.Value);
					var dataPointErrorLogs = GetTickerDataPointErrorLog(inValidTickers).ToList();
					ctx.DataErrorLog.AddRange(dataPointErrorLogs);
					await ctx.SaveChangesAsync();
					progress?.Report(GetProgressManager($"Processing invalid tickers completed: {source}", source));


					progress?.Report(GetProgressManager($"Processing valid tickers started: {source}", source));
					//process valid tickers
					var validTickers = validatedData.Where(x => x.Value).Select(x => x.Item);

					//there should only be one goldern source for the ticker data
					var distinctTickers = validTickers.DistinctBy(x => x.Symbol).ToList();

					ctx.Ticker.AddRange(distinctTickers);
					await ctx.SaveChangesAsync();


					stopWatch.Stop();


					progress?.Report(GetProgressManager(
						$"Processing valid tickers completed: {source}, Elapsed Seconds {stopWatch.Elapsed.TotalSeconds}", source));

				}

				progress?.Report(GetProgressManager($"PopulateTimeSeriesDataPoint Completed {source}", source));
			}
			catch (Exception e)
			{
				return new ContextLoaderDataValidation(false, e.InnerException.ToString());

			}

			return new ContextLoaderDataValidation(true, "Tickers data population completed");

		}


	
		//assign an interface so we get the items properties
		private static IEnumerable<DataPointErrorLog> GetTimeSeriesDataDataPointErrorLog(IEnumerable<LoaderDataValidation<TimeSeriesDataPoint>> inValidData)
		{
			
			var dataPointErrorLogs = inValidData.Select(dataLoadValidation =>
			{
				var dpel = new DataPointErrorLog
				{
					ClassName = dataLoadValidation.Item.GetType().ToString(),
					Message = dataLoadValidation.Message,
					Symbol = dataLoadValidation.Item.Symbol,
					Date = dataLoadValidation.Item.Date,
					Source = dataLoadValidation.Item.Source,
					LastUpdated = ConfigDataHandler.SystemName,
					UpdateDate = DateTime.UtcNow
				};

				return dpel;
			});

			return dataPointErrorLogs;
		}
		/// <summary>
		/// Invalid data to be assigned a data log message
		/// </summary>
		/// <param name="inValidData"></param>
		/// <returns></returns>
		private static IEnumerable<DataPointErrorLog> GetTickerDataPointErrorLog(IEnumerable<LoaderDataValidation<Ticker>> inValidData)
		{
			
			var dataPointErrorLogs = inValidData.Select(dataLoadValidation =>
			{
				var dpel = new DataPointErrorLog
				{
					ClassName = dataLoadValidation.Item.GetType().ToString(),
					Message = dataLoadValidation.Message,
					Symbol = dataLoadValidation.Item.Symbol,
					Date = DateTime.UtcNow,
					Source = dataLoadValidation.Item.Source,
					LastUpdated = ConfigDataHandler.SystemName,
					UpdateDate = DateTime.UtcNow
				};

				return dpel;
			});

			return dataPointErrorLogs;
		}
	}
}
