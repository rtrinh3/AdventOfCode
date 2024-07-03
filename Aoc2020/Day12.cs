using AocCommon;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/12
    // --- Day 12: Rain Risk ---
    public class Day12(string input) : IAocDay
    {
        private readonly (char Move, int Length)[] moves = input.TrimEnd().Split('\n').Select(line => (line[0], int.Parse(line.AsSpan()[1..]))).ToArray();

        public long Part1()
        {
            VectorXY position = VectorXY.Zero;
            VectorXY orientation = VectorXY.Right;
            foreach (var (move, length) in moves)
            {
                switch (move)
                {
                    case 'N':
                        position += VectorXY.Up.Scale(length);
                        break;
                    case 'S':
                        position += VectorXY.Down.Scale(length);
                        break;
                    case 'E':
                        position += VectorXY.Right.Scale(length);
                        break;
                    case 'W':
                        position += VectorXY.Left.Scale(length);
                        break;
                    case 'L':
                        if (length % 90 == 0)
                        {
                            int quarters = length / 90;
                            for (int i = 0; i < quarters; i++)
                            {
                                orientation = orientation.RotatedLeft();
                            }
                        }
                        else
                        {
                            throw new Exception("What is this angle " + move + length.ToString());
                        }
                        break;
                    case 'R':
                        if (length % 90 == 0)
                        {
                            int quarters = length / 90;
                            for (int i = 0; i < quarters; i++)
                            {
                                orientation = orientation.RotatedRight();
                            }
                        }
                        else
                        {
                            throw new Exception("What is this angle " + move + length.ToString());
                        }
                        break;
                    case 'F':
                        position += orientation.Scale(length);
                        break;
                    default:
                        throw new Exception("What is this move " + move + length.ToString());
                }
            }
            return position.ManhattanMetric();
        }

        public long Part2()
        {
            VectorXY ship = new(0, 0);
            VectorXY waypoint = new(10, 1);
            foreach (var (move, length) in moves)
            {
                switch (move)
                {
                    case 'N':
                        waypoint += VectorXY.Up.Scale(length);
                        break;
                    case 'S':
                        waypoint += VectorXY.Down.Scale(length);
                        break;
                    case 'E':
                        waypoint += VectorXY.Right.Scale(length);
                        break;
                    case 'W':
                        waypoint += VectorXY.Left.Scale(length);
                        break;
                    case 'L':
                        if (length % 90 == 0)
                        {
                            int quarters = length / 90;
                            var waypointRelativeToShip = waypoint - ship;
                            for (int i = 0; i < quarters; i++)
                            {
                                waypointRelativeToShip = waypointRelativeToShip.RotatedLeft();
                            }
                            waypoint = waypointRelativeToShip + ship;
                        }
                        else
                        {
                            throw new Exception("What is this angle " + move + length.ToString());
                        }
                        break;
                    case 'R':
                        if (length % 90 == 0)
                        {
                            int quarters = length / 90;
                            var waypointRelativeToShip = waypoint - ship;
                            for (int i = 0; i < quarters; i++)
                            {
                                waypointRelativeToShip = waypointRelativeToShip.RotatedRight();
                            }
                            waypoint = waypointRelativeToShip + ship;
                        }
                        else
                        {
                            throw new Exception("What is this angle " + move + length.ToString());
                        }
                        break;
                    case 'F':
                        var forward = (waypoint - ship).Scale(length);
                        ship += forward;
                        waypoint += forward;
                        break;
                    default:
                        throw new Exception("What is this move " + move + length.ToString());
                }
            }
            return ship.ManhattanMetric();
        }
    }
}
