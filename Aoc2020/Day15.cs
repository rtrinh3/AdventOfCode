using AocCommon;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/15
    // --- Day 15: Rambunctious Recitation ---
    public class Day15(string input) : IAocDay
    {
        public long Part1()
        {
            return DoPuzzle(2020);
        }

        public long Part2()
        {
            return DoPuzzle(30000000);
        }

        private long DoPuzzle(int iterations)
        {
            int[] seed = input.Split(',').Select(int.Parse).ToArray();
            DefaultDict<int, (int, int)> recitals = new(() => (-1, -1));
            int lastNumber = -1;
            for (int i = 0; i < iterations; i++)
            {
                if (i < seed.Length)
                {
                    lastNumber = seed[i];
                }
                else if (recitals[lastNumber].Item2 < 0)
                {
                    lastNumber = 0;
                }
                else
                {
                    lastNumber = recitals[lastNumber].Item1 - recitals[lastNumber].Item2;
                }
                recitals[lastNumber] = (i, recitals[lastNumber].Item1);
            }
            return lastNumber;
        }
    }
}
