using System.Collections;
using System.Collections.Immutable;

namespace AocCommon
{
    public readonly struct EquatableSet<T> : IEquatable<EquatableSet<T>>, IImmutableSet<T>
    {
        private readonly ImmutableHashSet<T> data;

        public EquatableSet(ImmutableHashSet<T> data)
        {
            this.data = data;
        }
        public EquatableSet(IEnumerable<T> data)
        {
            this.data = data.ToImmutableHashSet();
        }

        public int Count => data.Count;

        public EquatableSet<T> Add(T value) => new EquatableSet<T>(data.Add(value));
        IImmutableSet<T> IImmutableSet<T>.Add(T value) => Add(value);

        public EquatableSet<T> Clear() => new EquatableSet<T>(data.Clear());
        IImmutableSet<T> IImmutableSet<T>.Clear() => Clear();

        public bool Contains(T value) => data.Contains(value);

        public override bool Equals(object? obj) => obj is EquatableSet<T> otherSet && ((IEquatable<EquatableSet<T>>)this).Equals(otherSet);

        public bool Equals(EquatableSet<T> other) => data.Equals(other.data) || data.SetEquals(other.data);

        public EquatableSet<T> Except(IEnumerable<T> other) => new EquatableSet<T>(data.Except(other));
        IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other) => Except(other);

        public ImmutableHashSet<T>.Enumerator GetEnumerator() => data.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)data).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)data).GetEnumerator();

        public override int GetHashCode() => data.Aggregate(0, (seed, x) => seed ^ x.GetHashCode());

        public EquatableSet<T> Intersect(IEnumerable<T> other) => new EquatableSet<T>(data.Intersect(other));
        IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other) => Intersect(other);

        public bool IsProperSubsetOf(IEnumerable<T> other) => data.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => data.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => data.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => data.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => data.Overlaps(other);

        public EquatableSet<T> Remove(T value) => new EquatableSet<T>(data.Remove(value));
        IImmutableSet<T> IImmutableSet<T>.Remove(T value) => Remove(value);

        public bool SetEquals(IEnumerable<T> other) => data.SetEquals(other);

        public EquatableSet<T> SymmetricExcept(IEnumerable<T> other) => new EquatableSet<T>(data.SymmetricExcept(other));
        IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other) => SymmetricExcept(other);

        public bool TryGetValue(T equalValue, out T actualValue) => data.TryGetValue(equalValue, out actualValue);

        public EquatableSet<T> Union(IEnumerable<T> other) => new EquatableSet<T>(data.Union(other));
        IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other) => Union(other);

        public static bool operator ==(EquatableSet<T> left, EquatableSet<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EquatableSet<T> left, EquatableSet<T> right)
        {
            return !(left == right);
        }
    }
}
