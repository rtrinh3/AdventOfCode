using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/1
// --- Day 1: No Time for a Taxicab ---
public class Day01(string input) : IAocDay
{
    private readonly string[] steps = Regex.Matches(input, @"L|R|(\d+)").Select(m => m.Value).ToArray();

    public string Part1()
    {
        var position = VectorRC.Zero;
        var direction = VectorRC.Up;
        foreach (var step in steps)
        {
            if (step == "L")
            {
                direction = direction.RotatedLeft();
            }
            else if (step == "R")
            {
                direction = direction.RotatedRight();
            }
            else
            {
                var distance = int.Parse(step);
                position += direction.Scale(distance);
            }
        }
        var answer = position.ManhattanMetric();
        return answer.ToString();
    }

    public string Part2()
    {
        var position = VectorRC.Zero;
        var direction = VectorRC.Up;
        HashSet<VectorRC> visited = [VectorRC.Zero];
        var doublePosition = VectorRC.Zero;
        foreach (var step in steps)
        {
            if (step == "L")
            {
                direction = direction.RotatedLeft();
            }
            else if (step == "R")
            {
                direction = direction.RotatedRight();
            }
            else
            {
                var distance = int.Parse(step);
                for (int i = 0; i < distance; i++)
                {
                    position += direction;
                    if (!visited.Add(position))
                    {
                        doublePosition = position;
                        goto ANSWER_FOUND;
                    }
                }
            }
        }
    ANSWER_FOUND:
        var answer = doublePosition.ManhattanMetric();
        return answer.ToString();
    }
}
