using System.Reflection;
using System.Threading;

namespace SecuritiesLoaderWebApi.IntergrationTest.Util
{
	public class NullCurrentPrincipalAttribute : BeforeAfterTestAttribute
	{
		public override void Before(MethodInfo methodUnderTest)
		{

			Thread.CurrentPrincipal = null;
		}
	}
}
