using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/7
    public class Day07
    {
        private readonly (string hand, int bid)[] hands;

        public Day07(string input)
        {
            hands = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line => {
                var parts = line.Split(' ');
                return (parts[0], int.Parse(parts[1]));
            }).ToArray();
        }

        private static int CompareHands(string a, string b) {
            Debug.Assert(a.Length == b.Length);
            // Weaker < Stronger
            var handComparison = EvaluateHand(a).CompareTo(EvaluateHand(b));
            if (handComparison != 0) return handComparison;
            const string CARD_ORDER = "23456789TJQKA";
            for (int i = 0; i < a.Length; i++) {
                int cardComparison = CARD_ORDER.IndexOf(a[i]).CompareTo(CARD_ORDER.IndexOf(b[i]));
                if (cardComparison != 0) return cardComparison;
            }
            // No tiebreakers
            return 0;
        }

        private static int EvaluateHand(string hand) {
            var groups = hand.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToArray();
            if (groups[0].Item2 >= 5) {
                return 6;
            } else if (groups[0].Item2 == 4) {
                return 5;
            } else if (groups[0].Item2 == 3 && groups[1].Item2 == 2) {
                return 4;
            } else if (groups[0].Item2 == 3 && groups[1].Item2 == 1) {
                return 3;
            } else if (groups[0].Item2 == 2 && groups[1].Item2 == 2) {
                return 2;
            } else if (groups[0].Item2 == 2 && groups[1].Item2 == 1) {
                return 1;
            } else {
                return 0;
            }
        }

        public int Part1()
        {
            var partOneHands = hands.ToArray();
            Array.Sort(partOneHands, (a, b) => CompareHands(a.hand, b.hand));
            int winnings = 0;
            for (int i = 0; i < partOneHands.Length; i++) {
                Console.WriteLine($"Rank {i + 1}\tBid {partOneHands[i].bid}\tHand {partOneHands[i].hand}\tType {EvaluateHand(partOneHands[i].hand)}");
                winnings += (i + 1) * partOneHands[i].bid;
            }
            return winnings;
        }

        public int Part2()
        {
            return -2;
        }
    }
}
