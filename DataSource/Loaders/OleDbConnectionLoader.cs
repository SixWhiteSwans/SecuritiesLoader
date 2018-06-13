using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Model;
using DataSource.Configuration;

namespace DataSource.Loaders
{

	

	public class OleDbConnectionLoader: IDataLoaderTimeSeries
	{

		public async Task<TimeSeries> LoadData(ValidFileInfo fileInfo)
		{
			return await Task.Run(() =>
			{
				try
				{

					var oleDbConnectionString = new OleDbConnectionString(fileInfo);

					var progressItem = new ProgressManager
					{
						TimeStamp = DateTime.UtcNow,
						Progress = 1,
						Action = "OleDbLoader Load data: ",
						Message = "OleDbLoader Loader Load data started:" + fileInfo.Source
					};

					oleDbConnectionString.ValidFileInfo.ProgressLogger?.Report(progressItem);

					var stopWatch = new Stopwatch();
					stopWatch.Start();

					var timeSeries = new TimeSeries {Source = oleDbConnectionString.ValidFileInfo.Source};

				

					//;HDR=yes
					//;Format=xlsx;IMEX=1
					
					var oledbConn = new System.Data.OleDb.OleDbConnection(oleDbConnectionString.Value);

					var command = new System.Data.OleDb.OleDbDataAdapter(oleDbConnectionString.Sql, oledbConn);

					command.TableMappings.Add("Table", "LoadTable");
					var dtSet = new DataSet();
					command.Fill(dtSet);
					var table = dtSet.Tables[0];



					var dateCol = table.Columns["date"];
					var closeCol = table.Columns["close"];
					var nameCol = table.Columns["Name"];

					foreach (DataRow tableRow in table.Rows)
					{

						var date = Utils.SafeCastToDate(tableRow[dateCol].ToString());
						var close = Utils.SafeCastToDoubleParse(tableRow[closeCol].ToString());

						var tsdp = new TimeSeriesDataPoint
						{


							Date = Utils.SafeCastToDate(date.ToString()),
							Close = Utils.SafeCastToDoubleParse(close.ToString()),
							Symbol = tableRow[nameCol].ToString(),
							Source = oleDbConnectionString.ValidFileInfo.Source,
							LastUpdated = ConfigDataHandler.SystemName,
							UpdateDate = DateTime.UtcNow
						};
						timeSeries.Add(tsdp);
					}
					
					oledbConn.Close();


					stopWatch.Stop();
					timeSeries.LoadTime = stopWatch.Elapsed;

					progressItem = new ProgressManager
					{
						TimeStamp = DateTime.UtcNow,
						Progress = 1,
						Action = "OleDbLoader Load data",
						Message = "OleDbLoader Loader Load data stopped" + fileInfo.Source
					};

					oleDbConnectionString.ValidFileInfo.ProgressLogger?.Report(progressItem);

					return timeSeries;



				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}



				return new TimeSeries();

			});
		}


	}
}
