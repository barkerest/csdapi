using System.Text.Json.Serialization;

namespace CSDAPI.Models.Internal
{
	internal class GetAssetTypesResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public AssetType[] AssetTypes { get; set; }
	}
}