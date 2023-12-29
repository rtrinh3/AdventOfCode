namespace Aoc2019
{
    // https://adventofcode.com/2019/day/4
    public class Day04(string input) : IAocDay
    {
        private string lowerBound = input.Split('-')[0].Trim();
        private string upperBound = input.Split('-')[1].Trim();

        private static bool AdjacentCriterion(string s)
        {
            return Enumerable.Range(0, s.Length - 1).Any(i => s[i] == s[i + 1]);
        }
        private static bool IncreasingCriterion(string s)
        {
            return Enumerable.Range(0, s.Length - 1).All(i => s[i] <= s[i + 1]);
        }

        public string Part1()
        {
            string pass = lowerBound;
            int count = 0;
            while (pass != upperBound)
            {
                if (AdjacentCriterion(pass) && IncreasingCriterion(pass))
                {
                    count++;
                }
                pass = (1 + int.Parse(pass)).ToString();
            }
            return count.ToString();
        }

        public string Part2()
        {
            string pass2 = lowerBound;
            int count2 = 0;
            while (pass2 != upperBound)
            {
                if (IncreasingCriterion(pass2))
                {
                    // if IncreasingCriterion is true, then all identical digits are adjacent
                    var digitCounts = pass2.GroupBy(c => c);
                    if (digitCounts.Any(g => g.Count() == 2))
                    {
                        count2++;
                    }
                }
                pass2 = (1 + int.Parse(pass2)).ToString();
            }
            return count2.ToString();
        }
    }
}
