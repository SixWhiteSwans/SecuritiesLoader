using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Core.Utils
{
	public static class SerializeItem
	{
		public static MemoryStream Serialize<T>(this T t)
		{
			var memoryStream = new MemoryStream();
			var binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, t);

			return memoryStream;

		}

		public static T Deserialize<T>(this MemoryStream memoryStream)
		{

			var binaryFormatter = new BinaryFormatter();

			memoryStream.Position = 0;
			T returnValue = (T)binaryFormatter.Deserialize(memoryStream);

			memoryStream.Close();
			memoryStream.Dispose();

			return returnValue;

		}

	}
}
