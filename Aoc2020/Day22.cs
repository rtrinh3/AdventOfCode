namespace Aoc2020
{
    // https://adventofcode.com/2020/day/22
    // --- Day 22: Crab Combat ---
    public class Day22 : IAocDay
    {
        private readonly int[] player1Init;
        private readonly int[] player2Init;

        public Day22(string input)
        {
            var split = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            player1Init = split[0].Split('\n').Skip(1).Select(int.Parse).ToArray();
            player2Init = split[1].Split('\n').Skip(1).Select(int.Parse).ToArray();
        }

        public long Part1()
        {
            var player1 = new Queue<int>(player1Init);
            var player2 = new Queue<int>(player2Init);
            while (player1.Count > 0 && player2.Count > 0)
            {
                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();
                if (card1 > card2)
                {
                    player1.Enqueue(card1);
                    player1.Enqueue(card2);
                }
                else
                {
                    player2.Enqueue(card2);
                    player2.Enqueue(card1);
                }
            }
            var winningHand = player1.Count > player2.Count ? player1.ToArray() : player2.ToArray();
            long score = 0;
            for (int i = 0; i < winningHand.Length; i++)
            {
                score += Math.BigMul(winningHand[i], (winningHand.Length - i));
            }
            return score;
        }

        public long Part2()
        {
            throw new NotImplementedException();
        }
    }
}
