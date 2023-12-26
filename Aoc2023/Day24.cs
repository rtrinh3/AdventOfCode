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
            List<int> hailstoneIndices = new();
            while (hailstoneIndices.Count < 3)
            {
                int index = Random.Shared.Next(hailstones.Length);
                if (!hailstoneIndices.Contains(index))
                {
                    hailstoneIndices.Add(index);
                }
            }
            var hailA = hailstoneVectors[hailstoneIndices[0]];
            var hailB = hailstoneVectors[hailstoneIndices[1]];
            var hailC = hailstoneVectors[hailstoneIndices[2]];
            var coefficients = Matrix<double>.Build.Dense(6, 6);
            var constants = Vector<double>.Build.Dense(6);
            coefficients[0, 0] = hailA.velocity[1] - hailB.velocity[1];
            coefficients[0, 1] = hailB.velocity[0] - hailA.velocity[0];
            coefficients[0, 3] = hailB.position[1] - hailA.position[1];
            coefficients[0, 4] = hailA.position[0] - hailB.position[0];
            constants[0] = hailB.position[1] * hailB.velocity[0] - hailB.position[0] * hailB.velocity[1] - hailA.position[1] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[1];
            coefficients[1, 0] = hailA.velocity[2] - hailB.velocity[2];
            coefficients[1, 2] = hailB.velocity[0] - hailA.velocity[0];
            coefficients[1, 3] = hailB.position[2] - hailA.position[2];
            coefficients[1, 5] = hailA.position[0] - hailB.position[0];
            constants[1] = hailB.position[2] * hailB.velocity[0] - hailB.position[0] * hailB.velocity[2] - hailA.position[2] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[2];
            coefficients[2, 1] = hailA.velocity[2] - hailB.velocity[2];
            coefficients[2, 2] = hailB.velocity[1] - hailA.velocity[1];
            coefficients[2, 4] = hailB.position[2] - hailA.position[2];
            coefficients[2, 5] = hailA.position[1] - hailB.position[1];
            constants[2] = hailB.position[2] * hailB.velocity[1] - hailB.position[1] * hailB.velocity[2] - hailA.position[2] * hailA.velocity[1] + hailA.position[1] * hailA.velocity[2];
            coefficients[3, 0] = hailA.velocity[1] - hailC.velocity[1];
            coefficients[3, 1] = hailC.velocity[0] - hailA.velocity[0];
            coefficients[3, 3] = hailC.position[1] - hailA.position[1];
            coefficients[3, 4] = hailA.position[0] - hailC.position[0];
            constants[3] = hailC.position[1] * hailC.velocity[0] - hailC.position[0] * hailC.velocity[1] - hailA.position[1] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[1];
            coefficients[4, 0] = hailA.velocity[2] - hailC.velocity[2];
            coefficients[4, 2] = hailC.velocity[0] - hailA.velocity[0];
            coefficients[4, 3] = hailC.position[2] - hailA.position[2];
            coefficients[4, 5] = hailA.position[0] - hailC.position[0];
            constants[4] = hailC.position[2] * hailC.velocity[0] - hailC.position[0] * hailC.velocity[2] - hailA.position[2] * hailA.velocity[0] + hailA.position[0] * hailA.velocity[2];
            coefficients[5, 1] = hailA.velocity[2] - hailC.velocity[2];
            coefficients[5, 2] = hailC.velocity[1] - hailA.velocity[1];
            coefficients[5, 4] = hailC.position[2] - hailA.position[2];
            coefficients[5, 5] = hailA.position[1] - hailC.position[1];
            constants[5] = hailC.position[2] * hailC.velocity[1] - hailC.position[1] * hailC.velocity[2] - hailA.position[2] * hailA.velocity[1] + hailA.position[1] * hailA.velocity[2];

            var solution = coefficients.Solve(constants);
            var coords = solution.Take(3).Select(x => (long)Math.Round(x)).ToList();
            return coords.Sum();
        }
    }
}
