using System.Net.Http;
using JonnyGallo.Abstractions;

namespace JonnyGallo.Common.Droid
{
	public class HttpClientHandlerFactory : IHttpClientHandlerFactory
	{
		public HttpClientHandler GetHttpClientHandler()
		{
			// not needed on Android
			return null;
		}
	}
}

