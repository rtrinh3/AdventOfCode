using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/6
    // --- Day 6: Guard Gallivant ---
    public class Day06(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';
        private readonly Grid grid = new(input, OUTSIDE);

        public string Part1()
        {
            var startPos = grid.Iterate().Where(x => x.Value == '^').Select(x => x.Position).Single();
            var startDir = VectorRC.Up;

            HashSet<VectorRC> visited = new();
            VectorRC pos = startPos;
            VectorRC dir = startDir;
            char tile;
            while (OUTSIDE != (tile = grid.Get(pos)))
            {
                visited.Add(pos);
                VectorRC nextDir = dir;
                while (true)
                {
                    VectorRC nextPos = pos + nextDir;
                    char nextTile = grid.Get(nextPos);
                    if (nextTile != '#')
                    {
                        break;
                    }
                    nextDir = nextDir.RotatedRight();
                }
                pos = pos + nextDir;
                dir = nextDir;
            }

            return visited.Count.ToString();
        }

        public string Part2()
        {
            return "2";
        }
    }
}
