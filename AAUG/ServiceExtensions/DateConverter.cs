using System.Text.Json;
using System.Text.Json.Serialization;

namespace AAUG.Api.ServiceExtensions
{
    public class DateConverter : JsonConverter<DateOnly?>
    {
        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.TryParse(reader.GetString(), out var date) ? date : (DateOnly?)null;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd"));
        }
    }
}
