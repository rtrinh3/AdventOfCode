using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AocCommon;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/17
    public class Day17(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly Grid map = new Grid(input, OUTSIDE);

        private record Node(VectorRC Position, VectorRC Direction);

        public long Part1()
        {
            Node start = new(VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Node, int)> GetNeighbors(Node node)
            {
                VectorRC[] nextDirections = node.Direction == VectorRC.Zero ? [VectorRC.Right, VectorRC.Down] : [node.Direction.RotatedLeft(), node.Direction.RotatedRight()];
                foreach (var dir in nextDirections)
                {
                    var nextPos = node.Position;
                    int cost = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        nextPos += dir;
                        char nextTile = map.Get(nextPos);
                        if (nextTile == OUTSIDE)
                        {
                            break;
                        }
                        cost += nextTile - '0';
                        yield return (new Node(nextPos, dir), cost);
                    }
                }
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, node => node.Position == end);

            //// Visualization
            //var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            //foreach (var step in result.path)
            //{
            //    mapLines[step.Position.Row][step.Position.Col] = '.';
            //}
            //foreach (var line in mapLines)
            //{
            //    Console.WriteLine(line.ToString());
            //}

            return result.distance;
        }

        public long Part2()
        {
            Node start = new(VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Node, int)> GetNeighbors(Node node)
            {
                VectorRC[] nextDirections = node.Direction == VectorRC.Zero ? [VectorRC.Right, VectorRC.Down] : [node.Direction.RotatedLeft(), node.Direction.RotatedRight()];
                foreach (var dir in nextDirections)
                {
                    var nextPos = node.Position;
                    int cost = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        nextPos += dir;
                        char nextTile = map.Get(nextPos);
                        if (nextTile == OUTSIDE)
                        {
                            break;
                        }
                        cost += nextTile - '0';
                        if (i >= 3)
                        {
                            yield return (new Node(nextPos, dir), cost);
                        }
                    }
                }
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, node => node.Position == end);

            //// Visualization
            //var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            //foreach (var step in result.path)
            //{
            //    mapLines[step.Position.Row][step.Position.Col] = '.';
            //}
            //foreach (var line in mapLines)
            //{
            //    Console.WriteLine(line.ToString());
            //}

            return result.distance;
        }
    }
}
