using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/2
    // --- Day 2: Password Philosophy ---
    public class Day02(string input) : IAocDay
    {
        private readonly string[] entries = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

        public long Part1()
        {
            long counter = 0;
            foreach (var entry in entries)
            {
                var match = Regex.Match(entry, @"^(\d+)-(\d+) (.): (.+)$");
                if (!match.Success)
                {
                    throw new Exception("Bad format? " + entry);
                }
                int lowerBound = int.Parse(match.Groups[1].ValueSpan);
                int upperBound = int.Parse(match.Groups[2].ValueSpan);
                char requiredChar = match.Groups[3].ValueSpan[0];
                string password = match.Groups[4].Value;

                var charCounts = password.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                if (charCounts.TryGetValue(requiredChar, out int count) && lowerBound <= count && count <= upperBound)
                {
                    counter++;
                }
            }
            return counter;
        }

        public long Part2()
        {
            long counter = 0;
            foreach (var entry in entries)
            {
                var match = Regex.Match(entry, @"^(\d+)-(\d+) (.): (.+)$");
                if (!match.Success)
                {
                    throw new Exception("Bad format? " + entry);
                }
                int pos1 = int.Parse(match.Groups[1].ValueSpan);
                int pos2 = int.Parse(match.Groups[2].ValueSpan);
                char requiredChar = match.Groups[3].ValueSpan[0];
                string password = match.Groups[4].Value;

                bool pos1ok = password[pos1 - 1] == requiredChar;
                bool pos2ok = password[pos2 - 1] == requiredChar;
                if (pos1ok != pos2ok) // != on bools is XOR
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
