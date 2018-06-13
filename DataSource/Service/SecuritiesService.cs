using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces.Services;
using Core.Model;
using Core.Utils;
using DataSource.Providers;

namespace DataSource.Service
{
	public class SecuritiesService: ISecuritiesService
	{
		private readonly ApplicationDbContext DbContext;

		public SecuritiesService(ApplicationDbContext dbContext,string sourceList)
		{
			DbContext = dbContext;
			SourceList = sourceList;

		}
		
		public string SourceList { get;private  set; }

		public async Task<IEnumerable<DataPoint>> GetMultiSecuritiesTimeSeries(SecurityQuery securityQuery, DateTime startDate, DateTime endDate)
		{

			//handle is enddate<startdate: start date = enddate. first date defined
			if (endDate < startDate)
				startDate =endDate;
			
			//data error

			if (string.IsNullOrEmpty(SourceList))
				return new List<DataPoint>();


			var sourceHierarchyQueue = new Queue<string>(SourceList.Split(','));

			var sql = securityQuery.GetTickerSql();

			IEnumerable<string> symbols;
			var sqlResults = DbContext.Ticker.SqlQuery(sql);
			try
			{
				symbols = sqlResults.ToList().Select(x => x.Symbol);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
			

			var tsdp = DbContext.TimeSeries.Where(x => symbols.Any(y=>y.Equals(x.Symbol)) && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(x.Date) <= DbFunctions.TruncateTime(endDate));
			
			
			if (!string.IsNullOrEmpty(securityQuery.Source))
				 tsdp = tsdp.Where(x => x.Source.Equals(securityQuery.Source));


			var results = tsdp.ToList();

			IEnumerable<DataPoint> dataPoints;

			try
			{
				dataPoints = results.Select(x =>
				{

					var ticker = sqlResults.SingleOrDefault(y => y.Symbol.Equals(x.Symbol));
					var dp = new DataPoint
					{
						Source = x.Source,
						Symbol = x.Symbol,
						Date = x.Date,
						Close = x.Close,
						Sector = ticker.Sector,
						SubIndustry = ticker.SubIndustry
					};


					return dp;

				});

				var data = await ProcessSourceHierarchy(dataPoints, sourceHierarchyQueue);
				
				if (string.IsNullOrEmpty(securityQuery.OrderByField))
					return data.OrderBy(x => x.Date);



				var isReverse = string.IsNullOrEmpty(securityQuery.OrderBy) || securityQuery.Equals("ASC");
				var ordered = data.AsQueryable().Sort(securityQuery.OrderByField, isReverse).OfType<DataPoint>();

				return ordered;

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
		
			}


			return new List<DataPoint>();

		}


		public async Task<IEnumerable<Ticker>> GetTickers()
		{
			return await Task.Run(() =>
			{

				return DbContext.Ticker.ToList();
			});

			
		}

		public async Task<IEnumerable<DataPoint>> GetSecuritiesTimeSeries(string symbol,DateTime startDate, DateTime endDate,string order,string source)
		{

			//handle is enddate<startdate: start date = enddate. first date defined
			if (endDate < startDate)
				startDate = endDate;

			
				try
				{
						var count = DbContext.Ticker.Count();

					var ticker = DbContext.Ticker.Find(symbol);
					var tsdp = DbContext.TimeSeries.Where(x => x.Symbol.Equals(ticker.Symbol) && DbFunctions.TruncateTime(x.Date) >= DbFunctions.TruncateTime(startDate) && DbFunctions.TruncateTime(x.Date) <= DbFunctions.TruncateTime(endDate)).ToList();


					var dataPoints = tsdp.ToList().Select(x =>
					{
						var dp = new DataPoint
						{
							Source = x.Source,
							Symbol = x.Symbol,
							Date = x.Date,
							Close = x.Close,
							Sector = ticker.Sector,
							SubIndustry = ticker.SubIndustry

						};
						return dp;
					});

					var sourceLlist = string.IsNullOrEmpty(source) ? SourceList : source;

					var data = await ProcessSourceHierarchy(dataPoints, GetSourceHierarchyQueue(sourceLlist));

					var isReverse = string.IsNullOrEmpty(order) || order.ToUpper().Equals("ASC");
					var ordered = isReverse ? data.OrderBy(x => x.Date) : data.OrderByDescending(x => x.Date);

					return ordered.ToList();
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}

				

	

			

		}

		private Queue<string> GetSourceHierarchyQueue(string source)
		{
			return new Queue<string>(source.Split(',')); ;
			
		}


		public async Task<IEnumerable<DataPoint>> ProcessSourceHierarchy(IEnumerable<DataPoint> timeSeriesDataPoints, Queue<string> sourceHierarchyQueue)
		{
			return await Task.Run(() =>
			{
				var data = new List<DataPoint>();
				
				foreach (var source in sourceHierarchyQueue)
				{
					var filtered = timeSeriesDataPoints.ToList().Where(x => x.Source.Equals(source));
					var result = filtered.Where(t => !data.Any(f => f.Symbol.Equals(t.Symbol) && f.Date.Equals(t.Date)));
					data.AddRange(result);
					
				}

				return data;

			});
			
	
		
		}
	}
}
