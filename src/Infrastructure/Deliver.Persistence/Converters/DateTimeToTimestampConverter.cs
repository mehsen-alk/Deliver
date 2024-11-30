using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persistence.Converters;

public class DateTimeToTimestampConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var timestamp = reader.GetInt64();
        return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options
    )
    {
        var timestamp = new DateTimeOffset(value).ToUnixTimeSeconds();
        writer.WriteNumberValue(timestamp);
    }
}