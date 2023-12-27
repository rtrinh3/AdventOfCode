using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2022
{
    public class Day22 : IAocDay
    {
        private string mazeText;
        private string movesText;

        public Day22(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");
            mazeText = paragraphs[0].TrimEnd();
            movesText = paragraphs[1].TrimEnd();
        }

        public string Part1()
        {
            List<object> moves = new();
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

            string[] maze = mazeText.Split('\n');
            char GetTile(VectorRC coord)
            {
                if (coord.Row < 0 || coord.Row >= maze.Length)
                {
                    return '\0';
                }
                var row = maze[coord.Row];
                if (coord.Col < 0 || coord.Col >= row.Length)
                {
                    return '\0';
                }
                return row[coord.Col];
            }
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
                        if (0 == GetTile(nextCoord) || char.IsWhiteSpace(GetTile(nextCoord)))
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
            int facing = orientation switch
            {
                (0, +1) => 0,
                (+1, 0) => 1,
                (0, -1) => 2,
                (-1, 0) => 3,
                _ => throw new Exception("What is this orientation")
            };
            int password = 1000 * (coord.Row + 1) + 4 * (coord.Col + 1) + facing;
            return password.ToString();
        }

        public string Part2()
        {
            return "TODO";
        }
    }
}
