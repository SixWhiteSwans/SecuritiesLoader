using System;
using System.Runtime.Serialization;

namespace Core.Model
{
	public class DataPoint
	{


		public DataPoint()
		{

		}


		public DataPoint(SerializationInfo info, StreamingContext context) 
		{
			Symbol = (string)info.GetValue("Symbol", typeof(string));	
			Sector = (string)info.GetValue("Sector", typeof(string));
			SubIndustry = (string)info.GetValue("SubIndustry", typeof(string));

			Source = (string)info.GetValue("Source", typeof(string));
			Close = (double)info.GetValue("Close", typeof(double));
			Date = (DateTime)info.GetValue("Date", typeof(DateTime));

		}
		
		[DataMember]		
		public string Symbol { get; set; }

		[DataMember]
		
		public string Sector { get; set; }
		[DataMember]		
		public string SubIndustry { get; set; }

		[DataMember]

		public string Source { get; set; }

		[DataMember]
		public double? Close { get; set; }

		[DataMember]
		public DateTime Date { get; set; }


		public  void GetObjectData(SerializationInfo info, StreamingContext context)
		{

			info.AddValue("Symbol", Symbol);
	
			info.AddValue("Sector", Sector);
			info.AddValue("SubIndustry", SubIndustry);
			info.AddValue("SubIndustry", SubIndustry);
			info.AddValue("Source", Source);
			info.AddValue("Close", Close);
			info.AddValue("Date", Date);




		}


	}
}
