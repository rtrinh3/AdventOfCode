using AocCommon;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2024
{
    public class Day14(string input) : IAocDay
    {
        private const int WIDTH = 101;
        private const int HEIGHT = 103;

        private readonly (VectorXY Position, VectorXY Velocity)[] guards = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(line =>
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
            foreach (var g in guards)
            {
                var newX = (g.Position.X + g.Velocity.X * time) % width;
                if (newX < 0) newX += width;
                var newY = (g.Position.Y + g.Velocity.Y * time) % height;
                if (newY < 0) newY += height;
                newPositions.Add(new VectorXY(newX, newY));
            }
            return newPositions.ToArray();
        }

        public string Part2()
        {
            var stopwatch = Stopwatch.StartNew();
            int time = 0;
            VectorXY guardsRegion = new VectorXY(-1, -1);
            int candidateTime = 0;
            while (true)
            {
                var simulation = Simulate(WIDTH, HEIGHT, time).ToHashSet();
                UnionFind<VectorXY> regions = new();
                IEnumerable<VectorXY> allPositions = Enumerable.Range(0, HEIGHT).SelectMany(y => Enumerable.Range(0, WIDTH).Select(x => new VectorXY(x, y)));
                foreach (var pos in allPositions)
                {
                    if (simulation.Contains(pos))
                    {
                        regions.Union(guardsRegion, pos);
                    }
                    else
                    {
                        foreach (var next in pos.NextFour())
                        {
                            if (!simulation.Contains(next))
                            {
                                regions.Union(pos, next);
                            }
                        }
                    }
                }
                var regionGroups = allPositions.GroupBy(regions.Find).Select(g => (g.Key, g.Count())).ToList();
                if (regionGroups.Count > 2 && regionGroups.All(g => g.Item2 > 10))
                {
                    candidateTime = time;
                    Console.WriteLine("Found");
                    Visualize(time, simulation);
                    break;
                }
                if (Console.KeyAvailable)
                {
                    Console.WriteLine("Interrupted");
                    Visualize(time, simulation);
                    break;
                }
                if ((time & 0x3ff) == 0)
                {
                    Console.WriteLine("Working...");
                    Visualize(time, simulation);
                    Console.WriteLine("Working... " + stopwatch.Elapsed);
                }

                time++;
            }
            return candidateTime.ToString();
        }

        private static void Visualize(int time, ISet<VectorXY> guards)
        {
            StringBuilder[] rows = new StringBuilder[HEIGHT];
            for (int y = 0; y < HEIGHT; y++)
            {
                StringBuilder row = rows[y] = new StringBuilder();
                for (int x = 0; x < WIDTH; x++)
                {
                    char newChar = guards.Contains(new VectorXY(x, y)) ? '█' : ' ';
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
