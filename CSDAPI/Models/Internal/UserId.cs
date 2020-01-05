using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models.Internal
{
	internal class UserId
	{
		[JsonPropertyName("userId")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
	}
}