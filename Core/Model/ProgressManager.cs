using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
	public class ProgressManager{
	public int Progress { get; set; }
	public Guid Guid { get; set; }
	public DateTime TimeStamp { get; set; }
	public string Message { get; set; }

		public string Action { get; set; }
	}
}
