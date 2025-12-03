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
        if (count == 0)
        {
            return "";
        }
        char maxValue = '\0';
        int maxIndex = -1;
        for (int i = 0; i <= adapters.Length - count; i++)
        {
            if (adapters[i] > maxValue)
            {
                maxValue = adapters[i];
                maxIndex = i;
            }
        }
        var maxRemainder = FindMaxJoltage(adapters.Slice(maxIndex + 1), count - 1);
        var answer = maxValue + maxRemainder;
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