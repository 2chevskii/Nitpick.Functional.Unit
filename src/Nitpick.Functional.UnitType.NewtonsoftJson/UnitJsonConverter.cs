using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Nitpick.Functional
{
    [PublicAPI]
    public sealed class UnitJsonConverter : JsonConverter<Unit>
    {
        public override void WriteJson(JsonWriter writer, Unit value, JsonSerializer serializer)
        {
            writer.WriteNull();
        }

        public override Unit ReadJson(
            JsonReader reader,
            Type objectType,
            Unit existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            if (reader.TokenType != JsonToken.Null)
            {
                throw new JsonException(
                    $"Unexpected token '{reader.TokenType:G}'. Expected '{JsonToken.Null:G}'."
                );
            }

            return Unit.Value;
        }
    }
}
