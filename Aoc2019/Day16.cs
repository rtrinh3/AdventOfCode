using System.Diagnostics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/16
    public class Day16(string input) : IAocDay
    {
        public string Part1()
        {
            int[] fftIteration(int[] digits)
            {
                int[] output = new int[digits.Length];
                for (int i = 0; i < digits.Length; i++)
                {
                    var pattern = basePatternRepeated(i + 1).Skip(1).Take(digits.Length);
                    output[i] = Math.Abs(digits.Zip(pattern, (d, p) => d * p).Sum() % 10);
                }
                return output;
            }

            IEnumerable<int> basePatternRepeated(int repetitions)
            {
                int[] pattern = { 0, 1, 0, -1 };
                int i = 0;
                while (true)
                {
                    var output = pattern[i];
                    for (int j = 0; j < repetitions; j++)
                    {
                        yield return output;
                    }
                    i = (i + 1) % 4;
                }
            }

            int[] puzzleDigits = input.Where(char.IsDigit).Select(c => c - '0').ToArray();
            int[] digits = puzzleDigits;
            for (int i = 0; i < 100; i++)
            {
                digits = fftIteration(digits);
            }
            string answer = string.Join("", digits.Take(8));
            return answer;
        }

        public string Part2()
        {
            // See https://www.reddit.com/r/adventofcode/comments/ebf5cy/2019_day_16_part_2_understanding_how_to_come_up/
            // With the 7 digit offset, we will be in the second half of the input
            // The pattern looks like an upper triangular matrix of ones
            // ... 1 1 1
            // ... 0 1 1
            // ... 0 0 1
            // so the digit n of iteration i is equal to digit n of iteration i-1 + digit n-1 of iteration i
            // Let's calculate them from right to left
            string puzzle = input.TrimEnd();
            int offset = int.Parse(puzzle[0..7]);
            int workLength = puzzle.Length * 10000 - offset;
            int[] digits = new int[workLength];
            for (int pos = 0; pos < digits.Length; pos++)
            {
                int index = puzzle.Length - 1 - (pos % puzzle.Length);
                digits[pos] = puzzle[index];
            }
            for (int iteration = 0; iteration < 100; iteration++)
            {
                // Skip pos 0, it stays the last digit of the puzzle input
                for (int pos = 1; pos < digits.Length; pos++)
                {
                    digits[pos] = (digits[pos] + digits[pos - 1]) % 10;
                }
            }
            var answer = string.Join("", digits.Reverse().Take(8));
            return answer;
        }
    }
}
