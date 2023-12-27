namespace Aoc2022
{
    // https://adventofcode.com/2022/day/1
    public class Day01 : IAocDay
    {
        private readonly int[][] elves;

        public Day01(string input)
        {
            const StringSplitOptions TrimAndDiscard = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
            elves = input.ReplaceLineEndings("\n").Split("\n\n", TrimAndDiscard).Select(s =>
            {
                return s.Split('\n', TrimAndDiscard).Select(int.Parse).ToArray();
            }).ToArray();
        }

        public string Part1()
        {
            return elves.Max(elf => elf.Sum()).ToString();
        }

        public string Part2()
        {
            return elves.Select(elf => elf.Sum()).OrderByDescending(x => x).Take(3).Sum().ToString();
        }
    }
}
