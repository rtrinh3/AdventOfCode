using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/10
    public class Day10
    {
        private readonly string[] maze;
        private readonly Dictionary<VectorRC, int> loopDistances;
        private readonly VectorRC startCoord;

        public Day10(string input)
        {
            maze = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);

            for (int row = 0; row < maze.Length; row++)
            {
                for (int col = 0; col < maze[row].Length; col++)
                {
                    if (maze[row][col] == 'S')
                    {
                        startCoord = new(row, col);
                        goto FOUND_START;
                    }
                }
            }
            throw new Exception("Did not find starting point");
        FOUND_START:
            Debug.Assert(GetConnections(startCoord).Length == 2);
            var bfsResult = GraphAlgos.BfsToAll(startCoord, GetConnections);
            loopDistances = bfsResult.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.distance);
        }

        private char GetTile(VectorRC coord)
        {
            if (coord.Row < 0 || coord.Row >= maze.Length)
            {
                return '.';
            }
            if (coord.Col < 0 || coord.Col >= maze[coord.Row].Length)
            {
                return '.';
            }
            return maze[coord.Row][coord.Col];
        }

        private VectorRC[] GetConnections(VectorRC coord)
        {
            return GetTile(coord) switch
            {
                '|' => [coord + (+1, 0), coord + (-1, 0)],
                '-' => [coord + (0, +1), coord + (0, -1)],
                'L' => [coord + (-1, 0), coord + (0, +1)],
                'J' => [coord + (-1, 0), coord + (0, -1)],
                '7' => [coord + (+1, 0), coord + (0, -1)],
                'F' => [coord + (+1, 0), coord + (0, +1)],
                '.' => [],
                'S' => coord.FourNeighbors().Where(neighbor => GetConnections(neighbor).Contains(coord)).ToArray(),
                _ => throw new Exception($"What tile is this!? {GetTile(coord)} at {coord}")
            };
        }


        public int Part1()
        {
            int maxDistance = loopDistances.Values.Max();
            var countMaxDistance = loopDistances.Values.Count(d => d == maxDistance);
            Debug.Assert(countMaxDistance == 1 || countMaxDistance == 2);
            return maxDistance;
        }

        public int Part2()
        {
            // Prepare startActualShape and GetTilePlainStart
            char startActualShape;
            var startConnections = GetConnections(startCoord);
            if (startConnections.Contains(startCoord + (+1, 0)) && startConnections.Contains(startCoord + (-1, 0)))
            {
                startActualShape = '|';
            }
            else if (startConnections.Contains(startCoord + (0, +1)) && startConnections.Contains(startCoord + (0, -1)))
            {
                startActualShape = '-';
            }
            else if (startConnections.Contains(startCoord + (-1, 0)) && startConnections.Contains(startCoord + (0, +1)))
            {
                startActualShape = 'L';
            }
            else if (startConnections.Contains(startCoord + (-1, 0)) && startConnections.Contains(startCoord + (0, -1)))
            {
                startActualShape = 'J';
            }
            else if (startConnections.Contains(startCoord + (+1, 0)) && startConnections.Contains(startCoord + (0, -1)))
            {
                startActualShape = '7';
            }
            else if (startConnections.Contains(startCoord + (+1, 0)) && startConnections.Contains(startCoord + (0, +1)))
            {
                startActualShape = 'F';
            }
            else
            {
                throw new Exception("What is this start?!");
            }
            char GetTilePlainStart(VectorRC coord)
            {
                return (coord == startCoord) ? startActualShape : GetTile(coord);
            }

            // From https://www.reddit.com/r/adventofcode/comments/18eza5g/2023_day_10_animated_visualization/
            // We will find the spaces inside the loop by scanning the grid row by row,
            // and keeping track of whether we're inside the loop by flipping the inLoop variable
            // every time the loop touches our ray from the bottom.
            // (We could also flip it when the loop touches from the top;
            // the number of times it touches from the top versus from the bottom
            // should differ by an even number, so it shouldn't affect calculations.)
            // (Flipping a boolean works because the loop doesn't cross over itself.
            // If it did, we'd have to track the winding number instead.)
            HashSet<VectorRC> enclosedTiles = new();
            var minRow = loopDistances.Keys.Min(c => c.Row);
            var maxRow = loopDistances.Keys.Max(c => c.Row);
            var minCol = loopDistances.Keys.Min(c => c.Col);
            var maxCol = loopDistances.Keys.Max(c => c.Col);
            for (int row = minRow; row <= maxRow; row++)
            {
                bool inLoop = false;
                for (int col = minCol; col <= maxCol; col++)
                {
                    var coord = new VectorRC(row, col);
                    if (loopDistances.ContainsKey(coord))
                    {
                        char loopTile = GetTilePlainStart(coord);
                        // if loop touches from the bottom
                        if (loopTile == '|' || loopTile == '7' || loopTile == 'F')
                        {
                            inLoop = !inLoop;
                        }
                    }
                    else
                    {
                        if (inLoop)
                        {
                            enclosedTiles.Add(coord);
                        }
                    }
                }
            }

            //// Visualisation
            //Console.WriteLine($"Start is {startActualShape}");
            //for (int row = 0; row < maze.Length; row++)
            //{
            //    for (int col = 0; col < maze[row].Length; col++)
            //    {
            //        var coord = new VectorRC(row, col);
            //        if (loopDistances.ContainsKey(coord))
            //        {
            //            if (coord == startCoord)
            //            {
            //                Console.ForegroundColor = ConsoleColor.Green;
            //            }
            //            else
            //            {
            //                Console.ResetColor();
            //            }
            //            char tile = GetTilePlainStart(coord);
            //            char toDraw = tile switch
            //            {
            //                '|' => '║',
            //                '-' => '═',
            //                'L' => '╚',
            //                'J' => '╝',
            //                '7' => '╗',
            //                'F' => '╔',
            //                _ => throw new Exception($"This tile {tile} can't be part of the loop?!")
            //            };
            //            Console.Write(toDraw);
            //        }
            //        else if (enclosedTiles.Contains(coord))
            //        {
            //            Console.ForegroundColor = ConsoleColor.Red;
            //            Console.Write('█');
            //        }
            //        else
            //        {
            //            Console.ResetColor();
            //            Console.Write(maze[row][col]);
            //        }
            //    }
            //    Console.WriteLine();
            //}

            return enclosedTiles.Count;
        }
    }
}
