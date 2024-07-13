using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/24
    // --- Day 24: Lobby Layout ---
    public class Day24(string input) : IAocDay
    {
        // Reference: https://www.redblobgames.com/grids/hexagons/
        // Let's use VectorXYZ to represent cubic coordinates
        private static VectorXYZ East = new(+1, 0, -1);
        private static VectorXYZ SouthEast = new(0, +1, -1);
        private static VectorXYZ SouthWest = new(-1, +1, 0);
        private static VectorXYZ West = new(-1, 0, +1);
        private static VectorXYZ NorthWest = new(0, -1, +1);
        private static VectorXYZ NorthEast = new(+1, -1, 0);

        public string Part1()
        {
            var blackTiles = DoPart1();
            var answer = blackTiles.Count();
            return answer.ToString();
        }

        private IEnumerable<VectorXYZ> DoPart1()
        {
            string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
            DefaultDict<VectorXYZ, bool> floor = new(); // false = white, true = black
            foreach (var line in lines)
            {
                VectorXYZ position = VectorXYZ.Zero;
                var steps = Regex.Matches(line, @"(e|se|sw|w|nw|ne)");
                foreach (Match step in steps)
                {
                    VectorXYZ delta = step.Value switch
                    {
                        "e" => East,
                        "se" => SouthEast,
                        "sw" => SouthWest,
                        "w" => West,
                        "nw" => NorthWest,
                        "ne" => NorthEast,
                        _ => throw new Exception(step.Value)
                    };
                    position += delta;
                }
                floor[position] = !floor[position];
            }
            return floor.Where(x => x.Value).Select(x => x.Key);
        }

        public string Part2()
        {
            var initialTiles = DoPart1();
            HashSet<VectorXYZ> floor = new(initialTiles);
            for (int day = 0; day < 100; day++)
            {
                HashSet<VectorXYZ> nextFloor = new();
                // Any black tile with zero or more than 2 black tiles immediately adjacent to it is flipped to white.
                foreach (var black in floor)
                {
                    var neighbors = Neighbors(black);
                    var blackNeighbors = neighbors.Count(floor.Contains);
                    if (blackNeighbors == 0 || blackNeighbors > 2)
                    {
                        // Nothing
                    }
                    else
                    {
                        nextFloor.Add(black);
                    }
                }
                // Any white tile with exactly 2 black tiles immediately adjacent to it is flipped to black.
                // (The white tiles we're looking for are a subset of those next to a black tile)
                IEnumerable<VectorXYZ> whites = floor.SelectMany(Neighbors).Distinct().Where(w => !floor.Contains(w));
                foreach (var white in whites)
                {
                    var neighbors = Neighbors(white);
                    var blackNeighbors = neighbors.Count(floor.Contains);
                    if (blackNeighbors == 2)
                    {
                        nextFloor.Add(white);
                    }
                    // else nothing
                }
                floor = nextFloor;
            }
            var answer = floor.Count;
            return answer.ToString();
        }

        private static VectorXYZ[] Neighbors(VectorXYZ vec)
        {
            return [vec + East, vec + SouthEast, vec + SouthWest, vec + West, vec + NorthWest, vec + NorthEast];
        }
    }
}
