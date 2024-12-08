using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/8
    // --- Day 8: Resonant Collinearity ---
    public class Day08 : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly Grid map;
        private readonly DefaultDict<char, List<VectorRC>> antennasByFrequency;

        public Day08(string input)
        {
            map = new Grid(input, OUTSIDE);
            antennasByFrequency = new();
            foreach (var (Position, Value) in map.Iterate())
            {
                if (char.IsAsciiLetterOrDigit(Value))
                {
                    antennasByFrequency[Value].Add(Position);
                }
            }
        }

        private bool InBounds(VectorRC x)
        {
            return 0 <= x.Row && x.Row < map.Height && 0 <= x.Col && x.Col < map.Width;
        }

        public string Part1()
        {
            HashSet<VectorRC> antinodes = new();
            foreach (var (_, Positions) in antennasByFrequency)
            {
                if (Positions.Count >= 2)
                {
                    for (int i = 0; i < Positions.Count - 1; i++)
                    {
                        for (int j = i + 1; j < Positions.Count; j++)
                        {
                            var difference = Positions[j] - Positions[i];
                            var antinodeA = Positions[j] + difference;
                            if (InBounds(antinodeA))
                            {
                                antinodes.Add(antinodeA);
                            }
                            var antinodeB = Positions[i] - difference;
                            if (InBounds(antinodeB))
                            {
                                antinodes.Add(antinodeB);
                            }
                        }
                    }
                }
            }
            return antinodes.Count.ToString();
        }

        public string Part2()
        {
            HashSet<VectorRC> antinodes = new();
            foreach (var (_, Positions) in antennasByFrequency)
            {
                if (Positions.Count >= 2)
                {
                    for (int i = 0; i < Positions.Count - 1; i++)
                    {
                        for (int j = i + 1; j < Positions.Count; j++)
                        {
                            var difference = Positions[j] - Positions[i];
                            var gcd = MoreMath.Gcd(Math.Abs(difference.Row), Math.Abs(difference.Col));
                            var slope = new VectorRC(difference.Row / gcd, difference.Col / gcd);
                            var positive = Positions[i];
                            while (InBounds(positive))
                            {
                                antinodes.Add(positive);
                                positive += slope;
                            }
                            var negative = Positions[i];
                            while (InBounds(negative))
                            {
                                antinodes.Add(negative);
                                negative -= slope;
                            }
                        }
                    }
                }
            }
            return antinodes.Count.ToString();
        }
    }
}
