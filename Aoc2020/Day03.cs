namespace Aoc2020
{
    // https://adventofcode.com/2020/day/3
    // --- Day 3: Toboggan Trajectory ---
    public class Day03(string input): IAocDay
    {
        private readonly AocCommon.Grid forest = new(input, '.');

        public long Part1()
        {
            return CheckSlope(3, 1);
        }

        public long Part2()
        {
            return CheckSlope(1, 1) * CheckSlope(3, 1) * CheckSlope(5, 1) * CheckSlope(7, 1) * CheckSlope(1, 2);
        }

        private long CheckSlope(int right, int down)
        {
            long trees = 0;
            int row = 0;
            int col = 0;
            while (row < forest.Height)
            {
                if (forest.Get(row, col) == '#')
                {
                    trees++;
                }
                row += down;
                col = (col + right) % forest.Width;
            }
            return trees;
        }
    }
}
