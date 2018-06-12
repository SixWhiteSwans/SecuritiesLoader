using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
	public class ProcessDataLog
	{

		[Key]
		public int PrimaryTrackingKey { get; set; }

		public string Action { get; set; }

		public DateTime UpdateDate { get; set; }

		public string Message { get; set; }
	}
}
