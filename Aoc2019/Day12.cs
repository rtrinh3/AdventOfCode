using AocCommon;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/12
    public class Day12(string input) : IAocDay
    {
        private readonly VectorXYZ[] directions = [
            new(1, 0, 0),
            new(0, 1, 0),
            new(0, 0, 1),
        ];

        private readonly VectorXYZ[] moonInitialPositions = input.TrimEnd().Split('\n').Select(line => {
            var match = System.Text.RegularExpressions.Regex.Match(line, @"<x=(-?\d+), y=(-?\d+), z=(-?\d+)>");
            return new VectorXYZ(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
        }).ToArray();

        public string Part1()
        {
            return DoPart1(1000).ToString();
        }

        public int DoPart1(int timespan)
        {
            VectorXYZ[] moonPositions = moonInitialPositions.ToArray();
            VectorXYZ[] moonVelocities = Enumerable.Repeat(VectorXYZ.Zero, moonPositions.Length).ToArray();
            for (int time = 0; time < timespan; time++)
            {
                // Gravity
                for (int i = 0; i < moonPositions.Length; i++)
                {
                    for (int j = i; j < moonPositions.Length; j++)
                    {
                        foreach (var axis in directions)
                        {
                            int moonIPos = moonPositions[i].Dot(axis);
                            int moonJPos = moonPositions[j].Dot(axis);
                            if (moonIPos < moonJPos)
                            {
                                moonVelocities[i] += axis;
                                moonVelocities[j] -= axis;
                            }
                            else if (moonJPos < moonIPos)
                            {
                                moonVelocities[i] -= axis;
                                moonVelocities[j] += axis;
                            }
                            // Else nothing
                        }
                    }
                }
                // Velocity
                for (int i = 0; i < moonPositions.Length; i++)
                {
                    moonPositions[i] += moonVelocities[i];
                }
            }
            // Energy
            var potential = moonPositions.Select(p => Math.Abs(p.X) + Math.Abs(p.Y) + Math.Abs(p.Z));
            var kinetic = moonVelocities.Select(v => Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z));
            var total = potential.Zip(kinetic, (p, k) => p * k).Sum();
            return total;
        }

        // Part 2
        // Per https://www.reddit.com/r/adventofcode/comments/e9j0ve/2019_day_12_solutions/ ,
        // calculate period of each axis and take the LCM of all the periods
        private (long, long) FindPeriod(VectorXYZ axis)
        {
            VectorXYZ[] moonPositions = moonInitialPositions.ToArray();
            VectorXYZ[] moonVelocities = Enumerable.Repeat(VectorXYZ.Zero, moonPositions.Length).ToArray();
            Dictionary<string, long> states = new();
            string SerializeState() => string.Join("", moonPositions.Concat(moonVelocities));
            long time = 0;
            while (true)
            {
                string state = SerializeState();
                if (states.TryGetValue(state, out long seen))
                {
                    return (time, seen);
                }
                else
                {
                    states.Add(state, time);
                }
                // Gravity
                for (int i = 0; i < moonPositions.Length; i++)
                {
                    for (int j = i; j < moonPositions.Length; j++)
                    {
                        int moonIPos = moonPositions[i].Dot(axis);
                        int moonJPos = moonPositions[j].Dot(axis);
                        if (moonIPos < moonJPos)
                        {
                            moonVelocities[i] += axis;
                            moonVelocities[j] -= axis;
                        }
                        else if (moonJPos < moonIPos)
                        {
                            moonVelocities[i] -= axis;
                            moonVelocities[j] += axis;
                        }
                        // Else nothing
                    }
                }
                // Velocity
                for (int i = 0; i < moonPositions.Length; i++)
                {
                    moonPositions[i] += moonVelocities[i];
                }
                time++;
            }
        }

        public string Part2()
        {
            var periods = directions.Select(FindPeriod).Select(t => t.Item1);
            var commonPeriod = periods.Aggregate((a, b) => MoreMath.Lcm(a, b));
            return commonPeriod.ToString();
        }
    }
}
