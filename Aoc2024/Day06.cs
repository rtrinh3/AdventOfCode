using AocCommon;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/6
    // --- Day 6: Guard Gallivant ---
    public class Day06 : IAocDay
    {
        private readonly int width;
        private readonly int height;
        private readonly VectorRC startPos;
        private readonly ImmutableHashSet<VectorRC> inputObstacles;

        public Day06(string input)
        {
            Grid grid = new(input, '\0');
            width = grid.Width;
            height = grid.Height;
            startPos = grid.Iterate().Single(x => x.Value == '^').Position;
            inputObstacles = grid.Iterate().Where(x => x.Value == '#').Select(x => x.Position).ToImmutableHashSet();
        }

        private (HashSet<(VectorRC Pos, VectorRC Dir)> Path, bool IsLoop) Simulate(ImmutableHashSet<VectorRC> obstacles)
        {
            var startDir = VectorRC.Up;

            HashSet<(VectorRC Pos, VectorRC Dir)> visited = new();
            VectorRC pos = startPos;
            VectorRC dir = startDir;
            bool isLoop = false;
            while (true)
            {
                if (pos.Row < 0 || pos.Col < 0 || pos.Row >= height || pos.Col >= width)
                {
                    isLoop = false;
                    break;
                }
                if (!visited.Add((pos, dir))) // Add returns false if the item is already present
                {
                    isLoop = true;
                    break;
                }
                if (obstacles.Contains(pos+dir))
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
            var result = Simulate(inputObstacles);
            Debug.Assert(!result.IsLoop);
            var visited = result.Path.Select(x => x.Pos).Distinct().Count();
            return visited.ToString();
        }

        public string Part2()
        {
            var unobstructedPath = Simulate(inputObstacles).Path.Select(x => x.Pos).Where(x => x != startPos).Distinct().ToList();
            long obstructions = 0;
            Parallel.ForEach(unobstructedPath, step =>
            {
                var newObstacles = inputObstacles.Add(step);
                var result = Simulate(newObstacles);
                if (result.IsLoop)
                {
                    Interlocked.Increment(ref obstructions);
                }
            });
            return obstructions.ToString();
        }
    }
}
