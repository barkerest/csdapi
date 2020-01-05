using System.Text.Json.Serialization;

namespace CSDAPI.Converters
{
	public class JsonBoolStoredAsIntStringAttribute : JsonConverterAttribute
	{
		public JsonBoolStoredAsIntStringAttribute() : base(typeof(BoolStoredAsIntString))
		{
			
		}
	}
}