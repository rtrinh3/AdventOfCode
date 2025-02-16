using AocCommon;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/21
// --- Day 21: Scrambled Letters and Hash ---
public class Day21(string input) : IAocDay
{
    public string Scramble(string password)
    {
        char[] buffer = password.Trim().ToCharArray();
        char[] temp = new char[buffer.Length];
        var lines = Parsing.SplitLines(input);
        foreach (var line in lines)
        {
            if (line.StartsWith("swap position"))
            {
                var args = Parsing.IntsPositive(line);
                Debug.Assert(args.Length == 2);
                (buffer[args[0]], buffer[args[1]]) = (buffer[args[1]], buffer[args[0]]);
            }
            else if (line.StartsWith("swap letter"))
            {
                char letterA = line[12];
                char letterB = line[26];
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == letterA)
                    {
                        buffer[i] = letterB;
                    }
                    else if (buffer[i] == letterB)
                    {
                        buffer[i] = letterA;
                    }
                }
            }
            else if (line.StartsWith("rotate left"))
            {
                var args = Parsing.IntsPositive(line);
                Debug.Assert(args.Length == 1);
                int dist = args[0];
                buffer.CopyTo(temp, 0);
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = temp[(i + dist) % temp.Length];
                }
            }
            else if (line.StartsWith("rotate right"))
            {
                var args = Parsing.IntsPositive(line);
                Debug.Assert(args.Length == 1);
                int dist = args[0];
                int offset = -dist;
                while (offset < 0)
                {
                    offset += temp.Length;
                }
                buffer.CopyTo(temp, 0);
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = temp[(i + offset) % temp.Length];
                }
            }
            else if (line.StartsWith("rotate based on position of letter"))
            {
                char letter = line[35];
                int indexOfLetter = Array.IndexOf(buffer, letter);
                int dist = 1 + indexOfLetter + (indexOfLetter >= 4 ? 1 : 0);
                int offset = -dist;
                while (offset < 0)
                {
                    offset += temp.Length;
                }
                buffer.CopyTo(temp, 0);
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = temp[(i + offset) % temp.Length];
                }
            }
            else if (line.StartsWith("reverse positions"))
            {
                var args = Parsing.IntsPositive(line);
                Array.Reverse(buffer, args[0], args[1] - args[0] + 1);
            }
            else if (line.StartsWith("move position"))
            {
                var args = Parsing.IntsPositive(line);
                Debug.Assert(args.Length == 2);
                int src = args[0];
                int dst = args[1];
                int copyLength = Math.Abs(dst - src);
                buffer.CopyTo(temp, 0);
                if (src < dst)
                {
                    Array.Copy(temp, src + 1, buffer, src, copyLength);
                }
                else if (dst < src)
                {
                    Array.Copy(temp, dst, buffer, dst + 1, copyLength);
                }
                buffer[dst] = temp[src];
            }
        }
        string answer = new(buffer);
        return answer;
    }

    public string Part1()
    {
        var answer = Scramble("abcdefgh");
        return answer;
    }

    public string Part2()
    {
        string scrambled = "fbgdceah";
        var permutations = MoreMath.IteratePermutations(scrambled);
        foreach (var potentialSource in permutations)
        {
            string sourceString = new(potentialSource);
            var scramblerOutput = Scramble(sourceString);
            if (scramblerOutput == scrambled)
            {
                return sourceString;
            }
        }
        throw new Exception("No answer found");
    }
}
