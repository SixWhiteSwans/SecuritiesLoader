using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Core.Interfaces.Securities;

namespace Core.Model
{
	public class Audit: IAudit
	{
		public Audit()
		{
			
		}

		public Audit(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry entry in info)
			{
				switch (entry.Name)
				{
					case "Source":
						Source = (string)info.GetValue("Source", typeof(string));
						break;
					case "UpdateDate":
						UpdateDate = (DateTime)info.GetValue("UpdateDate", typeof(DateTime)); break;
					case "LastUpdated":
						LastUpdated = (string)info.GetValue("LastUpdated", typeof(string));
						break;
				}
			}
		}

		[DataMember]
		[XmlElement(ElementName = "Source")]
		public string Source { get; set; }

		[DataMember]
		[XmlElement(ElementName = "UpdateDate")]
		public DateTime? UpdateDate { get; set; }

		[DataMember]
		[XmlElement(ElementName = "LastUpdated")]
		public string LastUpdated { get; set; }

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Source", Source);
			info.AddValue("UpdateDate", UpdateDate);
			info.AddValue("LastUpdated", LastUpdated);


		}
	}
}
