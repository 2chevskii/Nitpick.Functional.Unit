using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Nitpick.Functional
{
    [PublicAPI]
    public sealed class UnitJsonConverter : JsonConverter<Unit>
    {
        public static readonly UnitJsonConverter Instance = new UnitJsonConverter();

        public override Unit Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            if (reader.TokenType != JsonTokenType.Null)
            {
                throw new JsonException(
                    $"Unexpected token '{reader.TokenType:G}'. Expected '{JsonTokenType.Null:G}'."
                );
            }

            return Unit.Value;
        }

        public override void Write(
            Utf8JsonWriter writer,
            Unit value,
            JsonSerializerOptions options
        ) => writer.WriteNullValue();
    }
}
