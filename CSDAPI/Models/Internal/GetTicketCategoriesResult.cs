using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CSDAPI.Models.Internal
{
	internal class GetTicketCategoriesResult : ServiceResultBase
	{
		[JsonPropertyName("data")]
		public TicketCategory[] TicketCategories { get; set; }
	}
}