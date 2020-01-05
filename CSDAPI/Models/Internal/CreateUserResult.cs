using System.Text.Json.Serialization;

namespace CSDAPI.Models.Internal
{
	internal class CreateUserResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public UserId UserId { get; set; }
	}
}