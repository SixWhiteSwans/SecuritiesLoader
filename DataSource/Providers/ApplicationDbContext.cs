using System.Data.Entity;
using Core.Model;

namespace DataSource.Providers
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(): base("SecuritesContext")	
		{
			
		}

		public DbSet<Ticker> Ticker { get; set; }

		public DbSet<TimeSeriesDataPoint> TimeSeries { get; set; }

		public DbSet<DataPointErrorLog> DataErrorLog { get; set; }

		public DbSet<ProcessDataLog> ProcessDataLog { get; set; }
	}
}
