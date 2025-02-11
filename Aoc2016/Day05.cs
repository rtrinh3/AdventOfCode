using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2016;

// https://adventofcode.com/2016/day/5
// --- Day 5: How About a Nice Game of Chess? ---
public class Day05(string input) : IAocDay
{
    private readonly string seed = input.Trim();
    private readonly System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5.Create();

    public string Part1()
    {
        StringBuilder answer = new StringBuilder();
        int i = 0;
        int found = 0;
        while (found < 8)
        {
            var value = seed + i;
            var hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(value));
            if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0F)
            {
                answer.AppendFormat("{0:x}", hash[2]);
                found++;
            }
            i++;
        }
        return answer.ToString();
    }

    public string Part2()
    {
        char[] answer = new char[8];
        int i = 0;
        int found = 0;
        while (found < 8)
        {
            var value = seed + i;
            var hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(value));
            if (hash[0] == 0 && hash[1] == 0 && hash[2] <= 0x0F)
            {
                int position = hash[2] & 0x0f;
                if (position < 8 && answer[position] == 0)
                {
                    int charValue = hash[3] >> 4;
                    string character = charValue.ToString("x");
                    Debug.Assert(character.Length == 1);
                    answer[position] = character[0];
                    found++;
                }
            }
            i++;
        }
        return string.Join("", answer);
    }
}
