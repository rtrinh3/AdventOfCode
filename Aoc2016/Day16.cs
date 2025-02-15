using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/16
// --- Day 16: Dragon Checksum ---
public class Day16(string input) : IAocDay
{
    public string Part1()
    {
        return DoPuzzle(272);
    }

    public string DoPuzzle(int diskSize)
    {
        bool[] a = input.Trim().Select(x => x == '1').ToArray();
        while (a.Length < diskSize)
        {
            var b = a.Reverse().Select(x => !x);
            a = [.. a, false, .. b];
        }
        bool[] checksum = a[..diskSize];
        while (checksum.Length % 2 == 0)
        {
            checksum = checksum.Chunk(2).Select(pair => pair[0] == pair[1]).ToArray();
        }
        var answer = string.Join("", checksum.Select(x => x ? '1' : '0'));
        return answer;
    }

    public string Part2()
    {
        return DoPuzzle(35651584);
    }
}
