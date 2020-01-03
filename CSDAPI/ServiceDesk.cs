using System;
using System.Net;
using System.Net.Http;
using CSDAPI.Interfaces;

namespace CSDAPI
{
	public sealed class ServiceDesk : IDisposable
	{
		private readonly IApiSettings _settings;
		private System.Net.Http.HttpClient _client;

		public ServiceDesk(IApiSettings settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_client = new HttpClient();
		}

		
		
		

		public void Dispose()
		{
			_client?.Dispose();
			_client = null;
		}
	}
}
