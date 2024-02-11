// Copyright (c) Davide Pierotti <d.pierotti@live.it>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Frozen;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

using FastEnumUtility;

namespace Aosta.Jikan;

internal class JsonNullableEnumMemberConverter<TEnum> : JsonConverter<TEnum?> where TEnum : struct, Enum
{
    private readonly FrozenDictionary<string, TEnum> _stringToEnum =
        FastEnum.GetValues<TEnum>().ToFrozenDictionary(e => e.GetEnumMemberValue()!);

    /// <inheritdoc />
    public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var type = reader.TokenType;

        if (type == JsonTokenType.String)
        {
            string stringValue = reader.GetString()!;
            if (_stringToEnum.TryGetValue(stringValue, out var parsedEnum))
            {
                return parsedEnum;
            }

            throw new SerializationException(
                $"The value \"{stringValue}\" could not be deserialized to a member of {typeof(TEnum)}");
        }

        if (type == JsonTokenType.Null) return null;

        throw new SerializationException(
            $"Tried to deserialize a {type} JSON field to {typeof(TEnum)} using {nameof(JsonEnumMemberConverter<TEnum>)}");
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            writer.WriteStringValue(value.Value.GetEnumMemberValue());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
