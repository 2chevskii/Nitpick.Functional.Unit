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

    [Fact]
    public void Serialize_Collection_WritesNullLiterals()
    {
        var json = JsonConvert.SerializeObject(new[] { Unit.Value, Unit.Value }, Settings);

        Assert.Equal("[null,null]", json);
    }

    [Fact]
    public void Deserialize_Collection_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonConvert.DeserializeObject<Unit[]>("[null,null]", Settings);

        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.All(result, value => Assert.Equal(Unit.Value, value));
    }

    [Fact]
    public void Serialize_ClassCollectionProperty_WritesNullLiterals()
    {
        var json = JsonConvert.SerializeObject(
            new CollectionPropertyContainer
            {
                Values = new List<Unit> { Unit.Value, Unit.Value },
            },
            Settings
        );

        Assert.Equal(@"{""Values"":[null,null]}", json);
    }

    [Fact]
    public void Deserialize_ClassCollectionProperty_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonConvert.DeserializeObject<CollectionPropertyContainer>(
            @"{""Values"":[null,null]}",
            Settings
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Values.Count);
        Assert.All(result.Values, value => Assert.Equal(Unit.Value, value));
    }

    [Fact]
    public void Serialize_Dictionary_WritesNullLiterals()
    {
        var json = JsonConvert.SerializeObject(
            new Dictionary<string, Unit> { ["first"] = Unit.Value, ["second"] = Unit.Value },
            Settings
        );

        Assert.Equal(@"{""first"":null,""second"":null}", json);
    }

    [Fact]
    public void Deserialize_Dictionary_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonConvert.DeserializeObject<Dictionary<string, Unit>>(
            @"{""first"":null,""second"":null}",
            Settings
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(Unit.Value, result["first"]);
        Assert.Equal(Unit.Value, result["second"]);
    }

    [Fact]
    public void Serialize_ClassDictionaryProperty_WritesNullLiterals()
    {
        var json = JsonConvert.SerializeObject(
            new DictionaryPropertyContainer
            {
                Values = new Dictionary<string, Unit>
                {
                    ["first"] = Unit.Value,
                    ["second"] = Unit.Value,
                },
            },
            Settings
        );

        Assert.Equal(@"{""Values"":{""first"":null,""second"":null}}", json);
    }

    [Fact]
    public void Deserialize_ClassDictionaryProperty_NullLiterals_ReturnsUnitValues()
    {
        var result = JsonConvert.DeserializeObject<DictionaryPropertyContainer>(
            @"{""Values"":{""first"":null,""second"":null}}",
            Settings
        );

        Assert.NotNull(result);
        Assert.Equal(2, result.Values.Count);
        Assert.Equal(Unit.Value, result.Values["first"]);
        Assert.Equal(Unit.Value, result.Values["second"]);
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

    private sealed class CollectionPropertyContainer
    {
        public List<Unit> Values { get; init; } = new();
    }

    private sealed class DictionaryPropertyContainer
    {
        public Dictionary<string, Unit> Values { get; init; } = new();
    }
}
