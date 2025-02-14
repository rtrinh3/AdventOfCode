using AocCommon;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/14
// --- Day 14: One-Time Pad ---
public class Day14(string input) : IAocDay
{
    private Regex trioMatcher = new(@"(.)\1\1");

    private static string HashString(string hashInputString)
    {
        var hashInputBytes = Encoding.ASCII.GetBytes(hashInputString);
        var hashBytes = MD5.HashData(hashInputBytes);
        var hashString = string.Join("", hashBytes.Select(b => b.ToString("x2")));
        return hashString;
    }

    private readonly Func<int, string> CalculateHashPart1 = Memoization.MakeInt((int suffix) =>
    {
        string hashInputString = input + suffix;
        return HashString(hashInputString);
    });

    public string Part1()
    {
        int i = 0;
        int found = 0;
        while (true)
        {
            var hashI = CalculateHashPart1(i);
            var trioMatch = trioMatcher.Match(hashI);
            if (trioMatch.Success)
            {
                string five = trioMatch.Value + trioMatch.ValueSpan[0] + trioMatch.ValueSpan[0];
                for (int j = 1; j <= 1000; j++)
                {
                    var hashJ = CalculateHashPart1(i + j);
                    if (hashJ.Contains(five))
                    {
                        found++;
                        if (found == 64)
                        {
                            var answer = i;
                            return answer.ToString();
                        }
                        break;
                    }
                }
            }
            i++;
        }
        throw new Exception("Answer not found");
    }
    private readonly Func<int, string> CalculateHashPart2 = Memoization.MakeInt((int suffix) =>
    {
        string hashString = input + suffix;
        for (int i = 0; i < 2017; i++)
        {
            hashString = HashString(hashString);
        }
        return hashString;
    });

    public string Part2()
    {
        int i = 0;
        int found = 0;
        while (true)
        {
            var hashI = CalculateHashPart2(i);
            var trioMatch = trioMatcher.Match(hashI);
            if (trioMatch.Success)
            {
                string five = trioMatch.Value + trioMatch.ValueSpan[0] + trioMatch.ValueSpan[0];
                for (int j = 1; j <= 1000; j++)
                {
                    var hashJ = CalculateHashPart2(i + j);
                    if (hashJ.Contains(five))
                    {
                        found++;
                        if (found == 64)
                        {
                            var answer = i;
                            return answer.ToString();
                        }
                        break;
                    }
                }
            }
            i++;
        }
        throw new Exception("Answer not found");
    }
}
