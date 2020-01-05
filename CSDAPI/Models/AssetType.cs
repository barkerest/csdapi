using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	/// <summary>
	/// An asset type.
	/// </summary>
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