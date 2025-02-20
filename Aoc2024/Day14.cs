﻿using AocCommon;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/14
    // --- Day 14: Restroom Redoubt ---
    public class Day14(string input) : IAocDay
    {
        private const int WIDTH = 101;
        private const int HEIGHT = 103;

        private readonly (VectorXY Position, VectorXY Velocity)[] robots = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(line =>
        {
            var numbers = Regex.Matches(line, @"-?\d+");
            Debug.Assert(numbers.Count == 4);
            var parsed = numbers.Select(s => int.Parse(s.ValueSpan)).ToList();
            return (new VectorXY(parsed[0], parsed[1]), new VectorXY(parsed[2], parsed[3]));
        }).ToArray();

        public string Part1()
        {
            var answer = DoPart1(WIDTH, HEIGHT);
            return answer.ToString();
        }

        public long DoPart1(int width, int height)
        {
            const int time = 100;
            int[,] quadrants = new int[2, 2]; // left-right, top-bottom
            var simulation = Simulate(width, height, time);
            foreach (var g in simulation)
            {
                int leftRight = g.X < width / 2 ? 0 : g.X > width / 2 ? 1 : -1;
                int topBottom = g.Y < height / 2 ? 0 : g.Y > height / 2 ? 1 : -1;

                if (leftRight >= 0 && topBottom >= 0)
                {
                    quadrants[leftRight, topBottom]++;
                }
            }
            long answer = (long)quadrants[0, 0] * quadrants[0, 1] * quadrants[1, 0] * quadrants[1, 1];
            return answer;
        }

        private VectorXY[] Simulate(int width, int height, int time)
        {
            List<VectorXY> newPositions = new();
            foreach (var r in robots)
            {
                var newX = (r.Position.X + r.Velocity.X * time) % width;
                if (newX < 0) newX += width;
                var newY = (r.Position.Y + r.Velocity.Y * time) % height;
                if (newY < 0) newY += height;
                newPositions.Add(new VectorXY(newX, newY));
            }
            return newPositions.ToArray();
        }

        public string Part2()
        {
            //var stopwatch = Stopwatch.StartNew();
            //int time = 0;
            static int Linearize(VectorXY v) => ((v.X & 0xFF) << 8) | (v.Y & 0xFF);
            int candidateTime = int.MaxValue;
            Parallel.For(0, HEIGHT * WIDTH, (time, loopState) =>
            {
                var originalSimulation = Simulate(WIDTH, HEIGHT, time);
                HashSet<int> simulation = originalSimulation.Select(Linearize).ToHashSet();
                UnionFindInt regions = new();
                IEnumerable<int> allPositions = Enumerable.Range(0, HEIGHT).SelectMany(y => Enumerable.Range(0, WIDTH).Select(x => (x << 8) | y));
                foreach (var pos in allPositions)
                {
                    bool isRobot = simulation.Contains(pos);
                    Span<int> nextFour = [pos + 1, pos - 1, pos + 0xFF, pos - 0xFF];
                    foreach (var next in nextFour)
                    {
                        if (next >= 0 && isRobot == simulation.Contains(next))
                        {
                            regions.Union(pos, next);
                        }
                    }
                }
                var regionGroups = allPositions.GroupBy(regions.Find).Select(g => (g.Key, g.Count())).ToList();
                if (regionGroups.Count(g => g.Item2 > 10) > 2)
                {
                    lock (this)
                    {
                        candidateTime = Math.Min(time, candidateTime);
                    }
                    Console.WriteLine("Found");
                    //Visualize(time, originalSimulation.ToHashSet());
                    loopState.Break();
                }
            });
            return candidateTime.ToString();
        }

        private static void Visualize(int time, ISet<VectorXY> robots)
        {
            StringBuilder[] rows = new StringBuilder[HEIGHT/2+1];
            for (int y = 0; y < HEIGHT; y+=2)
            {
                StringBuilder row = rows[y/2] = new StringBuilder();
                for (int x = 0; x < WIDTH; x++)
                {
                    bool up = robots.Contains(new VectorXY(x, y));
                    bool down = robots.Contains(new VectorXY(x, y+1));
                    int index = (up ? 2 : 0) + (down ? 1 : 0);
                    char[] chars = [' ', '▄', '▀', '█'];
                    char newChar = chars[index];
                    row.Append(newChar);
                }
            }

            Console.WriteLine(time);
            string serialized = string.Join('\n', rows.AsEnumerable());
            Console.WriteLine(serialized);
            Console.WriteLine(time);
            //Thread.Sleep(20);
        }
    }
}
