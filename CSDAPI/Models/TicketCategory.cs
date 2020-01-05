using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	/// <summary>
	/// A ticket category.
	/// </summary>
	/// <remarks>
	/// https://developer.itarian.com/frontend/web/topic/get-ticket-categories
	///
	/// As of 2020-01-04, the documentation is wrong.
	///
	/// The documented service name is "gethelptopics", but the observed service name is "getticketcategories".
	///
	/// The documented format is:
	///   {
	///     "topicId":"0",
	///     "topic":"name",
	///     "published":"1",
	///     "active":"1"
	///   }
	///
	/// The observed format is:
	///   {
	///     "topic_id":"0",
	///     "topic_pid":"0",
	///     "topic":"name",
	///     "isactive":"1",
	///     "ispublic":"1",
	///     "form_id":"0",
	///     "page_id":"0",
	///     "noautoresp":"1",
	///     "is_stage_alert_enabled":"1",
	///     "stage_alert_template_id":"0",
	///     "stage_name":"stage name",
	///     "dept_id":"0",
	///     "priority_id":"0",
	///     "sla_id":"0",
	///     "staff_id":"0",
	///     "team_id":"0",
	///     "notes":"notes",
	///     "sort":"0",
	///		"order":"0",
	///     "created":"2010-01-01 00:00:00",
	///     "updated":"2010-01-01 00:00:00"
	///   }
	///
	/// </remarks>
	public class TicketCategory
	{
		/// <summary>
		/// The ID for the ticket category.
		/// </summary>
		[JsonPropertyName("topic_id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		/// <summary>
		/// The parent ticket category ID (or 0).
		/// </summary>
		[JsonPropertyName("topic_pid")]
		[JsonIntStoredAsString]
		public int ParentID { get; set; }
		
		/// <summary>
		/// The name of this category.
		/// </summary>
		[JsonPropertyName("topic")]
		public string Name { get; set; }
		
		/// <summary>
		/// Is the category active?
		/// </summary>
		[JsonPropertyName("isactive")]
		[JsonBoolStoredAsIntString]
		public bool IsActive { get; set; }
		
		/// <summary>
		/// Is the category public or private/internal?
		/// </summary>
		[JsonPropertyName("ispublic")]
		[JsonBoolStoredAsIntString]
		public bool IsPublic { get; set; }
	}
	
}