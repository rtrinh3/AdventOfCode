using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/9
// --- Day 9: Explosives in Cyberspace ---
public class Day09(string input) : IAocDay
{
    private readonly string cleanInput = Regex.Replace(input, @"\s", "");

    public string Part1()
    {
        var answer = Decompress(cleanInput, false);
        return answer.ToString();
    }

    public string Part2()
    {
        var answer = Decompress(cleanInput, true);
        return answer.ToString();
    }

    private static long Decompress(string data, bool recurse)
    {
        var markers = Regex.Matches(data, @"\((\d+)x(\d+)\)");
        long decompressedLength = 0;
        int inputIndex = 0;
        foreach (var marker in markers.Cast<Match>())
        {
            if (marker.Index < inputIndex)
            {
                continue;
            }
            decompressedLength += marker.Index - inputIndex;
            int length = int.Parse(marker.Groups[1].ValueSpan);
            int repeat = int.Parse(marker.Groups[2].ValueSpan);
            long trueLength;
            if (recurse)
            {
                var capture = data.Substring(marker.Index + marker.Length, length);
                trueLength = Decompress(capture, true);
            }
            else
            {
                trueLength = length;
            }
            decompressedLength += trueLength * repeat;
            inputIndex = marker.Index + marker.Length + length;
        }
        decompressedLength += data.Length - inputIndex;
        return decompressedLength;
    }
}
