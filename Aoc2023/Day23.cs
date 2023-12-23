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

        public long Part2()
        {
            var timer = Stopwatch.StartNew();
            // Naive algo
            int maxPath = 0;
            Stack<(VectorRC position, ImmutableHashSet<VectorRC> visited)> queue = new();
            queue.Push((start, ImmutableHashSet<VectorRC>.Empty));
            var observer = Task.Run(() =>
            {
                while (queue.Count > 0)
                {
                    Console.WriteLine($"{timer.Elapsed} - Max {maxPath}\tQueue {queue.Count}");
                    Thread.Sleep(1000);
                }
            });
            while (queue.TryPop(out var current))
            {
                if (current.position == end)
                {
                    int path = current.visited.Count;
                    if (maxPath < path)
                    {
                        maxPath = path;
                    }
                    continue;
                }
                var visited = current.visited.Add(current.position);
                char tile = maze.Get(current.position);
                if (tile != '#')
                {
                    foreach (VectorRC next in current.position.NextFour())
                    {
                        if (maze.Get(next) != '#' && !visited.Contains(next))
                        {
                            queue.Push((next, visited));
                        }
                    }
                }
            }

            return maxPath;
        }
    }
}
