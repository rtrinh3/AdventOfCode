using AocCommon;

namespace Aoc2024;

public class Day19(string input) : IAocDay
{
    public string Part1()
    {
        var arrangements = FindAllArrangements();
        var answer = arrangements.Count(x => x > 0);
        return answer.ToString();
    }

    public string Part2()
    {
        var arrangements = FindAllArrangements();
        var answer = arrangements.Sum();
        return answer.ToString();
    }

    private long[] FindAllArrangements()
    {
        // Parse
        string[] paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        string[] towels = paragraphs[0].Split(',', StringSplitOptions.TrimEntries);
        string[] patterns = paragraphs[1].Split('\n');

        // Find arrangements
        long[] answers = new long[patterns.Length];
        for (int i = 0; i < patterns.Length; i++)
        {
            var pattern = patterns[i];
            Func<int, long> FindArrangements = _ => throw new Exception("Stub for memoized recursive function");
            FindArrangements = Memoization.Make((int index) =>
            {
                if (index >= pattern.Length)
                {
                    return 1L;
                }
                long possibleArrangements = 0;
                foreach (var towel in towels)
                {
                    if (pattern.AsSpan()[index..].StartsWith(towel))
                    {
                        var tails = FindArrangements(index + towel.Length);
                        possibleArrangements += tails;
                    }
                }
                return possibleArrangements;
            });
            var possible = FindArrangements(0);
            answers[i] = possible;
            //Console.WriteLine($"Done {i + 1}/{patterns.Length}: {possible}");
        }
        return answers;
    }
}
