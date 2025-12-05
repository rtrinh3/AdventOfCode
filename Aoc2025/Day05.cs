namespace Aoc2025;

// https://adventofcode.com/2025/day/5
// --- Day 5: Cafeteria ---
public class Day05(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        var paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");
        var ranges = paragraphs[0].Split('\n').Select(line =>
        {
            var parts = line.Split('-');
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        }).ToArray();
        var ingredients = paragraphs[1].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        int accumulator = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var (min, max) in ranges)
            {
                if (ingredient >= min && ingredient <= max)
                {
                    accumulator++;
                    break;
                }
            }
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}