using System.Text.Json;
using System.Text.Json.Serialization;
using Aosta.Jikan.Enums;
using FastEnumUtility;

namespace Aosta.Jikan.Converters;

public class AiringStatusEnumConverter : JsonConverter<AiringStatus?>
{
    /*
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            foreach (var status in FastEnum.GetValues<AiringStatus>())
            {
                if (stringValue.Equals(status.GetEnumMemberValue())) return status;
            }
        }
        return base.ConvertFrom(context, culture, value);
    }
*/

    public override AiringStatus? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var field = reader.GetString();

        if (field != null)
        {
            foreach (var status in FastEnum.GetValues<AiringStatus>())
            {
                if (field.Equals(status.GetEnumMemberValue())) return status;
            }
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, AiringStatus? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}