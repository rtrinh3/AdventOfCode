using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2022
{
    public class Day15 : IAocDay
    {
        private List<VectorXY> sensors = new List<VectorXY>();
        private HashSet<VectorXY> beacons = new HashSet<VectorXY>();
        private List<int> distances = new List<int>();
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;
        public Day15(string input)
        {
            foreach (string line in Constants.SplitLines(input))
            {
                var match = Regex.Match(line, @"Sensor at x=(.*), y=(.*): closest beacon is at x=(.*), y=(.*)");
                var sensor = new VectorXY(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                var beacon = new VectorXY(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                int distance = (beacon - sensor).ManhattanMetric();
                sensors.Add(sensor);
                beacons.Add(beacon);
                distances.Add(distance);
            }
            minX = Math.Min(sensors.Select(s => s.X).Min(), beacons.Select(b => b.X).Min());
            maxX = Math.Max(sensors.Select(s => s.X).Max(), beacons.Select(b => b.X).Max());
            minY = Math.Min(sensors.Select(s => s.Y).Min(), beacons.Select(b => b.Y).Min());
            maxY = Math.Max(sensors.Select(s => s.Y).Max(), beacons.Select(b => b.Y).Max());
        }

        public string Part1()
        {
            return DoPart1(2000000).ToString();
        }
        public long DoPart1(int row)
        {
            int margin = distances.Max();
            int noBeacon = 0;
            Parallel.For(minX - margin, maxX + margin + 1, x =>
            {
                VectorXY coords = new VectorXY(x, row);
                if (!beacons.Contains(coords))
                {
                    for (int i = 0; i < sensors.Count; ++i)
                    {
                        int distance = (coords - sensors[i]).ManhattanMetric();
                        if (distance <= distances[i])
                        {
                            Interlocked.Increment(ref noBeacon);
                            break;
                        }
                    }
                }
            });
            return noBeacon;
        }
        public string Part2()
        {
            return DoPart2(4000000, 4000000).ToString();
        }
        public long DoPart2(int upperX, int upperY)
        {
            int SignalStrength(VectorXY coords)
            {
                int sum = 0;
                for (int i = 0; i < sensors.Count; ++i)
                {
                    int distance = (coords - sensors[i]).ManhattanMetric();
                    int remainingDistance = distances[i] - distance;
                    if (remainingDistance >= 0)
                    {
                        sum += remainingDistance + 1;
                    }
                }
                return sum;
            }
            VectorXY emptySpot = new();

            // Perimeter walk solution
            VectorXY[] flips = {
                new (+1, +1),
                new (+1, -1),
                new (-1, -1),
                new (-1, +1),
            };
            var result = Parallel.For(0, sensors.Count, (i, state) =>
            {
                var center = sensors[i];
                int distance = 1 + distances[i];
                for (int a = 0; a <= distance && !state.ShouldExitCurrentIteration; ++a)
                {
                    VectorXY displacement = new VectorXY(a, distance - a);
                    foreach (var flip in flips)
                    {
                        VectorXY coords = center + new VectorXY(flip.X * displacement.X, flip.Y * displacement.Y); // flip.PairwiseMultiply(displacement);
                        if (0 <= coords.X && coords.X <= upperX && 0 <= coords.Y && coords.Y <= upperY && SignalStrength(coords) == 0)
                        {
                            emptySpot = coords;
                            state.Stop();
                        }
                    }
                }
            });
            long tuning = (long)emptySpot.X * 4000000L + (long)emptySpot.Y;
            return tuning;
        }
    }
}
