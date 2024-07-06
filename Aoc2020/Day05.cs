namespace Aoc2020
{
    // https://adventofcode.com/2020/day/5
    // --- Day 5: Binary Boarding ---
    public class Day05(string input) : IAocDay
    {
        private readonly HashSet<int> seats = new(input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(line =>
            Convert.ToInt32(line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)
        ));

        public long Part1()
        {
            return seats.Max();
        }

        public long Part2()
        {
            for (int i = 0; i < (1 << 10); i++)
            {
                if (!seats.Contains(i) && seats.Contains(i + 1) && seats.Contains(i - 1))
                {
                    return i;
                }
            }
            throw new Exception("Answer not found");
        }
    }
}
