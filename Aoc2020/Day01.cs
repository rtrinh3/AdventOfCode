namespace Aoc2020
{
    // https://adventofcode.com/2020/day/1
    // --- Day 1: Report Repair ---
    public class Day01(string input): IAocDay
    {
        const long SUM = 2020L;

        private readonly long[] entries = input.TrimEnd().Split('\n').Select(long.Parse).ToArray();

        public long Part1()
        {
            var entriesHash = new HashSet<long>(entries);
            foreach (var entry in entries)
            {
                var complement = SUM - entry;
                if (entriesHash.Contains(complement))
                {
                    var product = entry * complement;
                    return product;
                }
            }
            throw new Exception("Answer not found");
        }

        public long Part2()
        {
            var entriesHash = new HashSet<long>(entries);
            for (int i = 0; i < entries.Length; i++)
            {
                for (int j = 1; j < entries.Length; j++)
                {
                    var complement = SUM - entries[i] - entries[j];
                    if (entriesHash.Contains(complement))
                    {
                        var product = entries[i] * entries[j] * complement;
                        return product;
                    }
                }
            }
            throw new Exception("Answer not found");
        }
    }
}
