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
            int numberOfTiles = mazeText.Count(c => !char.IsWhiteSpace(c));
            int tilesPerFace = Math.DivRem(numberOfTiles, 6, out int remainder);
            Debug.Assert(remainder == 0);
            int sideLength = (int)Math.Sqrt(tilesPerFace);
            Debug.Assert(sideLength * sideLength == tilesPerFace);
            //Console.WriteLine($"{numberOfTiles} {tilesPerFace} {sideLength}");

            // Initialize the cube with placeholders
            Dictionary<VectorXYZ, (VectorRC FlatCoord, VectorXYZ OriginalUp, char Value)> cube = new();
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    cube[new VectorXYZ(i + 1, j + 1, 0)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                    cube[new VectorXYZ(i + 1, j + 1, sideLength + 1)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                    cube[new VectorXYZ(i + 1, 0, j + 1)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                    cube[new VectorXYZ(i + 1, sideLength + 1, j + 1)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                    cube[new VectorXYZ(0, i + 1, j + 1)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                    cube[new VectorXYZ(sideLength + 1, i + 1, j + 1)] = (VectorRC.Zero, VectorXYZ.Zero, '\0');
                }
            }
            Debug.Assert(cube.Count == numberOfTiles);
            (VectorXYZ nextCubePos, VectorXYZ nextNormal, VectorXYZ nextDir, VectorXYZ nextUpVector) GetNextPosition(VectorXYZ cubePos, VectorXYZ normal, VectorXYZ dir, VectorXYZ optionalUpVector)
            {
                VectorXYZ next = cubePos + dir;
                if (cube.ContainsKey(next))
                {
                    return (next, normal, dir, optionalUpVector);
                }
                else
                {
                    // We stepped off the cube, rotate onto the next face
                    var axis = normal.Cross(dir);
                    Debug.Assert(axis.ManhattanMetric() == 1);
                    var nextNormal = RotateCounterClockwise(normal, axis);
                    var nextDir = RotateCounterClockwise(dir, axis);
                    var nextUpVector = RotateCounterClockwise(optionalUpVector, axis);
                    next += nextDir;
                    return (next, nextNormal, nextDir, nextUpVector);
                }
            }

            // Paint the map onto the cube
            VectorRC initMapPos = new VectorRC(0, maze[0].IndexOf('.'));
            VectorXYZ initCubePos = new VectorXYZ(1, 1, 0);
            VectorXYZ initNormal = new VectorXYZ(0, 0, -1);
            VectorXYZ initUpVector = new VectorXYZ(0, -1, 0);
            HashSet<VectorRC> visited = new();
            Stack<(VectorRC mapPos, VectorXYZ cubePos, VectorXYZ normal, VectorXYZ upVector)> queue = new();
            queue.Push((initMapPos, initCubePos, initNormal, initUpVector));
            while (queue.TryPop(out var state))
            {
                if (!visited.Add(state.mapPos))
                {
                    continue;
                }
                // Assign data
                var existing = cube[state.cubePos];
                Debug.Assert(existing.Value == '\0', "Should be placeholder");
                cube[state.cubePos] = (state.mapPos, state.upVector, GetTile(state.mapPos));
                // Neighbors
                Debug.Assert(state.normal.ManhattanMetric() == 1);
                Debug.Assert(state.upVector.ManhattanMetric() == 1);
                var leftVector = state.normal.Cross(state.upVector);
                Debug.Assert(leftVector.ManhattanMetric() == 1);
                (VectorRC, VectorXYZ)[] directionMap =
                [
                    (VectorRC.Up, state.upVector),
                    (VectorRC.Down, -state.upVector),
                    (VectorRC.Left, leftVector),
                    (VectorRC.Right, -leftVector),
                ];
                foreach (var (mapDir, cubeDir) in directionMap)
                {
                    var next = state.mapPos + mapDir;
                    if (visited.Contains(next) || char.IsWhiteSpace(GetTile(next)))
                    {
                        continue;
                    }
                    var nextState = GetNextPosition(state.cubePos, state.normal, cubeDir, state.upVector);
                    queue.Push((next, nextState.nextCubePos, nextState.nextNormal, nextState.nextUpVector));
                }
            }
            Debug.Assert(cube.Count == numberOfTiles);
            var unmapped = cube.Where(x => x.Value.Value != '.' && x.Value.Value != '#').ToList();
            Debug.Assert(unmapped.Count == 0);

            // Execute the moves
            VectorXYZ cubePos = initCubePos;
            VectorXYZ normal = initNormal;
            VectorXYZ direction = initUpVector.Cross(initNormal); // right
            foreach (var move in moves)
            {
                //Console.WriteLine(move);
                if (move is int steps)
                {
                    for (int i = 0; i < steps; i++)
                    {
                        var nextState = GetNextPosition(cubePos, normal, direction, VectorXYZ.Zero);
                        char nextTile = cube[nextState.nextCubePos].Value;
                        if (nextTile == '#')
                        {
                            break;
                        }
                        Debug.Assert(nextTile == '.', "What is this tile " + cube[nextState.nextCubePos]);
                        (cubePos, normal, direction, _) = nextState;
                    }
                }
                else if ("L".Equals(move))
                {
                    direction = normal.Cross(direction);
                }
                else if ("R".Equals(move))
                {
                    direction = direction.Cross(normal);
                }
                else
                {
                    throw new Exception("What is this move " + move);
                }
            }
            VectorRC finalDir;
            var finalSpace = cube[cubePos];
            VectorXYZ leftDir = normal.Cross(finalSpace.OriginalUp);
            if (direction == finalSpace.OriginalUp)
            {
                finalDir = VectorRC.Up;
            }
            else if (direction == leftDir)
            {
                finalDir = VectorRC.Left;
            }
            else if (direction == -leftDir)
            {
                finalDir = VectorRC.Right;
            }
            else if (direction == -finalSpace.OriginalUp)
            {
                finalDir = VectorRC.Down;
            }
            else
            {
                throw new Exception("What is this direction");
            }
            var password = CalculatePassword(cube[cubePos].FlatCoord, finalDir);
            return password.ToString();
        }

        private static VectorXYZ MatrixMultiply(int[,] matrix, VectorXYZ vector)
        {
            if (matrix.GetLength(0) != 3)
            {
                throw new ArgumentException("Matrix must have height of 3");
            }
            if (matrix.GetLength(1) != 3)
            {
                throw new ArgumentException("Matrix must have width of 3");
            }
            return new VectorXYZ(
                matrix[0, 0] * vector.X + matrix[0, 1] * vector.Y + matrix[0, 2] * vector.Z,
                matrix[1, 0] * vector.X + matrix[1, 1] * vector.Y + matrix[1, 2] * vector.Z,
                matrix[2, 0] * vector.X + matrix[2, 1] * vector.Y + matrix[2, 2] * vector.Z
            );
        }

        private static VectorXYZ RotateCounterClockwise(VectorXYZ vector, VectorXYZ axis)
        {
            if (axis.ManhattanMetric() != 1)
            {
                throw new ArgumentException("Axis must be unit vector", nameof(axis));
            }
            // https://en.wikipedia.org/wiki/Rotation_matrix#Rotation_matrix_from_axis_and_angle
            const int CosAngle = 0;
            const int SinAngle = 1;
            int[,] matrix = new int[3, 3]
            {
                { CosAngle + axis.X * axis.X * (1 - CosAngle), axis.X * axis.Y * (1 - CosAngle) - axis.Z * SinAngle, axis.X * axis.Z * (1 - CosAngle) + axis.Y * SinAngle },
                { axis.Y * axis.X * (1 - CosAngle) + axis.Z * SinAngle, CosAngle + axis.Y * axis.Y * (1 - CosAngle), axis.Y * axis.Z * (1 - CosAngle) - axis.X * SinAngle },
                { axis.Z * axis.X * (1 - CosAngle) - axis.Y * SinAngle, axis.Z * axis.Y * (1 - CosAngle) + axis.X * SinAngle, CosAngle + axis.Z * axis.Z * (1 - CosAngle) }
            };
            var answer = MatrixMultiply(matrix, vector);
            return answer;
        }
    }
}
