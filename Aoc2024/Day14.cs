using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024
{
    public class Day14(string input) : IAocDay
    {
        private readonly (VectorXY Position, VectorXY Velocity)[] guards = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(line =>
        {
            var numbers = Regex.Matches(line, @"-?\d+");
            Debug.Assert(numbers.Count == 4);
            var parsed = numbers.Select(s => int.Parse(s.ValueSpan)).ToList();
            return (new VectorXY(parsed[0], parsed[1]), new VectorXY(parsed[2], parsed[3]));
        }).ToArray();

        public string Part1()
        {
            var answer = DoPart1(101, 103);
            return answer.ToString();
        }

        public long DoPart1(int width, int height)
        {
            const int time = 100;
            int[,] quadrants = new int[2, 2]; // left-right, top-bottom
            foreach (var g in guards)
            {
                var newX = (g.Position.X + g.Velocity.X * time) % width;
                if (newX < 0) newX += width;
                int leftRight = newX < width / 2 ? 0 : newX > width / 2 ? 1 : -1;

                var newY = (g.Position.Y + g.Velocity.Y * time) % height;
                if (newY < 0) newY += height;
                int topBottom = newY < height / 2 ? 0 : newY > height / 2 ? 1 : -1;

                if (leftRight >= 0 && topBottom >= 0)
                {
                    quadrants[leftRight,topBottom]++;
                }
            }
            long answer = (long)quadrants[0, 0] * quadrants[0, 1] * quadrants[1, 0] * quadrants[1, 1];
            return answer;
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
