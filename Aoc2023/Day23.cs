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
            Stack<(VectorRC position, ImmutableHashSet<VectorRC> visited)> queue = new();
            queue.Push((start, ImmutableHashSet<VectorRC>.Empty));
            while (queue.TryPop(out var current))
            {
                if (current.position == end)
                {
                    int path = current.visited.Count;
                    if (maxPath < path)
                    {
                        maxPath = path;
                    }
                }
                var visited = current.visited.Add(current.position);
                char tile = maze.Get(current.position);
                if (tile == '.')
                {
                    foreach (VectorRC next in current.position.NextFour())
                    {
                        if (maze.Get(next) != '#' && !visited.Contains(next))
                        {
                            queue.Push((next, visited));
                        }
                    }
                }
                else if (tile == '#')
                {
                    continue;
                }
                else
                {
                    VectorRC next = tile switch
                    {
                        '^' => current.position.NextUp(),
                        'v' => current.position.NextDown(),
                        '<' => current.position.NextLeft(),
                        '>' => current.position.NextRight(),
                        _ => throw new Exception(tile.ToString())
                    };
                    if (!visited.Contains(next))
                    {
                        queue.Push((next, visited));
                    }
                }
            }

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
            foreach (var node in nodes.Values.Where(n => n.Neighbors.Count == 2))
            {
                var connectionA = node.Neighbors.First();
                var connectionB = node.Neighbors.Last();
                int totalLength = connectionA.Length + connectionB.Length;
                connectionA.Other.Neighbors.RemoveWhere(c => c.Other == node);
                connectionA.Other.Neighbors.Add(new PartTwoEdge(connectionB.Other, totalLength));
                connectionB.Other.Neighbors.RemoveWhere(c => c.Other == node);
                connectionB.Other.Neighbors.Add(new PartTwoEdge(connectionA.Other, totalLength));
            }
            // Step 4: DFS!
            int maxPath = 0;
            Stack<(PartTwoNode node, ImmutableHashSet<PartTwoNode> visited, int length)> queue = new();
            PartTwoNode startNode = nodes[start];
            PartTwoNode endNode = nodes[end];
            queue.Push((startNode, ImmutableHashSet.Create(startNode), 0));
            //var observer = Task.Run(() =>
            //{
            //    while (queue.Count > 0)
            //    {
            //        Console.WriteLine($"{timer.Elapsed} - Max {maxPath}\tQueue {queue.Count}");
            //        Thread.Sleep(1000);
            //    }
            //});
            while (queue.TryPop(out var current))
            {
                if (current.node == endNode)
                {
                    int path = current.length;
                    if (maxPath < path)
                    {
                        maxPath = path;
                    }
                }
                foreach (var next in current.node.Neighbors)
                {
                    if (!current.visited.Contains(next.Other))
                    {
                        queue.Push((next.Other, current.visited.Add(next.Other), current.length + next.Length));
                    }
                }
            }
            return maxPath;
        }
    }
}
