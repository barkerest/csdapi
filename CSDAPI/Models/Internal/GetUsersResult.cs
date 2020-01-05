using System.Text.Json.Serialization;

namespace CSDAPI.Models.Internal
{
	internal class GetUsersResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public User[] Users { get; set; }
	}
}