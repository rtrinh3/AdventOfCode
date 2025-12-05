namespace Aoc2025;

// https://adventofcode.com/2025/day/5
// --- Day 5: Cafeteria ---
public class Day05 : AocCommon.IAocDay
{
    private record Range(long Min, long Max);

    private readonly Range[] ranges;
    private readonly long[] ingredients;

    public Day05(string input)
    {
        var paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");
        ranges = paragraphs[0].Split('\n').Select(line =>
        {
            var parts = line.Split('-');
            return new Range(long.Parse(parts[0]), long.Parse(parts[1]));
        }).ToArray();
        ingredients = paragraphs[1].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
    }

    public string Part1()
    {
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
        HashSet<Range> finalRanges = new();
        foreach (var range in ranges)
        {
            List<Range> toRemove = new();
            Range toAdd = range;
            foreach (var existing in finalRanges)
            {
                if (toAdd.Min <= existing.Max && toAdd.Max >= existing.Min)
                {
                    toRemove.Add(existing);
                    var newMin = Math.Min(toAdd.Min, existing.Min);
                    var newMax = Math.Max(toAdd.Max, existing.Max);
                    toAdd = new Range(newMin, newMax);
                }
            }
            finalRanges.ExceptWith(toRemove);
            finalRanges.Add(toAdd);
        }
        var answer = finalRanges.Sum(r => r.Max - r.Min + 1);
        return answer.ToString();
    }
}