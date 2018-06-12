using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using Core.Interfaces.Services;
using DataSource.Providers;
using DataSource.Service;
using Microsoft.Practices.Unity;
using SecuritiesLoaderWebApi.Resolver;

namespace SecuritiesLoaderWebApi.IntergrationTest
{
	public class UnityConfig
	{

		public static void Register(HttpConfiguration config)
		{
			var container = new UnityContainer();


			Trace.TraceInformation("Register set");

			config.DependencyResolver = new UnityResolver(container);

			if (!container.IsRegistered<ISecuritiesService>())
			{
				var ctx = new ApplicationDbContext();
				var securitiesService = new SecuritiesService(ctx, "BOB,ALICE,CHARLIE");

				container.RegisterInstance(typeof(ISecuritiesService), securitiesService);
			}


			Trace.TraceInformation("UnityContainerRegistration completed");

		}

	}
}
