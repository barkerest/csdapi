using System.Text.Json.Serialization;

namespace CSDAPI.Models.Internal
{
	internal class GetCategoriesResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public Category[] Categories { get; set; }
	}
}