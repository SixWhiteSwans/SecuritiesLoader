using System;
using System.Reflection;

namespace SecuritiesLoaderWebApi.IntergrationTest.Util
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class BeforeAfterTestAttribute : Attribute
	{

		public virtual void After(MethodInfo methodUnderTest) { }

		public virtual void Before(MethodInfo methodUnderTest) { }
	}
}
