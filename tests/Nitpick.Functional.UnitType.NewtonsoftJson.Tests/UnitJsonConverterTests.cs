using Newtonsoft.Json;

namespace Nitpick.Functional.UnitType.NewtonsoftJson.Tests;

public class UnitJsonConverterTests
{
    private static readonly JsonSerializerSettings Settings = new()
    {
        Converters = { new UnitJsonConverter() },
    };

    [Fact]
    public void Serialize_WritesNullLiteral()
    {
        var json = JsonConvert.SerializeObject(Unit.Value, Settings);

        Assert.Equal("null", json);
    }

    [Fact]
    public void Deserialize_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonConvert.DeserializeObject<Unit>("null", Settings);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public void Serialize_ClassProperty_WritesNullLiteral()
    {
        var json = JsonConvert.SerializeObject(new PropertyContainer(), Settings);

        Assert.Equal(@"{""Value"":null}", json);
    }

    [Fact]
    public void Deserialize_ClassProperty_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonConvert.DeserializeObject<PropertyContainer>(
            @"{""Value"":null}",
            Settings
        );

        Assert.NotNull(result);
        Assert.Equal(Unit.Value, result.Value);
    }

    [Fact]
    public void Serialize_ClassField_WritesNullLiteral()
    {
        var json = JsonConvert.SerializeObject(new FieldContainer(), Settings);

        Assert.Equal(@"{""Value"":null}", json);
    }

    [Fact]
    public void Deserialize_ClassField_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonConvert.DeserializeObject<FieldContainer>(@"{""Value"":null}", Settings);

        Assert.NotNull(result);
        Assert.Equal(Unit.Value, result.Value);
    }

    [Theory]
    [InlineData("1", "Integer")]
    [InlineData("\"x\"", "String")]
    [InlineData("{}", "StartObject")]
    [InlineData("true", "Boolean")]
    [InlineData("[]", "StartArray")]
    public void Deserialize_NonNullToken_ThrowsJsonException(string json, string tokenName)
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonConvert.DeserializeObject<Unit>(json, Settings)
        );

        Assert.Contains($"Unexpected token '{tokenName}'. Expected 'Null'.", ex.Message);
    }

    [Fact]
    public void Deserialize_ClassProperty_NonNullToken_ThrowsJsonException()
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonConvert.DeserializeObject<PropertyContainer>(@"{""Value"":1}", Settings)
        );

        Assert.Contains("Unexpected token 'Integer'. Expected 'Null'.", ex.Message);
    }

    [Fact]
    public void Deserialize_ClassField_NonNullToken_ThrowsJsonException()
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonConvert.DeserializeObject<FieldContainer>(@"{""Value"":1}", Settings)
        );

        Assert.Contains("Unexpected token 'Integer'. Expected 'Null'.", ex.Message);
    }

    private sealed class PropertyContainer
    {
        public Unit Value { get; init; } = Unit.Value;
    }

    private sealed class FieldContainer
    {
        public Unit Value = Unit.Value;
    }
}
