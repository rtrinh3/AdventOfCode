using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/8
    public class Day08 : IAocDay
    {
        private readonly string directions;
        private readonly Dictionary<string, (string Left, string Right)> map;
        public Day08(string input)
        {
            directions = input.Split('\n')[0].Trim();
            map = Regex.Matches(input, @"(\w+) = \((\w+), (\w+)\)").Cast<Match>()
                .ToDictionary(match => match.Groups[1].Value, match => (match.Groups[2].Value, match.Groups[3].Value));
        }

        private int FindPathLength(string start, Predicate<string> isEnd)
        {
            string node = start;
            int i = 0;
            while (!isEnd(node))
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

        public long Part1()
        {
            return FindPathLength("AAA", node => node == "ZZZ");
        }

        public long Part2()
        {
            string[] starts = map.Keys.Where(k => k.EndsWith('A')).ToArray();
            // Hack: returning the LCM of the length of the paths is good enough.
            // As seen on https://www.reddit.com/r/adventofcode/comments/18did3d/2023_day_8_part_1_my_input_maze_plotted_using/ ,
            // the paths are cycles that bring the ghosts back to the beginning right after they reach their destinations,
            // and it doesn't matter whether they go left or right at each step.
            var paths = starts.Select(start => FindPathLength(start, n => n.EndsWith('Z')));
            var commonMultipleOfPaths = paths.Aggregate(1L, (acc, path) => Lcm(acc, path));
            return commonMultipleOfPaths;
        }

        // https://en.wikipedia.org/wiki/Euclidean_algorithm#Implementations
        private static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        // https://en.wikipedia.org/wiki/Least_common_multiple#Using_the_greatest_common_divisor
        private static long Lcm(long a, long b)
        {
            return (a * b) / Gcd(a, b);
        }

        public long Part2BruteForce()
        {
            string[] nodes = map.Keys.Where(k => k.EndsWith('A')).ToArray();
            long dir = 0; // int overflowed in 7 minutes
            // The actual answer has 14 digits; clearly, brute force isn't going to cut it
            bool solved = false;
            Task.Run(() =>
            {
                while (!solved)
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
            solved = true;
            Console.WriteLine("Done!\a"); // Make a sound
            return dir;
        }
    }
}
