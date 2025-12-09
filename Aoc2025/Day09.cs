using AocCommon;

namespace Aoc2025;

// https://adventofcode.com/2025/day/9
// --- Day 9: Movie Theater ---
public class Day09(string input): IAocDay
{
    public string Part1()
    {
        // Parse
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        List<VectorRC> points = new();
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var point = new VectorRC(int.Parse(parts[0]), int.Parse(parts[1]));
            points.Add(point);
        }

        // Find largest rectangle
        long maxRectangle = 0;
        for (int a = 0; a < points.Count; a++)
        {
            for (int b = a + 1; b < points.Count; b++)
            {
                int height = Math.Abs(points[b].Row - points[a].Row) + 1;
                int width = Math.Abs(points[b].Col - points[a].Col) + 1;
                long area = Math.BigMul(height, width);
                if (area > maxRectangle)
                {
                    maxRectangle = area;
                }
            }
        }
        return maxRectangle.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}