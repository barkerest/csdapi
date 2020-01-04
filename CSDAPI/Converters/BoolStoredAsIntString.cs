using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSDAPI.Converters
{
	public class BoolStoredAsIntString : JsonConverter<bool>
	{
		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var tmp = JsonSerializer.Deserialize<string>(ref reader, options);
			int.TryParse(tmp, out var val);
			return val != 0;
		}

		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer, value ? "1" : "0", options);
		}
	}
}