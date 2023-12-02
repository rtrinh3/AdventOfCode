using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    public class Day02
    {
        private Dictionary<string, int>[] games;

        public Day02(string input)
        {
            const StringSplitOptions TrimAndDiscard = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;
            string[] lines = input.Split('\n', TrimAndDiscard);
            games = new Dictionary<string, int>[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] gameParts = lines[i].Split(':', TrimAndDiscard);
                string[] header = gameParts[0].Split(' ', TrimAndDiscard);
                Debug.Assert(header[0] == "Game" && header[1] == (i + 1).ToString());

                Dictionary<string, int> gameMinBalls = new();
                string[] sets = gameParts[1].Split(';', TrimAndDiscard);
                foreach (string set in sets)
                {
                    string[] balls = set.Split(',', TrimAndDiscard);
                    foreach (string ball in balls)
                    {
                        string[] ballParts = ball.Split(' ', TrimAndDiscard);
                        int ballCount = int.Parse(ballParts[0]);
                        string ballColor = ballParts[1];
                        if (gameMinBalls.GetValueOrDefault(ballColor) < ballCount)
                        {
                            gameMinBalls[ballColor] = ballCount;
                        }
                    }
                }
                games[i] = gameMinBalls;
            }
        }

        public int Part1()
        {
            int sumOfIds = 0;
            Dictionary<string, int> maxAllowedScores = new()
            {
                ["red"] = 12,
                ["green"] = 13,
                ["blue"] = 14,
            };
            for (int i = 0; i < games.Length; i++)
            {
                var game = games[i];
                bool isPossible = true;
                foreach (var maxForColor in maxAllowedScores)
                {
                    if (maxForColor.Value < game.GetValueOrDefault(maxForColor.Key))
                    {
                        isPossible = false;
                        break;
                    }
                }
                if (isPossible)
                {
                    sumOfIds += (i + 1);
                }
            }
            return sumOfIds;
        }

        public int Part2()
        {
            return games.Sum(game => game.GetValueOrDefault("red") * game.GetValueOrDefault("green") * game.GetValueOrDefault("blue"));
        }
    }
}
