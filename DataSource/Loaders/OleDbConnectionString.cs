using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;

namespace DataSource.Loaders
{
	public class OleDbConnectionString
	{

		public string Sql { get; }
		public string TableName { get; }

		public string Value { get; }

		public ValidFileInfo ValidFileInfo { get; }

		public OleDbConnectionString(ValidFileInfo fileInfo)
		{
			if (!fileInfo.FileType.Equals(FileTypes.Excel)   && !fileInfo.FileType.Equals(FileTypes.Csv))
				throw new ArgumentException("File format not setup for oledb loading");


			ValidFileInfo = fileInfo;
			//THIS CAN BE IMPROVED BY JUST HAVING THE FILE NAME WITHOUT EXTENSION.
			TableName = $"[StocksPrices-{fileInfo.Source}$]";			
			Sql = $"select * from {TableName}";
			



			if (fileInfo.FileType == FileTypes.Excel)
			{
				Value =
					$"provider = Microsoft.Jet.OLEDB.4.0; Data Source = '{fileInfo.Value.FullName}'; Extended Properties = Excel 8.0;";

				return;
			}


			if (fileInfo.FileType == FileTypes.Csv)
			{
				//Value =
				//	$"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{fileInfo.Value.FullName}';Extended Properties=Text;HDR=Yes;FMT=Delimited;";
				TableName = $"[{fileInfo.Value.Name}$]";
				Sql = $"select * from [{fileInfo.Value.Name}]";

				var provider = $"provider = Microsoft.Jet.OLEDB.4.0; Data Source = '{fileInfo.Value.Directory}\\';";

				Value = string.Concat(provider, "Extended Properties = 'text;HDR=Yes;FMT=Delimited(,)'; ");
				
			}

	
		}

	}
}
