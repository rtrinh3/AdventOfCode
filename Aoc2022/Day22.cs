using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/22
    public class Day22 : IAocDay
    {
        private readonly string mazeText;
        private readonly string[] maze;
        private readonly List<object> moves = new();

        public Day22(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");
            mazeText = paragraphs[0].TrimEnd();
            maze = mazeText.Split('\n');
            string movesText = paragraphs[1].TrimEnd();
            var movesMatches = Regex.Matches(movesText, @"(\d+)|([A-Z])");
            foreach (Match match in movesMatches.Cast<Match>())
            {
                if (int.TryParse(match.ValueSpan, out int number))
                {
                    moves.Add(number);
                }
                else
                {
                    moves.Add(match.Value);
                }
            }
        }

        char GetTile(VectorRC coord)
        {
            if (coord.Row < 0 || coord.Row >= maze.Length)
            {
                return ' ';
            }
            var row = maze[coord.Row];
            if (coord.Col < 0 || coord.Col >= row.Length)
            {
                return ' ';
            }
            return row[coord.Col];
        }

        public string Part1()
        {
            int startCol = maze[0].IndexOf('.');
            VectorRC coord = new VectorRC(0, startCol);
            VectorRC orientation = new VectorRC(0, +1);
            foreach (var move in moves)
            {
                if (move is int steps)
                {
                    for (int i = 0; i < steps; i++)
                    {
                        var nextCoord = coord + orientation;
                        if (char.IsWhiteSpace(GetTile(nextCoord)))
                        {
                            // Wrap
                            var back = -orientation;
                            var otherSide = coord;
                            while ('.' == GetTile(otherSide + back) || '#' == GetTile(otherSide + back))
                            {
                                otherSide += back;
                            }
                            nextCoord = otherSide;
                        }
                        var actualTile = GetTile(nextCoord);
                        if (actualTile == '#')
                        {
                            break;
                        }
                        if (actualTile != '.')
                        {
                            throw new Exception("What is this tile");
                        }
                        coord = nextCoord;
                    }
                }
                else if ("L".Equals(move))
                {
                    orientation = orientation.RotatedLeft();
                }
                else if ("R".Equals(move))
                {
                    orientation = orientation.RotatedRight();
                }
                else
                {
                    throw new Exception("What is this move");
                }
            }
            var password = CalculatePassword(coord, orientation);
            return password.ToString();
        }

        private static int CalculatePassword(VectorRC coord, VectorRC orientation)
        {
            int facing = orientation switch
            {
                (0, +1) => 0,
                (+1, 0) => 1,
                (0, -1) => 2,
                (-1, 0) => 3,
                _ => throw new Exception("What is this orientation")
            };
            int password = 1000 * (coord.Row + 1) + 4 * (coord.Col + 1) + facing;
            return password;
        }

        public string Part2()
        {
            var answer = DoPart2(ConnectionType.PUZZLE);
            return answer.ToString();
        }

        public enum ConnectionType
        {
            EXAMPLE,
            PUZZLE
        }

        private enum ConnectionSide
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        private class Face
        {
            public VectorRC origin;
            public int UpFace;
            public ConnectionSide UpSide;
            public int DownFace;
            public ConnectionSide DownSide;
            public int LeftFace;
            public ConnectionSide LeftSide;
            public int RightFace;
            public ConnectionSide RightSide;
        }

        public int DoPart2(ConnectionType connectionType)
        {
            int numberOfTiles = mazeText.Count(c => !char.IsWhiteSpace(c));
            int tilesPerFace = Math.DivRem(numberOfTiles, 6, out int remainder);
            Debug.Assert(remainder == 0);
            int sideLength = (int)Math.Sqrt(tilesPerFace);
            Debug.Assert(sideLength * sideLength == tilesPerFace);
            //Console.WriteLine($"{numberOfTiles} {tilesPerFace} {sideLength}");
            List<VectorRC> faceOrigins = new();
            for (int row = 0; row < maze.Length; row += sideLength)
            {
                for (int col = 0; col < maze[row].Length; col += sideLength)
                {
                    if (!char.IsWhiteSpace(maze[row][col]))
                    {
                        faceOrigins.Add(new VectorRC(row, col));
                    }
                }
            }
            Debug.Assert(faceOrigins.Count == 6);
            //Console.WriteLine(string.Join(",", faceOrigins));

            Face[] faces;
            if (connectionType == ConnectionType.EXAMPLE)
            {
                faces = [
                    new Face { origin = faceOrigins[0], UpFace = 1, UpSide = ConnectionSide.UP, LeftFace = 2, LeftSide = ConnectionSide.UP, RightFace = 5, RightSide = ConnectionSide.RIGHT, DownFace = 3, DownSide = ConnectionSide.UP },
                    new Face { origin = faceOrigins[1], UpFace = 0, UpSide = ConnectionSide.UP, LeftFace = 5, LeftSide = ConnectionSide.DOWN, RightFace = 2, RightSide = ConnectionSide.LEFT, DownFace = 4, DownSide = ConnectionSide.DOWN },
                    new Face { origin = faceOrigins[2], UpFace = 0, UpSide = ConnectionSide.LEFT, LeftFace = 1, LeftSide = ConnectionSide.RIGHT, RightFace = 3, RightSide = ConnectionSide.LEFT, DownFace = 4, DownSide = ConnectionSide.LEFT },
                    new Face { origin = faceOrigins[3], UpFace = 0, UpSide = ConnectionSide.DOWN, LeftFace = 2, LeftSide = ConnectionSide.RIGHT, RightFace = 5, RightSide = ConnectionSide.UP, DownFace = 4, DownSide = ConnectionSide.UP },
                    new Face { origin = faceOrigins[4], UpFace = 3, UpSide = ConnectionSide.DOWN, LeftFace = 2, LeftSide = ConnectionSide.DOWN, RightFace = 5, RightSide = ConnectionSide.LEFT, DownFace = 1, DownSide = ConnectionSide.DOWN },
                    new Face { origin = faceOrigins[5], UpFace = 3, UpSide = ConnectionSide.RIGHT, LeftFace = 4, LeftSide = ConnectionSide.RIGHT, RightFace = 0, RightSide = ConnectionSide.RIGHT, DownFace = 1, DownSide = ConnectionSide.LEFT },
                ];
            }
            else if (connectionType == ConnectionType.PUZZLE)
            {
                faces = [
                    new Face { origin = faceOrigins[0], UpFace = 5, UpSide = ConnectionSide.LEFT, LeftFace = 3, LeftSide = ConnectionSide.LEFT, RightFace = 1, RightSide = ConnectionSide.LEFT, DownFace = 2, DownSide = ConnectionSide.UP },
                    new Face { origin = faceOrigins[1], UpFace = 5, UpSide = ConnectionSide.DOWN, LeftFace = 0, LeftSide = ConnectionSide.RIGHT, RightFace = 4, RightSide = ConnectionSide.RIGHT, DownFace = 2, DownSide = ConnectionSide.RIGHT },
                    new Face { origin = faceOrigins[2], UpFace = 0, UpSide = ConnectionSide.DOWN, LeftFace = 3, LeftSide = ConnectionSide.UP, RightFace = 1, RightSide = ConnectionSide.DOWN, DownFace = 4, DownSide = ConnectionSide.UP },
                    new Face { origin = faceOrigins[3], UpFace = 2, UpSide = ConnectionSide.LEFT, LeftFace = 0, LeftSide = ConnectionSide.LEFT, RightFace = 4, RightSide = ConnectionSide.LEFT, DownFace = 5, DownSide = ConnectionSide.UP },
                    new Face { origin = faceOrigins[4], UpFace = 2, UpSide = ConnectionSide.DOWN, LeftFace = 3, LeftSide = ConnectionSide.RIGHT, RightFace = 1, RightSide = ConnectionSide.RIGHT, DownFace = 5, DownSide = ConnectionSide.RIGHT },
                    new Face { origin = faceOrigins[5], UpFace = 3, UpSide = ConnectionSide.DOWN, LeftFace = 0, LeftSide = ConnectionSide.UP, RightFace = 4, RightSide = ConnectionSide.DOWN, DownFace = 1, DownSide = ConnectionSide.UP }
                ];
            }
            else
            {
                throw new ArgumentException(nameof(connectionType));
            }

            (VectorRC Position, VectorRC Direction) MapToOtherFace(int edgeCoordinate, ConnectionSide side)
            {
                VectorRC pos = side switch
                {
                    ConnectionSide.UP => new VectorRC(0, edgeCoordinate),
                    ConnectionSide.DOWN => new VectorRC(sideLength - 1, sideLength - edgeCoordinate - 1),
                    ConnectionSide.LEFT => new VectorRC(sideLength - edgeCoordinate - 1, 0),
                    ConnectionSide.RIGHT => new VectorRC(edgeCoordinate, sideLength - 1),
                    _ => throw new Exception("WTF")
                };
                VectorRC dir = side switch
                {
                    ConnectionSide.UP => VectorRC.Down,
                    ConnectionSide.DOWN => VectorRC.Up,
                    ConnectionSide.LEFT => VectorRC.Right,
                    ConnectionSide.RIGHT => VectorRC.Left,
                    _ => throw new Exception("WTF")
                };
                return (pos, dir);
            }

            int face = 0;
            VectorRC position = VectorRC.Zero;
            VectorRC orientation = VectorRC.Right;
            //HashSet<VectorRC> visited = new(); // For visualisation
            foreach (var move in moves)
            {
                //visited.Add(faces[face].origin + position); // For visualisation
                if (move is int steps)
                {
                    for (int i = 0; i < steps; i++)
                    {
                        //visited.Add(faces[face].origin + position); // For visualisation
                        var nextPos = position + orientation;
                        var nextDir = orientation;
                        var nextFace = face;
                        if (nextPos.Row < 0)
                        {
                            // OOB Up
                            nextFace = faces[face].UpFace;
                            int normalizedPos = sideLength - nextPos.Col - 1;
                            (nextPos, nextDir) = MapToOtherFace(normalizedPos, faces[face].UpSide);
                        }
                        else if (sideLength <= nextPos.Row)
                        {
                            // OOB Down
                            nextFace = faces[face].DownFace;
                            int normalizedPos = nextPos.Col;
                            (nextPos, nextDir) = MapToOtherFace(normalizedPos, faces[face].DownSide);
                        }
                        else if (nextPos.Col < 0)
                        {
                            // OOB Left
                            nextFace = faces[face].LeftFace;
                            int normalizedPos = nextPos.Row;
                            (nextPos, nextDir) = MapToOtherFace(normalizedPos, faces[face].LeftSide);
                        }
                        else if (sideLength <= nextPos.Col)
                        {
                            // OOB Right
                            nextFace = faces[face].RightFace;
                            int normalizedPos = sideLength - nextPos.Row - 1;
                            (nextPos, nextDir) = MapToOtherFace(normalizedPos, faces[face].RightSide);
                        }
                        char nextTile = GetTile(faces[nextFace].origin + nextPos);
                        if (nextTile == '#')
                        {
                            break;
                        }
                        if (nextTile != '.')
                        {
                            throw new Exception("What is this tile");
                        }
                        (face, position, orientation) = (nextFace, nextPos, nextDir);
                    }
                }
                else if ("L".Equals(move))
                {
                    orientation = orientation.RotatedLeft();
                }
                else if ("R".Equals(move))
                {
                    orientation = orientation.RotatedRight();
                }
                else
                {
                    throw new Exception("What is this move");
                }
            }

            //// Visualisation
            //for (int row = 0; row < maze.Length; row++)
            //{
            //    for (int col = 0; col < maze[row].Length; col++)
            //    {
            //        VectorRC pos = new(row, col);
            //        if (visited.Contains(pos))
            //        {
            //            Console.ForegroundColor = ConsoleColor.Red;
            //        }
            //        else
            //        {
            //            Console.ResetColor();
            //        }
            //        Console.Write(GetTile(pos));
            //    }
            //    Console.WriteLine();
            //}

            var password = CalculatePassword(faces[face].origin + position, orientation);
            return password;
        }
    }
}
