using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Core.Model
{
	[DataContract(Name = "TimeSeries")]
	[Serializable]
	[XmlRoot("root")]
	public class TimeSeries: List<TimeSeriesDataPoint>
	{
		
		public TimeSeries()
		{
			
		}

		public TimeSeries(SerializationInfo info, StreamingContext context)
		{
			var list = (List<TimeSeriesDataPoint>)info.GetValue("TimeSeriesDataPoints", typeof(List<TimeSeriesDataPoint>));

			this.AddRange(list);
			TimeSeriesDataPoints = list;

		}

		[XmlArrayItem(typeof(Row))]
		[XmlArray("root")]
		[XmlElement(ElementName = "row")]
		public List<TimeSeriesDataPoint> TimeSeriesDataPoints { get; set; } = new List<TimeSeriesDataPoint>();

		public TimeSpan LoadTime { get; set; }

		public string Source { get; set; }


		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("TimeSeriesDataPoints", TimeSeriesDataPoints);
		}
	}
}
