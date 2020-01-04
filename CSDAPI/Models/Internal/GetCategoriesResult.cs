using System.Text.Json.Serialization;

namespace CSDAPI.Implementations.Internal
{
	internal class GetCategoriesResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public Category[] Categories { get; set; }
	}
}