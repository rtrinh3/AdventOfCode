using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/7
    // --- Day 7: Bridge Repair ---
    public class Day07 : IAocDay
    {
        private readonly List<(long Target, long[] Operands)> equations = new();

        public Day07(string input)
        {
            foreach (string line in input.TrimEnd().ReplaceLineEndings("\n").Split('\n'))
            {
                var parts = line.Split(':', StringSplitOptions.TrimEntries);
                long target = long.Parse(parts[0]);
                long[] operands = parts[1].Split(' ').Select(long.Parse).ToArray();
                equations.Add((target, operands));
            }
        }

        public string Part1()
        {
            long total = 0;
            foreach (var (target, operands) in equations)
            {
                bool matchTarget(long[] stack)
                {
                    if (stack.Length == 0)
                    {
                        throw new Exception("Empty stack!");
                    }
                    if (stack.Length == 1)
                    {
                        return target == stack[0];
                    }
                    var tail = stack.AsSpan()[0..^2];
                    return matchTarget([.. tail, stack[^1] + stack[^2]]) || matchTarget([.. tail, stack[^1] * stack[^2]]);
                }
                var initialStack = operands.Reverse().ToArray();
                bool isMatch = matchTarget(initialStack);
                if (isMatch)
                {
                    total += target;
                }
            }
            return total.ToString();
        }

        public string Part2()
        {
            long total = 0;
            foreach (var (target, operands) in equations)
            {
                bool matchTarget(long[] stack)
                {
                    if (stack.Length == 0)
                    {
                        throw new Exception("Empty stack!");
                    }
                    if (stack.Length == 1)
                    {
                        return target == stack[0];
                    }
                    var tail = stack.AsSpan()[0..^2];
                    return matchTarget([.. tail, stack[^1] + stack[^2]]) || matchTarget([.. tail, stack[^1] * stack[^2]]) || matchTarget([.. tail, long.Parse(stack[^1].ToString() + stack[^2].ToString())]);
                }
                var initialStack = operands.Reverse().ToArray();
                bool isMatch = matchTarget(initialStack);
                if (isMatch)
                {
                    total += target;
                }
            }
            return total.ToString();
        }
    }
}
