namespace Aoc2020
{
    // https://adventofcode.com/2020/day/9
    // --- Day 9: Encoding Error ---
    public class Day09(string input) : IAocDay
    {
        private const int PREAMBLE_LENGTH = 25;

        private readonly long[] numbers = input.TrimEnd().Split('\n').Select(long.Parse).ToArray();

        public long Part1()
        {
            return DoPart1(PREAMBLE_LENGTH);
        }

        public long DoPart1(int preambleLength)
        {
            for (int i = preambleLength; i < numbers.Length; i++)
            {
                var current = numbers[i];
                var window = numbers.Skip(i - preambleLength).Take(preambleLength);
                if (window.All(candidate => !window.Contains(current - candidate)))
                {
                    return current;
                }
            }
            throw new Exception("No answer found");
        }

        public long Part2()
        {
            return DoPart2(PREAMBLE_LENGTH);
        }

        public long DoPart2(int preambleLength)
        {
            var target = DoPart1(preambleLength);
            // range is [begin, end]
            for (int begin = 0; begin < numbers.Length; begin++)
            {
                long accumulator = numbers[begin];
                for (int end = begin + 1; end < numbers.Length; end++)
                {
                    accumulator += numbers[end];
                    if (accumulator == target)
                    {
                        var contiguous = numbers.Skip(begin).Take(end - begin + 1);
                        var answer = contiguous.Min() + contiguous.Max();
                        return answer;
                    }
                    if (accumulator > target)
                    {
                        break;
                    }
                }
            }
            throw new Exception("No answer found");
        }
    }
}
