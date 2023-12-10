using System.Text.Json;
using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Converters;

public class AiringStatusEnumConverter : JsonConverter<AiringStatus?>
{
    public override AiringStatus? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var field = reader.GetString();

        if (field == null) return null;

        return FastEnum.GetValues<AiringStatus>()
            .FirstOrDefault(status => status.GetEnumMemberValue() == field);
    }

    public override void Write(Utf8JsonWriter writer, AiringStatus? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
