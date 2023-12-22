using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/22
    public class Day22 : IAocDay
    {
        private (VectorXYZ, VectorXYZ)[] bricks;
        public Day22(string input)
        {
            bricks = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(line =>
            {
                string[] coords = line.Split('~');
                string[] coordsA = coords[0].Split(',');
                VectorXYZ pointA = new VectorXYZ(int.Parse(coordsA[0]), int.Parse(coordsA[1]), int.Parse(coordsA[2]));
                string[] coordsB = coords[1].Split(',');
                VectorXYZ pointB = new VectorXYZ(int.Parse(coordsB[0]), int.Parse(coordsB[1]), int.Parse(coordsB[2]));
                return (pointA, pointB);
            }).ToArray();
        }
        public long Part1()
        {
            // If A rests on B, then aRestOnB[A].Contains(B) and aSupportB[B].Contains(A)
            HashSet<int>[] aRestOnB = new HashSet<int>[bricks.Length];
            HashSet<int>[] aSupportB = new HashSet<int>[bricks.Length];
            Dictionary<VectorXY, (int height, int blockNumber)> heightMap = new();
            (VectorXYZ, VectorXYZ)[] orderedBricks = bricks.OrderBy(b => Math.Min(b.Item1.Z, b.Item2.Z)).ToArray();
            for (int i = 0; i < orderedBricks.Length; i++)
            {
                var brick = orderedBricks[i];
                int brickHeight = 1 + Math.Abs(brick.Item1.Z - brick.Item2.Z);
                int xMin = Math.Min(brick.Item1.X, brick.Item2.X);
                int xMax = Math.Max(brick.Item1.X, brick.Item2.X);
                int yMin = Math.Min(brick.Item1.Y, brick.Item2.Y);
                int yMax = Math.Max(brick.Item1.Y, brick.Item2.Y);
                // Find where the brick lands
                int landing = 0;
                aRestOnB[i] = new();
                for (int x = xMin; x <= xMax; x++)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        VectorXY coord = new VectorXY(x, y);
                        if (heightMap.TryGetValue(coord, out var heightData))
                        {
                            if (heightData.height > landing)
                            {
                                landing = heightData.height;
                                aRestOnB[i].Clear();
                                aRestOnB[i].Add(heightData.blockNumber);
                            }
                            else if (heightData.height == landing)
                            {
                                aRestOnB[i].Add(heightData.blockNumber);
                            }
                            // ignore heightData.height < landing
                        }
                    }
                }
                // Update heightMap
                for (int x = xMin; x <= xMax; x++)
                {
                    for (int y = yMin; y <= yMax; y++)
                    {
                        VectorXY coord = new VectorXY(x, y);
                        heightMap[coord] = (landing + brickHeight, i);
                    }
                }
                // Update aSupportB
                aSupportB[i] = new();
                foreach (int supportBrick in aRestOnB[i])
                {
                    aSupportB[supportBrick].Add(i);
                }
            }

            // Find bricks which are safe to disintegrate
            int safeToDisintegrate = 0;
            for (int support = 0; support < aSupportB.Length; support++)
            {
                if (aSupportB[support].Count == 0 || aSupportB[support].All(supported => aRestOnB[supported].Count > 1))
                {
                    safeToDisintegrate++;
                }
            }

            return safeToDisintegrate;
        }
        public long Part2()
        {
            return -2;
        }
    }
}
