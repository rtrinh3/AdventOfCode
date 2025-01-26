using System.Text;

namespace Aoc2022
{
    public class Day10 : IAocDay
    {
        int sum = 0;
        bool[,] screen = new bool[6, 40];

        public Day10(string input)
        {
            PriorityQueue<Action<float>, float> queue = new();

            int x = 1;

            int[] milestones = { 20, 60, 100, 140, 180, 220 };
            foreach (int m in milestones)
            {
                queue.Enqueue(cycle =>
                {
                    sum += m * x;
                }, m);
            }

            float cycle = 0.5f;
            foreach (string line in AocCommon.Parsing.SplitLines(input))
            {
                if (line.StartsWith("noop"))
                {
                    cycle += 1;
                    queue.Enqueue(c => { }, cycle);
                }
                else if (line.StartsWith("addx"))
                {
                    int arg = int.Parse(line[5..]);
                    cycle += 2;
                    queue.Enqueue(c =>
                    {
                        x += arg;
                    }, cycle);
                }
            }

            int row = 0;
            int col = 0;
            void CRT(float cycle)
            {
                if (Math.Abs(col - x) <= 1)
                {
                    screen[row, col] = true;
                }
                ++col;
                if (col >= 40)
                {
                    col = 0;
                    ++row;
                }
            }
            for (int i = 1; i <= 240; ++i)
            {
                queue.Enqueue(CRT, i);
            }

            while (queue.Count > 0)
            {
                queue.TryDequeue(out var action, out float c);
                action(c);
            }
        }
        public string Part1()
        {
            return sum.ToString();
        }
        public string Part2()
        {
            StringBuilder buffer = new();
            for (int row = 0; row < screen.GetLength(0); row++)
            {
                for (int col = 0; col < screen.GetLength(1); col++)
                {
                    buffer.Append(screen[row, col] ? '#' : '.');
                }
                buffer.AppendLine();
            }
            return buffer.ToString();
        }
    }
}
