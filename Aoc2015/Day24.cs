using AocCommon;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Aoc2015;

// https://adventofcode.com/2015/day/24
// --- Day 24: It Hangs in the Balance ---
public class Day24(string input) : IAocDay
{
    private readonly int[] weights = Parsing.IntsPositive(input);


    private long DoPuzzle(int divisor)
    {
        int totalWeight = weights.Sum();
        Debug.Assert(totalWeight % divisor == 0);
        int targetWeight = totalWeight / divisor;

        var rightSizedGroups = GenerateSubsets(weights, targetWeight).ToArray();
        //foreach (var g in rightSizedGroups)
        //{
        //    Console.WriteLine(string.Join(" ", g));
        //}

        var minimumSize = rightSizedGroups.Min(g => g.Length);

        var minimumGroups = rightSizedGroups.Where(g => g.Length == minimumSize);
        var minimumQuantum = minimumGroups.Min(g => g.Aggregate(1L, (a, b) => a * b));

        return minimumQuantum;
    }

    private static IEnumerable<int[]> GenerateSubsets(IEnumerable<int> items, int targetWeight)
    {
        Stack<(ImmutableHashSet<int> prefix, ImmutableHashSet<int> candidates)> stack = new();
        stack.Push(([], items.ToImmutableHashSet()));
        while (stack.TryPop(out var state))
        {
            var weight = state.prefix.Sum();
            if (weight == targetWeight)
            {
                yield return state.prefix.ToArray();
            }
            else if (weight < targetWeight)
            {
                var subset = state.candidates;
                foreach (var c in state.candidates)
                {
                    subset = subset.Remove(c);
                    var newPrefix = state.prefix.Add(c);
                    stack.Push((newPrefix, subset));
                }
            }
            else
            {
                //"NOP".ToString();
            }
        }
    }

    public string Part1()
    {
        var answer = DoPuzzle(3);
        return answer.ToString();
    }

    public string Part2()
    {
        var answer = DoPuzzle(4);
        return answer.ToString();
    }
}
