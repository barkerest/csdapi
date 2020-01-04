using System;

namespace CSDAPI.Interfaces
{
	/// <summary>
	/// The interface for API settings.
	/// </summary>
	public interface IApiSettings
	{
		/// <summary>
		/// The API key to use.
		/// </summary>
		string ApiKey { get; }

		/// <summary>
		/// Gets the URI for the specific service.
		/// </summary>
		/// <param name="serviceName"></param>
		/// <returns></returns>
		Uri GetUriFor(string serviceName);

	}
}
