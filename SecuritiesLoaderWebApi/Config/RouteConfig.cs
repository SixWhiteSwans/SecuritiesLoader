using System;
using System.Web.Http;

namespace SecuritiesLoaderWebApi.Config
{
	public class RouteConfig
	{
		public static void RegisterRoutes(HttpConfiguration config)
		{

			var routes = config.Routes;

			routes.MapHttpRoute(
				name: "ActionApi",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

		}
	}
}
