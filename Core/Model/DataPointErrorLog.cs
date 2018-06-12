using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
	public class DataPointErrorLog:Audit
	{
		[Key]
		public int PrimaryTrackingKey { get; set; }

		public string ClassName { get; set; }
		public string Symbol { get; set; }
		public DateTime Date { get; set; }

		public string Message { get; set; }

	}
}
