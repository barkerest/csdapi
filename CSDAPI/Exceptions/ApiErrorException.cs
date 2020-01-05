using CSDAPI.Models.Internal;

namespace CSDAPI.Exceptions
{
	/// <summary>
	/// The request against the server returned an error in the JSON response.
	/// </summary>
	public class ApiErrorException : ApiException
	{
		internal ApiErrorException(IServiceResultBase result) : base($"API responded with {result.Code} code, {result.Message}.")
		{
			
		}
	}
}