using AocCommon;

namespace Aoc2024;

public class Day19(string input) : IAocDay
{
    //private record class ArrangementNode(string Head, ArrangementNode? Tail);

    public string Part1()
    {
        // Parse
        string[] paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        string[] towels = paragraphs[0].Split(',', StringSplitOptions.TrimEntries).OrderByDescending(t => t.Length).ToArray();
        string[] patterns = paragraphs[1].Split('\n');

        // Find arrangements
        //ArrangementNode?[] emptyArrangement = [null];
        int answer = 0;
        for (int i = 0; i < patterns.Length; i++)
        {
            var pattern = patterns[i];
            Func<int, bool> CheckArrangements = _ => throw new Exception("Stub for memoized recursive function");
            CheckArrangements = Memoization.Make((int index) =>
            {
                if (index >= pattern.Length)
                {
                    //return emptyArrangement;
                    return true;
                }
                //List<ArrangementNode> possibleArrangements = new();
                foreach (var towel in towels)
                {
                    if (pattern.AsSpan()[index..].StartsWith(towel))
                    {
                        //var tails = FindArrangements(index + towel.Length);
                        //foreach (var tail in tails)
                        //{
                        //    ArrangementNode arrangement = new(towel, tail);
                        //    possibleArrangements.Add(arrangement);
                        //}
                        bool tailPossible = CheckArrangements(index + towel.Length);
                        if (tailPossible)
                        {
                            return true;
                        }
                    }
                }
                //return possibleArrangements.ToArray();
                return false;
            });
            var possible = CheckArrangements(0);
            if (possible)
            {
                answer++;
            }
            //Console.WriteLine($"Done {i + 1}/{patterns.Length} -- answer={answer}");
        }
        return answer.ToString();
    }

    public string Part2()
    {
        // Parse
        string[] paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        string[] towels = paragraphs[0].Split(',', StringSplitOptions.TrimEntries).OrderByDescending(t => t.Length).ToArray();
        string[] patterns = paragraphs[1].Split('\n');

        // Find arrangements
        //ArrangementNode?[] emptyArrangement = [null];
        long answer = 0;
        for (int i = 0; i < patterns.Length; i++)
        {
            var pattern = patterns[i];
            Func<int, long> FindArrangements = _ => throw new Exception("Stub for memoized recursive function");
            FindArrangements = Memoization.Make((int index) =>
            {
                if (index >= pattern.Length)
                {
                    return 1;
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
            answer += possible;
            Console.WriteLine($"Done {i + 1}/{patterns.Length} -- answer={answer}");
        }
        return answer.ToString();
    }
}
