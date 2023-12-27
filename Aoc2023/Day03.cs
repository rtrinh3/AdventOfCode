using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AocCommon;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/3
    public class Day03 : IAocDay
    {
        private readonly Grid grid;

        public Day03(string input)
        {
            grid = new Grid(input, '.');
        }

        public long Part1()
        {
            int sum = 0;
            for (int row = 0; row < grid.Height; row++)
            {
                var matches = Regex.EnumerateMatches(grid.Data[row], @"\d+");
                foreach (var match in matches)
                {
                    bool foundSymbol = false;
                    for (int i = 0; i < match.Length && !foundSymbol; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        if (coord.NextEight().Select(grid.Get).Any(c => !char.IsDigit(c) && c != '.'))
                        {
                            sum += int.Parse(grid.Data[row].AsSpan().Slice(match.Index, match.Length));
                            foundSymbol = true;
                        }
                    }
                }
            }
            return sum;
        }

        public long Part2()
        {
            Dictionary<VectorRC, (VectorRC start, int value)> gridNumbers = new();
            for (int row = 0; row < grid.Height; row++)
            {
                var matches = Regex.EnumerateMatches(grid.Data[row], @"\d+");
                foreach (var match in matches)
                {
                    VectorRC start = new VectorRC(row, match.Index);
                    int value = int.Parse(grid.Data[row].AsSpan().Slice(match.Index, match.Length));
                    for (int i = 0; i < match.Length; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        gridNumbers[coord] = (start, value);
                    }
                }
            }

            int sum = 0;
            for (int row = 0; row < grid.Height; row++)
            {
                var matches = Regex.EnumerateMatches(grid.Data[row], @"\*");
                foreach (var match in matches)
                {
                    VectorRC coord = new VectorRC(row, match.Index);
                    var adjacentNumbers = coord.NextEight().Where(gridNumbers.ContainsKey).Select(c => gridNumbers[c]).Distinct().ToList();
                    if (adjacentNumbers.Count == 2)
                    {
                        sum += adjacentNumbers[0].value * adjacentNumbers[1].value;
                    }
                }
            }
            return sum;
        }
    }
}
