using System;
using System.Text.RegularExpressions;
using System.Web;
using CSDAPI.Interfaces;

namespace CSDAPI.Implementations
{
	/// <summary>
	/// Standard implementation of IApiSettings.
	/// </summary>
	public class ApiSettings : IApiSettings
	{
		/// <summary>
		/// Creates a new API settings.
		/// </summary>
		/// <param name="apiKey">The API key.</param>
		/// <param name="hostName">The host name.</param>
		/// <param name="port">The port.</param>
		/// <param name="basePath">The base path.</param>
		/// <exception cref="ArgumentException"></exception>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public ApiSettings(string apiKey, string hostName, int port = 443, string basePath = "/clientapi/index.php")
		{
			ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
			HostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
			BasePath = basePath ?? throw new ArgumentNullException(nameof(basePath));
			Port = port;

			if (string.IsNullOrWhiteSpace(ApiKey)) throw new ArgumentException("Cannot be blank.", nameof(apiKey));
			
			if (string.IsNullOrWhiteSpace(HostName)) throw new ArgumentException("Cannot be blank.", nameof(hostName));
			
			if (string.IsNullOrWhiteSpace(BasePath)) throw new ArgumentException("Cannot be blank.", nameof(basePath));
			
			if (Port == 0) Port = 443;
			if (Port < 1 || Port > ushort.MaxValue) throw new ArgumentOutOfRangeException(nameof(port));
		}

		/// <summary>
		/// Creates the settings for the standard Comodo service desk portal.
		/// </summary>
		/// <param name="apiKey">The API key.</param>
		/// <param name="companySubDomain">The company sub-domain (eg - "my-company").</param>
		/// <param name="usPortal">Determines if we should be using "servicedesk-us" or "servicedesk" in the hostname.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static ApiSettings Create(string apiKey, string companySubDomain, bool usPortal = true)
		{
			if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentException("Cannot be blank.", nameof(apiKey));
			if (string.IsNullOrWhiteSpace(companySubDomain)) throw new ArgumentException("Cannot be blank.", nameof(companySubDomain));
			var us = usPortal ? "-us" : "";
			return new ApiSettings(apiKey, companySubDomain + ".servicedesk" + us + ".comodo.com");
		}
			
		
		/// <summary>
		/// The hostname for the API.
		/// </summary>
		public string HostName { get; }

		/// <summary>
		/// The port for the API.
		/// </summary>
		public int Port { get; }

		/// <summary>
		/// The base path for the API.
		/// </summary>
		public string BasePath { get; }
		
		/// <inheritdoc />
		public string ApiKey { get; }

		/// <inheritdoc />
		public Uri GetUriFor(string serviceName)
		{
			var builder = new UriBuilder()
			{
				Scheme = "https",
				Host = HostName ?? throw new ArgumentException("HostName cannot be null"),
				Port = Port,
				Path = BasePath ?? throw new ArgumentException("BasePath cannot be null"),
				Query = "serviceName=" + HttpUtility.UrlEncode(serviceName)
			};

			return builder.Uri;
		}
	}
}
