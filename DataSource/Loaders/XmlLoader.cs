using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Core.Interfaces.DataSource;
using Core.Model;

namespace DataSource.Loaders
{
	public class XmlLoader : IDataLoaderTimeSeries
	{
		
		public async Task<TimeSeries> LoadData(ValidFileInfo fileInfo)
		{
			return await Task.Run(() =>
			{
				

				var timeSeries = new TimeSeries {Source = fileInfo.Source};
				var stopWatch = new Stopwatch();
				stopWatch.Start();

				var progressItem = new ProgressManager
				{
					TimeStamp = DateTime.UtcNow,
					Progress = 1,
					Action = "XmlLoader Load data",
					Message = $"Xml Loader Load data started {fileInfo.Source}"
				};

				fileInfo.ProgressLogger?.Report(progressItem);


				using (Stream s = File.OpenRead(fileInfo.Value.FullName))
				{
					var xdoc = XElement.Load(s, LoadOptions.None);
					foreach (var element in xdoc.Elements())
					{
						//.AsParallel() hold of parallising for the moment.
						//this can be improved to hold a column mappingname names.
						var tsdp = new TimeSeriesDataPoint
						{
							Date = Utils.ConvertValueToDate(element, "date"),
							Symbol = Utils.ConvertValueToString(element, "Name"),
							Close = Utils.ConvertValueToDouble(element, "close"),
							Source = fileInfo.Source,
							LastUpdated = "AutoLoad",
							UpdateDate = DateTime.UtcNow
						};
						timeSeries.Add(tsdp);
					}

				stopWatch.Stop();
				timeSeries.LoadTime = stopWatch.Elapsed;

					progressItem = new ProgressManager
					{
						TimeStamp = DateTime.UtcNow,
						Progress = 1,
						Action = "XmlLoader Load data",
						Message = $"Xml Loader Load data stopped {fileInfo.Source}"
					};

					fileInfo.ProgressLogger?.Report(progressItem);

					return timeSeries;
			
				}
			});


			//return await Task.Run(() =>
			//{


			//	var deserializer = new XmlSerializer(typeof(TimeSeries));

			//	Stream s = File.OpenRead(fileInfo.Value.FullName);
			//	var xdoc = XElement.Load(s, LoadOptions.None);

			//	//s.Position = 0;
			//	//var x = new StreamReader(s);
			//	//var xy = deserializer.Deserialize(x);

			//	using (StreamReader reader = new StreamReader(fileInfo.Value.FullName))
			//	{								
			//		var obj = deserializer.Deserialize(reader);
			//		var xmlData = (List<TimeSeriesDataPoint>)obj;
			//		return new TimeSeries();
			//	}



			//});





		}

	

		
	}

	
}
