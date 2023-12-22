using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    public class Day21 : IAocDay
    {
        private readonly Grid maze;
        private readonly VectorRC start;

        public Day21(string input)
        {
            maze = new(input, '#');
            start = maze.Iterate().Where(x => x.Value == 'S').Select(x => x.Position).Single();
        }

        public long Part1()
        {
            const int PART_ONE_STEPS = 64;
            var count = SimpleStepsCounter(start, PART_ONE_STEPS);
            return count;
        }

        private long SimpleStepsCounter(VectorRC localStart, int steps)
        {
            int stepsParity = steps % 2;
            IEnumerable<VectorRC> GetNeighbors(VectorRC position)
            {
                return position.NextFour().Where(p => maze.Get(p) != '#');
            }
            var bfsResult = GraphAlgos.BfsToAll(localStart, GetNeighbors);
            var count = bfsResult.Count(x => x.Value.distance <= steps && x.Value.distance % 2 == stepsParity);
            return count;
        }

        public long Part2()
        {
            const int steps = 26501365;

            // Check assumptions
            long repetitions = steps / maze.Height;
            int halfMaze = maze.Height / 2;
            Debug.Assert(maze.Height == maze.Width);
            Debug.Assert(maze.Height % 2 == 1);
            Debug.Assert(halfMaze % 2 == 1);
            Debug.Assert(steps % maze.Height == halfMaze);
            Debug.Assert(repetitions % 2 == 0);
            Debug.Assert(start.Row == halfMaze && start.Col == halfMaze);

            // Thanks to https://www.reddit.com/r/adventofcode/comments/18nsan0/2023_day_21_defeat_coders_with_this_one_simple/
            // for reminding me about the inaccessible holes in the garden
            long interiorOddTiles = SimpleStepsCounter(start, maze.Height);
            long interiorEvenTiles = SimpleStepsCounter(start, maze.Height + 1);

            long topCorner = SimpleStepsCounter(new VectorRC(maze.Height - 1, halfMaze), maze.Height - 1);
            long bottomCorner = SimpleStepsCounter(new VectorRC(0, halfMaze), maze.Height - 1);
            long leftCorner = SimpleStepsCounter(new VectorRC(halfMaze, maze.Width - 1), maze.Height - 1);
            long rightCorner = SimpleStepsCounter(new VectorRC(halfMaze, 0), maze.Height - 1);

            long topRightEvenDiagonal = SimpleStepsCounter(new VectorRC(maze.Height - 1, 0), halfMaze - 1);
            long topRightOddDiagonal = SimpleStepsCounter(new VectorRC(maze.Height - 1, 0), maze.Height + halfMaze - 1);
            long bottomRightEvenDiagonal = SimpleStepsCounter(new VectorRC(0, 0), halfMaze - 1);
            long bottomRightOddDiagonal = SimpleStepsCounter(new VectorRC(0, 0), maze.Height + halfMaze - 1);
            long bottomLeftEvenDiagonal = SimpleStepsCounter(new VectorRC(0, maze.Width - 1), halfMaze - 1);
            long bottomLeftOddDiagonal = SimpleStepsCounter(new VectorRC(0, maze.Width - 1), maze.Height + halfMaze - 1);
            long topLeftEvenDiagonal = SimpleStepsCounter(new VectorRC(maze.Height - 1, maze.Width - 1), halfMaze - 1);
            long topLeftOddDiagonal = SimpleStepsCounter(new VectorRC(maze.Height - 1, maze.Width - 1), maze.Height + halfMaze - 1);

            long sum = interiorOddTiles +
                topCorner + bottomCorner + leftCorner + rightCorner +
                repetitions * (topRightEvenDiagonal + bottomRightEvenDiagonal + bottomLeftEvenDiagonal + topLeftEvenDiagonal) +
                (repetitions - 1) * (topRightOddDiagonal + bottomRightOddDiagonal + bottomLeftOddDiagonal + topLeftOddDiagonal);
            // case i == 0 already handled as the first term of the sum above
            for (long i = 1; i < repetitions; i++)
            {
                if (i % 2 == 0)
                {
                    sum += 4 * i * interiorOddTiles;
                }
                else
                {
                    sum += 4 * i * interiorEvenTiles;
                }
            }
            return sum;
        }

        private long Part2BruteForce(int steps)
        {
            int stepsParity = steps % 2;
            IEnumerable<VectorRC> GetNeighbors(VectorRC position)
            {
                if (position.ManhattanMetric() >= steps + maze.Width)
                {
                    return Array.Empty<VectorRC>();
                }
                else
                {
                    return position.NextFour().Where(n =>
                    {
                        int row = ((n.Row % maze.Height) + maze.Height) % maze.Height;
                        int col = ((n.Col % maze.Width) + maze.Width) % maze.Width;
                        return maze.Get(row, col) != '#';
                    });
                }
            }
            var bfsResult = GraphAlgos.BfsToAll(start, GetNeighbors);
            int count = bfsResult.Values.Count(x => x.distance <= steps && x.distance % 2 == stepsParity);
            return count;
        }
    }
}
