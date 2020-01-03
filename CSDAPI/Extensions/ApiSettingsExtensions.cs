using System;
using System.Web;
using CSDAPI.Interfaces;

namespace CSDAPI.Extensions
{
	public static class ApiSettingsExtensions
	{
		/// <summary>
		/// Creates a URI for the specified service.
		/// </summary>
		/// <param name="settings">The API settings.</param>
		/// <param name="serviceName">The service name.</param>
		/// <returns>Returns the URI.</returns>
		public static Uri CreateUri(this IApiSettings settings, string serviceName)
		{
			if (settings is null) return null;
			
			var builder = new UriBuilder()
			{
				Scheme = "https",
				Host = settings.HostName ?? throw new ArgumentException("HostName cannot be null"),
				Port = settings.Port,
				Path = settings.BasePath ?? throw new ArgumentException("BasePath cannot be null"),
				Query = "serviceName=" + HttpUtility.UrlEncode(serviceName)
			};
			
			return builder.Uri;
		}

	}
}
