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
            Day17Part2Node start = new(Enumerable.Repeat(VectorRC.Zero, 11));
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Day17Part2Node, int)> GetNeighbors(Day17Part2Node node)
            {
                List<(Day17Part2Node, int)> candidates = new();
                var current = node.Steps[^1];
                if (current == node.Steps[^2])
                {
                    // Immobile? Allow all directions
                    foreach (var next in current.NextFour())
                    {
                        char nextTile = map.Get(next);
                        if (nextTile != OUTSIDE)
                        {
                            int cost = nextTile - '0';
                            (Day17Part2Node, int) nextNode = (new Day17Part2Node(node.Steps.Skip(1).Append(next)), cost);
                            candidates.Add(nextNode);
                        }
                    }
                }
                else if ((current - node.Steps[^5]).ChebyshevMetric() < 4)
                {
                    // Less than 4 steps in a line, keep going
                    var direction = current - node.Steps[^2];
                    var next = current + direction;
                    char nextTile = map.Get(next);
                    if (nextTile != OUTSIDE)
                    {
                        int cost = nextTile - '0';
                        (Day17Part2Node, int) nextNode = (new Day17Part2Node(node.Steps.Skip(1).Append(next)), cost);
                        candidates.Add(nextNode);
                    }
                }
                else if ((current - node.Steps[0]).ChebyshevMetric() < 10)
                {
                    // Between [4, 10[ steps in a row, allow straight and turns
                    foreach (var next in current.NextFour())
                    {
                        if (next != node.Steps[^2])
                        {
                            char nextTile = map.Get(next);
                            if (nextTile != OUTSIDE)
                            {
                                int cost = nextTile - '0';
                                (Day17Part2Node, int) nextNode = (new Day17Part2Node(node.Steps.Skip(1).Append(next)), cost);
                                candidates.Add(nextNode);
                            }
                        }
                    }
                }
                else
                {
                    // 10 steps in a row, must turn
                    var direction = current - node.Steps[^2];
                    var front = current + direction;
                    foreach (var next in current.NextFour())
                    {
                        if (next != node.Steps[^2] && next != front)
                        {
                            char nextTile = map.Get(next);
                            if (nextTile != OUTSIDE)
                            {
                                int cost = nextTile - '0';
                                (Day17Part2Node, int) nextNode = (new Day17Part2Node(node.Steps.Skip(1).Append(next)), cost);
                                candidates.Add(nextNode);
                            }
                        }
                    }
                }

                return candidates;
            }
            bool IsSafelyAtEnd(Day17Part2Node node)
            {
                if (node.Steps[^1] != end)
                {
                    return false;
                }
                if ((node.Steps[^1] - node.Steps[^5]).ChebyshevMetric() < 4)
                {
                    return false;
                }
                return true;
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, IsSafelyAtEnd);

            //// Visualization
            //var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            //foreach (var states in result.path)
            //{
            //    mapLines[states.Steps[^1].Row][states.Steps[^1].Col] = '.';
            //}
            //foreach (var line in mapLines)
            //{
            //    Console.WriteLine(line.ToString());
            //}

            return result.distance;
        }

        private readonly struct Day17Part2Node : IEquatable<Day17Part2Node>
        {
            public readonly ImmutableArray<VectorRC> Steps;
            public Day17Part2Node(IEnumerable<VectorRC> steps)
            {
                Steps = steps.ToImmutableArray();
            }
            public bool Equals(Day17Part2Node other)
            {
                return Steps.SequenceEqual(other.Steps);
            }
            public override int GetHashCode()
            {
                return Steps.Aggregate(0, (acc, val) => (acc << 1) + val.GetHashCode());
            }

            // Generated IEquatable implementation via Quick Actions and Refactorings
            public override bool Equals(object? obj)
            {
                return obj is Day17Part2Node node && Equals(node);
            }
            public static bool operator ==(Day17Part2Node left, Day17Part2Node right)
            {
                return left.Equals(right);
            }
            public static bool operator !=(Day17Part2Node left, Day17Part2Node right)
            {
                return !(left == right);
            }
        }
    }
}
