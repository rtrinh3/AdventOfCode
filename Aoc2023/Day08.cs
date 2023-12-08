using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/8
    public class Day08
    {
        private readonly string directions;
        private readonly Dictionary<string, (string Left, string Right)> map;
        public Day08(string input)
        {
            directions = input.Split('\n')[0].Trim();
            map = Regex.Matches(input, @"(\w+) = \((\w+), (\w+)\)").Cast<Match>()
                .ToDictionary(match => match.Groups[1].Value, match => (match.Groups[2].Value, match.Groups[3].Value));
        }

        public int Part1()
        {
            string node = "AAA";
            int i = 0;
            while (node != "ZZZ")
            {
                if (directions[i % directions.Length] == 'L')
                {
                    node = map[node].Left;
                }
                else
                {
                    node = map[node].Right;
                }
                i++;
            }
            return i;
        }

        public long Part2()
        {
            string[] nodes = map.Keys.Where(k => k.EndsWith('A')).ToArray();
            long dir = 0; // int overflowed in 7 minutes
            // Clearly, brute force isn't going to cut it
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine(dir);
                    Console.WriteLine(string.Join(",", nodes));
                    Thread.Sleep(1000);
                }
            });
            while (nodes.Any(n => !n.EndsWith('Z')))
            {
                for (int n = 0; n < nodes.Length; n++)
                {
                    if (directions[(int)(dir % directions.Length)] == 'L')
                    {
                        nodes[n] = map[nodes[n]].Left;
                    }
                    else
                    {
                        nodes[n] = map[nodes[n]].Right;
                    }
                }
                dir++;
            }
            Console.WriteLine("Done!\a"); // Make a sound
            return dir;
        }
    }
}
