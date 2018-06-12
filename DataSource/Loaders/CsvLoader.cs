using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Model;
using Core.Utils;
using DataSource.Configuration;

namespace DataSource.Loaders
{
	public class CsvLoader: OleDbConnectionLoader
	{

		//redundant
		//public async Task<TimeSeries> LoadData(ValidFileInfo fileInfo)
		//{
		//	try
		//	{
		//		var timeSeries = new TimeSeries();
		//		var stopWatch = new Stopwatch();
		//		stopWatch.Start();
		//		timeSeries.Source = fileInfo.Source;

		//		var progressItem = new ProgressManager
		//		{
		//			TimeStamp = DateTime.UtcNow,
		//			Progress = 1,
		//			Action = "CsvLoader Load data",
		//			Message = "Csv Loader Load data started"
		//		};

		//		fileInfo.ProgressLogger?.Report(progressItem);

		//		using (var reader = new CsvFileReader(fileInfo.Value.FullName))
		//		{

		//			var isFirstRow = true;
		//			var indexDate = -1;
		//			var indexClose = -1;
		//			var indexName = -1;

		//			var row = new CsvRow();
		//			while (await reader.ReadRow(row))
		//			{
		//				if (row.ToArray().Length == 0)
		//					continue;

		//				if (isFirstRow)
		//				{
		//					//this can be improved to hold a column mappingname and index.
		//					var headers = new List<string>(row.ToArray());
		//					indexDate = headers.IndexOf("date");
		//					indexClose = headers.IndexOf("close");
		//					indexName = headers.IndexOf("Name");
		//					isFirstRow = false;
		//					continue;
		//				}


		//				var tsdp = new TimeSeriesDataPoint
		//				{
		//					Close = Utils.SafeCastToDoubleParse(row[indexClose]),
		//					Date = Utils.SafeCastToDate(row[indexDate]),
		//					Symbol = row[indexName],
		//					Source = fileInfo.Source,
		//					LastUpdated = ConfigDataHandler.SystemName,
		//					UpdateDate = DateTime.UtcNow
		//				};


		//				timeSeries.Add(tsdp);


		//			}

		//			stopWatch.Stop();
		//			timeSeries.LoadTime = stopWatch.Elapsed;

		//			progressItem = new ProgressManager
		//			{
		//				TimeStamp = DateTime.UtcNow,
		//				Progress = 1,
		//				Action = "CsvLoader Load data",
		//				Message = "csv Loader Load data stopped"
		//			};

		//			fileInfo.ProgressLogger?.Report(progressItem);
		//			return timeSeries;


		//		}
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e);
		//		throw;
		//	}

			

			
			
		//	//using (StreamReader reader = new StreamReader(fileInfo.Value.FullName))
		//	//{
		//	//	var csv = new CsvFileReader(new StreamReader(fileInfo.Value.FullName).BaseStream);
	
		//	//	//var tsdp = new TimeSeriesDataPoint(); ;
		//	//	////TODO: CLEAN UP TO HANDLING OF THIS.
		//	//	//tsdp.Date = DateTime.Parse(ConvertValueToString(element, "date"));
		//	//	//tsdp.Name = ConvertValueToString(element, "Name");
		//	//	//tsdp.Close = ConvertValueToDouble(element, "close");
		//	//	//ts.Add(tsdp);





		//	//}

			

		//}
	}
}
