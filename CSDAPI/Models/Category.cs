using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Implementations
{
	public class Category
	{
		[JsonPropertyName("id")]
		[JsonConverter(typeof(IntStoredAsString))]
		public int ID { get; set; }
		
		[JsonPropertyName("name")]
		public string Name { get; set; }
		
		[JsonPropertyName("desc")]
		public string Description { get; set; }
	}
}