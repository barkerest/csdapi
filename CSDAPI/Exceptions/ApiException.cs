using System;

namespace CSDAPI.Exceptions
{
	/// <summary>
	/// The base class for API exceptions.
	/// </summary>
	public class ApiException : ApplicationException
	{
		public ApiException()
		{
			
		}

		public ApiException(string message) : base(message)
		{
			
		}

		public ApiException(string message, Exception innerException) : base(message, innerException)
		{
			
		}
	}
}