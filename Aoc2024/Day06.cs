using AocCommon;
using System.Diagnostics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/6
    // --- Day 6: Guard Gallivant ---
    public class Day06(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly string[] inputLines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

        private static (HashSet<(VectorRC Pos, VectorRC Dir)> Path, bool IsLoop) Simulate(string[] maze)
        {
            Grid grid = new(maze, OUTSIDE);
            var startPos = grid.Iterate().Where(x => x.Value == '^').Select(x => x.Position).Single();
            var startDir = VectorRC.Up;

            HashSet<(VectorRC Pos, VectorRC Dir)> visited = new();
            VectorRC pos = startPos;
            VectorRC dir = startDir;
            bool isLoop = false;
            while (true)
            {
                if (OUTSIDE == grid.Get(pos))
                {
                    isLoop = false;
                    break;
                }
                if (!visited.Add((pos, dir))) // Add returns false if the item is already present
                {
                    isLoop = true;
                    break;
                }
                if (grid.Get(pos+dir) == '#')
                {
                    dir = dir.RotatedRight();
                }
                else
                {
                    pos += dir;
                }
            }
            return (visited, isLoop);
        }

        public string Part1()
        {
            var result = Simulate(inputLines);
            Debug.Assert(!result.IsLoop);
            var visited = result.Path.Select(x => x.Pos).Distinct().Count();
            return visited.ToString();
        }

        public string Part2()
        {
            Grid initialGrid = new(inputLines, OUTSIDE);
            var startPos = initialGrid.Iterate().Where(x => x.Value == '^').Select(x => x.Position).Single();
            var unobstructedPath = Simulate(inputLines).Path.Select(x => x.Pos).Where(x => x != startPos).Distinct().ToList();
            long obstructions = 0;
            Parallel.ForEach(unobstructedPath, step =>
            {
                string[] newMaze = initialGrid.Data.ToArray();
                char[] newRowBuffer = newMaze[step.Row].ToCharArray();
                newRowBuffer[step.Col] = '#';
                newMaze[step.Row] = new string(newRowBuffer);
                var result = Simulate(newMaze);
                if (result.IsLoop)
                {
                    Interlocked.Increment(ref obstructions);
                }
            });
            return obstructions.ToString();
        }
    }
}
