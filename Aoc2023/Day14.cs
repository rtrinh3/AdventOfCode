using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/14
    public class Day14(string input) : IAocDay
    {
        public long Part1()
        {
            char[,] grid = TextToGrid(input);
            RollRocksNorth(grid);
            var answer = CalculateLoad(grid);
            return answer;
        }

        public long Part2()
        {
            char[,] grid = TextToGrid(input);
            var seen = new Dictionary<string, int>();
            var seenList = new List<string>();
            const int ITERATIONS = 1_000_000_000;
            for (int i = 0; i < ITERATIONS; i++)
            {
                string save = SerializeGrid(grid);
                if (seen.TryGetValue(save, out int loopStart))
                {
                    // Found cycle
                    int loopLength = i - loopStart;
                    string finalState = seenList[(ITERATIONS - loopStart) % loopLength + loopStart];
                    char[,] finalGrid = TextToGrid(finalState);
                    int finalLoad = CalculateLoad(finalGrid);
                    return finalLoad;
                }
                else
                {
                    seen.Add(save, i);
                    seenList.Add(save);
                }
                Cycle(grid);
            }
            // Found out the hard way
            return CalculateLoad(grid);
        }

        private static char[,] TextToGrid(string text)
        {
            string[] lines = text.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            char[,] grid = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }
            return grid;
        }

        private static int CalculateLoad(char[,] grid)
        {
            int load = 0;
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (grid[row, col] == 'O')
                    {
                        load += (height - row);
                    }
                }
            }
            return load;
        }

        private static void RollRocksNorth(char[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int landing = 0;
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    if (grid[row, col] == 'O')
                    {
                        grid[row, col] = '.';
                        grid[landing, col] = 'O';
                        landing++;
                    }
                    else if (grid[row, col] == '#')
                    {
                        landing = row + 1;
                    }
                }
            }
        }

        private static void RollRocksWest(char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                int landing = 0;
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    if (grid[row, col] == 'O')
                    {
                        grid[row, col] = '.';
                        grid[row, landing] = 'O';
                        landing++;
                    }
                    else if (grid[row, col] == '#')
                    {
                        landing = col + 1;
                    }
                }
            }
        }

        private static void RollRocksSouth(char[,] grid)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                int landing = grid.GetLength(0) - 1;
                for (int row = grid.GetLength(0) - 1; row >= 0; row--)
                {
                    if (grid[row, col] == 'O')
                    {
                        grid[row, col] = '.';
                        grid[landing, col] = 'O';
                        landing--;
                    }
                    else if (grid[row, col] == '#')
                    {
                        landing = row - 1;
                    }
                }
            }
        }

        private static void RollRocksEast(char[,] grid)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                int landing = grid.GetLength(1) - 1;
                for (int col = grid.GetLength(1) - 1; col >= 0; col--)
                {
                    if (grid[row, col] == 'O')
                    {
                        grid[row, col] = '.';
                        grid[row, landing] = 'O';
                        landing--;
                    }
                    else if (grid[row, col] == '#')
                    {
                        landing = col - 1;
                    }
                }
            }
        }

        private static void Cycle(char[,] grid)
        {
            RollRocksNorth(grid);
            RollRocksWest(grid);
            RollRocksSouth(grid);
            RollRocksEast(grid);
        }

        private static string SerializeGrid(char[,] grid)
        {
            var buf = new StringBuilder();
            int height = grid.GetLength(0);
            int width = grid.GetLength(1);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    buf.Append(grid[row, col]);
                }
                buf.AppendLine();
            }
            return buf.ToString();
        }
    }
}
