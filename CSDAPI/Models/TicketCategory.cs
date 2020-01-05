using System.Text.Json.Serialization;
using CSDAPI.Converters;

namespace CSDAPI.Models
{
	public class TicketCategory
	{
		[JsonPropertyName("topic_id")]
		[JsonIntStoredAsString]
		public int ID { get; set; }
		
		[JsonPropertyName("topic_pid")]
		[JsonIntStoredAsString]
		public int ParentID { get; set; }
		
		[JsonPropertyName("topic")]
		public string Name { get; set; }
		
		[JsonPropertyName("isactive")]
		[JsonBoolStoredAsIntString]
		public bool IsActive { get; set; }
		
		[JsonPropertyName("ispublic")]
		[JsonBoolStoredAsIntString]
		public bool IsPublic { get; set; }
		
		[JsonPropertyName("form_id")]
		[JsonIntStoredAsString]
		public int FormID { get; set; }
		
		[JsonPropertyName("page_id")]
		[JsonIntStoredAsString]
		public int ThankYouPageID { get; set; }
		
		[JsonPropertyName("noautoresp")]
		[JsonBoolStoredAsIntString]
		public bool DisableAutoResponse { get; set; }

		[JsonPropertyName("is_stage_alert_enabled")]
		[JsonBoolStoredAsIntString]
		public bool EnableTicketStageAlerts { get; set; }
		
		[JsonPropertyName("stage_alert_template_id")]
		[JsonIntStoredAsString]
		public int StageTransferEmailTemplateID { get; set; }
		
		[JsonPropertyName("stage_name")]
		public string StageName { get; set; }
		
		[JsonPropertyName("dept_id")]
		[JsonIntStoredAsString]
		public int DepartmentID { get; set; }
		
		[JsonPropertyName("priority_id")]
		[JsonIntStoredAsString]
		public int PriorityID { get; set; }
		
		[JsonPropertyName("sla_id")]
		[JsonIntStoredAsString]
		public int ServiceLevelAgreementID { get; set; }
		
		[JsonPropertyName("staff_id")]
		[JsonIntStoredAsString]
		public int AutoAssignToStaffID { get; set; }
		
		[JsonPropertyName("team_id")]
		[JsonIntStoredAsString]
		public int AutoAssignToTeamID { get; set; }
		
		[JsonPropertyName("notes")]
		public string Notes { get; set; }
	}
	
}