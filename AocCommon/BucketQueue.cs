using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace AocCommon
{
    // This post https://www.reddit.com/r/adventofcode/comments/18sfwnd/2023_50_stars_on_the_commodore_64/
    // suggests that a Bucket Queue ( https://en.wikipedia.org/wiki/Bucket_queue )
    // might be more efficient that PriorityQueue for many problems, including most Advent of Code problems.
    // So here's my attempt at implementing one.
    public class BucketQueue<T> : IEnumerable<T>
    {
        public int Count { get; private set; } = 0;

        private readonly List<Queue<T>> buckets = new();
        private int currentPriority = 0;

        public void Clear()
        {
            buckets.Clear();
            Count = 0;
        }

        private void FindLowestPriority()
        {
            while (currentPriority < buckets.Count && buckets[currentPriority].Count == 0)
            {
                currentPriority++;
            }
        }

        public T Dequeue()
        {
            FindLowestPriority();
            if (currentPriority >= buckets.Count)
            {
                throw new InvalidOperationException("The queue is empty");
            }
            Count--;
            return buckets[currentPriority].Dequeue();
        }

        public void Enqueue(T item, int priority)
        {
            if (priority < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(priority), "Must be zero or greater");
            }
            if (priority < currentPriority)
            {
                currentPriority = priority;
            }
            while (priority >= buckets.Count)
            {
                buckets.Add(new Queue<T>());
            }
            Count++;
            buckets[priority].Enqueue(item);
        }

        public T Peek()
        {
            FindLowestPriority();
            if (currentPriority >= buckets.Count)
            {
                throw new InvalidOperationException("The queue is empty");
            }
            return buckets[currentPriority].Peek();
        }

        public bool TryDequeue([MaybeNullWhen(false)] out T item, out int priority)
        {
            FindLowestPriority();
            if (currentPriority >= buckets.Count)
            {
                item = default;
                priority = -1;
                return false;
            }
            else
            {
                Count--;
                item = buckets[currentPriority].Dequeue();
                priority = currentPriority;
                return true;
            }
        }

        public bool TryPeek([MaybeNullWhen(false)] out T item, out int priority)
        {
            FindLowestPriority();
            if (currentPriority >= buckets.Count)
            {
                item = default;
                priority = -1;
                return false;
            }
            else
            {
                item = buckets[currentPriority].Peek();
                priority = currentPriority;
                return true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return buckets.SelectMany(b => b).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
