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

    [Fact]
    public void Serialize_Collection_WritesNullLiterals()
    {
        var json = JsonSerializer.Serialize(new[] { Unit.Value, Unit.Value }, Options);

        Assert.Equal("[null,null]", json);
    }

    [Fact]
    public void Deserialize_Collection_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonSerializer.Deserialize<Unit[]>("[null,null]", Options);

        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.All(result, value => Assert.Equal(Unit.Value, value));
    }

    [Fact]
    public void Serialize_ClassCollectionProperty_WritesNullLiterals()
    {
        var json = JsonSerializer.Serialize(
            new CollectionPropertyContainer
            {
                Values = new List<Unit> { Unit.Value, Unit.Value },
            },
            Options
        );

        Assert.Equal("""{"Values":[null,null]}""", json);
    }

    [Fact]
    public void Deserialize_ClassCollectionProperty_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonSerializer.Deserialize<CollectionPropertyContainer>(
            """{"Values":[null,null]}""",
            Options
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Values.Count);
        Assert.All(result.Values, value => Assert.Equal(Unit.Value, value));
    }

    [Fact]
    public void Serialize_Dictionary_WritesNullLiterals()
    {
        var json = JsonSerializer.Serialize(
            new Dictionary<string, Unit>
            {
                ["first"] = Unit.Value,
                ["second"] = Unit.Value,
            },
            Options
        );

        Assert.Equal("""{"first":null,"second":null}""", json);
    }

    [Fact]
    public void Deserialize_Dictionary_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonSerializer.Deserialize<Dictionary<string, Unit>>(
            """{"first":null,"second":null}""",
            Options
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(Unit.Value, result["first"]);
        Assert.Equal(Unit.Value, result["second"]);
    }

    [Fact]
    public void Serialize_ClassDictionaryProperty_WritesNullLiterals()
    {
        var json = JsonSerializer.Serialize(
            new DictionaryPropertyContainer
            {
                Values = new Dictionary<string, Unit>
                {
                    ["first"] = Unit.Value,
                    ["second"] = Unit.Value,
                },
            },
            Options
        );

        Assert.Equal("""{"Values":{"first":null,"second":null}}""", json);
    }

    [Fact]
    public void Deserialize_ClassDictionaryProperty_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonSerializer.Deserialize<DictionaryPropertyContainer>(
            """{"Values":{"first":null,"second":null}}""",
            Options
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Values.Count);
        Assert.Equal(Unit.Value, result.Values["first"]);
        Assert.Equal(Unit.Value, result.Values["second"]);
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

    private sealed class CollectionPropertyContainer
    {
        public List<Unit> Values { get; init; } = new();
    }

    private sealed class DictionaryPropertyContainer
    {
        public Dictionary<string, Unit> Values { get; init; } = new();
    }
}
