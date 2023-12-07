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
            hands = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(line =>
            {
                var parts = line.Split(' ');
                return (parts[0], int.Parse(parts[1]));
            }).ToArray();
        }

        private static int CompareHandsPart1(string a, string b)
        {
            Debug.Assert(a.Length == b.Length);
            // Weaker < Stronger
            var handComparison = EvaluateHand(a).CompareTo(EvaluateHand(b));
            if (handComparison != 0) return handComparison;
            const string CARD_ORDER = "23456789TJQKA";
            for (int i = 0; i < a.Length; i++)
            {
                int cardComparison = CARD_ORDER.IndexOf(a[i]).CompareTo(CARD_ORDER.IndexOf(b[i]));
                if (cardComparison != 0) return cardComparison;
            }
            // No tiebreakers
            return 0;
        }

        private static int EvaluateHand(string hand)
        {
            var groups = hand.GroupBy(c => c).Select(g => (g.Key, g.Count())).OrderByDescending(g => g.Item2).ToArray();
            if (groups[0].Item2 >= 5)
            {
                return 6;
            }
            else if (groups[0].Item2 == 4)
            {
                return 5;
            }
            else if (groups[0].Item2 == 3 && groups[1].Item2 == 2)
            {
                return 4;
            }
            else if (groups[0].Item2 == 3 && groups[1].Item2 == 1)
            {
                return 3;
            }
            else if (groups[0].Item2 == 2 && groups[1].Item2 == 2)
            {
                return 2;
            }
            else if (groups[0].Item2 == 2 && groups[1].Item2 == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Part1()
        {
            var partOneHands = hands.ToArray();
            Array.Sort(partOneHands, (a, b) => CompareHandsPart1(a.hand, b.hand));
            int winnings = 0;
            for (int i = 0; i < partOneHands.Length; i++)
            {
                winnings += (i + 1) * partOneHands[i].bid;
            }
            return winnings;
        }

        private static int EvaluateHandWithJokers(string hand)
        {
            if (hand == "JJJJJ")
            {
                return EvaluateHand(hand);
            }
            else
            {
                // Replacing the jokers with the most common non-joker generates the best hand:
                // 4 jokers: the hand becomes a five-of-a-kind
                // 3 jokers: the hand becomes either a four-of-a-kind or a five-of-a-kind
                // 2 jokers:
                // - if the other cards are ABC, this creates a three-of-a-kind
                // - if the other cards are AAB, replacing with A creates a four-of-a-kind; this is better than replacing with B which creates a full-house
                // 1 joker:
                // - if the other cards are ABCD, this creates one-pair, which is better than a high-card
                // - if the other cards are AABC, this creates a three-of-a-kind, which is better than a two-pair
                // - if the other cards are AABB, this creates a full-house
                // - if the other cards are AAAB, replacing with A creates a four-of-a-kind; this is better than replacing with B which creates a full-house
                char mostCommonNonJoker = hand.Where(c => c != 'J').GroupBy(c => c).MaxBy(g => g.Count()).Key;
                string handWithSubstitutions = hand.Replace('J', mostCommonNonJoker);
                return EvaluateHand(handWithSubstitutions);
            }
        }

        private static int CompareHandsWithJokers(string a, string b)
        {
            Debug.Assert(a.Length == b.Length);
            // Weaker < Stronger
            var handComparison = EvaluateHandWithJokers(a).CompareTo(EvaluateHandWithJokers(b));
            if (handComparison != 0) return handComparison;
            const string CARD_ORDER = "J23456789TQKA";
            for (int i = 0; i < a.Length; i++)
            {
                int cardComparison = CARD_ORDER.IndexOf(a[i]).CompareTo(CARD_ORDER.IndexOf(b[i]));
                if (cardComparison != 0) return cardComparison;
            }
            // No tiebreakers
            return 0;
        }

        public int Part2()
        {
            var partTwoHands = hands.ToArray();
            Array.Sort(partTwoHands, (a, b) => CompareHandsWithJokers(a.hand, b.hand));
            int winnings = 0;
            for (int i = 0; i < partTwoHands.Length; i++)
            {
                winnings += (i + 1) * partTwoHands[i].bid;
            }
            return winnings;
        }
    }
}
