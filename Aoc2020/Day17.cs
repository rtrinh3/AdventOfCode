using AocCommon;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/17
    // --- Day 17: Conway Cubes ---
    public class Day17 : IAocDay
    {
        private readonly ImmutableArray<VectorRC> initialCells;
        private readonly int rows;
        private readonly int columns;

        public Day17(string input)
        {
            List<VectorRC> cells = new();
            var lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    if (lines[row][col] == '#')
                    {
                        cells.Add(new(row, col));
                    }
                }
            }
            initialCells = cells.ToImmutableArray();
            rows = lines.Length;
            columns = lines[0].Length;
        }

        public long Part1()
        {
            HashSet<VectorXYZ> activeCells = initialCells.Select(rc => new VectorXYZ(rc.Row, rc.Col, 0)).ToHashSet();
            int minX = 0;
            int maxX = rows - 1;
            int minY = 0;
            int maxY = columns - 1;
            int minZ = 0;
            int maxZ = 0;
            for (int cycle = 0; cycle < 6; cycle++)
            {
                HashSet<VectorXYZ> newActiveCells = new();
                for (int x = minX - cycle - 1; x <= maxX + cycle + 1; x++)
                {
                    for (int y = minY - cycle - 1; y <= maxY + cycle + 1; y++)
                    {
                        for (int z = minZ - cycle - 1; z <= maxZ + cycle + 1; z++)
                        {
                            VectorXYZ pos = new(x, y, z);
                            int activeNeighbors = Next26(pos).Count(activeCells.Contains);
                            if (activeCells.Contains(pos))
                            {
                                if (activeNeighbors == 2 || activeNeighbors == 3)
                                {
                                    newActiveCells.Add(pos);
                                }
                            }
                            else
                            {
                                if (activeNeighbors == 3)
                                {
                                    newActiveCells.Add(pos);
                                }
                            }
                        }
                    }
                }
                activeCells = newActiveCells;
            }
            return activeCells.Count;
        }

        private static IEnumerable<VectorXYZ> Next26(VectorXYZ vec)
        {
            var next = from x in Enumerable.Range(-1, 3)
                       from y in Enumerable.Range(-1, 3)
                       from z in Enumerable.Range(-1, 3)
                       where !(x == 0 && y == 0 && z == 0)
                       select vec + new VectorXYZ(x, y, z);
            return next;
        }

        public long Part2()
        {
            HashSet<(int, int, int, int)> activeCells = initialCells.Select(rc => (rc.Row, rc.Col, 0, 0)).ToHashSet();
            int minW = 0;
            int maxW = rows - 1;
            int minX = 0;
            int maxX = columns - 1;
            int minY = 0;
            int maxY = 0;
            int minZ = 0;
            int maxZ = 0;
            for (int cycle = 0; cycle < 6; cycle++)
            {
                HashSet<(int, int, int, int)> newActiveCells = new();
                for (int w = minW - cycle - 1; w <= maxW + cycle + 1; w++)
                {
                    for (int x = minX - cycle - 1; x <= maxX + cycle + 1; x++)
                    {
                        for (int y = minY - cycle - 1; y <= maxY + cycle + 1; y++)
                        {
                            for (int z = minZ - cycle - 1; z <= maxZ + cycle + 1; z++)
                            {
                                var pos = (w, x, y, z);
                                int activeNeighbors = Next80(pos).Count(activeCells.Contains);
                                if (activeCells.Contains(pos))
                                {
                                    if (activeNeighbors == 2 || activeNeighbors == 3)
                                    {
                                        newActiveCells.Add(pos);
                                    }
                                }
                                else
                                {
                                    if (activeNeighbors == 3)
                                    {
                                        newActiveCells.Add(pos);
                                    }
                                }
                            }
                        }
                    }
                }
                activeCells = newActiveCells;
            }
            return activeCells.Count;
        }

        private static IEnumerable<(int, int, int, int)> Next80((int, int, int, int) vec)
        {
            var next = from w in Enumerable.Range(-1, 3)
                       from x in Enumerable.Range(-1, 3)
                       from y in Enumerable.Range(-1, 3)
                       from z in Enumerable.Range(-1, 3)
                       where !(w == 0 && x == 0 && y == 0 && z == 0)
                       select (w + vec.Item1, x + vec.Item2, y + vec.Item3, z + vec.Item4);
            return next;
        }
    }
}
