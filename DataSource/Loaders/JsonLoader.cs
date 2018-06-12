using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Interfaces.Securities;
using Core.Model;
using DataSource.Configuration;
using Newtonsoft.Json;

namespace DataSource.Loaders
{
    public class JsonLoader<T>: IDataLoader<T>
    {		
		public async Task<IEnumerable<T>> LoadData(ValidFileInfo fileInfo)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();
			

			var progressItem = new ProgressManager
			{
				TimeStamp = DateTime.UtcNow,
				Progress = 1,
				Action = "JsonLoader Load data",
				Message = "Json Loader Load data started"
			};

			fileInfo.ProgressLogger?.Report(progressItem);

			using (StreamReader r = new StreamReader(fileInfo.Value.FullName))
			{
				var json = await r.ReadToEndAsync();
				var items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

				var results = items.OfType<IAudit>().Select(x =>
				{
					x.Source = fileInfo.Source;
					x.LastUpdated = ConfigDataHandler.SystemName;
					x.UpdateDate = DateTime.UtcNow;

					return x;

				});


				progressItem = new ProgressManager
				{
					TimeStamp = DateTime.UtcNow,
					Progress = 1,
					Action = "JsonLoader Load data",
					Message = "Json Loader Load data started"
				};

				fileInfo.ProgressLogger?.Report(progressItem);

				return results.OfType<T>();
			}
		}
	}
}
