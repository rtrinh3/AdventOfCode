namespace Aoc2022
{
    // https://adventofcode.com/2022/day/3
    public class Day03(string input) : IAocDay
    {
        int priority(char c)
        {
            if ('a' <= c && c <= 'z')
            {
                return 1 + (c - 'a');
            }
            else if ('A' <= c && c <= 'Z')
            {
                return 27 + (c - 'A');
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public string Part1()
        {
            var commons = AocCommon.Constants.SplitLines(input).Select(line => {
                var firstHalf = line.Take(line.Length / 2);
                var secondHalf = line.Skip(line.Length / 2);
                var common = firstHalf.Distinct().Intersect(secondHalf.Distinct()).Single();
                return common;
            });
            return commons.Select(priority).Sum().ToString();
        }
        public string Part2()
        {
            var inputs = AocCommon.Constants.SplitLines(input);
            int sum = 0;
            for (int i = 0; i < inputs.Length; i += 3)
            {
                char common = inputs[i..(i + 3)].Cast<IEnumerable<char>>().Aggregate((a, b) => a.Distinct().Intersect(b.Distinct())).First();
                sum += priority(common);
            }
            return sum.ToString();
        }
    }
}
