using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    public class Day23 : IAocDay
    {
        private readonly Grid maze;
        private readonly VectorRC start;
        private readonly VectorRC end;

        public Day23(string input)
        {
            maze = new(input, '#');
            int startCol = maze.Data[0].IndexOf('.');
            start = new(0, startCol);
            int endCol = maze.Data.Last().LastIndexOf('.');
            end = new(maze.Height - 1, endCol);
        }

        public long Part1()
        {
            // Naive algo
            // It turns out we don't need this insight from Wikipedia:
            // https://en.wikipedia.org/wiki/Longest_path_problem#Acyclic_graphs
            int maxPath = 0;
            HashSet<VectorRC> visited = [start];
            void Visit(VectorRC position)
            {
                if (position == end)
                {
                    int length = visited.Count - 1;
                    if (maxPath < length)
                    {
                        maxPath = length;
                    }
                    return;
                }
                char tile = maze.Get(position);
                List<VectorRC> placesToVisit = new();
                if (tile == '.')
                {
                    foreach (VectorRC next in position.NextFour())
                    {
                        if (maze.Get(next) != '#' && !visited.Contains(next))
                        {
                            placesToVisit.Add(next);
                        }
                    }
                }
                else
                {
                    VectorRC next = tile switch
                    {
                        '^' => position.NextUp(),
                        'v' => position.NextDown(),
                        '<' => position.NextLeft(),
                        '>' => position.NextRight(),
                        _ => throw new Exception(tile.ToString())
                    };
                    if (!visited.Contains(next))
                    {
                        placesToVisit.Add(next);
                    }
                }
                foreach (VectorRC next in placesToVisit)
                {
                    visited.Add(next);
                    Visit(next);
                    visited.Remove(next);
                }
            }
            Visit(start);

            return maxPath;
        }

        private class PartTwoNode
        {
            public readonly HashSet<PartTwoEdge> Neighbors = new();
        }
        private record struct PartTwoEdge(PartTwoNode Other, int Length);

        public long Part2()
        {
            var timer = Stopwatch.StartNew();
            // Step 1: create all the nodes
            Dictionary<VectorRC, PartTwoNode> nodes = new();
            foreach (var (position, value) in maze.Iterate())
            {
                if (value != '#')
                {
                    nodes[position] = new PartTwoNode();
                }
            }
            // Step 2: connect the nodes
            foreach (var (position, node) in nodes)
            {
                foreach (var next in position.NextFour())
                {
                    if (nodes.TryGetValue(next, out var otherNode))
                    {
                        node.Neighbors.Add(new PartTwoEdge(otherNode, 1));
                        otherNode.Neighbors.Add(new PartTwoEdge(node, 1));
                    }
                }
            }
            // Step 3: eliminate nodes that have exactly 2 neighbors
            foreach (var node in nodes.Values.Where(n => n.Neighbors.Count == 2).ToList())
            {
                var connectionA = node.Neighbors.First();
                var connectionB = node.Neighbors.Last();
                int totalLength = connectionA.Length + connectionB.Length;
                var removeA = connectionA.Other.Neighbors.RemoveWhere(c => c.Other == node);
                var addA = connectionA.Other.Neighbors.Add(new PartTwoEdge(connectionB.Other, totalLength));
                var removeB = connectionB.Other.Neighbors.RemoveWhere(c => c.Other == node);
                var addB = connectionB.Other.Neighbors.Add(new PartTwoEdge(connectionA.Other, totalLength));
                Debug.Assert(removeA == 1);
                Debug.Assert(addA);
                Debug.Assert(removeB == 1);
                Debug.Assert(addB);
                node.Neighbors.Clear();
            }
            // Step 4: If we step on the only node connected to the exit,
            // we must go to the exit, since otherwise we'd block it off.
            // To reflect this, change that node so that it only has one neighbor: the exit.
            // From https://www.reddit.com/r/adventofcode/comments/18p0tcn/comment/kel1jmx/?utm_source=reddit&utm_medium=web2x&context=3
            // (This turns the graph back into a directed graph.)
            PartTwoNode endNode = nodes[end];
            Debug.Assert(endNode.Neighbors.Count == 1);
            var nodeConnectedToEnd = endNode.Neighbors.Single().Other;
            nodeConnectedToEnd.Neighbors.RemoveWhere(n => n.Other != endNode);
            // Step 5: DFS!
            int maxPath = 0;
            PartTwoNode startNode = nodes[start];
            HashSet<PartTwoNode> visited = [startNode];
            void Visit(PartTwoNode node, int length)
            {
                if (node == endNode)
                {
                    if (maxPath < length)
                    {
                        maxPath = length;
                    }
                    return;
                }
                foreach (var next in node.Neighbors)
                {
                    if (!visited.Contains(next.Other))
                    {
                        visited.Add(next.Other);
                        Visit(next.Other, length + next.Length);
                        visited.Remove(next.Other);
                    }
                }
            }
            Visit(startNode, 0);
            return maxPath;
        }
    }
}
