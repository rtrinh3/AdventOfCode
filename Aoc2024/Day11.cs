using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/11
    // --- Day 11: Plutonian Pebbles ---
    public class Day11(string input) : IAocDay
    {
        private static string NormalizeStone(string stone)
        {
            stone = stone.TrimStart('0');
            if (stone == "")
            {
                return "0";
            }
            else
            {
                return stone;
            }
        }

        public string Part1()
        {
            List<string> stones = input.TrimEnd().Split(' ').ToList();
            for (int i = 0; i < 25; i++)
            {
                List<string> nextStones = new();
                foreach (var stone in stones)
                {
                    if (stone == "0")
                    {
                        nextStones.Add("1");
                    }
                    else if (stone.Length % 2 == 0)
                    {
                        string left = NormalizeStone(stone.Substring(0, stone.Length / 2));
                        nextStones.Add(left);
                        string right = NormalizeStone(stone.Substring(stone.Length / 2, stone.Length / 2));
                        nextStones.Add(right);
                    }
                    else
                    {
                        var value = long.Parse(stone);
                        var nextValue = value * 2024;
                        nextStones.Add(nextValue.ToString());
                    }
                }
                stones = nextStones;
            }
            return stones.Count.ToString();
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
