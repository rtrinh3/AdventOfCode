using AocCommon;
using System.Diagnostics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/6
    // --- Day 6: Guard Gallivant ---
    public class Day06(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';        

        private static (HashSet<(VectorRC Pos, VectorRC Dir)> Path, bool IsLoop) Simulate(string maze)
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
                VectorRC nextDir = dir;
                while (true)
                {
                    VectorRC nextPos = pos + nextDir;
                    char nextTile = grid.Get(nextPos);
                    if (nextTile != '#')
                    {
                        break;
                    }
                    nextDir = nextDir.RotatedRight();
                }
                pos = pos + nextDir;
                dir = nextDir;
            }
            return (visited, isLoop);
        }

        public string Part1()
        {
            var result = Simulate(input);
            Debug.Assert(!result.IsLoop);
            var visited = result.Path.Select(x => x.Pos).Distinct().Count();
            return visited.ToString();
        }

        public string Part2()
        {
            long obstructions = 0;
            Parallel.For(0, input.Length, i =>
            {
                if (input[i] != '.')
                {
                    return;
                }
                char[] newMazeBuffer = input.ToCharArray();
                newMazeBuffer[i] = '#';
                string newMaze = new string(newMazeBuffer);
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
