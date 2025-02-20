﻿using AocCommon;
using System.Collections.Immutable;

namespace Aoc2015;

// https://adventofcode.com/2015/day/17
// --- Day 17: No Such Thing as Too Much ---
public class Day17(string input, int targetWeight) : IAocDay
{
    private readonly int[] containersInput = Parsing.IntsPositive(input);

    public Day17(string input): this(input, 150)
    {
    }

    private IEnumerable<ImmutableArray<int>> GenerateSubsets()
    {
        Stack<(ImmutableArray<int> prefix, int sum, ImmutableStack<int> candidates)> stack = new();
        var initialCandidates = ImmutableStack.CreateRange(containersInput);
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
                    yield return newPrefix;
                }
                else if (newSum < targetWeight)
                {
                    stack.Push((newPrefix, newSum, subset));
                }
            }
        }
    }

    public string Part1()
    {
        var subsets = GenerateSubsets();
        var answer = subsets.Count();
        return answer.ToString();
    }

    public string Part2()
    {
        var subsets = GenerateSubsets();
        var counts = new Dictionary<int, int>();
        foreach (var containers in subsets)
        {
            int count = containers.Length;
            counts[count] = 1 + counts.GetValueOrDefault(count, 0);
        }
        var minimumContainers = counts.Keys.Min();
        var minimumCount = counts[minimumContainers];
        return minimumCount.ToString();
    }
}
