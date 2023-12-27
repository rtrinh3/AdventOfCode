namespace Aoc2022
{
    // https://adventofcode.com/2022/day/6
    public class Day06(string input) : IAocDay
    {
        int FindMarker(int window)
        {
            for (int i = 0; i < input.Length - window; ++i)
            {
                var sub = input.Skip(i).Take(window);
                bool allDifferent = sub.Distinct().Count() == window;
                if (allDifferent)
                {
                    return (i + window);
                }
            }
            return -1;
        }

        public string Part1()
        {
            return FindMarker(4).ToString();
        }
        public string Part2()
        {
            return FindMarker(14).ToString();
        }
    }
}
