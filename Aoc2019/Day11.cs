using AocCommon;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/11
    public class Day11(string input) : IAocDay
    {
        public string Part1()
        {
            Console.WriteLine("Part 1");
            Dictionary<VectorXY, int> grid1 = new();
            RunRobot(grid1);
            return grid1.Count.ToString();
        }

        public string Part2()
        {
            Dictionary<VectorXY, int> grid2 = new()
            {
                [VectorXY.Zero] = 1
            };
            RunRobot(grid2);
            StringBuilder canvas = new StringBuilder();
            int xmin = grid2.Keys.Min(v => v.X);
            int xmax = grid2.Keys.Max(v => v.X);
            int ymin = grid2.Keys.Min(v => v.Y);
            int ymax = grid2.Keys.Max(v => v.Y);
            for (int y = ymax; y >= ymin; y--)
            {
                for (int x = xmin; x <= xmax; x++)
                {
                    canvas.Append((grid2.GetValueOrDefault(new VectorXY(x, y)) == 0) ? '.' : '#');
                }
                canvas.AppendLine();
            }
            return canvas.ToString();
        }

        void RunRobot(Dictionary<VectorXY, int> grid)
        {
            IntcodeInterpreter brain = new IntcodeInterpreter(input);
            VectorXY position = VectorXY.Zero;
            VectorXY orientation = VectorXY.Up;
            while (true)
            {
                var currentColor = grid.GetValueOrDefault(position);
                var color = brain.RunUntilOutput(currentColor);
                if (color == null)
                {
                    break;
                }
                grid[position] = (int)color.Value;

                var turn = brain.RunUntilOutput();
                if (turn == null)
                {
                    break;
                }
                orientation.RotatedLeft();
                orientation = (int)turn switch
                {
                    0 => orientation.RotatedLeft(),
                    1 => orientation.RotatedRight(),
                    _ => throw new NotImplementedException()
                };
                position += orientation;
            }
        }
    }
}
