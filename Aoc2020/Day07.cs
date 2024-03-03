using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/7
    // --- Day 7: Handy Haversacks ---
    public class Day07(string input): IAocDay
    {
        private const string TARGET = "shiny gold";
        private readonly string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

        public long Part1()
        {
            DefaultDict<string, List<string>> ContainedMap = new();
            foreach (string line in lines)
            {
                string[] firstSplit = line.Split("contain");
                string subjectColor = Regex.Match(firstSplit[0], @"^(.+) bag").Groups[1].Value;
                var containedMatches = Regex.Matches(firstSplit[1], @"(\d+) ([a-z ]+) bag");
                foreach (var m in containedMatches.AsEnumerable())
                {
                    var quantity = m.Groups[1].Value;
                    var contained = m.Groups[2].Value;
                    ContainedMap[contained].Add(subjectColor);
                }
            }

            HashSet<string> visited = new();
            void Visit(string color)
            {
                if (visited.Add(color))
                {
                    foreach (var next in ContainedMap[color])
                    {
                        Visit(next);
                    }
                }
            }
            Visit(TARGET);
            visited.Remove(TARGET);
            var answer = visited.Count;
            return answer;
        }

        public long Part2()
        {
            DefaultDict<string, List<(long quantity, string color)>> ContainsMap = new();
            foreach (string line in lines)
            {
                string[] firstSplit = line.Split("contain");
                string subjectColor = Regex.Match(firstSplit[0], @"^(.+) bag").Groups[1].Value;
                var containedMatches = Regex.Matches(firstSplit[1], @"(\d+) ([a-z ]+) bag");
                foreach (var m in containedMatches.AsEnumerable())
                {
                    var quantity = long.Parse(m.Groups[1].ValueSpan);
                    var contained = m.Groups[2].Value;
                    ContainsMap[subjectColor].Add((quantity, contained));
                }
            }

            Func<string, long> numberOfBagsInColor = _ => throw new NotImplementedException();
            numberOfBagsInColor = Memoization.Make((string color) =>
            {
                long sum = 0;
                foreach (var criterion in ContainsMap[color])
                {
                    sum += criterion.quantity;
                    sum += criterion.quantity * numberOfBagsInColor(criterion.color);
                }
                return sum;
            });

            var answer = numberOfBagsInColor(TARGET);
            return answer;
        }
    }
}
