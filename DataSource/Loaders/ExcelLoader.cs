//using System.Threading.Tasks;
//using Core.Interfaces.DataSource;
//using Core.Model;
//using Excel = Microsoft.Office.Interop.Excel;

namespace DataSource.Loaders
{
	public class ExcelLoader : OleDbConnectionLoader
	{

		//redundant
		//public async Task<TimeSeries> LoadData(ValidFileInfo fileInfo)
		//{
		//	var old = new OleDbConnectionString(fileInfo);
		//	return await OleDbConnectionLoader.LoadData(old);

		//	//return await Task.Run(() =>
		//	//{
		//	//	try
		//	//	{
		//	//		var progressItem = new ProgressManager
		//	//		{
		//	//			TimeStamp = DateTime.UtcNow,
		//	//			Progress = 1,
		//	//			Action = "ExcelLoader Load data",
		//	//			Message = "Excel Loader Load data started"
		//	//		};

		//	//		fileInfo.ProgressLogger?.Report(progressItem);

		//	//		var stopWatch = new Stopwatch();
		//	//		stopWatch.Start();

		//	//		var timeSeries = new TimeSeries();
		//	//		timeSeries.Source = fileInfo.Source;


		//	//		var conString = $"provider = Microsoft.Jet.OLEDB.4.0; Data Source = '{fileInfo.Value.FullName}'; Extended Properties = Excel 8.0;";
		//	//		//;HDR=yes
		//	//		//;Format=xlsx;IMEX=1
		//	//		System.Data.OleDb.OleDbConnection MyConnection;
		//	//		DataSet DtSet;
		//	//		System.Data.OleDb.OleDbDataAdapter MyCommand;
		//	//		MyConnection = new System.Data.OleDb.OleDbConnection(conString);
		//	//		MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [StocksPrices-BOB$]", MyConnection);
		//	//		MyCommand.TableMappings.Add("Table", "LoadTable");
		//	//		DtSet = new DataSet();
		//	//		MyCommand.Fill(DtSet);
		//	//		var table =  DtSet.Tables[0];




		//	//		var dateCol = table.Columns["date"];
		//	//		var closeCol = table.Columns["close"];
		//	//		var nameCol = table.Columns["Name"];

		//	//		foreach (DataRow tableRow in table.Rows)
		//	//		{

		//	//			var date = Utils.SafeCastToDate(tableRow[dateCol].ToString());
		//	//			var close = Utils.SafeCastToDoubleParse(tableRow[closeCol].ToString());

		//	//			var tsdp = new TimeSeriesDataPoint
		//	//			{


		//	//				Date = Utils.SafeCastToDate(date.ToString()),
		//	//				Close = Utils.SafeCastToDoubleParse(close.ToString()),
		//	//				Symbol = tableRow[nameCol].ToString(),
		//	//				Source = fileInfo.Source,
		//	//				LastUpdated = ConfigDataHandler.SystemName,
		//	//				UpdateDate = DateTime.UtcNow
		//	//			};
		//	//			timeSeries.Add(tsdp);
		//	//		}


		//	//		MyConnection.Close();


		//	//	stopWatch.Stop();
		//	//	timeSeries.LoadTime = stopWatch.Elapsed;

		//	//	progressItem = new ProgressManager
		//	//	{
		//	//		TimeStamp = DateTime.UtcNow,
		//	//		Progress = 1,
		//	//		Action = "ExcelLoader Load data",
		//	//		Message = "Excel Loader Load data stopped"
		//	//	};

		//	//	fileInfo.ProgressLogger?.Report(progressItem);

		//	//	return timeSeries;



		//	//	}
		//	//	catch (Exception ex)
		//	//	{
		//	//		Console.WriteLine(ex.Message);
		//	//	}



		//	//	return new TimeSeries();

		//	//});
		//}

		//public async Task<TimeSeries> LoadData(ValidFileInfo fileInfo)
		//{
		//	return await Task.Run(() =>
		//	{

		//		Excel.Range newRng = null;

		//		try
		//		{


		//			//newRng = xlApp.get_Range(xlWorksheet.Cells[1, 1], xlWorksheet.Cells[1, 1]);
		//		}
		//		catch (Exception e)
		//		{
		//			Console.WriteLine(e);
		//			throw;
		//		}

		//		Excel.Application xlApp = new Excel.Application();
		//		Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileInfo.Value.FullName);
		//		Excel._Worksheet xlWorksheet = xlApp.Sheets[1];

		//		//Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];

		//		//Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
		//		//Excel.Range xlRange = xlWorksheet.UsedRange;
		//		newRng = xlWorksheet.UsedRange;

		//		var progressItem = new ProgressManager
		//		{
		//			TimeStamp = DateTime.UtcNow,
		//			Progress = 1,
		//			Action = "ExcelLoader Load data",
		//			Message = "Excel Loader Load data started"
		//		};

		//		fileInfo.ProgressLogger?.Report(progressItem);

		//		try
		//		{
		//			var stopWatch = new Stopwatch();
		//			stopWatch.Start();

		//			var timeSeries = new TimeSeries();
		//			timeSeries.Source = fileInfo.Source;

		//			var columns = newRng.get_End(Excel.XlDirection.xlToRight).Column;
		//			var rows = newRng.get_End(Excel.XlDirection.xlDown).Row;


		//			//todo can improve
		//			var closeIndex = GetColumnIndex(newRng, columns, "close");
		//			var nameIndex = GetColumnIndex(newRng, columns, "Name");
		//			var dateIndex = GetColumnIndex(newRng, columns, "date");

		//			//firt rows is the header row
		//			for (var i = 2; i < rows; i++)
		//			{
		//				var tsdp = new TimeSeriesDataPoint
		//				{
		//					Date = Utils.SafeCastToOADate(newRng.Cells[i, dateIndex].Value2.ToString()),
		//					Close = Utils.SafeCastToDoubleParse(newRng.Cells[i, closeIndex].Value2.ToString()),
		//					Symbol = newRng.Cells[i, nameIndex].Value2.ToString(),
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
		//				Action = "ExcelLoader Load data",
		//				Message = "Excel Loader Load data stopped"
		//			};

		//			fileInfo.ProgressLogger?.Report(progressItem);

		//			return timeSeries;
		//		}
		//		finally
		//		{
		//			GC.Collect();
		//			GC.WaitForPendingFinalizers();
		//			Marshal.ReleaseComObject(newRng);
		//			Marshal.ReleaseComObject(xlWorksheet);
		//			Marshal.ReleaseComObject(xlWorkbook);
		//			xlApp.Quit();
		//			Marshal.ReleaseComObject(xlApp);

		//		}

		//	});

		//}

		//private int GetColumnIndex(Excel.Range xlRange,int columns, string name)
		//{

		//	for (var j = 1; j <= columns; j++)
		//	{
		//		string colName = xlRange.Cells[1, j].Value2.ToString();

		//		if (name.ToUpper().Equals(colName.ToUpper()))
		//			return j;
		//	}
		//	return -1;

		//}

	}
}
