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
        private long[][] hailstones;
        public Day24(string input)
        {
            // EXAMPLE
//            input = @"19, 13, 30 @ -2,  1, -2
//18, 19, 22 @ -1, -1, -2
//20, 25, 34 @ -2, -2, -4
//12, 31, 28 @ -1, -2, -1
//20, 19, 15 @  1, -5, -3
//";
            hailstones = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(line =>
            {
                long[] numbers = line.Split('@').SelectMany(s => s.Split(',')).Select(s => long.Parse(s)).ToArray();
                return numbers;
            }).ToArray();
        }

        private enum IntersectionType { INTERSECT, COINCIDE, NEVER };
        public long Part1()
        {
            (Vector<double> position, Vector<double> velocity)[] flatHailstones = hailstones.Select(h =>
            {
                var position = Vector<double>.Build.DenseOfEnumerable(h[0..2].Select(x => (double)x));
                var velocity = Vector<double>.Build.DenseOfEnumerable(h[3..5].Select(x => (double)x));
                Debug.Assert(position.Count == 2);
                Debug.Assert(velocity.Count == 2);
                return (position, velocity);
            }).ToArray();
            // EXAMPLE
            double lowerBound = 200000000000000; // 7;
            double upperBound = 400000000000000; // 27;
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
