using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace DataSource.Loaders
{
	public static class DataValidationRules
	{

		//todo: checks:
		//close: is a valid number: not na
		//close: we want to check for outliers
		//security: security exists in the ticker data set. Only save data is it exists.
		//date is a valid trading date. check holiay calendar and if a valid trading day.


		public static LoaderDataValidation<Ticker> IsTimeSeriesDataValid(Ticker ticker)
		{

			if (ticker==null)
			{
				return new LoaderDataValidation<Ticker>(ticker,false, "100: Ticker is not defined in securities data set");
			}

			return new LoaderDataValidation<Ticker>(ticker,true, string.Empty);
		}
		public static LoaderDataValidation<TimeSeriesDataPoint> IsTimeSeriesDataValid(TimeSeriesDataPoint timeSeriesDataPoint)
		{

			if (!timeSeriesDataPoint.Close.HasValue || double.IsNaN((double)timeSeriesDataPoint.Close))
			{
				return new LoaderDataValidation<TimeSeriesDataPoint>(timeSeriesDataPoint,false, "101: Closing value is NA");
			}

			return new LoaderDataValidation<TimeSeriesDataPoint>(timeSeriesDataPoint,true, string.Empty);
		}

		public static LoaderDataValidation<Ticker> IsTickerDataValid(Ticker ticker)
		{

			if (string.IsNullOrEmpty(ticker.Symbol) || string.IsNullOrEmpty(ticker.Sector) || string.IsNullOrEmpty(ticker.Security) || string.IsNullOrEmpty(ticker.SubIndustry))
			{
				return new LoaderDataValidation<Ticker>(ticker, false, "All fields have to be populated");
			}

			return new LoaderDataValidation<Ticker>(ticker, true, string.Empty);
		}

	}
}
