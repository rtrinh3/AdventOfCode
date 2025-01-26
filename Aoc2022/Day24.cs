using System.Text;
using AocCommon;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/24
    public class Day24 : IAocDay
    {
        private readonly string[] maze;
        private readonly int height;
        private readonly int width;
        private readonly int valleyHeight;
        private readonly int valleyWidth;
        private VectorRC start;
        private VectorRC end;
        private readonly List<(VectorRC position, VectorRC orientation)> blizzards;
        public Day24(string input)
        {
            maze = Parsing.SplitLines(input);
            height = maze.Length;
            width = maze[0].Length;
            valleyHeight = height - 2;
            valleyWidth = width - 2;
            start = new VectorRC(0, maze[0].IndexOf('.'));
            end = new VectorRC(height - 1, maze[height - 1].IndexOf('.'));
            blizzards = new();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    VectorRC position = new VectorRC(row, col);
                    switch (maze[row][col])
                    {
                        case '>':
                            blizzards.Add((position, new VectorRC(0, +1)));
                            break;
                        case '<':
                            blizzards.Add((position, new VectorRC(0, -1)));
                            break;
                        case '^':
                            blizzards.Add((position, new VectorRC(-1, 0)));
                            break;
                        case 'v':
                            blizzards.Add((position, new VectorRC(+1, 0)));
                            break;
                    }
                }
            }
        }

        public string Part1()
        {
            var answer = FindRoute(start, end, 0);
            return answer.ToString();
        }

        public string Part2()
        {
            int lap1 = FindRoute(start, end, 0);
            int lap2 = FindRoute(end, start, lap1);
            int lap3 = FindRoute(start, end, lap1 + lap2);
            var answer = lap1 + lap2 + lap3;
            return answer.ToString();
        }

        private char GetMazeTile(VectorRC pos)
        {
            var (row, col) = pos;
            if (row < 0 || row >= height || col < 0 || col >= width)
            {
                return '#';
            }
            return maze[row][col];
        }
        private IEnumerable<VectorRC> GetNeighbors(VectorRC pos)
        {
            var (row, col) = pos;
            VectorRC[] neighbors = [pos, new VectorRC(row + 1, col), new VectorRC(row - 1, col), new VectorRC(row, col + 1), new VectorRC(row, col - 1)];
            return neighbors.Where(p => GetMazeTile(p) != '#');
        }
        private int FindRoute(VectorRC thisStart, VectorRC thisEnd, int startTime)
        {
            var (Distance, Steps) = GraphAlgos.BfsToEnd((thisStart.Row, thisStart.Col, startTime), GetNextSteps, ((int Row, int Col, int Steps) s) => s.Row == thisEnd.Row && s.Col == thisEnd.Col);

            //// Visualise route
            //foreach (var step in Steps.Reverse())
            //{
            //    var (myRow, myCol, time) = step;
            //    Console.WriteLine(VisualizeState(myRow, myCol, time));
            //}

            return Distance;
        }
        private IEnumerable<(int, int, int)> GetNextSteps((int, int, int) current)
        {
            var (row, col, step) = current;
            int nextTime = step + 1;
            // Evaluate blizzards
            HashSet<VectorRC> blizzardNextPositions = EvaluateBlizzards(nextTime);
            //Console.WriteLine(VisualizeState(row, col, nextTime));
            var neighbors = GetNeighbors(new VectorRC(row, col)).Where(p => !blizzardNextPositions.Contains(p)).Select(p => (p.Row, p.Col, nextTime));
            return neighbors;
        }
        private readonly Dictionary<int, HashSet<VectorRC>> memoEvaluateBlizzards = new();
        private HashSet<VectorRC> EvaluateBlizzards(int time)
        {
            int loopTime = time % (valleyHeight * valleyWidth);
            if (memoEvaluateBlizzards.TryGetValue(loopTime, out var ans))
            {
                return ans;
            }
            return memoEvaluateBlizzards[loopTime] = EvaluateBlizzardsImpl(loopTime);
        }
        private HashSet<VectorRC> EvaluateBlizzardsImpl(int time)
        {
            return new HashSet<VectorRC>(blizzards.Select(b =>
            {
                if (b.orientation.Row == 0)
                {
                    int newCol = (b.position.Col - 1 + b.orientation.Col * (time % valleyWidth) + valleyWidth) % valleyWidth + 1;
                    return new VectorRC(b.position.Row, newCol);
                }
                else
                {
                    int newRow = (b.position.Row - 1 + b.orientation.Row * (time % valleyHeight) + valleyHeight) % valleyHeight + 1;
                    return new VectorRC(newRow, b.position.Col);
                }
            }));
        }
        private string VisualizeState(int myRow, int myCol, int time)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine($"Time: {time}");
            var currentBlizzards = EvaluateBlizzards(time);
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (row == myRow && col == myCol)
                    {
                        sb.Append('E');
                    }
                    else if (currentBlizzards.Contains(new VectorRC(row, col)))
                    {
                        sb.Append('X');
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
