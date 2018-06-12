using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Core.Interfaces;

namespace Core.Model
{
	[DataContract(Name = "Ticker")]
	[Serializable]
	public class Ticker: Audit, ITicker,  ISerializable
	{
		public Ticker()
		{
			
		}


		public Ticker(SerializationInfo info, StreamingContext context) : base(info, context)
		{

			foreach (SerializationEntry entry in info)
			{
				switch (entry.Name)
				{
					case "Ticker.symbol":
						Symbol = (string)info.GetValue("Ticker.symbol", typeof(string));
						break;
					case "Symbol":
						Symbol = (string)info.GetValue("Symbol", typeof(string));
						break;
					case "Security":
						Security = (string)info.GetValue("Security", typeof(string)); break;
					case "Sector":
						Sector = (string)info.GetValue("Sector", typeof(string)); break;
					case "SubIndustry":
						SubIndustry = (string)info.GetValue("SubIndustry", typeof(string)); break;				
				}
			}


			
			
			
			

		}
		[Key]
		[DataMember]
		[Required]
		[Index("SymbolIndex", 1, IsUnique = true)]
		public string Symbol { get; set; }
		[DataMember]
		[Required]
		public string Security { get; set; }
		[DataMember]
		[Required]
		public string Sector { get; set; }
		[DataMember]
		[Required]
		public string SubIndustry { get; set; }

		public ICollection<TimeSeriesDataPoint> TimeSeries { get; set; }



		public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
		{

			info.AddValue("Symbol", Symbol);
			info.AddValue("Security", Security);
			info.AddValue("Sector", Sector);
			info.AddValue("SubIndustry", SubIndustry);

			base.GetObjectData(info, context);


		}
	}
}
