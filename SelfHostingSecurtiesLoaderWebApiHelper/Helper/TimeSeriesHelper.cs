using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace SelfHostingSecurtiesLoaderWebApiHelper.Helper
{
	public static class TimeSeriesHelper
	{
		
		private static readonly string ApiBaseRequestPath = "api/timeseries/";

		public static HttpClient GetHttpClient(string baseAddress)
		{

			var httpclient = new HttpClient();

			httpclient.BaseAddress = new Uri(baseAddress);
			httpclient.DefaultRequestHeaders.Accept.Clear();
			//httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/JSON"));


			return httpclient;

		}

		public static void Process(string baseAddress)
		{
			Console.WriteLine("AnalyticsControllerHelper Started");

			var httpclient = GetHttpClient(baseAddress);
			httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/JSON"));

			GetDataPoints(httpclient);
		}

		private static async void GetDataPoints(HttpClient httpclient)
		{

			var symbol = "AAPL";
			var startDate = DateTime.Parse("01/01/2018");
			var endDate = DateTime.Parse("01/06/2018");


			var time_slice = $"&start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
			var url = $"{ApiBaseRequestPath}datapoints?symbol={symbol}{time_slice}";

			var response = await httpclient.GetAsync(url);			
			var result = await response.Content.ReadAsAsync<IEnumerable<DataPoint>>();


			try
			{
				result.ToList().ForEach(x =>
					{
						Console.WriteLine($"symbol={x.Symbol}:date={x.Date}:close={x.Close}");
					}
				);
			}
			catch (AggregateException aggregateException)
			{

				foreach (var e in aggregateException.InnerExceptions)
				{
					Console.WriteLine(e);
				}

			
			}

			


		}

	}
}
