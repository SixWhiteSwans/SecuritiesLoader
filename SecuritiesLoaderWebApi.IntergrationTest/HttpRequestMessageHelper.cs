using System.Net.Http;

namespace SecuritiesLoaderWebApi.IntergrationTest
{
	internal static class HttpRequestMessageHelper
	{

		internal static HttpRequestMessage ConstructRequest(
			HttpMethod httpMethod, string uri)
		{

			return new HttpRequestMessage(httpMethod, uri);
		}

		


		
	}
}
