using System;
using System.Text.RegularExpressions;
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
			if (!Regex.IsMatch(HostName, @"^[a-z0-9-]+(\.[a-z0-9-]+)+$")) throw new ArgumentException("Must be in server.domain format.", nameof(hostName));
			
			if (Port == 0) Port = 443;
			if (Port < 1 || Port > ushort.MaxValue) throw new ArgumentOutOfRangeException(nameof(port));
		}

		/// <inheritdoc />
		public string HostName { get; }

		/// <inheritdoc />
		public int Port { get; }

		/// <inheritdoc />
		public string BasePath { get; }

		/// <inheritdoc />
		public string ApiKey { get; }
	}
}
