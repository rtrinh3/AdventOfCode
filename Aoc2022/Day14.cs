using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/14
    public class Day14 : IAocDay
    {
        private static readonly VectorXY[] fallDirections = { new(0, +1), new(-1, +1), new(+1, +1) };
        private readonly HashSet<VectorXY> initial = new();
        private readonly Lazy<(int answer, HashSet<VectorXY> filled)> partOneCalculations;

        public Day14(string input)
        {
            foreach (string line in Constants.SplitLines(input))
            {
                var matches = Regex.Matches(line, @"((\d+),(\d+))");
                for (int i = 0; i < matches.Count - 1; ++i)
                {
                    var left = matches[i];
                    int leftX = int.Parse(left.Groups[2].Value);
                    int leftY = int.Parse(left.Groups[3].Value);
                    var right = matches[i + 1];
                    int rightX = int.Parse(right.Groups[2].Value);
                    int rightY = int.Parse(right.Groups[3].Value);
                    for (int x = Math.Min(leftX, rightX); x <= Math.Max(leftX, rightX); ++x)
                    {
                        for (int y = Math.Min(leftY, rightY); y <= Math.Max(leftY, rightY); ++y)
                        {
                            initial.Add(new VectorXY(x, y));
                        }
                    }
                }
            }
            partOneCalculations = new(DoPart1);
        }

        public string Part1()
        {
            var (answer, _) = partOneCalculations.Value;
            return answer.ToString();
        }

        private (int answer, HashSet<VectorXY> filled) DoPart1()
        {
            int bottom = initial.Select(p => p.Y).Max();
            HashSet<VectorXY> filled = new HashSet<VectorXY>(initial);
            while (true)
            {
                VectorXY sand = new VectorXY(500, 0);
                while (true)
                {
                    bool moved = false;
                    foreach (var dir in fallDirections)
                    {
                        var candidate = sand + dir;
                        if (!filled.Contains(candidate))
                        {
                            sand = candidate;
                            moved = true;
                            break;
                        }
                    }
                    if (!moved)
                    {
                        filled.Add(sand);
                        break;
                    }
                    else if (sand.Y > bottom)
                    {
                        goto GET_OUT;
                    }
                }
            }
        GET_OUT:
            var answer = (filled.Count - initial.Count);
            return (answer, filled);
        }

        public string Part2()
        {
            HashSet<VectorXY> partTwoInitial = new HashSet<VectorXY>(initial);
            var (_, filled) = partOneCalculations.Value;
            int floor = initial.Select(p => p.Y).Max() + 2;
            int minX = filled.Select(p => p.X).Min();
            int maxX = filled.Select(p => p.X).Max();
            for (int x = minX - floor; x <= maxX + floor; ++x)
            {
                partTwoInitial.Add(new(x, floor));
            }
            HashSet<VectorXY> partTwoFilled = new HashSet<VectorXY>(partTwoInitial);
            VectorXY spawn = new VectorXY(500, 0);
            while (!partTwoFilled.Contains(spawn))
            {
                VectorXY sand = spawn;
                while (true)
                {
                    bool moved = false;
                    foreach (var dir in fallDirections)
                    {
                        var candidate = sand + dir;
                        if (!partTwoFilled.Contains(candidate))
                        {
                            sand = candidate;
                            moved = true;
                            break;
                        }
                    }
                    if (!moved)
                    {
                        partTwoFilled.Add(sand);
                        break;
                    }
                }
            }
            var answer = (partTwoFilled.Count - partTwoInitial.Count);
            return answer.ToString();
        }
    }
}
