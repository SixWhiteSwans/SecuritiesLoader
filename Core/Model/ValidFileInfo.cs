using System;
using System.IO;

namespace Core.Model
{
	public class ValidFileInfo
	{

		public IProgress<ProgressManager> ProgressLogger { get; set; }


		public FileInfo Value { get; }
		public string Source { get; }
		public FileTypes FileType { get; }
		public ValidFileInfo(string path,string source, FileTypes fileType)
		{			
			

			var fileInfo = new FileInfo(path);
			if (!fileInfo.Exists)
				throw new ArgumentException("The file path is not valid.");

			if(String.IsNullOrEmpty(source))
				throw new ArgumentException("Data source is required to be populated");


			FileType = fileType;
			Source = source;

			//todo: extend to ensure the file is not locked as well.
			
			Value = fileInfo;

		}

		


	}
}
