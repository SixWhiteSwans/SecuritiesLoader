using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Core.Interfaces.Services;
using Core.Model;
using Microsoft.Practices.Unity;

namespace SecuritiesLoaderWebApi.Controller
{
	[RoutePrefix("api/TimeSeries")]
	[AllowAnonymous]	
	public class TimeSeriesController : ApiController
	{
		[Dependency]
		public ISecuritiesService SecuritiesService { get; set; }

		private string _sourceHierarchy;
		public TimeSeriesController(IUnityContainer container)
		{
			SecuritiesService = container.Resolve<ISecuritiesService>();
			_sourceHierarchy = ConfigurationManager.AppSettings["SourceHierarchy"];
		}

		//TODO: we can cache the static and queried data on request or on startup.		
		[HttpGet]
		[Route("Tickers")]
		[ActionName("Tickers")]
		[AllowAnonymous]
		[ResponseType(typeof(IEnumerable<Ticker>))]
		public async Task<IHttpActionResult> GetTickers()
		{

			//we can log to db as well. but trace logs to the service logger and can be accessed if migrated to azure
			Trace.TraceInformation("GetTickers request");

			var tickers = await SecuritiesService.GetTickers();

			Trace.TraceInformation("GetTickers queries");
			return Ok(tickers);

		}


		[HttpGet]
		[Route("DataPoints")]
		[ActionName("DataPoints")]

		[AllowAnonymous]
		[ResponseType(typeof(IEnumerable<DataPoint>))]
		public async Task<IHttpActionResult> GetDataPoints(string symbol,string start_date,string end_date, string source=null,string order=null)
		{
			
			//we can log to db as well. but trace logs to the service logger and can be accessed if migrated to azure
			Trace.TraceInformation("GetDataPoints request");


			if (!DateTime.TryParse(start_date, out var startDate) || !DateTime.TryParse(end_date, out var endDate))
				return BadRequest("Date Format is not correct. Please pass in format: yyyy-MM-dd");

		
			var dataPoints = await SecuritiesService.GetSecuritiesTimeSeries(symbol, startDate, endDate, order,source);

			Trace.TraceInformation("GetDataPoints queries");
			return Ok(dataPoints);

		

		}

		[HttpGet]
		[Route("SecurityDataPoints")]
		[ActionName("SecurityDataPoints")]

		[AllowAnonymous]
		[ResponseType(typeof(IEnumerable<DataPoint>))]
		public async Task<IHttpActionResult> GetSecurityDataPoints(string start_date, string end_date, string symbol=null,string sector=null, string subIndustry = null, string source = null, string order=null, string orderField=null)
		{
			sector = sector == "null" ? string.Empty : sector;
			subIndustry = subIndustry == "null" ? string.Empty : subIndustry;
			source = source == "null" ? string.Empty : source;
			order = order == "null" ? string.Empty : order;
			orderField = orderField == "null" ? string.Empty : orderField;


			//we can log to db as well. but trace logs to the service logger and can be accessed if migrated to azure
			Trace.TraceInformation("GetDataPoints request");

			if (!DateTime.TryParse(start_date, out var startDate) || !DateTime.TryParse(end_date, out var endDate))
				return BadRequest("Date Format is not correct. Please pass in format: dd-mmm-yyyy");



			if (!string.IsNullOrEmpty(order) && string.IsNullOrEmpty(orderField))
				return BadRequest("An order field is required when sorting asc or desc requested. The field is either: Sector,SubIndustry,Source");


			var fields = new List<string>() { "Sector", "SubIndustry", "Source" };

			if (!string.IsNullOrEmpty(orderField) && !fields.Contains(orderField))
				return BadRequest("An order field can be only either: Sector,SubIndustry,Source");


			if (!string.IsNullOrEmpty(symbol))
			{
				var securityDataPoints = await SecuritiesService.GetSecuritiesTimeSeries(symbol, startDate, endDate, order, source);
				Trace.TraceInformation("GetDataPoints queries");
				return Ok(securityDataPoints);
			}
			

			var securityQuery = new SecurityQuery
			{
				Source = source,
				Sector = sector,
				SubIndustry = subIndustry,
				OrderBy = order,
				OrderByField = orderField
			};


			var dataPoints = await SecuritiesService.GetMultiSecuritiesTimeSeries(securityQuery, startDate, endDate);

			Trace.TraceInformation("GetDataPoints queries");
			return Ok(dataPoints);



		}






	}
}
