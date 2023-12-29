using AocCommon;
using System.Numerics;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/15
    public class Day15(string input) : IAocDay
    {
        private static readonly Dictionary<VectorXY, int> dirCmds = new()
        {
            { new VectorXY(+1, 0), 4 },
            { new VectorXY(0, +1), 1 },
            { new VectorXY(-1, 0), 3 },
            { new VectorXY(0, -1), 2 }
        };
        private static readonly VectorXY origin = VectorXY.Zero;

        public string Part1()
        {
            var (map, oxygen) = MapArea();
            var route = CalculateRoute(map, origin, oxygen);
            var answer = route.Count;
            return answer.ToString();
        }

        private (Dictionary<VectorXY, int> map, VectorXY oxygen) MapArea()
        {
            IntcodeInterpreter interpreter = new(input);
            Stack<VectorXY> placesToVisit = new();
            placesToVisit.Push(origin);
            Dictionary<VectorXY, int> map = new();
            map[origin] = 1;
            VectorXY position = origin;
            while (placesToVisit.Count > 0)
            {
                //DrawMap(map, position);
                //System.Threading.Thread.Sleep(1);
                if (position == placesToVisit.Peek())
                {
                    placesToVisit.Pop();
                }
                foreach (var next in position.NextFour())
                {
                    if (!map.ContainsKey(next))
                    {
                        placesToVisit.Push(next);
                    }
                }
                if (!placesToVisit.Any()) break;
                var routeToNext = CalculateRoute(map, position, placesToVisit.Peek());
                VectorXY nextDir = routeToNext.Last() - position;
                BigInteger nextCommand = dirCmds[nextDir];
                var droidResponse = interpreter.RunUntilOutput(nextCommand);
                if (droidResponse == 0)
                {
                    map[position + nextDir] = 0;
                }
                else if (droidResponse == 1)
                {
                    map[position + nextDir] = 1;
                    position += nextDir;
                }
                else if (droidResponse == 2)
                {
                    map[position + nextDir] = 2;
                    position += nextDir;
                }
                while (placesToVisit.TryPeek(out var peeked) && map.ContainsKey(peeked))
                {
                    placesToVisit.Pop();
                }
            }
            //DrawMap(map, origin);
            VectorXY destination = map.First(kvp => kvp.Value == 2).Key;
            return (map, destination);
        }

        public string Part2()
        {
            var (map, oxygen) = MapArea();
            var distances = CalculateDistances(map, oxygen);
            var farthest = distances.Values.Max();
            return farthest.ToString();
        }

        void DrawMap(Dictionary<VectorXY, int> map, VectorXY? droid)
        {
            StringBuilder sb = new();
            int xMin = map.Keys.Select(v => v.X).Min();
            int xMax = map.Keys.Select(v => v.X).Max();
            int yMin = map.Keys.Select(v => v.Y).Min();
            int yMax = map.Keys.Select(v => v.Y).Max();
            for (int row = yMax; row >= yMin; row--)
            {
                for (int col = xMin; col <= xMax; col++)
                {
                    VectorXY coords = new VectorXY(col, row);
                    if (droid != null && coords == droid.Value)
                    {
                        sb.Append('X');
                    }
                    else if (map.ContainsKey(coords))
                    {
                        var value = map[coords];
                        sb.Append(value switch
                        {
                            0 => '█',
                            1 => '.',
                            2 => '2',
                            _ => value.ToString()[0]
                        });
                    }
                    else
                    {
                        sb.Append(' ');
                    }
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        List<VectorXY> CalculateRoute(Dictionary<VectorXY, int> map, VectorXY start, VectorXY end)
        {
            IEnumerable<VectorXY> GetNext(VectorXY current)
            {
                return current.NextFour().Where(next => !map.ContainsKey(next) || map[next] != 0);
            }
            var bfsResult = GraphAlgos.BfsToEnd(start, GetNext, pos => pos == end);
            return bfsResult.path.ToList();
        }

        Dictionary<VectorXY, int> CalculateDistances(Dictionary<VectorXY, int> map, VectorXY start)
        {
            IEnumerable<VectorXY> GetNext(VectorXY current)
            {
                return current.NextFour().Where(next => !map.ContainsKey(next) || map[next] != 0);
            }
            var bfsResult = GraphAlgos.BfsToAll(start, GetNext);
            return bfsResult.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.distance);
        }
    }
}
