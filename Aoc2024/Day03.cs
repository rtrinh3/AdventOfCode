using System.Text.RegularExpressions;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/3
    // --- Day 3: Mull It Over ---
    public class Day03(string input) : AocCommon.IAocDay
    {
        public string Part1()
        {
            var instructions = Regex.Matches(input, @"mul\((\d+),(\d+)\)");

            long acc = 0;
            foreach (Match m in instructions)
            {
                acc += long.Parse(m.Groups[1].ValueSpan) * long.Parse(m.Groups[2].ValueSpan);
            }
            return acc.ToString();
        }

        public string Part2()
        {
            var instructions = Regex.Matches(input, @"don't|do|mul\((\d+),(\d+)\)");

            long acc = 0;
            bool enabled = true;
            foreach (Match m in instructions)
            {
                if (m.Value == "don't")
                {
                    enabled = false;
                }
                else if (m.Value == "do")
                {
                    enabled = true;
                }
                else
                {
                    if (enabled)
                    {
                        acc += long.Parse(m.Groups[1].ValueSpan) * long.Parse(m.Groups[2].ValueSpan);
                    }
                }

            }
            return acc.ToString();
        }
    }
}
