namespace Nitpick.Functional.Tests;

[Collection("unit_tests")]
public sealed class UnitTypeTests
{
    [Fact]
    public void Value_IsAccessible()
    {
        _ = Unit.Value;
    }

    [Fact]
    public void ToString_ReturnsParentheses()
    {
        Assert.Equal("()", Unit.Value.ToString());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void GetHashCode_AlwaysReturnsZero(bool useDefault)
    {
        var unit = useDefault ? default(Unit) : Unit.Value;
        Assert.Equal(0, unit.GetHashCode());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Equals_Unit_ReturnsTrue(bool useDefaultRhs)
    {
        var rhs = useDefaultRhs ? default(Unit) : Unit.Value;
        Assert.True(Unit.Value.Equals(rhs));
    }

    [Fact]
    public void Equals_BoxedUnit_ReturnsTrue()
    {
        object obj = Unit.Value;
        Assert.True(Unit.Value.Equals(obj));
    }

    [Theory]
    [InlineData("not unit")]
    [InlineData(42)]
    [InlineData(null)]
    public void Equals_NonUnit_ReturnsFalse(object? value)
    {
        Assert.False(Unit.Value.Equals(value));
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void EqualityOperator_ReturnsTrue(bool useDefaultRhs)
    {
        var rhs = useDefaultRhs ? default(Unit) : Unit.Value;
        Assert.True(Unit.Value == rhs);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void InequalityOperator_ReturnsFalse(bool useDefaultRhs)
    {
        var rhs = useDefaultRhs ? default(Unit) : Unit.Value;
        Assert.False(Unit.Value != rhs);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CompareTo_Unit_ReturnsZero(bool useDefaultRhs)
    {
        var rhs = useDefaultRhs ? default(Unit) : Unit.Value;
        Assert.Equal(0, Unit.Value.CompareTo(rhs));
    }

    [Fact]
    public void ImplicitConversion_FromValueTuple()
    {
        Unit unit = default(ValueTuple);
        Assert.Equal(Unit.Value, unit);
    }

    [Fact]
    public void ImplicitConversion_ToValueTuple()
    {
        ValueTuple tuple = Unit.Value;
        Assert.Equal(default, tuple);
    }

    [Fact]
    public void NullableEqualityOperator_BothNonNull_ReturnsTrue()
    {
        Unit? lhs = Unit.Value;
        Unit? rhs = default(Unit);

        Assert.True(lhs == rhs);
        Assert.False(lhs != rhs);
    }

    [Fact]
    public void NullableEqualityOperator_OnlyLeftIsNull_ReturnsFalse()
    {
        Unit? lhs = null;
        Unit? rhs = Unit.Value;

        Assert.False(lhs == rhs);
        Assert.True(lhs != rhs);
    }

    [Fact]
    public void NullableEqualityOperator_OnlyRightIsNull_ReturnsFalse()
    {
        Unit? lhs = Unit.Value;
        Unit? rhs = null;

        Assert.False(lhs == rhs);
        Assert.True(lhs != rhs);
    }

    [Fact]
    public void NullableEqualityOperator_BothNull_ReturnsFalse()
    {
        Unit? lhs = null;
        Unit? rhs = null;

        Assert.False(lhs == rhs);
        Assert.True(lhs != rhs);
    }

    [Fact]
    public void EqualityOperator_WithObjectOnRight_UsesUnitEquality()
    {
        object unitObject = Unit.Value;

        Assert.True(Unit.Value == unitObject);
        Assert.False(Unit.Value != unitObject);
    }

    [Fact]
    public void EqualityOperator_WithNonUnitObjectOnRight_ReturnsFalse()
    {
        object value = "not unit";

        Assert.False(Unit.Value == value);
        Assert.True(Unit.Value != value);
    }

    [Fact]
    public void EqualityOperator_WithObjectOnLeft_UsesUnitEquality()
    {
        object lhs = Unit.Value;
        object rhs = "not unit";
        object? nullObject = null;

        Assert.True(lhs == Unit.Value);
        Assert.False(lhs != Unit.Value);
        Assert.False(rhs == Unit.Value);
        Assert.True(rhs != Unit.Value);
        Assert.False(nullObject == Unit.Value);
        Assert.True(nullObject != Unit.Value);
    }

    [Fact]
    public void IsReadonlyStruct()
    {
        Assert.True(typeof(Unit).IsValueType);
    }

    [Fact]
    public void ImplementsIEquatable()
    {
        Assert.True(typeof(IEquatable<Unit>).IsAssignableFrom(typeof(Unit)));
    }

    [Fact]
    public void ImplementsIComparable()
    {
        Assert.True(typeof(IComparable<Unit>).IsAssignableFrom(typeof(Unit)));
    }
}
