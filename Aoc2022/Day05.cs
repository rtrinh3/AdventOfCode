using System.Text.RegularExpressions;

namespace Aoc2022
{
    // // https://adventofcode.com/2022/day/5
    public class Day05 : IAocDay
    {
        private readonly string inputStacks;
        private readonly string inputMoves;
        public Day05(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");
            inputStacks = paragraphs[0][..paragraphs[0].LastIndexOf('\n')];
            inputMoves = paragraphs[1].TrimEnd();
        }
        public string Part1()
        {
            List<Stack<char>> stacks = new List<Stack<char>>();
            Stack<char> GetStack(int i)
            {
                while (stacks.Count <= i)
                {
                    stacks.Add(new Stack<char>());
                }
                return stacks[i];
            }
            // Parse inputStacks into stacks
            foreach (string line in inputStacks.Split('\n'))
            {
                for (int i = 1; i < line.Length; i += 4)
                {
                    if (line[i] != ' ')
                    {
                        GetStack(i / 4).Push(line[i]);
                    }
                }
            }
            // Reversal
            for (int i = 0; i < stacks.Count; ++i)
            {
                stacks[i] = new Stack<char>(stacks[i]);
            }
            // Execute moves
            foreach (string line in inputMoves.Split('\n'))
            {
                var parsed = Regex.Match(line, @"^move (\d+) from (\d+) to (\d+)");
                int quantity = int.Parse(parsed.Groups[1].ValueSpan);
                int origin = int.Parse(parsed.Groups[2].ValueSpan);
                int destination = int.Parse(parsed.Groups[3].ValueSpan);
                for (int i = 0; i < quantity; ++i)
                {
                    GetStack(destination - 1).Push(GetStack(origin - 1).Pop());
                }
            }
            // Peek
            return string.Concat(stacks.Select(s => s.Peek()));
        }
        public string Part2()
        {
            List<List<char>> stacks = new List<List<char>>();
            List<char> GetStack(int i)
            {
                while (stacks.Count <= i)
                {
                    stacks.Add(new List<char>());
                }
                return stacks[i];
            }
            // Parse inputStacks into stacks
            foreach (string line in inputStacks.Split('\n'))
            {
                for (int i = 1; i < line.Length; i += 4)
                {
                    if (line[i] != ' ')
                    {
                        GetStack(i / 4).Add(line[i]);
                    }
                }
            }
            // Reversal
            for (int i = 0; i < stacks.Count; ++i)
            {
                stacks[i].Reverse();
            }
            // Execute moves
            foreach (string line in inputMoves.Split('\n'))
            {
                var parsed = Regex.Match(line, @"^move (\d+) from (\d+) to (\d+)");
                int quantity = int.Parse(parsed.Groups[1].ValueSpan);
                int origin = int.Parse(parsed.Groups[2].ValueSpan);
                int destination = int.Parse(parsed.Groups[3].ValueSpan);
                var originStack = GetStack(origin - 1);
                GetStack(destination - 1).AddRange(originStack.TakeLast(quantity));
                originStack.RemoveRange(originStack.Count - quantity, quantity);
            }
            // Peek
            return string.Concat(stacks.Select(s => s.Last()));
        }
    }
}
