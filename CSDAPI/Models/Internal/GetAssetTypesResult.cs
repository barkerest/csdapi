using System.Text.Json.Serialization;

namespace CSDAPI.Implementations.Internal
{
	internal class GetAssetTypesResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public AssetType[] AssetTypes { get; set; }
	}
}