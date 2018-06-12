using System;
using Core.Model;
using NUnit.Framework;

namespace Core.Tests
{


	[TestFixture()]
	public class ValidFileInfoTest
	{
		//TODO: PUT INTO CONFIG
		private string path = @"C:\Users\Anthony Johnson\hsbc_codeproject\SecuritiesLoader\Data\securities.json";


		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void ValidateFileInfo()
		{
			var valid = new ValidFileInfo(path,"Source",FileTypes.Json);
			Assert.IsNotNull(valid.Value); 

		}

		[Test]
		public void InValidateFileInfo()
		{
			var ex = Assert.Throws<ArgumentException>(() => new ValidFileInfo("FileDoesNot Exist","Source", FileTypes.Json));			
			StringAssert.AreEqualIgnoringCase(ex.Message , "The file path is not valid.");

		}

	}
}

