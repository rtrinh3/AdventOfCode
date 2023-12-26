using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/25
    public class Day25(string input) : IAocDay
    {
        private static (string, string) MakeEdge(string a, string b)
        {
            return (a.CompareTo(b) <= 0) ? (a, b) : (b, a);
        }

        private static readonly char[] Separators = { ':', ' ' };

        public long Part1()
        {
            // Inspired by https://www.reddit.com/r/adventofcode/comments/18qbsxs/comment/keug4yl
            // to find the clusters by putting the nodes on a number line and averaging their positions with their neighbors'

            // Parse
            const StringSplitOptions TrimAndDiscard = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
            string[] lines = input.ReplaceLineEndings("\n").Split('\n', TrimAndDiscard);
            HashSet<(string, string)> edges = new();
            DefaultDict<string, HashSet<string>> neighbors = new();
            foreach (string line in lines)
            {
                string[] parts = line.Split(Separators, TrimAndDiscard);
                string head = parts[0];
                var tails = parts.Skip(1);
                foreach (string tail in tails)
                {
                    edges.Add(MakeEdge(head, tail));
                    neighbors[head].Add(tail);
                    neighbors[tail].Add(head);
                }
            }

            // From a random node, find the furthest node A
            List<string> nodes = neighbors.Keys.ToList();
            string? furthestA = null;
            {
                string start = nodes[Random.Shared.Next(nodes.Count)];
                HashSet<string> visited = new();
                Queue<string> queue = new();
                queue.Enqueue(start);
                while (queue.TryDequeue(out var node))
                {
                    visited.Add(node);
                    furthestA = node;
                    foreach (string next in neighbors[node])
                    {
                        if (!visited.Contains(next))
                        {
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            Debug.Assert(furthestA != null);

            // From A, find the furthest node B
            string? furthestB = null;
            {
                HashSet<string> visited = new();
                Queue<string> queue = new();
                queue.Enqueue(furthestA);
                while (queue.TryDequeue(out var node))
                {
                    visited.Add(node);
                    furthestB = node;
                    foreach (string next in neighbors[node])
                    {
                        if (!visited.Contains(next))
                        {
                            queue.Enqueue(next);
                        }
                    }
                }
            }
            Debug.Assert(furthestB != null);
            Debug.Assert(furthestA != furthestB);

            // Initialize the nodes on the imaginary line
            DefaultDict<string, double> nodePos = new(() => 0.5);
            nodePos[furthestA] = 0;
            nodePos[furthestB] = 1;

            // Pull the nodes towards nodes A and B until the three links are visible
            // I should only need 1 iteration, but I can't guarantee it, so I put it in a loop.
            List<(string, string)>? edgesStraddlingMiddle = null;
            while (edgesStraddlingMiddle == null || edgesStraddlingMiddle.Count > 3)
            {
                HashSet<string> visited = new();
                Queue<string> queue = new();
                queue.Enqueue(furthestA);
                queue.Enqueue(furthestB);
                while (queue.TryDequeue(out var node))
                {
                    visited.Add(node);

                    if (node != furthestA && node != furthestB)
                    {
                        nodePos[node] = neighbors[node].Average(next => nodePos[next]);
                    }

                    foreach (string next in neighbors[node])
                    {
                        if (!visited.Contains(next))
                        {
                            queue.Enqueue(next);
                        }
                    }
                }

                edgesStraddlingMiddle = edges.Where(e => (nodePos[e.Item1] < 0.5 && 0.5 < nodePos[e.Item2]) || (nodePos[e.Item2] < 0.5 && 0.5 < nodePos[e.Item1])).ToList();
            }
            Debug.Assert(edgesStraddlingMiddle.Count == 3);

            // Cut the links
            edges.ExceptWith(edgesStraddlingMiddle);
            foreach (var edge in edgesStraddlingMiddle)
            {
                neighbors[edge.Item1].Remove(edge.Item2);
                neighbors[edge.Item2].Remove(edge.Item1);
            }

            // Find the connected components
            UnionFind<string> connectionTracker = new();
            foreach (var node in nodes)
            {
                foreach (var next in neighbors[node])
                {
                    connectionTracker.Union(node, next);
                }
            }
            var components = nodes.GroupBy(n => connectionTracker.Find(n)).ToList();
            Debug.Assert(components.Count == 2);
            long answer = components[0].LongCount() * components[1].LongCount();
            return answer;
        }

        public long Part2()
        {
            Console.WriteLine("Merry Christmas!");
            return 2023_12_25;
        }
    }
}
