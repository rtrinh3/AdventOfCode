using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/11
    // --- Day 11: Plutonian Pebbles ---
    public class Day11(string input) : IAocDay
    {
        private readonly long[] initialStones = input.TrimEnd().Split(' ').Select(long.Parse).ToArray();
        private readonly Dictionary<(long, int), long> memoSimulateStone = new();

        private long SimulateStone(long number, int iterations)
        {
            if (memoSimulateStone.TryGetValue((number, iterations), out var answer))
            {
                return answer;
            }
            if (iterations == 0)
            {
                return memoSimulateStone[(number, iterations)] = 1;
            }
            if (number == 0)
            {
                return memoSimulateStone[(number, iterations)] = SimulateStone(1, iterations - 1);
            }
            string digits = number.ToString();
            if (digits.Length % 2 == 0)
            {
                var left = long.Parse(digits.Substring(0, digits.Length / 2));
                var leftStones = SimulateStone(left, iterations - 1);
                var right = long.Parse(digits.Substring(digits.Length / 2, digits.Length / 2));
                var rightStones = SimulateStone(right, iterations - 1);
                return memoSimulateStone[(number, iterations)] = leftStones + rightStones;
            }
            else
            {
                return memoSimulateStone[(number, iterations)] = SimulateStone(number * 2024, iterations - 1);
            }
        }

        private long DoPuzzle(int iterations)
        {
            long answer = 0;
            foreach (var stone in initialStones)
            {
                var partial = SimulateStone(stone, iterations);
                answer += partial;
            }
            return answer;
        }

        public string Part1()
        {
            long answer = DoPuzzle(25);
            return answer.ToString();
        }

        public string Part2()
        {
            long answer = DoPuzzle(75);
            return answer.ToString();
        }
    }
}
