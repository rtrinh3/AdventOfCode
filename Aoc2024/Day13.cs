using AocCommon;
using MathNet.Numerics.LinearAlgebra;
using System.Text.RegularExpressions;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/13
    // --- Day 13: Claw Contraption ---
    public class Day13(string input) : IAocDay
    {
        public string Part1()
        {
            var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            double counter = 0;
            foreach (var p in paragraphs)
            {
                var m = Regex.Match(p, @"^Button A: X\+(\d+), Y\+(\d+)\nButton B: X\+(\d+), Y\+(\d+)\nPrize: X=(\d+), Y=(\d+)$");
                double[] ba = [double.Parse(m.Groups[1].ValueSpan), double.Parse(m.Groups[2].ValueSpan)];
                double[] bb = [double.Parse(m.Groups[3].ValueSpan), double.Parse(m.Groups[4].ValueSpan)];
                double tx = double.Parse(m.Groups[5].ValueSpan);
                double ty = double.Parse(m.Groups[6].ValueSpan);
                Matrix<double> eqs = Matrix<double>.Build.DenseOfColumnArrays(ba, bb);
                Vector<double> ts = Vector<double>.Build.Dense([tx, ty]);
                var solve = eqs.Solve(ts);
                var round = solve.Select(x => Math.Round(x, 3)).ToList();
                if (round.All(x => x != 0 && x % 1 == 0))
                {
                    counter += round[0] * 3 + round[1] * 1;
                }
            }

            return counter.ToString();
        }

        public string Part2()
        {
            var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            double counter = 0;
            foreach (var p in paragraphs)
            {
                var m = Regex.Match(p, @"^Button A: X\+(\d+), Y\+(\d+)\nButton B: X\+(\d+), Y\+(\d+)\nPrize: X=(\d+), Y=(\d+)$");
                double[] ba = [double.Parse(m.Groups[1].ValueSpan), double.Parse(m.Groups[2].ValueSpan)];
                double[] bb = [double.Parse(m.Groups[3].ValueSpan), double.Parse(m.Groups[4].ValueSpan)];
                double tx = double.Parse(m.Groups[5].ValueSpan) + 10000000000000;
                double ty = double.Parse(m.Groups[6].ValueSpan) + 10000000000000;
                Matrix<double> eqs = Matrix<double>.Build.DenseOfColumnArrays(ba, bb);
                Vector<double> ts = Vector<double>.Build.Dense([tx, ty]);
                var solve = eqs.Solve(ts);
                var round = solve.Select(x => Math.Round(x, 3)).ToList();
                if (round.All(x => x != 0 && x % 1 == 0))
                {
                    counter += round[0] * 3 + round[1] * 1;
                }
            }

            return counter.ToString();
        }
    }
}
