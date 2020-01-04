using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Implementations
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
		[JsonConverter(typeof(IntStoredAsString))]
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
		[JsonConverter(typeof(BoolStoredAsIntString))]
		public bool IsDefault { get; set; }
	}
}