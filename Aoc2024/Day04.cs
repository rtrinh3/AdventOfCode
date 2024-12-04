using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/4
    // --- Day 4: Ceres Search ---
    public class Day04(string input) : IAocDay
    {
        private readonly Grid grid = new(input, '\0');

        public string Part1()
        {
            VectorRC[][] directions =
            [
                [new(0, +1), new(0, +2), new(0, +3)],
                [new(0, -1), new(0, -2), new(0, -3)],
                [new(+1, 0), new(+2, 0), new(+3, 0)],
                [new(-1, 0), new(-2, 0), new(-3, 0)],
                [new(+1, +1), new(+2, +2), new(+3, +3)],
                [new(-1, -1), new(-2, -2), new(-3, -3)],
                [new(+1, -1), new(+2, -2), new(+3, -3)],
                [new(-1, +1), new(-2, +2), new(-3, +3)],
            ];
            long count = 0;
            foreach (var (Pos, Value) in grid.Iterate())
            {
                if (Value == 'X')
                {
                    foreach (var dir in directions)
                    {
                        if (grid.Get(Pos + dir[0]) == 'M' && grid.Get(Pos + dir[1]) == 'A' && grid.Get(Pos + dir[2]) == 'S')
                        {
                            count++;
                        }
                    }
                }
            }
            return count.ToString();
        }

        public string Part2()
        {
            VectorRC[][] directions =
            [
                [new (+1, +1), new(-1, -1)],
                [new (-1, -1), new(+1, +1)],
                [new (+1, -1), new(-1, +1)],
                [new (-1, +1), new(+1, -1)],
            ];
            long count = 0;
            foreach (var (Pos, Value) in grid.Iterate())
            {
                if (Value == 'A')
                {
                    int crossCount = 0;
                    foreach (var dir in directions)
                    {
                        if (grid.Get(Pos + dir[0]) == 'M' && grid.Get(Pos + dir[1]) == 'S')
                        {
                            crossCount++;
                        }
                    }
                    if (crossCount == 2)
                    {
                        count++;
                    }
                }
            }
            return count.ToString();
        }
    }
}
