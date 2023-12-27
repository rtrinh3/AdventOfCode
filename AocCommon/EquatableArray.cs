using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public readonly struct EquatableArray<T>: IEquatable<EquatableArray<T>>
    {
        public readonly ImmutableArray<T> Data;

        public EquatableArray(ImmutableArray<T> data)
        {
            Data = data;
        }
        public EquatableArray(IEnumerable<T> data)
        {
            Data = data.ToImmutableArray();
        }
        public EquatableArray(params T[] data)
        {
            Data = data.ToImmutableArray();
        }

        public bool Equals(EquatableArray<T> other)
        {
            return Data.SequenceEqual(other.Data);
        }
        public override int GetHashCode()
        {
            return Data.Aggregate(0, (acc, val) => (acc << 1) + (val?.GetHashCode() ?? 0));
        }

        // Generated IEquatable implementation via Quick Actions and Refactorings
        public override bool Equals(object? obj)
        {
            return obj is EquatableArray<T> other && Equals(other);
        }
        public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right)
        {
            return !(left == right);
        }
    }
}
