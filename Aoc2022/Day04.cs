using System.Text.RegularExpressions;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/4
    public class Day04(string input) : IAocDay
    {
        private readonly IEnumerable<(int, int, int, int)> inputs = AocCommon.Parsing.SplitLines(input).Select(line =>
        {
            var parsed = Regex.Match(line, @"^(\d+)-(\d+),(\d+)-(\d+)");
            int leftStart = int.Parse(parsed.Groups[1].ValueSpan);
            int leftEnd = int.Parse(parsed.Groups[2].ValueSpan);
            if (leftStart > leftEnd)
            {
                int temp = leftStart;
                leftStart = leftEnd;
                leftEnd = temp;
            }
            int rightStart = int.Parse(parsed.Groups[3].ValueSpan);
            int rightEnd = int.Parse(parsed.Groups[4].ValueSpan);
            if (rightStart > rightEnd)
            {
                int temp = rightStart;
                rightStart = rightEnd;
                rightEnd = temp;
            }
            return (leftStart, leftEnd, rightStart, rightEnd);
        });
        public string Part1()
        {
            bool LeftContainsRight(int leftStart, int leftEnd, int rightStart, int rightEnd) => (leftStart <= rightStart && rightEnd <= leftEnd);
            bool OneContainsOther((int, int, int, int) t) => (LeftContainsRight(t.Item1, t.Item2, t.Item3, t.Item4) || LeftContainsRight(t.Item3, t.Item4, t.Item1, t.Item2));
            return inputs.Count(i => OneContainsOther(i)).ToString();
        }
        public string Part2()
        {
            bool Overlaps((int, int, int, int) t) => (t.Item1 <= t.Item4 && t.Item3 <= t.Item2);
            return inputs.Count(i => Overlaps(i)).ToString();
        }
    }
}
