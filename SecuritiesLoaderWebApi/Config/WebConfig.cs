using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SecuritiesLoaderWebApi.Config
{
	public class WebApiConfig
	{

		public static void Configure(HttpConfiguration config)
		{
			config.Filters.Add(new AuthorizeAttribute());
		}

	}
}
