using System.Text.Json.Serialization;

namespace CSDAPI.Implementations.Internal
{
	internal class ServiceResultBase : IServiceResultBase
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }
		
		[JsonPropertyName("code")]
		public int Code { get; set; }
		
		[JsonPropertyName("status")]
		public string Status { get; set; }

	}
}