using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Loaders;

namespace SelfHostingSecuritiesLoader
{
	public static class Processor
	{

		public static void Run()
		{
			try
			{
				var stopwatch = new Stopwatch();
				stopwatch.Start();
				Console.WriteLine("Securities Batch Loader Started");

				var task = TimeSeriesBatchJob.RunTickerLoader().ContinueWith((t) =>
				{
					var timeSeriesLoader = TimeSeriesBatchJob.RunTimeSeriesDataPointsLoader();

					try
					{
						foreach (var validation in timeSeriesLoader.Result)
						{
							Console.WriteLine(validation.Message);
						}
					}
					catch (AggregateException e)
					{

						foreach (var exceptions in e.InnerExceptions)
						{
							Console.WriteLine(exceptions.InnerException);
						}
					}



				});


				Task.WaitAll(task);

				stopwatch.Stop();
				Console.WriteLine($"Securities Batch Loader Completed:  Total Seconds {stopwatch.Elapsed.TotalSeconds}");



			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

		}
		
	}
}
