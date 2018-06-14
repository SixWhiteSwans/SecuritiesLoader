using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RDotNet;

namespace SecuritiesViewerR
{
	class Program
	{
		private static readonly string BaseAddress = "http://localhost:8080/";
		private static readonly string ApiBaseRequestPath = "api/timeseries/";

		static void Main(string[] args)
		{
			using (REngine engine = REngine.GetInstance())
			{
				engine.Initialize();

				var result = EvaluateExpression(engine);

				Console.Write(result);
				Console.ReadKey();
			}

		}

		private static object EvaluateExpression(REngine engine)
		{
	
			var startDate = DateTime.Parse("01/01/2018");
			var endDate = DateTime.Parse("01/06/2018");
			var symbol = "AAPL";

			var time_slice = $"start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}";
			var url = $"{ApiBaseRequestPath}securitydatapoints?{time_slice}&symbol={symbol}";
			
			var result = engine.Evaluate($"symbolData <-fromJSON({url})");
			

			return result;
		}
	}
}
