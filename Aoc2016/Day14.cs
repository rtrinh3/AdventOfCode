using AocCommon;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/14
// --- Day 14: One-Time Pad ---
public partial class Day14(string input) : IAocDay
{
    [GeneratedRegex(@"(.)\1\1")]
    private static partial Regex TrioMatcherGenerator();
    private readonly Regex trioMatcher = TrioMatcherGenerator();

    private static string HashString(string hashInputString)
    {
        var hashInputBytes = Encoding.ASCII.GetBytes(hashInputString);
        var hashBytes = MD5.HashData(hashInputBytes);
        var hashString = string.Join("", hashBytes.Select(b => b.ToString("x2")));
        return hashString;
    }

    private int DoPuzzle(int hashIterations)
    {
        Func<int, string> CalculateHash = Memoization.MakeInt((int suffix) =>
        {
            string hashString = input + suffix;
            for (int i = 0; i < hashIterations; i++)
            {
                hashString = HashString(hashString);
            }
            return hashString;
        });
        int i = 0;
        int found = 0;
        while (found < 64)
        {
            var hashI = CalculateHash(i);
            var trioMatch = trioMatcher.Match(hashI);
            if (trioMatch.Success)
            {
                string five = trioMatch.Value + trioMatch.ValueSpan[0] + trioMatch.ValueSpan[0];
                for (int j = 1; j <= 1000; j++)
                {
                    var hashJ = CalculateHash(i + j);
                    if (hashJ.Contains(five))
                    {
                        found++;
                        break;
                    }
                }
            }
            i++;
        }
        return i - 1;
    }

    public string Part1()
    {
        var answer = DoPuzzle(1);
        return answer.ToString();
    }

    public string Part2()
    {
        var answer = DoPuzzle(2017);
        return answer.ToString();
    }
}
