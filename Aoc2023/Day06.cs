using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/6
    public class Day06 : IAocDay
    {
        private readonly string[] times;
        private readonly string[] distances;

        public Day06(string input)
        {
            string[] lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
            times = Regex.Matches(lines.First(line => line.StartsWith("Time")), @"\d+").Select(m => m.Value).ToArray();
            distances = Regex.Matches(lines.First(line => line.StartsWith("Distance")), @"\d+").Select(m => m.Value).ToArray();
        }

        public long Part1()
        {
            return times.Zip(distances, (t, d) => SolveRace(double.Parse(t), double.Parse(d)))
                .Aggregate(1L, (a, b) => a * b);
        }

        public long Part2()
        {
            return SolveRace(double.Parse(string.Join("", times)), double.Parse(string.Join("", distances)));
        }

        private static long SolveRace(double time, double distance)
        {
            var discriminant = Math.Sqrt(time * time - 4 * distance);
            var low = Math.Floor((time - discriminant) / 2) + 1;
            var high = Math.Ceiling((time + discriminant) / 2) - 1;
            return (long)(high - low + 1);
        }
    }
}
