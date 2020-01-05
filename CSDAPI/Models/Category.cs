using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	/// <summary>
	/// A category for ticket details.
	/// </summary>
	/// <remarks>
	/// https://developer.itarian.com/frontend/web/topic/get-categories
	///
	/// As of 2020-01-04, the documentation is wrong.
	/// 
	/// The documented format is:
	///   {
	///     "categoryId":"0",
	///     "name":"name"
	///   }
	///
	/// The observed format is:
	///   {
	///     "id":"0",,
	///     "name":"name",
	///     "desc":"description"
	///   }
	/// 
	/// </remarks>
	public class Category
	{
		/// <summary>
		/// The category ID.
		/// </summary>
		[JsonPropertyName("id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		/// <summary>
		/// The category name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		/// <summary>
		/// The category description.
		/// </summary>
		[JsonPropertyName("desc")]
		public string Description { get; set; }
	}
}