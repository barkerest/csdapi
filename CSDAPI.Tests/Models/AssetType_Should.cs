using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CSDAPI.Models;
using CSDAPI.Tests.Config;
using Xunit;

namespace CSDAPI.Tests.Models
{
	public class AssetType_Should : IClassFixture<TestSettings>
	{
		
		public static IEnumerable<object[]> JsonConvertData()
			=> new (AssetType, string)[]
			{
				(new AssetType(), @"{""id"":""0"",""name"":null,""desc"":null,""isDefault"":""0""}"),
				(new AssetType(){ID = 1234, Name = "TestVal1", Description = "The first test value.", IsDefault = true}, @"{""id"":""1234"",""name"":""TestVal1"",""desc"":""The first test value."",""isDefault"":""1""}" ),
				(new AssetType(){ID = 4321, Name = "TestVal2", Description = "The second test value.", IsDefault = false}, @"{""id"":""4321"",""name"":""TestVal2"",""desc"":""The second test value."",""isDefault"":""0""}" ),
			}.Select(x => new object[] {x.Item1, x.Item2});
		
		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertToJson(AssetType assetType, string json)
		{
			var result = JsonSerializer.Serialize(assetType);
			Assert.Equal(json, result);
		}

		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertFromJson(AssetType assetValue, string json)
		{
			var result = JsonSerializer.Deserialize<AssetType>(json);
			Assert.NotNull(result);
			Assert.Equal(assetValue.ID, result.ID);
			Assert.Equal(assetValue.Name, result.Name);
			Assert.Equal(assetValue.Description, result.Description);
			Assert.Equal(assetValue.IsDefault, result.IsDefault);
		}
	}
}
