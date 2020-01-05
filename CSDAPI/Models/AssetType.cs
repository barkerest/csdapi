using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	/// <summary>
	/// An asset type.
	/// </summary>
	/// <remarks>
	/// https://developer.itarian.com/frontend/web/topic/get-asset-types
	/// 
	/// As of 2020-01-04, the documentation is wrong.
	/// 
	/// The documented format is:
	///   {
	///     "assetId":"0",
	///     "asset":"name",
	///     "isDefault":"1"
	///   }
	///
	/// The observed format is:
	///   {
	///     "id":"0",
	///     "name":"name",
	///     "desc":"description",
	///     "isDefault":"1",
	///     "deleted_at":null
	///   }
	/// 
	/// </remarks>
	public class AssetType
	{
		/// <summary>
		/// The asset type ID.
		/// </summary>
		[JsonPropertyName("id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		/// <summary>
		/// The asset name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		/// <summary>
		/// The asset description.
		/// </summary>
		[JsonPropertyName("desc")]
		public string Description { get; set; }
		
		/// <summary>
		/// Is this a default (built-in) asset type?
		/// </summary>
		[JsonPropertyName("isDefault")]
		[JsonBoolStoredAsIntString]
		public bool IsDefault { get; set; }
	}
}