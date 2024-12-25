using AocCommon;
using System.Diagnostics;

namespace Aoc2024;

// https://adventofcode.com/2024/day/25
// --- Day 25: Code Chronicle ---
public class Day25(string input) : IAocDay
{
    public string Part1()
    {
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        int height = 0;
        List<int[]> locks = new();
        List<int[]> keys = new();
        foreach (var paragraph in paragraphs)
        {
            var lines = paragraph.Split('\n');
            if (height == 0)
            {
                height = lines.Length;
            }
            else
            {
                Debug.Assert(lines.Length == height);
            }
            Debug.Assert(lines.All(l => l.Length == lines[0].Length));
            int[] heights = new int[lines[0].Length];
            for (int col = 0; col < heights.Length; col++)
            {
                foreach (var line in lines)
                {
                    if (line[col] == '#')
                    {
                        heights[col]++;
                    }
                }
            }

            bool isHash = lines[0].Contains('#');
            bool isDot = lines[0].Contains('.');
            if (isHash && !isDot)
            {
                locks.Add(heights);
            }
            else if (isDot && !isHash)
            {
                keys.Add(heights);
            }
            else
            {
                throw new Exception("what is this\n" + paragraph);
            }
        }
        long answer = 0;
        foreach (var key in keys)
        {
            // "lock" is a reserved word
            foreach (var ll in locks)
            {
                Debug.Assert(key.Length == ll.Length);
                bool compatible = true;
                for (int i = 0; i < key.Length && compatible; i++)
                {
                    int combined = key[i] + ll[i];
                    if (combined > height)
                    {
                        compatible = false;
                    }
                }
                if (compatible)
                {
                    answer++;
                }
            }
        }
        return answer.ToString();
    }

    public string Part2()
    {
        return "Merry Christmas!";
    }
}
