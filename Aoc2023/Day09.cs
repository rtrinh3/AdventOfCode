using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/9
    public class Day09(string input)
    {
        private int[][] histories = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray())
                .ToArray();

        public int Part1()
        {
            var answer = histories.Sum(h => Extrapolate(h));
            return answer;
        }

        private static int Extrapolate(int[] list)
        {
            if (list.Length == 0) return 0;
            if (list.Length == 1) return list[0];
            if (list.All(x => x == 0)) return 0;

            int[] differences = new int[list.Length - 1];
            for (int i = 0; i < differences.Length; i++)
            {
                differences[i] = list[i + 1] - list[i];
            }
            var nextDifference = Extrapolate(differences);
            return list.Last() + nextDifference;
        }

        public int Part2()
        {
            var answer = histories.Sum(h => Extrapolate(h.Reverse().ToArray()));
            return answer;
        }
    }
}
