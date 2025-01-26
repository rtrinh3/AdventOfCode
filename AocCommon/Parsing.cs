using System.Text.RegularExpressions;

namespace AocCommon
{
    public static class Parsing
    {
        public const StringSplitOptions TrimAndDiscard = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public static string[] SplitLines(string input)
        {
            return input.ReplaceLineEndings("\n").Split('\n', TrimAndDiscard);
        }

        public static int[] IntsPositive(string input)
        {
            return Regex.Matches(input, @"\d+").Select(m => int.Parse(m.ValueSpan)).ToArray();
        }
        public static int[] IntsNegative(string input)
        {
            return Regex.Matches(input, @"-?\d+").Select(m => int.Parse(m.ValueSpan)).ToArray();
        }
    }
}
