using System;

namespace CSDAPI.Exceptions
{
	/// <summary>
	/// The request against the server did not return a valid JSON response.
	/// </summary>
	public class ApiJsonException : ApiException
	{
		public ApiJsonException() : base("Failed to deserialize API response.")
		{
			
		}

		public ApiJsonException(string message) : base(message)
		{
			
		}

		public ApiJsonException(string message, Exception innerException) : base(message, innerException)
		{
			
		}

		public ApiJsonException(Exception innerException) : base("Failed to deserialize API response.", innerException)
		{
			
		}
	}
}