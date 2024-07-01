using AocCommon;
using System.Diagnostics;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/10
    // --- Day 10: Adapter Array ---
    public class Day10 : IAocDay
    {
        private readonly int[] adapters;
        private readonly int maxAdapter;

        public Day10(string input)
        {
            adapters = input.TrimEnd().Split('\n').Select(int.Parse).Order().ToArray();
            maxAdapter = adapters[^1];
        }

        public long Part1()
        {
            var (Ones, Threes) = DoPart1();
            var answer = Math.BigMul(Ones, Threes);
            return answer;
        }

        public (int Ones, int Threes) DoPart1()
        {
            List<int> sortedAdapters = [0, .. adapters, maxAdapter + 3];
            List<int> differences = new();
            for (int i = 0; i < sortedAdapters.Count - 1; i++)
            {
                differences.Add(sortedAdapters[i + 1] - sortedAdapters[i]);
            }
            var counts = differences.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            Debug.Assert(counts.Keys.All(x => x == 1 || x == 2 || x == 3));
            return (counts.GetValueOrDefault(1), counts.GetValueOrDefault(3));
        }

        public long Part2()
        {
            Func<int, long> countPossibilities = x => throw new NotImplementedException();
            countPossibilities = Memoization.Make((int joltage) =>
            {
                if (joltage == maxAdapter)
                {
                    return 1;
                }
                long possibilities = 0L;
                foreach (var adapter in adapters)
                {
                    var difference = adapter - joltage;
                    if (difference > 0 && difference <= 3)
                    {
                        possibilities += countPossibilities(adapter);
                    }
                }
                return possibilities;
            });
            return countPossibilities(0);
        }
    }
}
