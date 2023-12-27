using AocCommon;
using System.Numerics;

namespace Aoc2022
{
    public class Day09(string input) : IAocDay
    {
        VectorXY FixTailPosition(VectorXY newHead, VectorXY oldTail)
        {
            VectorXY delta = newHead - oldTail;
            VectorXY absDelta = new VectorXY(Math.Abs(delta.X), Math.Abs(delta.Y));
            VectorXY tailDelta;
            if (absDelta.X <= 1 && absDelta.Y <= 1)
            {
                tailDelta = new VectorXY(0, 0);
            }
            else if (absDelta.X == 0 && absDelta.Y >= 1)
            {
                tailDelta = new VectorXY(0, Math.Sign(delta.Y));
            }
            else if (absDelta.X >= 1 && absDelta.Y == 0)
            {
                tailDelta = new VectorXY(Math.Sign(delta.X), 0);
            }
            else
            {
                tailDelta = new VectorXY(Math.Sign(delta.X), Math.Sign(delta.Y));
            }
            return oldTail + tailDelta;
        }

        Dictionary<char, VectorXY> directions = new () {
            {'U', new VectorXY (0, +1) },
            {'D', new VectorXY (0, -1) },
            {'L', new VectorXY (-1, 0) },
            {'R', new VectorXY (+1, 0) }
        };

        int SimulateRope(int ropeLength)
        {
            VectorXY[] rope = new VectorXY[ropeLength];
            for (int i = 0; i < ropeLength; ++i)
            {
                rope[i] = new VectorXY(0, 0);
            }
            HashSet<VectorXY> tailPositions = new HashSet<VectorXY>();
            tailPositions.Add(rope[^1]);

            using (System.IO.StringReader reader = new System.IO.StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    char dir = line[0];
                    int len = int.Parse(line[2..]);
                    for (int i = 0; i < len; ++i)
                    {
                        rope[0] += directions[dir];
                        for (int j = 0; j < ropeLength - 1; ++j)
                        {
                            rope[j + 1] = FixTailPosition(rope[j], rope[j + 1]);
                        }
                        tailPositions.Add(rope[^1]);
                    }
                }
            }
            return tailPositions.Count;
        }

        public string Part1()
        {
            return SimulateRope(2).ToString();
        }
        public string Part2()
        {
            return SimulateRope(10).ToString();
        }
    }
}
