using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces.DataSource
{
	public interface IDataLoaderMapper<T>
	{

		Task<IEnumerable<T>> LoadData(ValidFileInfo fileInfo, IEnumerable<DataFieldMapping> fieldMappings);
	}
}
