using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using DataSource.Providers;

namespace DataSource.Queries
{
	public class TickerQuery
	{
		private readonly ApplicationDbContext _context;
		public TickerQuery(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Ticker> Execute()
		{
			return _context.Ticker.SqlQuery("Select AMAT from Ticker", null);

		}

	}
}
