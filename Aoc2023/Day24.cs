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
            //return DoPart1(200000000000000, 400000000000000);
            return DoPart1(7, 27); // EXAMPLE
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
            double[] times = Enumerable.Range(0, hailstones.Length).Select(x => (double)x).ToArray();
            //double[] times = Enumerable.Range(0, hailstones.Length).Select(x => Random.Shared.NextDouble() * hailstoneVectors[x].position.L1Norm()).ToArray();
            //double[] times = [5, 3, 4, 6, 1];
            double[][] timesAxes = Enumerable.Range(0, hailstones.Length).Select(x => new double[3]).ToArray();
            double[] originParameters = new double[3];
            double[] speedParameters = new double[3];
            const int ITERATIONS = 1000;
            for (int iteration = 0; iteration < ITERATIONS; iteration++)
            {
                Console.WriteLine($"Iteration {iteration}");
                for (int axis = 0; axis < 3; axis++)
                {
                    double[] hailAtT = Enumerable.Range(0, hailstones.Length).Select(i =>
                    {
                        return hailstoneVectors[i].position[axis] + hailstoneVectors[i].velocity[axis] * times[i];
                    }).ToArray();
                    var (origin, speed) = Fit.Line(times, hailAtT);
                    originParameters[axis] = origin;
                    speedParameters[axis] = speed;
                    var goodness = GoodnessOfFit.RSquared(times.Select(x => origin + speed * x), hailAtT);
                    Console.WriteLine(goodness);
                    for (int i = 0; i < hailstones.Length; i++)
                    {
                        double actualIntersectionTime = (hailstoneVectors[i].position[axis] - origin) / (speed - hailstoneVectors[i].velocity[axis]);
                        timesAxes[i][axis] = actualIntersectionTime;
                    }
                }
                for (int i = 0; i < hailstones.Length; i++)
                {
                    Debug.Assert(timesAxes[i].Length == 3);
                    //times[i] = double.Max(0, double.Round(timesAxes[i].Where(x => x.IsFinite()).Average()));
                    var timeStats = timesAxes[i].Where(x => x.IsFinite()).MeanStandardDeviation();
                    times[i] = Normal.Sample(timeStats.Mean, timeStats.StandardDeviation);
                }
            }
            Console.WriteLine("Times:" + string.Join(",", times));
            Console.WriteLine("Origin: " + string.Join(",", originParameters));
            Console.WriteLine("Speed: " + string.Join(",", speedParameters));
            return (long)double.Round(originParameters.Sum());
        }
    }
}
