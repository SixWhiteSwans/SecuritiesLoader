using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using SecuritiesLoaderWebApi;
using SelfHostingSecurtiesLoaderWebApiHelper.Helper;

namespace SelfHostingSecurtiesLoaderWebApiHelper
{
	class Program
	{
		static void Main(string[] args)
		{
			const string baseAddress = "http://localhost:8080/";
			using (WebApp.Start<Startup>(url: baseAddress))
			{

				TimeSeriesHelper.Process(baseAddress);

				Console.ReadLine();

			}

		}
	}
}
