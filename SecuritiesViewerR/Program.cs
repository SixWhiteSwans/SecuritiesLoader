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

                            EvaluateExpression(engine);
				
                            Console.ReadKey();
                }

            }

            private static void EvaluateExpression(REngine engine)
            {
                              

                var lib = "jsonlite";

                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("library(\"jsonlite\")");
                stringBuilder.AppendLine("url<-\"http://localhost:8080/api/timeseries/tickers\"");
                stringBuilder.AppendLine("jsondata<-fromJSON(url)");
                stringBuilder.AppendLine("jsondata");


                var script = stringBuilder.ToString();
                                 
                var result = engine.Evaluate(script);
				Console.WriteLine(result);
                              

            }

	}
}
