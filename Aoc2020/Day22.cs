using AocCommon;

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

        public string Part1()
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
            int score = 0;
            for (int i = 0; i < winningHand.Length; i++)
            {
                score += winningHand[i] * (winningHand.Length - i);
            }
            return score.ToString();
        }

        public string Part2()
        {
            var topLevelGame = RecursiveGame(player1Init, player2Init);
            return topLevelGame.Score.ToString();
        }

        private static (bool Player1Wins, int Score) RecursiveGame(int[] player1Deck, int[] player2Deck)
        {
            HashSet<(EquatableArray<int>, EquatableArray<int>)> rounds = new();
            bool tieBreaker = false;
            var player1 = new Queue<int>(player1Deck);
            var player2 = new Queue<int>(player2Deck);
            while (player1.Count > 0 && player2.Count > 0)
            {
                var state = (new EquatableArray<int>(player1), new EquatableArray<int>(player2));
                if (!rounds.Add(state)) // Add returns false if already present
                {
                    tieBreaker = true;
                    break;
                }
                int card1 = player1.Dequeue();
                int card2 = player2.Dequeue();
                bool roundWinner;
                if (card1 <= player1.Count && card2 <= player2.Count)
                {
                    // Recurse
                    int[] player1DeckRecurse = player1.Take(card1).ToArray();
                    int[] player2DeckRecurse = player2.Take(card2).ToArray();
                    roundWinner = RecursiveGame(player1DeckRecurse, player2DeckRecurse).Player1Wins;
                }
                else
                {
                    roundWinner = card1 > card2;
                }
                if (roundWinner)
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
            bool gameWinner = tieBreaker || player1.Count > player2.Count;
            var winningHand = gameWinner ? player1.ToArray() : player2.ToArray();
            int score = 0;
            for (int i = 0; i < winningHand.Length; i++)
            {
                score += winningHand[i] * (winningHand.Length - i);
            }
            return (gameWinner, score);
        }
    }
}
