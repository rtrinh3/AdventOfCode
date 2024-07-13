﻿using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/23
    // --- Day 23: Crab Cups ---
    public class Day23(string input) : IAocDay
    {
        public string Part1()
        {
            Node seed = new() { Value = 0 };
            seed.Next = seed;
            int[] labels = input.Trim().Select(c => c - '0').ToArray();
            int maxLabel = labels.Max();
            Node[] nodes = new Node[labels.Length + 1];
            nodes[0] = seed;
            Node previous = seed;
            foreach (int label in labels)
            {
                Node node = new() { Value = label };
                previous.Next = node;
                nodes[label] = node;
                previous = node;
            }
            previous.Next = nodes[labels[0]]; // Take out the seed and close the loop
            Debug.Assert(nodes.Skip(1).All(x => x != null));

            Node current = nodes[labels[0]];
            for (int move = 0; move < 100; move++)
            {
                // The crab picks up the three cups that are immediately clockwise of the current cup.
                // They are removed from the circle; cup spacing is adjusted as necessary to maintain the circle.
                int[] pickups = new int[3];
                Node n1 = current.Next!;
                pickups[0] = n1.Value;
                Node n2 = n1.Next!;
                pickups[1] = n2.Value;
                Node n3 = n2.Next!;
                pickups[2] = n3.Value;
                current.Next = n3.Next;
                // The crab selects a destination cup: the cup with a label equal to the current cup's label minus one.
                // If this would select one of the cups that was just picked up,
                // the crab will keep subtracting one until it finds a cup that wasn't just picked up.
                // If at any point in this process the value goes below the lowest value on any cup's label,
                // it wraps around to the highest value on any cup's label instead.
                int destination = current.Value;
                while (destination == current.Value || pickups.Contains(destination))
                {
                    destination--;
                    if (destination < 1)
                    {
                        destination = maxLabel;
                    }
                }
                // The crab places the cups it just picked up so that they are immediately clockwise of the destination cup.
                // They keep the same order as when they were picked up.
                Node insertAfter = nodes[destination];
                Node insertBefore = insertAfter.Next!;
                insertAfter.Next = n1;
                n3.Next = insertBefore;
                // The crab selects a new current cup: the cup which is immediately clockwise of the current cup.
                current = current.Next!;
            }

            StringBuilder answerBuilder = new();
            Node answerStart = nodes[1];
            Node answerCurrent = answerStart.Next!;
            while (answerCurrent != answerStart)
            {
                answerBuilder.Append(answerCurrent.Value);
                answerCurrent = answerCurrent.Next!;
            }

            string answer = answerBuilder.ToString();
            return answer;
        }

        private class Node
        {
            public int Value;
            public Node? Next;
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
