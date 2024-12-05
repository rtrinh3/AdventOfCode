using System.Diagnostics;
using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/5
    // --- Day 5: Print Queue ---
    public class Day05(string input) : IAocDay
    {
        public string Part1()
        {
            // Parse
            var paragraphs = input.ReplaceLineEndings("\n").TrimEnd().Split("\n\n");
            var orderLines = paragraphs[0].Split('\n');
            Dictionary<long, HashSet<long>> beforeAfter = orderLines
                .Select(line => line.Split('|').Select(long.Parse).ToArray())
                .GroupBy(line => line[0])
                .ToDictionary(g => g.Key, g => g.Select(line => line[1]).ToHashSet());

            var printLines = paragraphs[1].Split('\n');
            long[][] prints = printLines.Select(line => line.Split(',').Select(long.Parse).ToArray()).ToArray();

            //
            List<long[]> correctPrints = new();
            foreach (var print in prints)
            {
                for (int i = 0; i < print.Length - 1; i++) 
                {
                    for (int j = i + 1; j < print.Length; j++)
                    {
                        if (beforeAfter.ContainsKey(print[j]) && beforeAfter[print[j]].Contains(print[i])) 
                        {
                            goto INCORRECT;
                        }
                    }
                }
                correctPrints.Add(print);
                INCORRECT:;
            }
            long sum = 0;
            foreach (var print in correctPrints)
            {
                Debug.Assert(print.Length % 2 == 1);
                var middle = print[print.Length / 2];
                sum += middle;
            }

            return sum.ToString();
        }

        public string Part2()
        {
            return "NO";
        }
    }
}
