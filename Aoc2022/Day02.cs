namespace Aoc2022
{
    public class Day02(string input) : IAocDay
    {
        private string[] lines = input.ReplaceLineEndings("\n").Split('\n', AocCommon.Constants.TrimAndDiscard);
        public string Part1()
        {
            int[,] scoreMatrix = {
                {3, 6, 0},
                {0, 3, 6},
                {6, 0, 3}
            };
            int[] playScore = { 1, 2, 3 };
            int scoreTotal = 0;
            foreach (string line in lines)
            {
                int opponentMove = (int)line[0] - (int)'A';
                int ownMove = (int)line[2] - (int)'X';
                int score = playScore[ownMove] + scoreMatrix[opponentMove, ownMove];
                scoreTotal += score;
            }
            return scoreTotal.ToString();
        }
        public string Part2()
        {
            int[,] scoreMatrix = {
                {0+3, 3+1, 6+2},
                {0+1, 3+2, 6+3},
                {0+2, 3+3, 6+1}
            };
            int scoreTotal = 0;
            foreach (string line in lines)
            {
                int opponentMove = (int)line[0] - (int)'A';
                int ownMove = (int)line[2] - (int)'X';
                int score = scoreMatrix[opponentMove, ownMove];
                scoreTotal += score;
            }
            return scoreTotal.ToString();
        }
    }
}
