using System;
using JetBrains.Annotations;

namespace Nitpick.Functional
{
    [PublicAPI]
    public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>
    {
        public static readonly Unit Value;

        public static bool operator ==(Unit lhs, Unit rhs) => lhs.Equals(rhs);

        public static bool operator !=(Unit lhs, Unit rhs) => !lhs.Equals(rhs);

        public static bool operator ==(Unit? lhs, Unit? rhs) => lhs?.Equals(rhs) ?? false;

        public static bool operator !=(Unit? lhs, Unit? rhs) => !(lhs?.Equals(rhs) ?? false);

        public static bool operator ==(Unit lhs, Unit? rhs) => rhs.HasValue;

        public static bool operator !=(Unit lhs, Unit? rhs) => !rhs.HasValue;

        public static bool operator ==(Unit? lhs, Unit rhs) => lhs.HasValue;

        public static bool operator !=(Unit? lhs, Unit rhs) => !lhs.HasValue;

        public static bool operator ==(Unit lhs, object? rhs) => lhs.Equals(rhs);

        public static bool operator !=(Unit lhs, object? rhs) => !lhs.Equals(rhs);

        public static bool operator ==(object? lhs, Unit rhs) => rhs.Equals(lhs);

        public static bool operator !=(object? lhs, Unit rhs) => !rhs.Equals(lhs);

        public static implicit operator Unit(ValueTuple _) => default;

        public static implicit operator ValueTuple(Unit _) => default;

        public override bool Equals(object? obj) => obj is Unit;

        public override int GetHashCode() => 0;

        public override string ToString() => "()";

        public bool Equals(Unit other) => true;

        public int CompareTo(Unit other) => 0;
    }
}
