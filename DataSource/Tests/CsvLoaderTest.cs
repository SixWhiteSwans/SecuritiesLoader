using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Model;
using DataSource.Loaders;
using NUnit.Framework;

namespace DataSource.Tests
{
	public class CsvLoaderTest
	{

		//todo 
		//can the file creation to make the tests shorter.
		//apply these tests to the operation tests cases.

		private readonly IDataLoaderTimeSeries _dataLoader = new CsvLoader();
		private string _filePath;
		private ValidFileInfo _validFileInfo;
		[SetUp]
		public void Setup()
		{
			var fileName = ConfigurationManager.AppSettings["ALICE"];
			_filePath = @"C: \Users\Anthony Johnson\hsbc_codeproject\SecuritiesLoader\Data\";
		}

		[TestCase("StocksPrices-ALICE.csv", "ALICE")]
		public void LoadSecurities(string fileName, string source)
		{
			var fileLocation = string.Concat(_filePath, fileName);
			_validFileInfo = new ValidFileInfo(fileLocation, source,FileTypes.Csv);
			Task<TimeSeries> result = _dataLoader.LoadData(_validFileInfo);
			result.Wait();

			var list = result.Result.ToList();

			Assert.Greater(list.Count, 0);



		}

	}
}
