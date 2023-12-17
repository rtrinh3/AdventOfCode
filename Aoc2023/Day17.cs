using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aoc2023
{
    using Day17Part1Node = (VectorRC, VectorRC, VectorRC, VectorRC);
    using Day17Part2Node = (VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC, VectorRC);

    // https://adventofcode.com/2023/day/17
    public class Day17(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly Grid map = new Grid(input, OUTSIDE);

        public long Part1()
        {
            Day17Part1Node start = (VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Day17Part1Node, int)> GetNeighbors(Day17Part1Node node)
            {
                VectorRC directionFromThreeStepsAgo = node.Item4 - node.Item1;
                VectorRC straightLineDirection = new VectorRC(directionFromThreeStepsAgo.Row / 3, directionFromThreeStepsAgo.Col / 3);
                List<(Day17Part1Node, int)> candidates = new();
                foreach (var next in node.Item4.NextFour())
                {
                    if (next != node.Item3 && next != node.Item4 + straightLineDirection)
                    {
                        char nextTile = map.Get(next);
                        if (nextTile != OUTSIDE)
                        {
                            int cost = nextTile - '0';
                            (Day17Part1Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, next), cost);
                            candidates.Add(nextNode);
                        }
                    }
                }
                return candidates;
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, node => node.Item4 == end);

            //// Visualization
            //var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            //foreach (var step in result.path)
            //{
            //    mapLines[step.Item4.Row][step.Item4.Col] = '.';
            //}
            //foreach (var line in mapLines)
            //{
            //    Console.WriteLine(line.ToString());
            //}

            return result.distance;
        }

        public long Part2()
        {
            Day17Part2Node start = (VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Day17Part2Node, int)> GetNeighbors(Day17Part2Node node)
            {
                List<(Day17Part2Node, int)> candidates = new();
                var current = node.Item11;
                if (current == node.Item10)
                {
                    // Immobile? Allow all directions
                    foreach (var next in current.NextFour())
                    {
                        char nextTile = map.Get(next);
                        if (nextTile != OUTSIDE)
                        {
                            int cost = nextTile - '0';
                            (Day17Part2Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6, node.Item7, node.Item8, node.Item9, node.Item10, node.Item11, next), cost);
                            candidates.Add(nextNode);
                        }
                    }
                }
                else if ((current - node.Item7).ChebyshevMetric() < 4)
                {
                    // Less than 4 steps in a line, keep going
                    var direction = current - node.Item10;
                    var next = current + direction;
                    char nextTile = map.Get(next);
                    if (nextTile != OUTSIDE)
                    {
                        int cost = nextTile - '0';
                        (Day17Part2Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6, node.Item7, node.Item8, node.Item9, node.Item10, node.Item11, next), cost);
                        candidates.Add(nextNode);
                    }
                }
                else if ((current - node.Item1).ChebyshevMetric() < 10)
                {
                    // Between [4, 10[ steps in a row, allow straight and turns
                    foreach (var next in current.NextFour())
                    {
                        if (next != node.Item10)
                        {
                            char nextTile = map.Get(next);
                            if (nextTile != OUTSIDE)
                            {
                                int cost = nextTile - '0';
                                (Day17Part2Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6, node.Item7, node.Item8, node.Item9, node.Item10, node.Item11, next), cost);
                                candidates.Add(nextNode);
                            }
                        }
                    }
                }
                else
                {
                    // 10 steps in a row, must turn
                    var direction = current - node.Item10;
                    var front = current + direction;
                    foreach (var next in current.NextFour())
                    {
                        if (next != node.Item10 && next != front)
                        {
                            char nextTile = map.Get(next);
                            if (nextTile != OUTSIDE)
                            {
                                int cost = nextTile - '0';
                                (Day17Part2Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, node.Item5, node.Item6, node.Item7, node.Item8, node.Item9, node.Item10, node.Item11, next), cost);
                                candidates.Add(nextNode);
                            }
                        }
                    }
                }

                return candidates;
            }
            bool IsSafelyAtEnd(Day17Part2Node node)
            {
                if (node.Item11 != end)
                {
                    return false;
                }
                if ((node.Item11 - node.Item7).ChebyshevMetric() < 4)
                {
                    return false;
                }
                return true;
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, IsSafelyAtEnd);

            // Visualization
            var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            foreach (var states in result.path)
            {
                mapLines[states.Item11.Row][states.Item11.Col] = '.';
            }
            foreach (var line in mapLines)
            {
                Console.WriteLine(line.ToString());
            }

            return result.distance;
        }
    }
}
