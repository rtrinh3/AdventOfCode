using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

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
                double[] numbers = line.Split('@').SelectMany(s => s.Split(',')).Select(s => double.Parse(s)).ToArray();
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
                    if (coefficients.Determinant() != 0)
                    {
                        var constants = b.position - a.position;
                        var solvedTimes = coefficients.Solve(constants);
                        if (solvedTimes.All(t => t >= 0))
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
            }
            return intersections;
        }

        public long Part2()
        {
            return -2;
        }
    }
}
