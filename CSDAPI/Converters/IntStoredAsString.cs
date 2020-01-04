using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSDAPI.Converters
{
	public class IntStoredAsString : JsonConverter<int>
	{
		public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var tmp = JsonSerializer.Deserialize<string>(ref reader, options);
			int.TryParse(tmp, out var val);
			return val;
		}

		public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
		{
			JsonSerializer.Serialize(writer, value.ToString(), options);
		}
	}
}