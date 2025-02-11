using AocCommon;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/6
// --- Day 6: Signals and Noise ---
public class Day06 : IAocDay
{
    private readonly string[] lines;
    private readonly int length;

    public Day06(string input)
    {
        lines = Parsing.SplitLines(input);
        length = lines[0].Length;
        Debug.Assert(lines.All(l => l.Length == length));
    }

    public string Part1()
    {
        char[] code = new char[length];
        for (int i = 0; i < length; i++)
        {
            char mostFrequent = lines
                .Select(l => l[i])
                .GroupBy(c => c)
                .Select(c => (c.Key, Count: c.Count()))
                .OrderByDescending(c => c.Count)
                .Select(c => c.Key)
                .First();
            code[i] = mostFrequent;
        }
        var answer = string.Join("", code);
        return answer;
    }

    public string Part2()
    {
        char[] code = new char[length];
        for (int i = 0; i < length; i++)
        {
            char mostFrequent = lines
                .Select(l => l[i])
                .GroupBy(c => c)
                .Select(c => (c.Key, Count: c.Count()))
                .OrderBy(c => c.Count)
                .Select(c => c.Key)
                .First();
            code[i] = mostFrequent;
        }
        var answer = string.Join("", code);
        return answer;
    }
}
