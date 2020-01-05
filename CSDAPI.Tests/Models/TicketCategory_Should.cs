using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CSDAPI.Models;
using Xunit;

namespace CSDAPI.Tests.Models
{
	public class TicketCategory_Should
	{
		public static IEnumerable<object[]> JsonConvertData()
			=> new (TicketCategory, string)[]
			{
				(
					new TicketCategory(), 
					@"{""topic_id"":""0"",""topic_pid"":""0"",""topic"":null,""isactive"":""0"",""ispublic"":""0""}"
				),
				(
					new TicketCategory() { ID = 1234, ParentID = 4321, Name = "Hello", IsActive = true, IsPublic = true }, 
					@"{""topic_id"":""1234"",""topic_pid"":""4321"",""topic"":""Hello"",""isactive"":""1"",""ispublic"":""1""}"
				),
				
			}.Select(x => new object[] {x.Item1, x.Item2});

		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertToJson(TicketCategory category, string json)
		{
			var result = JsonSerializer.Serialize(category);
			Assert.Equal(json, result);
		}
		
		[Theory]
		[MemberData(nameof(JsonConvertData))]
		public void ConvertFromJson(TicketCategory category, string json)
		{
			var result = JsonSerializer.Deserialize<TicketCategory>(json);
			Assert.Equal(category.ID, result.ID);
			Assert.Equal(category.ParentID, result.ParentID);
			Assert.Equal(category.Name, result.Name);
			Assert.Equal(category.IsActive, result.IsActive);
			Assert.Equal(category.IsPublic, result.IsPublic);
		}
	}
}