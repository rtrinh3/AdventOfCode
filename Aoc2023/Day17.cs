using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    using Day17Node = (VectorRC, VectorRC, VectorRC, VectorRC);

    // https://adventofcode.com/2023/day/17
    public class Day17(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly Grid map = new Grid(input, OUTSIDE);

        public long Part1()
        {
            Day17Node start = (VectorRC.Zero, VectorRC.Zero, VectorRC.Zero, VectorRC.Zero);
            VectorRC end = new VectorRC(map.Height - 1, map.Width - 1);
            IEnumerable<(Day17Node, int)> GetNeighbors(Day17Node node)
            {
                VectorRC directionFromThreeStepsAgo = node.Item4 - node.Item1;
                VectorRC straightLineDirection = new VectorRC(directionFromThreeStepsAgo.Row / 3, directionFromThreeStepsAgo.Col / 3);
                List<(Day17Node, int)> candidates = new();
                foreach (var next in node.Item4.NextFour())
                {
                    if (next != node.Item3 && next != node.Item4 + straightLineDirection)
                    {
                        char nextTile = map.Get(next);
                        if (nextTile != OUTSIDE)
                        {
                            int cost = nextTile - '0';
                            (Day17Node, int) nextNode = ((node.Item2, node.Item3, node.Item4, next), cost);
                            candidates.Add(nextNode);
                        }
                    }
                }
                return candidates;
            }
            var result = GraphAlgos.DijkstraToEnd(start, GetNeighbors, node => node.Item4 == end);

            // Visualization
            var mapLines = map.Data.Select(s => new StringBuilder(s)).ToArray();
            foreach (var step in result.path)
            {
                mapLines[step.Item4.Row][step.Item4.Col] = '.';
            }
            foreach (var line in mapLines)
            {
                Console.WriteLine(line.ToString());
            }

            return result.distance;
        }

        public long Part2()
        {
            return -2;
        }
    }
}
