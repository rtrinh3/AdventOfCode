namespace Aoc2025;

// https://adventofcode.com/2025/day/3
// --- Day 3: Lobby ---
public class Day03(string input) : AocCommon.IAocDay
{
    private readonly string[] lines = AocCommon.Parsing.SplitLines(input);

    public string Part1()
    {
        long accumulator = 0;
        foreach (var line in lines)
        {
            var value = FindMaxJoltage(line, 2);
            accumulator += long.Parse(value);
        }
        return accumulator.ToString();
    }

    private static string FindMaxJoltage(ReadOnlySpan<char> adapters, int count)
    {
        string answer = "";
        int startIndex = 0;
        for (int answerPos = 0; answerPos < count; answerPos++)
        {
            char maxValue = '\0';
            for (int i = startIndex; i <= adapters.Length - (count - answerPos); i++)
            {
                if (adapters[i] > maxValue)
                {
                    maxValue = adapters[i];
                    startIndex = i + 1;
                }
            }
            answer += maxValue;
        }
        return answer;
    }

    public string Part2()
    {
        long accumulator = 0;
        foreach (var line in lines)
        {
            var value = FindMaxJoltage(line, 12);
            accumulator += long.Parse(value);
        }
        return accumulator.ToString();
    }
}