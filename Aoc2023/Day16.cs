using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/16
    public class Day16(string input): IAocDay
    {
        private string[] grid = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);

        private char GetTile(VectorRC coord)
        {
            if (coord.Row < 0 || coord.Row >= grid.Length || coord.Col < 0 || coord.Col >= grid[coord.Row].Length)
            {
                return '\0';
            }
            return grid[coord.Row][coord.Col];
        }

        public long Part1()
        {
            return SimulateLight(new VectorRC(0, 0), new VectorRC(0, +1));
        }

        public long Part2()
        {
            long maxEnergy = -1;
            for (int row =  0; row < grid.Length; row++)
            {
                long leftToRight = SimulateLight(new VectorRC(row, 0), new VectorRC(0, +1));
                if (maxEnergy < leftToRight)
                {
                    maxEnergy = leftToRight;
                }
                long rightToLeft = SimulateLight(new VectorRC(row, grid[0].Length - 1), new VectorRC(0, -1));
                if (maxEnergy < rightToLeft)
                {
                    maxEnergy = rightToLeft;
                }
            }
            for (int col = 0; col < grid[0].Length; col++)
            {
                long topToBottom = SimulateLight(new VectorRC(0, col), new VectorRC(+1, 0));
                if (maxEnergy < topToBottom)
                {
                    maxEnergy = topToBottom;
                }
                long bottomToTop = SimulateLight(new VectorRC(grid.Length - 1, col), new VectorRC(-1, 0));
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
                if (seen.Contains(thisPhoton))
                {
                    continue;
                }
                else
                {
                    seen.Add(thisPhoton);
                }
                var (pos, dir) = thisPhoton;

                if (0 <= pos.Row && pos.Row < grid.Length && 0 <= pos.Col && pos.Col < grid[pos.Row].Length)
                {
                    energized.Add(pos);
                }

                char tile = GetTile(pos);
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
                            photons.Push((pos, new VectorRC(+1, 0)));
                            photons.Push((pos, new VectorRC(-1, 0)));
                        }
                        break;
                    case '-':
                        if (dir.Row == 0)
                        {
                            photons.Push((pos + dir, dir));
                        }
                        else
                        {
                            photons.Push((pos, new VectorRC(0, +1)));
                            photons.Push((pos, new VectorRC(0, -1)));
                        }
                        break;
                    case '\0':
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
