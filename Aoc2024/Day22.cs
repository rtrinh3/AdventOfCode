using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/22
// --- Day 22: Monkey Market ---
public class Day22(string input) : IAocDay
{
    private readonly long[] initialSecretNumbers = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(long.Parse).ToArray();

    public string Part1()
    {
        long answer = 0;
        foreach (var secret in initialSecretNumbers)
        {
            var number = secret;
            for (int i = 0; i < 2000; i++)
            {
                number = ((number * 64) ^ number) % 16777216;
                number = ((number / 32) ^ number) % 16777216;
                number = ((number * 2048) ^ number) % 16777216;
            }
            answer += number;
        }
        return answer.ToString();
    }

    public string Part2()
    {
        return nameof(Part2);
    }
}
