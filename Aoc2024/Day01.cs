using System.Diagnostics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/1
    // Day 1: Historian Hysteria
    public class Day01: AocCommon.IAocDay
    {
        private readonly long[] listA;
        private readonly long[] listB;

        public Day01(string input)
        {
            List<long> listABuffer = new();
            List<long> listBBuffer = new();
            foreach (string line in input.ReplaceLineEndings("\n").TrimEnd().Split('\n'))
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                Debug.Assert(parts.Length == 2);
                listABuffer.Add(long.Parse(parts[0]));
                listBBuffer.Add(long.Parse(parts[1]));
            }
            listA = listABuffer.ToArray();
            listB = listBBuffer.ToArray();
        }

        public string Part1()
        {
            var sortedA = listA.Order().ToList();
            var sortedB = listB.Order().ToList();
            Debug.Assert(sortedA.Count == sortedB.Count);
            long totalDistance = 0;
            for (int i = 0; i < sortedA.Count; i++)
            {
                totalDistance += Math.Abs(sortedA[i] - sortedB[i]);
            }
            return totalDistance.ToString();
        }

        public string Part2()
        {
            Dictionary<long, int> leftEntries = listA.GroupBy(n => n).ToDictionary(g => g.Key, g => g.Count());
            long totalSimilarity = 0;
            foreach (long n in listB)
            {
                totalSimilarity += n * leftEntries.GetValueOrDefault(n, 0);
            }
            return totalSimilarity.ToString();
        }
    }
}
