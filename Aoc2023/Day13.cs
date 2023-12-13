using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/13
    public class Day13(string input): IAocDay
    {
        string[][] maps = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries)
            .Select(paragraph => paragraph.Split('\n'))
            .ToArray();

        public long Part1()
        {
            return DoPuzzle(0);
        }

        public long Part2()
        {
            return DoPuzzle(1);
        }

        private long DoPuzzle(int matchSmudges)
        {
            long sum = 0;
            foreach (var map in maps)
            {
                int vertical = FindVerticalMirror(map, matchSmudges);
                if (vertical > 0)
                {
                    sum += vertical;
                }
                else
                {
                    int horizontal = FindHorizontalMirror(map, matchSmudges);
                    sum += 100 * horizontal;
                }
            }
            return sum;
        }

        private static int FindVerticalMirror(string[] map, int matchSmudges)
        {
            int height = map.Length;
            int width = map[0].Length;
            for (int mirror = 1; mirror < width; mirror++)
            {
                int smudges = 0;
                int rightSide = width - mirror;
                int reflectionWidth = Math.Min(mirror, rightSide);
                for (int d = 0; d < reflectionWidth && smudges <= matchSmudges; d++)
                {
                    for (int row = 0; row < height && smudges <= matchSmudges; row++)
                    {
                        if (map[row][mirror - 1 - d] != map[row][mirror + d])
                        {
                            smudges++;
                        }
                    }
                }
                if (smudges == matchSmudges)
                {
                    return mirror;
                }
            }
            return 0;
        }

        private static int FindHorizontalMirror(string[] map, int matchSmudges)
        {
            int height = map.Length;
            int width = map[0].Length;
            for (int mirror = 1; mirror < height; mirror++)
            {
                int smudges = 0;
                int bottomSide = height - mirror;
                int reflectionHeight = Math.Min(mirror, bottomSide);
                for (int d = 0; d < reflectionHeight && smudges <= matchSmudges; d++)
                {
                    for (int col = 0; col < width && smudges <= matchSmudges; col++)
                    {
                        if (map[mirror - 1 - d][col] != map[mirror + d][col])
                        {
                            smudges++;
                        }
                    }
                }
                if (smudges == matchSmudges)
                {
                    return mirror;
                }
            }
            return 0;
        }
    }
}
