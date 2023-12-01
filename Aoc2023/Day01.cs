using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aoc2023
{
    public class Day01
    {
        string[] inputLines;
        public Day01(string input)
        {
            inputLines = input.TrimEnd().Split(Environment.NewLine);
        }

        public int Part1()
        {
            int sum = 0;
            foreach (var line in inputLines)
            {
                string cs = $"{line.First(char.IsDigit)}{line.Last(char.IsDigit)}";
                sum += int.Parse(cs);
            }
            return sum;
        }
        public int Part2()
        {
            int sum = 0;
            foreach (var line in inputLines)
            {
                var first = Regex.Match(line, "one|two|three|four|five|six|seven|eight|nine|[0-9]");
                var last = Regex.Match(line, "one|two|three|four|five|six|seven|eight|nine|[0-9]", RegexOptions.RightToLeft);
                sum += 10 * GetValue(first.Value) + GetValue(last.Value);
            }
            return sum;
        }

        private static int GetValue(string s)
        {
            return s switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                _ => int.Parse(s)
            };
        }
    }
}
