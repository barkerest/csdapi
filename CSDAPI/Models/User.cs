using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	/// <summary>
	/// An end user.
	/// </summary>
	/// <remarks>
	/// https://developer.itarian.com/frontend/web/topic/get-users
	///
	/// The documentation matches for this service, the service name is case-sensitive "getUsers".
	///   {
	///     "id":"0",
	///     "name":"name",
	///     "address":"user@domain",
	///     "created":"2010-01-01 00:00:00"
	///   }
	/// 
	/// </remarks>
	public class User
	{
		[JsonPropertyName("id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		[JsonPropertyName("address")]
		public string Address { get; set; }
	}
	
}