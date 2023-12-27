using AocCommon;
using System.Numerics;
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
            for (int x = minX - margin; x <= maxX + margin; ++x)
            {
                VectorXY coords = new VectorXY(x, row);
                if (beacons.Contains(coords))
                {
                    //Console.Write("B");
                }
                else
                {
                    for (int i = 0; i < sensors.Count; ++i)
                    {
                        int distance = (coords - sensors[i]).ManhattanMetric();
                        if (distance <= distances[i])
                        {
                            ++noBeacon;
                            //Console.Write("#");
                            goto FOUND;
                        }
                    }
                //Console.Write(".");
                FOUND:
                    ;
                }
            }
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
            for (int i = 0; i < sensors.Count; ++i)
            {
                var center = sensors[i];
                int distance = 1 + distances[i];
                for (int a = 0; a <= distance; ++a)
                {
                    VectorXY displacement = new VectorXY(a, distance - a);
                    foreach (var flip in flips)
                    {
                        VectorXY coords = center + new VectorXY(flip.X * displacement.X, flip.Y * displacement.Y); // flip.PairwiseMultiply(displacement);
                        if (0 <= coords.X && coords.X <= upperX && 0 <= coords.Y && coords.Y <= upperY && SignalStrength(coords) == 0)
                        {
                            emptySpot = coords;
                            goto EMPTY_SPOT;
                        }
                    }
                }
            }

        EMPTY_SPOT:
            //emptySpot.Dump("Part 2 Coords");
            //SignalStrength(emptySpot).Dump("Part 2 Strength");
            long tuning = (long)emptySpot.X * (long)4000000 + (long)emptySpot.Y;
            //tuning.Dump("Part 2 Tuning");
            //hop.Dump("Hops");
            return tuning;
        }
    }
}
