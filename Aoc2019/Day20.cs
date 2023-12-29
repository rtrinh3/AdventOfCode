using AocCommon;
using System.Linq;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/20
    public class Day20 : IAocDay
    {
        private readonly Grid maze;
        readonly DefaultDict<VectorRC, HashSet<VectorRC>> neighbors = new();
        readonly DefaultDict<string, List<VectorRC>> portals = new();

        public Day20(string input)
        {
            maze = new Grid(input, ' ');
            foreach (var (pos, tile) in maze.Iterate())
            {
                if (tile == '.')
                {
                    foreach (var next in pos.NextFour())
                    {
                        if (maze.Get(next) == '.')
                        {
                            neighbors[pos].Add(next);
                            neighbors[next].Add(pos);
                        }
                    }
                }
                if (char.IsLetter(tile))
                {
                    var (row, col) = pos;
                    if (char.IsLetter(maze.Get(row, col + 1)) && maze.Get(row, col + 2) == '.')
                    {
                        string name = "" + maze.Get(row, col) + maze.Get(row, col + 1);
                        portals[name].Add(new VectorRC(row, col + 2));
                    }
                    if (char.IsLetter(maze.Get(row, col + 1)) && maze.Get(row, col - 1) == '.')
                    {
                        string name = "" + maze.Get(row, col) + maze.Get(row, col + 1);
                        portals[name].Add(new VectorRC(row, col - 1));
                    }
                    if (char.IsLetter(maze.Get(row + 1, col)) && maze.Get(row + 2, col) == '.')
                    {
                        string name = "" + maze.Get(row, col) + maze.Get(row + 1, col);
                        portals[name].Add(new VectorRC(row + 2, col));
                    }
                    if (char.IsLetter(maze.Get(row + 1, col)) && maze.Get(row - 1, col) == '.')
                    {
                        string name = "" + maze.Get(row, col) + maze.Get(row + 1, col);
                        portals[name].Add(new VectorRC(row - 1, col));
                    }
                }
            }
        }

        public string Part1()
        {
            Dictionary<VectorRC, HashSet<VectorRC>> partOneNeighbors = new(neighbors); // Deep copy neighbors -> partOneNeighbors
            foreach (var kvp in portals)
            {
                if (kvp.Value.Count == 2)
                {
                    partOneNeighbors[kvp.Value[0]].Add(kvp.Value[1]);
                    partOneNeighbors[kvp.Value[1]].Add(kvp.Value[0]);
                }
            }

            var start = portals["AA"].Single();
            var end = portals["ZZ"].Single();
            var partOneAnswer = GraphAlgos.BfsToEnd(start, p => partOneNeighbors[p], p => p == end);
            return partOneAnswer.distance.ToString();
        }

        public string Part2()
        {
            Dictionary<VectorRC, string> outerPortalsPosName = new();
            Dictionary<string, VectorRC> outerPortalsNamePos = new();
            Dictionary<VectorRC, string> innerPortalsPosName = new();
            Dictionary<string, VectorRC> innerPortalsNamePos = new();
            foreach (var kvp in portals)
            {
                foreach (var coord in kvp.Value)
                {
                    if (coord.Row == 2 || coord.Row == maze.Height - 3 || coord.Col == 2 || coord.Col == maze.Width - 3)
                    {
                        outerPortalsPosName[coord] = kvp.Key;
                        outerPortalsNamePos[kvp.Key] = coord;
                    }
                    else
                    {
                        innerPortalsPosName[coord] = kvp.Key;
                        innerPortalsNamePos[kvp.Key] = coord;
                    }
                }
            }
            IEnumerable<(int, int, int)> GetPartTwoNeighbors((int, int, int) pos)
            {
                var (row, col, depth) = pos;
                var flatPos = new VectorRC(row, col);
                var deepNeighbors = neighbors[flatPos].Select(n => (n.Row, n.Col, depth));
                if (outerPortalsPosName.TryGetValue(flatPos, out var outName))
                {
                    if (depth > 0 && innerPortalsNamePos.TryGetValue(outName, out var otherSide))
                    {
                        deepNeighbors = deepNeighbors.Append((otherSide.Row, otherSide.Col, depth - 1));
                    }
                }
                if (innerPortalsPosName.TryGetValue(flatPos, out var inName))
                {
                    if (outerPortalsNamePos.TryGetValue(inName, out var otherSide))
                    {
                        deepNeighbors = deepNeighbors.Append((otherSide.Row, otherSide.Col, depth + 1));
                    }
                }
                return deepNeighbors;
            }
            var startFlat = portals["AA"].Single();
            var endFlat = portals["ZZ"].Single();
            var start = (startFlat.Row, startFlat.Col, 0);
            var end = (endFlat.Row, endFlat.Col, 0);
            var partTwoAnswer = GraphAlgos.BfsToEnd(start, p => GetPartTwoNeighbors(p), p => p == end);
            return partTwoAnswer.distance.ToString();
        }
    }
}
