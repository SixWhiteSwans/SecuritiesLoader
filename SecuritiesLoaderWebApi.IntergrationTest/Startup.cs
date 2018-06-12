using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;
using SecuritiesLoaderWebApi.Config;
using SecuritiesLoaderWebApi.Resolver;

namespace SecuritiesLoaderWebApi.IntergrationTest
{
	public class Startup
	{

		public void Configuration(IAppBuilder app)
		{

			//EntityConfig.Configure(app);

			app.UseCors(CorsOptions.AllowAll);

			var config = new HttpConfiguration();

			UnityConfig.Register(config);
			WebApiConfig.Configure(config);
			RouteConfig.RegisterRoutes(config);

			app.UseWebApi(config);

		}
	}
}
