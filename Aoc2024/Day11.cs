using AocCommon;
using System.Numerics;

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
                var left = long.Parse(digits[..(digits.Length / 2)]);
                var leftStones = SimulateStone(left, iterations - 1);
                var right = long.Parse(digits[(digits.Length / 2)..]);
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

        public string Part3(int iterations)
        {
            // https://www.reddit.com/r/adventofcode/comments/1hqoc5w/2024_day_11_i_made_a_part_3_to_this_day/
            // https://breakmessage.com/aocextension/2024day11/
            Func<EquatableArray<long>, int, (BigInteger checksum, BigInteger sumValues, BigInteger count)> Recurse = (_, _) => throw new Exception("Stub for memoized recursive function");
            Recurse = Memoization.Make((EquatableArray<long> stones, int iterations) =>
            {
                if (iterations == 0)
                {
                    BigInteger checksum = 0;
                    BigInteger sumValues = 0;
                    BigInteger index = 0;
                    foreach (var stone in stones)
                    {
                        checksum += index * stone;
                        sumValues += stone;
                        index++;
                    }
                    return (checksum, sumValues, index);
                }
                else
                {
                    BigInteger checksum = 0;
                    BigInteger sumValues = 0;
                    BigInteger index = 0;
                    foreach (var stone in stones)
                    {
                        System.Collections.Immutable.ImmutableArray<long>? nextStones = null;
                        if (stone == 0)
                        {
                            nextStones = [1];
                        }
                        else
                        {
                            string digits = stone.ToString();
                            if (digits.Length % 2 == 0)
                            {
                                var left = long.Parse(digits[..(digits.Length / 2)]);
                                var right = long.Parse(digits[(digits.Length / 2)..]);
                                nextStones = [left, right];
                            }
                            else
                            {
                                nextStones = [stone * 2024];
                            }
                        }
                        var next = Recurse(new(nextStones), iterations - 1);
                        checksum += next.checksum + next.sumValues * index;
                        sumValues += next.sumValues;
                        index += next.count;
                    }
                    return (checksum, sumValues, index);
                }
            });

            var answers = Recurse(new(initialStones), iterations);
            return answers.checksum.ToString();
        }
    }
}
