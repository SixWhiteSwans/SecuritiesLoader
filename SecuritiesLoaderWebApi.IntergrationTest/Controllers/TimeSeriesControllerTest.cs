using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Web.Configuration;
using Core.Model;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using SecuritiesLoaderWebApi.IntergrationTest.Util;

namespace SecuritiesLoaderWebApi.IntergrationTest
{
	[TestFixture]
    public class TimeSeriesControllertest
    {
	    
		private static readonly string BaseAddress = "http://localhost:8080/";
	    private static readonly string ApiBaseRequestPath = "api/timeseries/";	  
	    private TestServer _server;

		[SetUp]
		public void Setup()
	    {
		    var sqlp = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

		    _server = TestServer.Create<Startup>();
		    _server.BaseAddress = new Uri(BaseAddress);
		    //_server.HttpClient.DefaultRequestHeaders.Accept.Clear();
		    _server.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

		    var sourceHierarchy2 = ConfigurationManager.AppSettings["SourceHierarchy"];
		    var sourceHierarchy = WebConfigurationManager.AppSettings["SourceHierarchy"];
		}


		[Test]
		[NullCurrentPrincipal]
		public void Returns_200_And_DataPoints_symbol_start_end_dates()
		{

			var symbol = "AAPL";
			var startDate = DateTime.Parse("01/01/2018");
			var endDate = DateTime.Parse("01/06/2018");

			
			var time_slice = $"&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
			var url = $"{ApiBaseRequestPath}datapoints?symbol={symbol}{time_slice}";

			//StringAssert.AreEqualIgnoringCase(url, "api/timeseries/datapoints?symbol=AAPL&start_date=/"2018 - 01 - 01 / "&end_date=/"2018 - 06 - 01 / "");

			var response = _server.HttpClient.GetAsync(url);
			var result = response.Result.Content.ReadAsAsync<IEnumerable<DataPoint>>();

		    Assert.AreEqual(result.Result.Count(), 25);


	    }

	    [Test]
	    [NullCurrentPrincipal]
	    public void Returns_200_And_DataPoints_sector_start_end_dates()
	    {

		    var symbol = "AAPL";
		    var startDate = DateTime.Parse("01/01/2018");
		    var endDate = DateTime.Parse("01/06/2018");


		    var time_slice = $"&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
		    var url = $"{ApiBaseRequestPath}datapoints?symbol={symbol}{time_slice}";

		    //StringAssert.AreEqualIgnoringCase(url, "api/timeseries/datapoints?symbol=AAPL&start_date=/"2018 - 01 - 01 / "&end_date=/"2018 - 06 - 01 / "");

		    var response = _server.HttpClient.GetAsync(url);
		    var result = response.Result.Content.ReadAsAsync<IEnumerable<DataPoint>>();

		    Assert.AreEqual(result.Result.Count(), 25);


	    }

	    [Test]
	    [NullCurrentPrincipal]
	    public void Returns_200_And_SecuritiesDataPoints_sector_start_end_dates()
	    {

		    var sector = "Health Care";
		    var startDate = DateTime.Parse("01/01/2018");
		    var endDate = DateTime.Parse("01/06/2018");


		    var time_slice = $"start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
		    var url = $"{ApiBaseRequestPath}securitydatapoints?{time_slice}&sector={sector}";

		    //StringAssert.AreEqualIgnoringCase(url, "api/timeseries/datapoints?start_date=/"2018-01-01 / "&end_date=/"2018-06-01/"");

		    var response = _server.HttpClient.GetAsync(url);
		    var result = response.Result.Content.ReadAsAsync<IEnumerable<DataPoint>>();

		    Assert.AreEqual(result.Result.Count(), 25);


	    }


		[TearDown]
	    public void TearDown()
	    {
		    _server.Dispose();
		    _server = null;

	    }

	}
}
