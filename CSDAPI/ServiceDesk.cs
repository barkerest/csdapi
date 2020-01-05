using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
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

		/// <summary>
		/// Gets the users from the server.
		/// </summary>
		/// <param name="keyword">A keyword to search for the user by name or address.</param>
		/// <param name="pageNum">The page to retrieve.</param>
		/// <param name="perPage">The number of records to return per page.</param>
		/// <returns></returns>
		public async Task<IEnumerable<User>> GetUsersAsync(
			string keyword = null, 
			int pageNum = 1,
			int perPage = 50)
		{
			var data = new Dictionary<string, string>()
			{
				{"pageNo", pageNum.ToString()},
				{"pageSize", perPage.ToString()}
			};
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				data["keyword"] = keyword;
			}

			var result = await ExecServiceAsync<GetUsersResult>("getUsers", data);
			return result.Users ?? new User[0];
		}
		
		/// <summary>
		/// Gets the users from the server.
		/// </summary>
		/// <param name="keyword">A keyword to search for the user by name or address.</param>
		/// <param name="pageNum">The page to retrieve.</param>
		/// <param name="perPage">The number of records to return per page.</param>
		/// <returns></returns>
		public IEnumerable<User> GetUsers(string keyword = null, int pageNum = 1, int perPage = 50)
			=> GetUsersAsync(keyword, pageNum, perPage).Result;

		/// <summary>
		/// Finds a user by email.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public async Task<User> FindUserAsync(string email)
		{
			var list = await GetUsersAsync(keyword: email);
			return list.FirstOrDefault();
		}

		/// <summary>
		/// Finds a user by email.
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public User FindUser(string email) => FindUserAsync(email).Result;

		/// <summary>
		/// Creates a user with the specified name and email.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public async Task<int> CreateUserAsync(string name, string email)
		{
			var data = new Dictionary<string, string>()
			{
				{"name", name},
				{"email", email}
			};
			var result = await ExecServiceAsync<CreateUserResult>("createuser", data);
			return result.UserId?.ID ?? 0;
		}

		/// <summary>
		/// Creates a user with the specified name and email.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public int CreateUser(string name, string email) => CreateUserAsync(name, email).Result;

		/// <summary>
		/// Finds or creates a user with the specified email.
		/// </summary>
		/// <param name="email"></param>
		/// <param name="name">Optional name to set if new user is created.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public async Task<User> FindOrCreateUserAsync(string email, string name = null)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be blank.");
			if (!email.Contains("@")) throw new ArgumentException("Email must be in user@domain format.");
			if (string.IsNullOrWhiteSpace(name)) name = email.Split('@')[0];

			var result = await FindUserAsync(email);
			
			if (result is null)
			{
				result = new User()
				{
					ID = await CreateUserAsync(name, email),
					Name = name,
					Address = email,
				};
			}

			if (result.ID == 0) return null;
			
			return result;
		}

		/// <summary>
		/// Finds or creates a user with the specified email.
		/// </summary>
		/// <param name="email"></param>
		/// <param name="name">Optional name to set if new user is created.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public User FindOrCreateUser(string email, string name = null) => FindOrCreateUserAsync(email, name).Result;

		/// <summary>
		/// Updates a user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public async Task<bool> UpdateUserAsync(User user)
		{
			if (user is null) throw new ArgumentNullException(nameof(user));
			if (user.ID == 0) throw new ArgumentException("User ID cannot be 0.");
			var data = new Dictionary<string, string>()
			{
				{"id", user.ID.ToString()},
				{"name", user.Name},
				{"email", user.Address}
			};
			
			try
			{
				var result = await ExecServiceAsync<ServiceResultBase>("updateuser", data);
				return result.Code == 200;
			}
			catch (ApiException e) when (e is ApiHttpException || e is ApiErrorException)
			{
				return false;
			}
		}

		/// <summary>
		/// Updates a user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool UpdateUser(User user) => UpdateUserAsync(user).Result;


		/// <inheritdoc />
		public void Dispose()
		{
			_client?.Dispose();
			_client = null;
		}
	}
}
