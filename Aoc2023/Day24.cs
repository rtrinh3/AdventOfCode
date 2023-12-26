using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using MathNet.Numerics.Statistics;

namespace Aoc2023
{
    public class Day24 : IAocDay
    {
        private readonly double[][] hailstones;
        public Day24(string input)
        {
            var lines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            hailstones = lines.Select(line =>
            {
                double[] numbers = line.Split('@', ',').Select(s => double.Parse(s)).ToArray();
                return numbers;
            }).ToArray();
        }

        public long Part1()
        {
            return DoPart1(200000000000000, 400000000000000);
        }
        public long DoPart1(double lowerBound, double upperBound)
        {
            (Vector<double> position, Vector<double> velocity)[] flatHailstones = hailstones.Select(h =>
            {
                var position = Vector<double>.Build.Dense(h[0..2]);
                var velocity = Vector<double>.Build.Dense(h[3..5]);
                Debug.Assert(position.Count == 2);
                Debug.Assert(velocity.Count == 2);
                return (position, velocity);
            }).ToArray();
            long intersections = 0;
            for (int i = 0; i < hailstones.Length; i++)
            {
                for (int j = i + 1; j < hailstones.Length; j++)
                {
                    var a = flatHailstones[i];
                    var b = flatHailstones[j];
                    var coefficients = Matrix<double>.Build.DenseOfColumnVectors(a.velocity, -b.velocity);
                    var constants = b.position - a.position;
                    var solvedTimes = coefficients.Solve(constants);
                    if (solvedTimes.All(t => t.IsFinite() && t >= 0))
                    {
                        var intersection = a.position + a.velocity * solvedTimes[0];
                        // should be equal to b.position + b.velocity * solvedTimes[1];
                        if (intersection.All(x => lowerBound <= x && x <= upperBound))
                        {
                            intersections++;
                        }
                    }
                }
            }
            return intersections;
        }

        public long Part2()
        {
            (Vector<double> position, Vector<double> velocity)[] hailstoneVectors = hailstones.Select(h =>
            {
                var position = Vector<double>.Build.Dense(h[0..3]);
                var velocity = Vector<double>.Build.Dense(h[3..6]);
                Debug.Assert(position.Count == 3);
                Debug.Assert(velocity.Count == 3);
                return (position, velocity);
            }).ToArray();
            // Inspired by https://reddit.com/r/adventofcode/comments/18q40he/2023_day_24_part_2_a_straightforward_nonsolver/
            // and this comment https://www.reddit.com/r/adventofcode/comments/18q40he/comment/kesv08n/
            List<double[]> coefficientList = new();
            List<double> constantList = new();
            for (int i = 0; i < hailstoneVectors.Length - 1; i++)
            {
                var hailA = hailstoneVectors[i];
                var hailB = hailstoneVectors[i + 1];
                var row0 = new double[6];
                row0[0] = hailA.velocity[1] - hailB.velocity[1];
                row0[1] = hailB.velocity[0] - hailA.velocity[0];
                row0[3] = hailB.position[1] - hailA.position[1];
                row0[4] = hailA.position[0] - hailB.position[0];
                coefficientList.Add(row0);
                constantList.Add(hailB.position[1] * hailB.velocity[0] - hailB.position[0] * hailB.velocity[1] - hailA.position[1] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[1]);
                var row1 = new double[6];
                row1[0] = hailA.velocity[2] - hailB.velocity[2];
                row1[2] = hailB.velocity[0] - hailA.velocity[0];
                row1[3] = hailB.position[2] - hailA.position[2];
                row1[5] = hailA.position[0] - hailB.position[0];
                coefficientList.Add(row1);
                constantList.Add(hailB.position[2] * hailB.velocity[0] - hailB.position[0] * hailB.velocity[2] - hailA.position[2] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[2]);
                var row2 = new double[6];
                row2[1] = hailA.velocity[2] - hailB.velocity[2];
                row2[2] = hailB.velocity[1] - hailA.velocity[1];
                row2[4] = hailB.position[2] - hailA.position[2];
                row2[5] = hailA.position[1] - hailB.position[1];
                coefficientList.Add(row2);
                constantList.Add(hailB.position[2] * hailB.velocity[1] - hailB.position[1] * hailB.velocity[2] - hailA.position[2] * hailA.velocity[1] + hailA.position[1] * hailA.velocity[2]);
            }
            var coefficients = Matrix<double>.Build.DenseOfRowArrays(coefficientList);
            var constants = Vector<double>.Build.DenseOfEnumerable(constantList);
            var solution = coefficients.Solve(constants);
            var coords = solution.Take(3).Select(x => (long)Math.Round(x)).ToList();
            return coords.Sum();
        }
    }
}
