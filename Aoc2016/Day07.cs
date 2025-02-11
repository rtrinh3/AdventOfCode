using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/7
// --- Day 7: Internet Protocol Version 7 ---
public class Day07(string input) : IAocDay
{
    private readonly string[] lines = Parsing.SplitLines(input);

    public string Part1()
    {
        var answer = lines.Count(IsValidPart1);
        return answer.ToString();
    }

    private static bool IsValidPart1(string address)
    {
        // Pass 1: find brackets
        int[] inBrackets = new int[address.Length];
        int bracketCount = 0;
        for (int i = 0; i < address.Length; i++)
        {
            if (address[i] == '[')
            {
                bracketCount++;
            }
            else if (address[i] == ']')
            {
                bracketCount--;
            }
            inBrackets[i] = bracketCount;
        }
        // Pass 2: find ABBA
        bool abbaOutside = false;
        bool abbaInside = false;
        for (int i = 0; i < address.Length - 3 && !(abbaOutside && abbaInside); i++)
        {
            if (char.IsAsciiLetter(address[i + 0]) &&
                char.IsAsciiLetter(address[i + 1]) &&
                address[i + 0] != address[i + 1] &&
                address[i + 1] == address[i + 2] &&
                address[i + 0] == address[i + 3])
            {
                if (inBrackets[i] > 0)
                {
                    abbaInside = true;
                }
                else
                {
                    abbaOutside = true;
                }
            }
        }
        var answer = abbaOutside && !abbaInside;
        //Console.WriteLine($"Accepted:{answer}\tOutside:{abbaOutside}\tInside:{abbaInside} \t{address}");
        return answer;
    }

    public string Part2()
    {
        var answer = lines.Count(IsValidPart2);
        return answer.ToString();
    }

    private static bool IsValidPart2(string address)
    {
        // Pass 1: find brackets
        int[] inBrackets = new int[address.Length];
        int bracketCount = 0;
        for (int i = 0; i < address.Length; i++)
        {
            if (address[i] == '[')
            {
                bracketCount++;
            }
            else if (address[i] == ']')
            {
                bracketCount--;
            }
            inBrackets[i] = bracketCount;
        }
        // Pass 2: find ABA
        HashSet<string> abaOutside = new();
        HashSet<string> abaInside = new();
        for (int i = 0; i < address.Length - 2; i++)
        {
            if (char.IsAsciiLetter(address[i + 0]) &&
                char.IsAsciiLetter(address[i + 1]) &&
                address[i + 0] != address[i + 1] &&
                address[i + 0] == address[i + 2])
            {
                if (inBrackets[i] > 0)
                {
                    abaInside.Add(address.Substring(i, 3));
                }
                else
                {
                    abaOutside.Add(address.Substring(i, 3));
                }
            }
        }
        // Find match between outside and inside
        foreach (var outside in abaOutside)
        {
            string counterPart = $"{outside[1]}{outside[0]}{outside[1]}";
            if (abaInside.Contains(counterPart))
            {
                return true;
            }
        }
        return false;
    }
}
