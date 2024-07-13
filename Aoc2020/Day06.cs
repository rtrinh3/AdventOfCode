namespace Aoc2020
{
    // https://adventofcode.com/2020/day/6
    // --- Day 6: Custom Customs ---
    public class Day06(string input): IAocDay
    {
        private readonly string[] groups = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");

        public string Part1()
        {
            var sum = groups.Sum(g => g.Where(char.IsLetter).Distinct().Count());
            return sum.ToString();
        }

        public string Part2()
        {
            int sum = 0;
            foreach (var g in groups)
            {
                var persons = g.Split('\n');
                var shortest = persons.MinBy(p => p.Length);
                foreach (var c in shortest)
                {
                    if (persons.All(p => p.Contains(c)))
                    {
                        sum++;
                    }
                }
            }
            return sum.ToString();
        }
    }
}
