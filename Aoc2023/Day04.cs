using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/4
    public class Day04 : IAocDay
    {
        private readonly int[] matches;

        public Day04(string input)
        {
            var deck = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
            matches = new int[deck.Length];
            for (int i = 0; i < deck.Length; i++)
            {
                var card = deck[i];
                var headerSplit = card.Split(':');
                var gameSplit = headerSplit[1].Split('|');
                var winners = gameSplit[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var draws = new HashSet<string>(gameSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries));
                matches[i] = winners.Count(w => draws.Contains(w));
            }
        }

        public long Part1()
        {
            return matches.Sum(x => (1 << x) >> 1);
        }

        public long Part2()
        {
            int[] piles = new int[matches.Length];
            Array.Fill(piles, 1);
            for (int cardNumber = 0; cardNumber < piles.Length; cardNumber++)
            {
                for (int following = 0; following < matches[cardNumber]; following++)
                {
                    piles[cardNumber + following + 1] += piles[cardNumber];
                }
            }
            return piles.Sum();
        }
    }
}
