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
        var rightSizedGroups = GenerateSubsets(targetWeight);
        var minimumSize = rightSizedGroups.Min(g => g.Length);
        var minimumGroups = rightSizedGroups.Where(g => g.Length == minimumSize);
        var minimumQuantum = minimumGroups.Min(g => g.Aggregate(1L, (a, b) => a * b));
        return minimumQuantum;
    }

    private List<ImmutableArray<int>> GenerateSubsets(int targetWeight)
    {
        List<ImmutableArray<int>> results = new();
        Stack<(ImmutableArray<int> prefix, int sum, ImmutableStack<int> candidates)> stack = new();
        var initialCandidates = ImmutableStack.CreateRange(weights);
        stack.Push(([], 0, initialCandidates));
        while (stack.TryPop(out var state))
        {
            var subset = state.candidates;
            while (!subset.IsEmpty)
            {
                subset = subset.Pop(out var c);
                var newPrefix = state.prefix.Add(c);
                var newSum = state.sum + c;
                if (newSum == targetWeight)
                {
                    results.Add(newPrefix);
                }
                else if (newSum < targetWeight)
                {
                    stack.Push((newPrefix, newSum, subset));
                }
            }
        }
        return results;
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
