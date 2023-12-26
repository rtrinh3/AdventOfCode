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
            List<string> nodes = neighbors.Keys.ToList();

        LABEL_START:
            // From a random node, find the furthest node A
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
            }
            List<(string, string)> edgesStraddlingMiddle = edges.Where(e => (nodePos[e.Item1] < 0.5 && 0.5 < nodePos[e.Item2]) || (nodePos[e.Item2] < 0.5 && 0.5 < nodePos[e.Item1])).ToList();
            // If I failed to make a clean cut, try again with another node
            if (edgesStraddlingMiddle.Count != 3)
            {
                goto LABEL_START;
            }

            // Count the clusters
            long leftCluster = 0;
            long rightCluster = 0;
            foreach (var node in nodes)
            {
                if (nodePos[node] < 0.5)
                {
                    leftCluster++;
                }
                if (nodePos[node] > 0.5)
                {
                    rightCluster++;
                }
                if (nodePos[node] == 0.5)
                {
                    throw new Exception("How is this still in the middle");
                }
            }

            return leftCluster * rightCluster;
        }

        public long Part2()
        {
            Console.WriteLine("Merry Christmas!");
            return 2023_12_25;
        }
    }
}
