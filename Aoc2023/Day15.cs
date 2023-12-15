using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/15
    public class Day15(string input) : IAocDay
    {
        private string[] steps = input.ReplaceLineEndings("").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        private static int Hash(string s)
        {
            int currentValue = 0;
            foreach (char c in s)
            {
                currentValue = ((currentValue + (int)c) * 17) % 256;
            }
            return currentValue;
        }

        public long Part1()
        {
            return steps.Sum(Hash);
        }

        public long Part2()
        {
            LinkedList<(string, int)>[] boxes = Enumerable.Range(0, 256).Select(i => new LinkedList<(string, int)>()).ToArray();
            foreach (string step in steps)
            {
                if (step.Contains('-'))
                {
                    string label = step[0..^1];
                    int boxNumber = Hash(label);
                    var box = boxes[boxNumber];
                    var node = box.First;
                    while (node != null)
                    {
                        if (node.Value.Item1 == label)
                        {
                            box.Remove(node);
                            break;
                        }
                        node = node.Next;
                    }
                }
                else if (step.Contains('='))
                {
                    var parts = step.Split('=');
                    string label = parts[0];
                    int focal = int.Parse(parts[1]);
                    int boxNumber = Hash(label);
                    var box = boxes[boxNumber];
                    var node = box.First;
                    while (node != null)
                    {
                        if (node.Value.Item1 == label)
                        {
                            node.Value = (label, focal);
                            break;
                        }
                        node = node.Next;
                    }
                    if (node == null)
                    {
                        box.AddLast((label, focal));
                    }
                }
                else
                {
                    throw new Exception(step);
                }
            }
            int answer = boxes.Select((list, boxNumber) =>
                (boxNumber + 1) * list.Select((lens, lensIndex) =>
                    (lensIndex + 1) * lens.Item2
                ).Sum()
            ).Sum();
            return answer;
        }
    }
}
