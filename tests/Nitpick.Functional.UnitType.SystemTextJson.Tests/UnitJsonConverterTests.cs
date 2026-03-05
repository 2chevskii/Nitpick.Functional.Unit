using System.Text.Json;

namespace Nitpick.Functional.UnitType.SystemTextJson.Tests;

public class UnitJsonConverterTests
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { UnitJsonConverter.Instance },
    };
    private static readonly JsonSerializerOptions FieldOptions = new()
    {
        Converters = { UnitJsonConverter.Instance },
        IncludeFields = true,
    };

    [Fact]
    public void Serialize_WritesNullLiteral()
    {
        var json = JsonSerializer.Serialize(Unit.Value, Options);

        Assert.Equal("null", json);
    }

    [Fact]
    public void Deserialize_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonSerializer.Deserialize<Unit>("null", Options);

        Assert.Equal(Unit.Value, result);
    }

    [Fact]
    public void Serialize_ClassProperty_WritesNullLiteral()
    {
        var json = JsonSerializer.Serialize(new PropertyContainer(), Options);

        Assert.Equal("""{"Value":null}""", json);
    }

    [Fact]
    public void Deserialize_ClassProperty_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonSerializer.Deserialize<PropertyContainer>("""{"Value":null}""", Options);

        Assert.NotNull(result);
        Assert.Equal(Unit.Value, result.Value);
    }

    [Fact]
    public void Serialize_ClassField_WritesNullLiteral()
    {
        var json = JsonSerializer.Serialize(new FieldContainer(), FieldOptions);

        Assert.Equal("""{"Value":null}""", json);
    }

    [Fact]
    public void Deserialize_ClassField_NullLiteral_ReturnsUnitValue()
    {
        var result = JsonSerializer.Deserialize<FieldContainer>("""{"Value":null}""", FieldOptions);

        Assert.NotNull(result);
        Assert.Equal(Unit.Value, result.Value);
    }

    [Theory]
    [InlineData("1", "Number")]
    [InlineData("\"x\"", "String")]
    [InlineData("{}", "StartObject")]
    [InlineData("true", "True")]
    [InlineData("[]", "StartArray")]
    public void Deserialize_NonNullToken_ThrowsJsonException(string json, string tokenName)
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<Unit>(json, Options)
        );

        Assert.Contains($"Unexpected token '{tokenName}'. Expected 'Null'.", ex.Message);
    }

    [Fact]
    public void Deserialize_ClassProperty_NonNullToken_ThrowsJsonException()
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<PropertyContainer>("""{"Value":1}""", Options)
        );

        Assert.Contains("Unexpected token 'Number'. Expected 'Null'.", ex.Message);
    }

    [Fact]
    public void Deserialize_ClassField_NonNullToken_ThrowsJsonException()
    {
        var ex = Assert.Throws<JsonException>(() =>
            JsonSerializer.Deserialize<FieldContainer>("""{"Value":1}""", FieldOptions)
        );

        Assert.Contains("Unexpected token 'Number'. Expected 'Null'.", ex.Message);
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
