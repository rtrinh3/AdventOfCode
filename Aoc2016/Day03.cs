using AocCommon;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/3
// --- Day 3: Squares With Three Sides ---
public class Day03(string input): IAocDay
{
    private readonly int[][] lines = Parsing.SplitLines(input).Select(Parsing.IntsPositive).ToArray();

    public string Part1()
    {
        int triangles = 0;
        foreach (var line in lines)
        {
            if (IsTriangle(line))
            {
                triangles++;
            }
        }
        return triangles.ToString();
    }

    public string Part2()
    {
        Debug.Assert(lines.Length % 3 == 0);
        var trios = lines.Select(x => x[0])
            .Concat(lines.Select(x => x[1]))
            .Concat(lines.Select(x => x[2]))
            .Chunk(3);
        int triangles = 0;
        foreach (var trio in trios)
        {
            if (IsTriangle(trio))
            {
                triangles++;
            }
        }
        return triangles.ToString();
    }

    private static bool IsTriangle(int[] trio)
    {
        Debug.Assert(trio.Length == 3);
        return (trio[0] + trio[1] > trio[2]) && (trio[0] + trio[2] > trio[1]) && (trio[1] + trio[2] > trio[0]);
    }
}
