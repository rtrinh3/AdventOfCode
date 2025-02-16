using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public static class GraphAlgos
    {
        public static Dictionary<T, (T parent, int distance)> BfsToAll<T>(T start, Func<T, IEnumerable<T>> getNeighbors)
            where T : notnull
        {
            Queue<T> queue = new();
            queue.Enqueue(start);
            Dictionary<T, (T, int)> parentsDistances = new();
            parentsDistances[start] = (start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                foreach (var next in getNeighbors(current))
                {
                    if (!parentsDistances.ContainsKey(next))
                    {
                        parentsDistances[next] = (current, parentsDistances[current].Item2 + 1);
                        queue.Enqueue(next);
                    }
                }
            }
            return parentsDistances;
        }

        public static (int distance, IEnumerable<T> path) BfsToEnd<T>(T start, Func<T, IEnumerable<T>> getNeighbors, Predicate<T> isEnd)
            where T : notnull
        {
            Queue<T> queue = new();
            queue.Enqueue(start);
            Dictionary<T, (T, int)> parentsDistances = new();
            parentsDistances[start] = (start, 0);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (isEnd(current))
                {
                    IEnumerable<T> GetSteps()
                    {
                        T cursor = current;
                        while (!object.Equals(cursor, start))
                        {
                            yield return cursor;
                            cursor = parentsDistances[cursor].Item1;
                        }
                        yield return start;
                    }
                    return (parentsDistances[current].Item2, GetSteps());
                }
                foreach (var next in getNeighbors(current))
                {
                    if (!parentsDistances.ContainsKey(next))
                    {
                        parentsDistances[next] = (current, parentsDistances[current].Item2 + 1);
                        queue.Enqueue(next);
                    }
                }
            }
            return (-1, Enumerable.Empty<T>());
        }

        public static Dictionary<T, (T parent, int distance)> DijkstraToAll<T>(T start, Func<T, IEnumerable<(T, int)>> getNeighbors)
            where T : notnull
        {
            PriorityQueue<T, int> queue = new();
            queue.Enqueue(start, 0);
            Dictionary<T, (T, int)> parentsDistances = new();
            parentsDistances[start] = (start, 0);
            while (queue.TryDequeue(out var current, out var currentDistance))
            {
                if (parentsDistances[current].Item2 < currentDistance)
                {
                    continue;
                }
                if (parentsDistances[current].Item2 > currentDistance)
                {
                    throw new Exception("?");
                }
                foreach (var (neighbor, distanceToNext) in getNeighbors(current))
                {
                    var nextDistance = currentDistance + distanceToNext;
                    if (!parentsDistances.TryGetValue(neighbor, out var distanceInPD) || nextDistance < distanceInPD.Item2)
                    {
                        parentsDistances[neighbor] = (current, nextDistance);
                        queue.Enqueue(neighbor, nextDistance);
                    }
                }
            }
            return parentsDistances;
        }

        public static (int distance, IEnumerable<T> path) DijkstraToEnd<T>(T start, Func<T, IEnumerable<(T, int)>> getNeighbors, Predicate<T> isEnd)
            where T : notnull
        {
            PriorityQueue<T, int> queue = new();
            queue.Enqueue(start, 0);
            Dictionary<T, (T parent, int distance)> parentsDistances = new();
            parentsDistances[start] = (start, 0);
            while (queue.TryDequeue(out var current, out var currentDistance))
            {
                if (parentsDistances[current].distance < currentDistance)
                {
                    continue;
                }
                if (parentsDistances[current].distance > currentDistance)
                {
                    throw new Exception("?");
                }
                if (isEnd(current))
                {
                    IEnumerable<T> GetSteps()
                    {
                        T cursor = current;
                        while (!object.Equals(cursor, start))
                        {
                            yield return cursor;
                            cursor = parentsDistances[cursor].parent;
                        }
                        yield return start;
                    }
                    return (parentsDistances[current].distance, GetSteps());
                }
                foreach (var (neighbor, distanceToNext) in getNeighbors(current))
                {
                    var nextDistance = currentDistance + distanceToNext;
                    if (!parentsDistances.TryGetValue(neighbor, out var distanceInPD) || nextDistance < distanceInPD.distance)
                    {
                        parentsDistances[neighbor] = (current, nextDistance);
                        queue.Enqueue(neighbor, nextDistance);
                    }
                }
            }
            return (-1, Enumerable.Empty<T>());
        }

        // https://en.wikipedia.org/wiki/Topological_sorting#Depth-first_search
        public static List<T> TopologicalSort<T>(IEnumerable<T> nodes, Func<T, IEnumerable<T>> getChildren)
        {
            HashSet<T> unmarked = nodes.ToHashSet();
            HashSet<T> permanentMark = new();
            HashSet<T> temporaryMark = new();
            List<T> result = new();

            void Visit(T n)
            {
                if (permanentMark.Contains(n))
                {
                    return;
                }
                if (temporaryMark.Contains(n))
                {
                    throw new Exception("Graph has cycle");
                }
                temporaryMark.Add(n);
                foreach (var m in getChildren(n))
                {
                    Visit(m);
                }
                unmarked.Remove(n);
                permanentMark.Add(n);
                result.Add(n);
            }

            while (unmarked.Any())
            {
                var pick = unmarked.First();
                Visit(pick);
            }
            result.Reverse();
            return result;
        }
    }
}
