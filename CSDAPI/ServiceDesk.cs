using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CSDAPI.Exceptions;
using CSDAPI.Interfaces;
using CSDAPI.Models;
using CSDAPI.Models.Internal;

namespace CSDAPI
{
	/// <summary>
	/// The service desk API interface.
	/// </summary>
	public sealed class ServiceDesk : IDisposable
	{
		private readonly IApiSettings _settings;
		private HttpClient _client;

		/// <summary>
		/// Creates a new service desk API interface using the provided settings.
		/// </summary>
		/// <param name="settings"></param>
		public ServiceDesk(IApiSettings settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_client = new HttpClient();
		}

		private async Task<T> ExecServiceAsync<T>(string service, object data) where T : class, IServiceResultBase
		{
			var uri = _settings.GetUriFor(service);
			var body = data is object ? JsonSerializer.Serialize(data) : "{}";

			var request = (HttpWebRequest) WebRequest.Create(uri);
			request.ContentType = "application/json";
			request.Method = "POST";
			
			await using (var writer = new StreamWriter(await request.GetRequestStreamAsync()))
			{
				writer.Write(body);
			}
			
			request.Headers.Add("Authorization", _settings.ApiKey);

			var response = (HttpWebResponse) await request.GetResponseAsync();

			await using (var stream = response.GetResponseStream())
			{
				if (stream is object)
				{
					using (var reader = new StreamReader(stream))
					{
						body = await reader.ReadToEndAsync();
					}
				}
			}
			
			T ret;
			
			try
			{
				ret = JsonSerializer.Deserialize<T>(body);
			}
			catch (JsonException)
			{
				ret = null;
			}

			if (response.StatusCode != HttpStatusCode.OK) 
			{
				if (ret is null)
				{
					throw new ApiHttpException(response.StatusCode);
				}
				throw new ApiErrorException(ret);
			}
			
			if (ret is null) throw new ApiJsonException();

			if (ret.Code != 200)
			{
				throw new ApiErrorException(ret);
			}

			return ret;
		}

		/// <summary>
		/// Gets the asset types from the server.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<AssetType>> GetAssetTypesAsync()
		{
			var result = await ExecServiceAsync<GetAssetTypesResult>("getassets", null);
			return result.AssetTypes ?? new AssetType[0];
		}
		
		/// <summary>
		/// Gets the asset types from the server.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<AssetType> GetAssetTypes() => GetAssetTypesAsync().Result;

		/// <summary>
		/// Gets the categories from the server.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Category>> GetCategoriesAsync()
		{
			var result = await ExecServiceAsync<GetCategoriesResult>("getcategories", null);
			return result.Categories ?? new Category[0];
		}

		/// <summary>
		/// Gets the categories from the server.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Category> GetCategories() => GetCategoriesAsync().Result;

		/// <summary>
		/// Gets the ticket categories from the server.
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<TicketCategory>> GetTicketCategoriesAsync()
		{
			var result = await ExecServiceAsync<GetTicketCategoriesResult>("getticketcategories", null);
			return result.TicketCategories ?? new TicketCategory[0];
		}
		
		/// <summary>
		/// Gets the ticket categories from the server.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TicketCategory> GetTicketCategories() => GetTicketCategoriesAsync().Result;

		
		
		/// <inheritdoc />
		public void Dispose()
		{
			_client?.Dispose();
			_client = null;
		}
	}
}
