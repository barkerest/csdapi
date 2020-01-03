namespace CSDAPI.Interfaces
{
	/// <summary>
	/// The interface for API settings.
	/// </summary>
	public interface IApiSettings
	{
		/// <summary>
		/// The hostname for the API.
		/// </summary>
		/// <remarks>
		/// eg - my-company.servicedesk-us.comodo.com
		/// </remarks>
		string HostName { get; }
		
		/// <summary>
		/// The port for the API.
		/// </summary>
		/// <remarks>
		/// eg - 443
		/// </remarks>
		int Port { get; }
		
		/// <summary>
		/// The base path for the API.
		/// </summary>
		/// <remarks>
		/// eg - /clientapi/index.php
		/// </remarks>
		string BasePath { get; }
		
		/// <summary>
		/// The API key to use.
		/// </summary>
		string ApiKey { get; }
		
	}
}
