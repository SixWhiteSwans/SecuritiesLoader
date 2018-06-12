using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Securities
{
	public interface IAudit
	{

		string Source { get; set; }
		
		DateTime? UpdateDate { get; set; }
		string LastUpdated { get; set; }
		
	}
}
