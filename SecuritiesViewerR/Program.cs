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
		static void Main(string[] args)
		{
			using (REngine engine = REngine.GetInstance())
			{
				engine.Initialize();

				var result = EvaluateExpression(engine, "5+2+9");

				Console.Write(result);
				Console.ReadKey();
			}

		}

		private static double EvaluateExpression(REngine engine, string expression)
		{
			var expressionVector = engine.CreateCharacterVector(new[] { expression });
			engine.SetSymbol("expr", expressionVector);

			//  WRONG -- Need to parse to expression before evaluation
			//var result = engine.Evaluate("eval(expr)");

			//  RIGHT way to do this!!!
			var result = engine.Evaluate("eval(parse(text=expr))");
			var ret = result.AsNumeric().First();

			return ret;
		}
	}
}
