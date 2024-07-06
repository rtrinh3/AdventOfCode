using System.Collections;
using System.Collections.Immutable;

namespace AocCommon
{
    public readonly struct EquatableArray<T> : IEquatable<EquatableArray<T>>, IImmutableList<T>
    {
        private readonly ImmutableArray<T> Data;

        public int Count => Data.Length;

        public T this[int index] => Data[index];

        public EquatableArray(ImmutableArray<T> data)
        {
            Data = data;
        }
        public EquatableArray(IEnumerable<T> data)
        {
            Data = data.ToImmutableArray();
        }

        public bool Equals(EquatableArray<T> other)
        {
            return Data.Equals(other.Data) || Data.SequenceEqual(other.Data);
        }
        public override int GetHashCode()
        {
            var hasher = new HashCode();
            foreach (var item in Data)
            {
                hasher.Add(item);
            }
            return hasher.ToHashCode();
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

        // Implementation of IImmutableList
        public EquatableArray<T> Add(T value)
        {
            return new EquatableArray<T>(Data.Add(value));
        }

        public EquatableArray<T> AddRange(IEnumerable<T> items)
        {
            return new EquatableArray<T>(Data.AddRange(items));
        }

        public EquatableArray<T> Clear()
        {
            return new EquatableArray<T>(Data.Clear());
        }

        public int IndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
        {
            return Data.IndexOf(item, index, count, equalityComparer);
        }

        public EquatableArray<T> Insert(int index, T element)
        {
            return new EquatableArray<T>(Data.Insert(index, element));
        }

        public EquatableArray<T> InsertRange(int index, IEnumerable<T> items)
        {
            return new EquatableArray<T>(Data.InsertRange(index, items));
        }

        public int LastIndexOf(T item, int index, int count, IEqualityComparer<T>? equalityComparer)
        {
            return Data.LastIndexOf(item, index, count, equalityComparer);
        }

        public EquatableArray<T> Remove(T value)
        {
            return new EquatableArray<T>(Data.Remove(value));
        }

        public EquatableArray<T> Remove(T value, IEqualityComparer<T>? equalityComparer)
        {
            return new EquatableArray<T>(Data.Remove(value, equalityComparer));
        }

        public EquatableArray<T> RemoveAll(Predicate<T> match)
        {
            return new EquatableArray<T>(Data.RemoveAll(match));
        }

        public EquatableArray<T> RemoveAt(int index)
        {
            return new EquatableArray<T>(Data.RemoveAt(index));
        }

        public EquatableArray<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer)
        {
            return new EquatableArray<T>(Data.RemoveRange(items, equalityComparer));
        }

        public EquatableArray<T> RemoveRange(int index, int count)
        {
            return new EquatableArray<T>(Data.RemoveRange(index, count));
        }

        public EquatableArray<T> Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
        {
            return new EquatableArray<T>(Data.Replace(oldValue, newValue, equalityComparer));
        }

        public EquatableArray<T> SetItem(int index, T value)
        {
            return new EquatableArray<T>(Data.SetItem(index, value));
        }

        public ImmutableArray<T>.Enumerator GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return ((IEnumerable<T>)Data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Data).GetEnumerator();
        }

        IImmutableList<T> IImmutableList<T>.Add(T value)
        {
            return Add(value);
        }

        IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
        {
            return AddRange(items);
        }

        IImmutableList<T> IImmutableList<T>.Clear()
        {
            return Clear();
        }

        IImmutableList<T> IImmutableList<T>.Insert(int index, T element)
        {
            return Insert(index, element);
        }

        IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
        {
            return InsertRange(index, items);
        }

        IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T>? equalityComparer)
        {
            return Remove(value, equalityComparer);
        }

        IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
        {
            return RemoveAll(match);
        }

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        IImmutableList<T> IImmutableList<T>.RemoveRange(IEnumerable<T> items, IEqualityComparer<T>? equalityComparer)
        {
            return RemoveRange(items, equalityComparer);
        }

        IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
        {
            return RemoveRange(index, count);
        }

        IImmutableList<T> IImmutableList<T>.Replace(T oldValue, T newValue, IEqualityComparer<T>? equalityComparer)
        {
            return Replace(oldValue, newValue, equalityComparer);
        }

        IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
        {
            return SetItem(index, value);
        }
    }
}
