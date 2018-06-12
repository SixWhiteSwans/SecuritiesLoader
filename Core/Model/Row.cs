using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Interfaces.Securities;

namespace Core.Model
{
	[DataContract(Name = "Row")]
	[Serializable]
	public class Row: Audit,  ISerializable
	{


		public Row()
		{

		}


		public Row(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			foreach (SerializationEntry entry in info)
			{
				switch (entry.Name)
				{
					case "Name":
						Name = (string)info.GetValue("Name", typeof(string)); break;
					case "Symbol":
						Name = (string)info.GetValue("Symbol", typeof(string)); break;
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


		[DataMember]
		[XmlElement(ElementName = "date")]
		public DateTime Date { get; set; }
		[DataMember]
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[DataMember]
		[XmlElement(ElementName = "close")]
		public double? Close { get; set; }


		public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Date", Date);
			info.AddValue("Name", Name);
			info.AddValue("Close", Close);

			base.GetObjectData(info, context);



		}
	}
}
