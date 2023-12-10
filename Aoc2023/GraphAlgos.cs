using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    internal static class GraphAlgos
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
    }
}
