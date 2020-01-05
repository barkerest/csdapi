using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CSDAPI.Models;
using Xunit;

namespace CSDAPI.Tests.Models
{
	public class User_Should
	{
		public static IEnumerable<object[]> JsonConvertData()
			=> new (User, string)[]
			{
				(new User(), @"{""id"":""0"",""name"":null,""address"":null}"),
				(new User(){ID = 1234,Name = "Hello",Address = "user@example.com"}, @"{""id"":""1234"",""name"":""Hello"",""address"":""user@example.com""}" )
			}.Select(x => new object[] {x.Item1, x.Item2});

		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertToJson(User user, string json)
		{
			var result = JsonSerializer.Serialize(user);
			Assert.Equal(json, result);
		}
		
		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertFromJson(User user, string json)
		{
			var result = JsonSerializer.Deserialize<User>(json);
			Assert.Equal(user.ID, result.ID);
			Assert.Equal(user.Name, result.Name);
			Assert.Equal(user.Address, result.Address);
		}
	}
}