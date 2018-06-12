using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces.DataSource
{
	public interface IDataLoader<T>
	{

		Task<IEnumerable<T>> LoadData(ValidFileInfo fileInfo);
	}
}
