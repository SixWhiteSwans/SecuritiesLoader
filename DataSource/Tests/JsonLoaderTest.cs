using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.DataSource;
using Core.Model;
using DataSource.Loaders;
using NUnit.Framework;

namespace DataSource.Tests
{
	[TestFixture]
	public class JsonLoaderTest
	{
		private readonly IDataLoader<Ticker> _dataLoader = new JsonLoader<Ticker>();

		//todo 
		//can the file creation to make the tests shorter.
		//apply these tests to the operation tests cases.


		private string _filePath;
		private ValidFileInfo _validFileInfo;
		[SetUp]
		public void Setup()
		{
		
			_filePath = @"C:\Users\Anthony Johnson\hsbc_codeproject\SecuritiesLoader\Data\";
		}

		[TestCase(@"securities.json", "Securities")]
		public void LoadSecurities(string fileName, string source)
		{

			var fileLocation = string.Concat(_filePath, fileName);

			_validFileInfo = new ValidFileInfo(fileLocation, source,FileTypes.Json);
			Task<IEnumerable<Ticker>> result = _dataLoader.LoadData(_validFileInfo);
			result.Wait();

			var list = result.Result;

			Assert.Greater(list.ToList().Count,0);

			

		}

	}
}
