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

        private long DoPuzzle(Func<long, long, long>[] operations)
        {
            long total = 0;
            Parallel.ForEach(equations, line =>
            {
                var (target, operands) = line;
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
                    if (stack[^1] > target)
                    {
                        return false;
                    }
                    var tail = stack.AsSpan()[0..^2];
                    foreach (var op in operations)
                    {
                        if (matchTarget([.. tail, op(stack[^1], stack[^2])]))
                        {
                            return true;
                        }
                    }
                    return false;
                }
                var initialStack = operands.Reverse().ToArray();
                bool isMatch = matchTarget(initialStack);
                if (isMatch)
                {
                    Interlocked.Add(ref total, target);
                }
            });
            return total;
        }

        public string Part1()
        {
            long add(long a, long b) => a + b;
            long mul(long a, long b) => a * b;
            var total = DoPuzzle([add, mul]);
            return total.ToString();
        }

        public string Part2()
        {
            long add(long a, long b) => a + b;
            long mul(long a, long b) => a * b;
            long concat(long a, long b)
            {
                var lengthB = Log10(b);
                var aa = a * Pow10(Math.Max(1, lengthB));
                var answer = aa + b;
                return answer;
            }
            var total = DoPuzzle([add, mul, concat]);
            return total.ToString();
        }

        private static int Log10(long n)
        {
            int answer = 0;
            while (n > 0)
            {
                n /= 10;
                answer++;
            }
            return answer;
        }

        private static long Pow10(int n)
        {
            long answer = 1;
            for (int i = 0; i < n; i++)
            {
                answer *= 10;
            }
            return answer;
        }
    }
}
