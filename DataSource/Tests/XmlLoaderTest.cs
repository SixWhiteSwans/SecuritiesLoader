using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using DataSource.Loaders;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DataSource.Tests
{
	[TestFixture()]
	public class XmlLoaderTest
	{
		//todo 
		//can the file creation to make the tests shorter.
		//apply these tests to the operation tests cases.


		private readonly XmlLoader _dataLoader = new XmlLoader();
		private string _filePath;
		private ValidFileInfo _validFileInfo;
		[SetUp]
		public void Setup()
		{
			
			_filePath = @"C:\Users\Anthony Johnson\hsbc_codeproject\SecuritiesLoader\Data\";

		}

		[TestCase(@"StocksPrices-CHARLIE.xml", "CHARLIE")]
		public void LoadSecurities(string fileName, string source)
		{
			var fileLocation = string.Concat(_filePath, fileName);
			_validFileInfo = new ValidFileInfo(fileLocation, source,FileTypes.Xml);
			Task<TimeSeries> result = _dataLoader.LoadData(_validFileInfo);
			result.Wait();

			var list = result.Result.ToList();

			Assert.Greater(list.Count, 0);



		}
	}
}
