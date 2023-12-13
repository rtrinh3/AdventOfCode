using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://adventofcode.com/2023/day/2

namespace Aoc2023
{
    public class Day02 : IAocDay
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

                Dictionary<string, int> gameMinBalls = new()
                {
                    ["red"] = 0,
                    ["green"] = 0,
                    ["blue"] = 0,
                };
                // No need to distinguish between sets
                string[] balls = gameParts[1].Split(new char[] { ';', ',' }, TrimAndDiscard);
                foreach (string ball in balls)
                {
                    string[] ballParts = ball.Split(' ', TrimAndDiscard);
                    int ballCount = int.Parse(ballParts[0]);
                    string ballColor = ballParts[1];
                    if (gameMinBalls[ballColor] < ballCount)
                    {
                        gameMinBalls[ballColor] = ballCount;
                    }
                }
                games[i] = gameMinBalls;
            }
        }

        public long Part1()
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
                    if (maxForColor.Value < game[maxForColor.Key])
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

        public long Part2()
        {
            return games.Sum(game => game["red"] * game["green"] * game["blue"]);
        }
    }
}
