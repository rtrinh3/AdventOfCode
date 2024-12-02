namespace Aoc2024
{
    // https://adventofcode.com/2024/day/2
    // --- Day 2: Red-Nosed Reports ---
    public class Day02: AocCommon.IAocDay
    {
        private readonly int[][] reports;
        public Day02(string input)
        {
            reports = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(
                line => line.Split(' ').Select(int.Parse).ToArray()
            ).ToArray();
        }

        private bool isSafe(IList<int> levels)
        {
            int direction = 0;
            bool monotonic = true;
            for (int i = 0; i < levels.Count - 1; i++)
            {
                int change = levels[i].CompareTo(levels[i + 1]);
                if (direction == 0)
                {
                    direction = change;
                }
                else
                {
                    if (direction != change)
                    {
                        monotonic = false;
                        break;
                    }
                }
            }
            if (!monotonic) return false;
            bool smooth = true;
            for (int i = 0; i < levels.Count - 1; i++)
            {
                int change = Math.Abs(levels[i] - levels[i + 1]);
                if (change < 1 || 3 < change)
                {
                    smooth = false;
                    break;
                }
            }
            return smooth;
        }

        public string Part1()
        {
            int safe = 0;
            foreach (var report in reports)
            {
                if (isSafe(report)) safe++;
            }
            return safe.ToString();
        }

        public string Part2()
        {
            int safe = 0;
            foreach (var report in reports)
            {
                if (isSafe(report))
                {
                    safe++;
                    continue;
                }
                for (int i = 0; i < report.Length; i++)
                {
                    var dampened = report.Where((item, index) => index != i).ToList();
                    if (isSafe(dampened))
                    {
                        safe++;
                        break;
                    }
                }
            }
            return safe.ToString();
        }
    }
}
