namespace Aoc2025;

// https://adventofcode.com/2025/day/3
// --- Day 3: Lobby ---
public class Day03(string input) : AocCommon.IAocDay
{
    private readonly string[] lines = AocCommon.Parsing.SplitLines(input);

    public string Part1()
    {
        int accumulator = 0;
        foreach (var line in lines)
        {
            int indexA = -1;
            char valueA = '\0';
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (line[i] > valueA)
                {
                    indexA = i;
                    valueA = line[i];
                }
            }
            int indexB = indexA + 1;
            char valueB = line[indexB];
            for (int i = indexA + 1; i < line.Length; i++)
            {
                if (line[i] > valueB)
                {
                    indexB = i;
                    valueB = line[i];
                }
            }
            int value = 10 * (valueA - '0') + (valueB - '0');
            accumulator += value;
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}