﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/16
    public class Day16(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';

        private readonly Grid grid = new Grid(input, OUTSIDE);

        public long Part1()
        {
            return SimulateLight(VectorRC.Zero, VectorRC.Right);
        }

        public long Part2()
        {
            long maxEnergy = -1;
            for (int row = 0; row < grid.Height; row++)
            {
                long leftToRight = SimulateLight(new VectorRC(row, 0), VectorRC.Right);
                if (maxEnergy < leftToRight)
                {
                    maxEnergy = leftToRight;
                }
                long rightToLeft = SimulateLight(new VectorRC(row, grid.Width - 1), VectorRC.Left);
                if (maxEnergy < rightToLeft)
                {
                    maxEnergy = rightToLeft;
                }
            }
            for (int col = 0; col < grid.Width; col++)
            {
                long topToBottom = SimulateLight(new VectorRC(0, col), VectorRC.Down);
                if (maxEnergy < topToBottom)
                {
                    maxEnergy = topToBottom;
                }
                long bottomToTop = SimulateLight(new VectorRC(grid.Height - 1, col), VectorRC.Up);
                if (maxEnergy < bottomToTop)
                {
                    maxEnergy = bottomToTop;
                }
            }
            return maxEnergy;
        }

        private long SimulateLight(VectorRC initialPosition, VectorRC initialDirection)
        {
            HashSet<VectorRC> energized = new();
            Stack<(VectorRC pos, VectorRC dir)> photons = new();
            photons.Push((initialPosition, initialDirection));
            HashSet<(VectorRC pos, VectorRC dir)> seen = new();
            while (photons.Count > 0)
            {
                var thisPhoton = photons.Pop();
                if (!seen.Add(thisPhoton))
                {
                    continue;
                }
                var (pos, dir) = thisPhoton;

                char tile = grid.Get(pos);
                if (tile != OUTSIDE)
                {
                    energized.Add(pos);
                }
                switch (tile)
                {
                    case '.':
                        photons.Push((pos + dir, dir));
                        break;
                    case '/':
                        VectorRC newDirA = new(-dir.Col, -dir.Row);
                        photons.Push((pos + newDirA, newDirA));
                        break;
                    case '\\':
                        VectorRC newDirB = new(dir.Col, dir.Row);
                        photons.Push((pos + newDirB, newDirB));
                        break;
                    case '|':
                        if (dir.Col == 0)
                        {
                            photons.Push((pos + dir, dir));
                        }
                        else
                        {
                            photons.Push((pos, VectorRC.Up));
                            photons.Push((pos, VectorRC.Down));
                        }
                        break;
                    case '-':
                        if (dir.Row == 0)
                        {
                            photons.Push((pos + dir, dir));
                        }
                        else
                        {
                            photons.Push((pos, VectorRC.Left));
                            photons.Push((pos, VectorRC.Right));
                        }
                        break;
                    case OUTSIDE:
                        // OOB, no more photons
                        break;
                    default:
                        throw new Exception("What is this tile " + tile);
                }
            }
            //// Visualization
            //for (int row = 0; row < grid.Length; row++)
            //{
            //    for (int col = 0; col < grid[row].Length; col++)
            //    {
            //        VectorRC pos = new VectorRC(row, col);
            //        if (energized.Contains(pos))
            //        {
            //            Console.Write('#');
            //        }
            //        else
            //        {
            //            Console.Write('.');
            //        }
            //    }
            //    Console.WriteLine();
            //}
            return energized.Count;
        }
    }
}