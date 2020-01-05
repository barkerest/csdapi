using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	public class Category
	{
		[JsonPropertyName("id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		[JsonPropertyName("desc")]
		public string Description { get; set; }
	}
}