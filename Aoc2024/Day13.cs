using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/13
    // --- Day 13: Claw Contraption ---
    public class Day13 : IAocDay
    {
        private readonly List<long[]> machines;
        public Day13(string input)
        {
            machines = new();
            var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            foreach (var p in paragraphs)
            {
                var matches = Regex.Matches(p, @"(\d+)");
                Debug.Assert(matches.Count == 6);
                var numbers = matches.Select(g => long.Parse(g.ValueSpan)).ToArray();
                machines.Add(numbers);
            }
        }

        private static long DoPuzzle(IEnumerable<long[]> machines)
        {
            long total = 0;
            foreach (var machine in machines)
            {
                Debug.Assert(machine.Length == 6);
                var xa = machine[0];
                var ya = machine[1];
                var xb = machine[2];
                var yb = machine[3];
                var xc = machine[4];
                var yc = machine[5];
                // https://en.wikipedia.org/wiki/Cramer%27s_rule#Explicit_formulas_for_small_systems
                var determinant = xa * yb - xb * ya;
                if (determinant == 0)
                {
                    throw new Exception("TODO handle collinear case");
                }
                var aNumerator = xc * yb - xb * yc;
                var a = Math.DivRem(aNumerator, determinant);
                if (a.Remainder != 0)
                {
                    continue;
                }
                var bNumerator = xa * yc - xc * ya;
                var b = Math.DivRem(bNumerator, determinant);
                if (b.Remainder != 0)
                {
                    continue;
                }
                long tokens = 3 * a.Quotient + b.Quotient;
                total += tokens;
            }
            return total;
        }

        public string Part1()
        {
            var answer = DoPuzzle(machines);
            return answer.ToString();
        }

        public string Part2()
        {
            var actualMachines = machines.Select(m =>
            {
                var copy = m.ToArray();
                copy[4] += 10000000000000;
                copy[5] += 10000000000000;
                return copy;
            });
            var answer = DoPuzzle(actualMachines);
            return answer.ToString();
        }
    }
}
