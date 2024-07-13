namespace Aoc2020
{
    // https://adventofcode.com/2020/day/15
    // --- Day 15: Rambunctious Recitation ---
    public class Day15(string input) : IAocDay
    {
        public string Part1()
        {
            return DoPuzzle(2020).ToString();
        }

        public string Part2()
        {
            return DoPuzzle(30_000_000).ToString();
        }

        private long DoPuzzle(int iterations)
        {
            int[] seed = input.Split(',').Select(int.Parse).ToArray();
            int[] recitals = new int[iterations];
            for (int i = 0; i < seed.Length - 1; i++)
            {
                recitals[seed[i]] = i + 1;
            }
            int numberToSpeak = seed[^1];
            for (int i = seed.Length - 1; i < iterations - 1; i++)
            {
                if (recitals[numberToSpeak] == 0)
                {
                    recitals[numberToSpeak] = i + 1;
                    numberToSpeak = 0;
                }
                else
                {
                    int nextNumber = i + 1 - recitals[numberToSpeak];
                    recitals[numberToSpeak] = i + 1;
                    numberToSpeak = nextNumber;
                }
            }
            return numberToSpeak;
        }
    }
}
