using System.Text.Json.Serialization;

namespace CSDAPI.Converters
{
	public class JsonIntStoredAsStringAttribute : JsonConverterAttribute
	{
		public JsonIntStoredAsStringAttribute() : base(typeof(IntStoredAsString))
		{
			
		}
	}
}