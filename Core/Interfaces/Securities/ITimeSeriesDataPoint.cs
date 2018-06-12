using System;

namespace Core.Interfaces.Securities
{
	public interface ITimeSeriesDataPoint
	{
		DateTime Date { get; set; }

		string Symbol { get; set; }

		double? Close { get; set; }


	}
}
