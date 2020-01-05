using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CSDAPI.Models;
using Xunit;

namespace CSDAPI.Tests.Models
{
	public class Category_Should
	{
		public static IEnumerable<object[]> JsonConvertData()
			=> new (Category, string)[]
			{
				(new Category(), @"{""id"":""0"",""name"":null,""desc"":null}"),
				(new Category(){ID = 1234,Name = "Hello",Description = "World"}, @"{""id"":""1234"",""name"":""Hello"",""desc"":""World""}" )
			}.Select(x => new object[] {x.Item1, x.Item2});

		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertToJson(Category category, string json)
		{
			var result = JsonSerializer.Serialize(category);
			Assert.Equal(json, result);
		}
		
		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertFromJson(Category category, string json)
		{
			var result = JsonSerializer.Deserialize<Category>(json);
			Assert.Equal(category.ID, result.ID);
			Assert.Equal(category.Name, result.Name);
			Assert.Equal(category.Description, result.Description);
		}
	}
}