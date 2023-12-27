namespace Aoc2022
{
    public class Day08(string input) : IAocDay
    {
        string[] forest = input.Split('\n', AocCommon.Constants.TrimAndDiscard);
        (int, int)[] dirs = { (0, -1), (0, +1), (-1, 0), (+1, 0) };

        IEnumerable<char> GetView(int row, int col, (int, int) dir)
        {
            while (true)
            {
                row += dir.Item1;
                col += dir.Item2;
                if (0 <= row && row < forest.Length && 0 <= col && col < forest[row].Length)
                {
                    yield return forest[row][col];
                }
                else
                {
                    break;
                }
            }
        }

        public string Part1()
        {
            bool isVisibleFrom(int row, int col, (int, int) dir)
            {
                char height = forest[row][col];
                return GetView(row, col, dir).All(h => h < height);
            }
            bool isVisible(int row, int col)
            {
                return dirs.Any(dir => isVisibleFrom(row, col, dir));
            }
            int count = 0;
            for (int i = 0; i < forest.Length; ++i)
            {
                for (int j = 0; j < forest[i].Length; ++j)
                {
                    count += isVisible(i, j) ? 1 : 0;
                }
            }
            return count.ToString();
        }

        public string Part2()
        {
            int maxScore = 0;
            for (int i = 0; i < forest.Length; ++i)
            {
                for (int j = 0; j < forest[i].Length; ++j)
                {
                    char height = forest[i][j];
                    int score = 1;
                    foreach (var dir in dirs)
                    {
                        int view = 0;
                        foreach (char h in GetView(i, j, dir))
                        {
                            if (h < height)
                            {
                                ++view;
                            }
                            else
                            {
                                ++view;
                                break;
                            }
                        }
                        score = score * view;
                    }
                    if (score > maxScore)
                    {
                        maxScore = score;
                    }
                }
            }
            return maxScore.ToString();
        }
    }
}
