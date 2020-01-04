using System;
using System.Net;

namespace CSDAPI.Exceptions
{
	/// <summary>
	/// The request against the server resulted in a code other than success.
	/// </summary>
	public class ApiHttpException : ApiException
	{
		public ApiHttpException(int code) 
			: base($"HTTP request resulted in {code} code.")
		{
			
		}

		public ApiHttpException(int code, Exception innerException) 
			: base($"HTTP request resulted in {code} code.", innerException)
		{
			
		}

		public ApiHttpException(HttpStatusCode code) 
			: base($"HTTP request resulted in {(int) code} ({code}) code.")
		{
			
		}
		
		public ApiHttpException(HttpStatusCode code, Exception innerException) 
			: base($"HTTP request resulted in {(int) code} ({code}) code.", innerException)
		{
			
		}
	}
}