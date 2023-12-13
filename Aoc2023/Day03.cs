using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/3
    public class Day03 : IAocDay
    {
        private string[] grid;

        public Day03(string input)
        {
            grid = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        }

        private char GetTile(VectorRC coord)
        {
            if (coord.Row < 0 || coord.Row >= grid.Length)
            {
                return '.';
            }
            if (coord.Col < 0 || coord.Col >= grid[coord.Row].Length)
            {
                return '.';
            }
            return grid[coord.Row][coord.Col];
        }

        public long Part1()
        {
            int sum = 0;
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\d+");
                foreach (var match in matches)
                {
                    bool foundSymbol = false;
                    for (int i = 0; i < match.Length && !foundSymbol; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        if (coord.EightNeighbors().Select(GetTile).Any(c => !char.IsDigit(c) && c != '.'))
                        {
                            sum += int.Parse(grid[row].AsSpan().Slice(match.Index, match.Length));
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
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\d+");
                foreach (var match in matches)
                {
                    VectorRC start = new VectorRC(row, match.Index);
                    int value = int.Parse(grid[row].AsSpan().Slice(match.Index, match.Length));
                    for (int i = 0; i < match.Length; i++)
                    {
                        VectorRC coord = new VectorRC(row, match.Index + i);
                        gridNumbers[coord] = (start, value);
                    }
                }
            }

            int sum = 0;
            for (int row = 0; row < grid.Length; row++)
            {
                var matches = Regex.EnumerateMatches(grid[row], @"\*");
                foreach (var match in matches)
                {
                    VectorRC coord = new VectorRC(row, match.Index);
                    var adjacentNumbers = coord.EightNeighbors().Where(gridNumbers.ContainsKey).Select(c => gridNumbers[c]).Distinct().ToList();
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
