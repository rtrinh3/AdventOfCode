using AocCommon;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/3
    public class Day03(string input) : IAocDay
    {
        private static readonly Dictionary<char, VectorXY> directions = new() {
            {'U', new VectorXY(0, +1)},
            {'D', new VectorXY(0, -1)},
            {'L', new VectorXY(-1, 0)},
            {'R', new VectorXY(+1, 0)}
        };

        private readonly string[] inputs = input.TrimEnd().Split('\n');

        public string Part1()
        {
            HashSet<VectorXY>[] wires = new HashSet<VectorXY>[inputs.Length];
            for (int i = 0; i < 2; i++)
            {
                HashSet<VectorXY> wire = new HashSet<VectorXY>();
                VectorXY pos = new(0, 0);
                //wire.Add(pos);
                foreach (string segment in inputs[i].Split(','))
                {
                    var dir = directions[segment[0]];
                    int len = int.Parse(segment[1..]);
                    for (int j = 0; j < len; j++)
                    {
                        pos += dir;
                        wire.Add(pos);
                    }
                }
                wires[i] = wire;
            }
            var intersections = wires[0].Intersect(wires[1]);
            var answer = intersections.Min(w => w.ManhattanMetric());
            return answer.ToString();
        }

        public string Part2()
        {
            Dictionary<VectorXY, int>[] measuredWires = new Dictionary<VectorXY, int>[inputs.Length];
            for (int i = 0; i < 2; i++)
            {
                Dictionary<VectorXY, int> measuredWire = new Dictionary<VectorXY, int>();
                VectorXY pos = new(0, 0);
                int dist = 0;
                foreach (string segment in inputs[i].Split(','))
                {
                    var dir = directions[segment[0]];
                    int len = int.Parse(segment[1..]);
                    for (int j = 0; j < len; j++)
                    {
                        pos += dir;
                        dist++;
                        if (!measuredWire.ContainsKey(pos))
                        {
                            measuredWire[pos] = dist;
                        }
                    }
                }
                measuredWires[i] = measuredWire;
            }
            var intersections = measuredWires[0].Keys.Intersect(measuredWires[1].Keys);
            var intersectionSteps = intersections.Select(x => measuredWires[0][x] + measuredWires[1][x]);
            var answer = intersectionSteps.Min();
            return answer.ToString();
        }
    }
}
