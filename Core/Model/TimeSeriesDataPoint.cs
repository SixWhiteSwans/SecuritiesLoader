using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Interfaces.Securities;

namespace Core.Model
{
	[DataContract(Name = "TimeSeriesDataPoint")]
	[Serializable]
	public class TimeSeriesDataPoint: Audit, ITimeSeriesDataPoint, ISerializable
	{


		public TimeSeriesDataPoint()
		{

		}


		public TimeSeriesDataPoint(SerializationInfo info, StreamingContext context):base(info,context)
		{

			foreach (SerializationEntry entry in info)
			{
				switch (entry.Name)
				{
					case "Name":
						Symbol = (string)info.GetValue("Name", typeof(string)); break;
					case "Symbol":
						Symbol = (string)info.GetValue("Symbol", typeof(string)); break;						
					case "date":					
						Date = (DateTime)info.GetValue("date", typeof(DateTime)); break;
					case "Date":
						Date = (DateTime)info.GetValue("date", typeof(DateTime)); break;
					case "close":					
						Close = (double)info.GetValue("close", typeof(double)); break;
					case "Close":
						Close = (double)info.GetValue("close", typeof(double)); break;
						
				}
			}
		}

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PrimaryTrackingKey { get; set; }

		[DataMember]
		[XmlElement(ElementName = "Name")]
		//[Index("SymbolDateIndex", 1, IsUnique = true)]
		//[ForeignKey("Symbol")]
		public string Symbol { get; set; }


		[DataMember]
		[XmlElement(ElementName = "date")]
		//[Index("SymbolDateIndex", 2, IsUnique = true)]
		public DateTime Date { get; set; }
	
		[DataMember]
		[XmlElement(ElementName = "close")]
		public double? Close { get; set; }


		public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Date", Date);
			info.AddValue("Symbol", Symbol);
			info.AddValue("Close", Close);

			base.GetObjectData(info,context);



		}
	}
}
