using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

// https://adventofcode.com/2023/day/1

namespace Aoc2023
{
    public class Day01 : IAocDay
    {
        string[] inputLines;
        public Day01(string input)
        {
            inputLines = input.TrimEnd().Split(Environment.NewLine);
        }

        public long Part1()
        {
            int sum = 0;
            foreach (var line in inputLines)
            {
                sum += 10 * (line.First(char.IsDigit) - '0') + (line.Last(char.IsDigit) - '0');
            }
            return sum;
        }
        public long Part2()
        {
            const string DIGIT_PATTERN = @"one|two|three|four|five|six|seven|eight|nine|\d";
            int sum = 0;
            foreach (var line in inputLines)
            {
                var first = Regex.Match(line, DIGIT_PATTERN);
                var last = Regex.Match(line, DIGIT_PATTERN, RegexOptions.RightToLeft);
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
