using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/10
// --- Day 10: Balance Bots ---
public class Day10(string input) : IAocDay
{
    private class Robot
    {
        private readonly int[] chips = new int[2];
        private int count = 0;
        private readonly Action<int> lowAction;
        private readonly Action<int> highAction;

        public Robot(string instruction, IDictionary<int, Robot> otherRobots, IDictionary<int, List<int>> outputs)
        {
            var parse = Regex.Match(instruction, @"^bot \d+ gives low to (\w+) (\d+) and high to (\w+) (\d+)$");
            int lowIndex = int.Parse(parse.Groups[2].ValueSpan);
            int highIndex = int.Parse(parse.Groups[4].ValueSpan);
            if (parse.Groups[1].Value == "bot")
            {
                lowAction = x => otherRobots[lowIndex].Add(x);
            }
            else if (parse.Groups[1].Value == "output")
            {
                lowAction = x => outputs[lowIndex].Add(x);
            }
            else
            {
                throw new Exception("Parse error: " + instruction);
            }

            if (parse.Groups[3].Value == "bot")
            {
                highAction = x => otherRobots[highIndex].Add(x);
            }
            else if (parse.Groups[3].Value == "output")
            {
                highAction = x => outputs[highIndex].Add(x);
            }
            else
            {
                throw new Exception("Parse error: " + instruction);
            }
        }

        public event Action<int, int> DidComparison;

        public void Add(int chip)
        {
            chips[count] = chip;
            count++;
            if (count == 2)
            {
                Array.Sort(chips);
                DidComparison?.Invoke(chips[0], chips[1]);
                lowAction(chips[0]);
                highAction(chips[1]);
                count = 0;
            }
        }
    }

    private (int Part1Answer, int Part2Answer) DoPuzzle()
    {
        var lines = Parsing.SplitLines(input);
        Dictionary<int, Robot> robots = new();
        DefaultDict<int, List<int>> outputs = new();
        List<(int Chip, int Robot)> handoutInstructions = new();
        int part1answer = 0;
        // Build robots
        foreach (var line in lines)
        {
            if (line.StartsWith("value"))
            {
                var ints = Parsing.IntsPositive(line);
                handoutInstructions.Add((ints[0], ints[1]));
            }
            else if (line.StartsWith("bot"))
            {
                var firstNumber = Regex.Match(line, @"\d+");
                int robotIndex = int.Parse(firstNumber.ValueSpan);
                robots[robotIndex] = new Robot(line, robots, outputs);
                robots[robotIndex].DidComparison += (lo, hi) =>
                {
                    if (lo == 17 && hi == 61)
                    {
                        part1answer = robotIndex;
                    }
                };
            }
        }
        // Handout chips
        foreach (var handout in handoutInstructions)
        {
            robots[handout.Robot].Add(handout.Chip);
        }
        int part2answer = outputs[0][0] * outputs[1][0] * outputs[2][0];

        return (part1answer, part2answer);
    }

    public string Part1()
    {
        var answers = DoPuzzle();
        return answers.Part1Answer.ToString();
    }

    public string Part2()
    {
        var answers = DoPuzzle();
        return answers.Part2Answer.ToString();
    }
}
