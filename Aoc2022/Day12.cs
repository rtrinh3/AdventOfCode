using AocCommon;

namespace Aoc2022
{
    public class Day12 : IAocDay
    {
        private static readonly VectorRC[] directions = {
            new(+1, 0),
            new(-1, 0),
            new(0, +1),
            new(0, -1)
        };

        private readonly string[] map;
        private readonly VectorRC begin;
        private readonly VectorRC end;

        public Day12(string input)
        {
            map = Parsing.SplitLines(input);
            for (int r = 0; r < map.Length; ++r)
            {
                for (int c = 0; c < map[r].Length; ++c)
                {
                    if (map[r][c] == 'S')
                    {
                        begin = new VectorRC(r, c);
                        map[r] = map[r].Replace('S', 'a');
                    }
                    else if (map[r][c] == 'E')
                    {
                        end = new VectorRC(r, c);
                        map[r] = map[r].Replace('E', 'z');
                    }
                }
            }
        }

        private IEnumerable<VectorRC> getAllNeighbors(VectorRC node) =>
            directions.Select(d => node + d)
            .Where(next => 0 <= next.Row && next.Row < map.Length
                && 0 <= next.Col && next.Col < map[next.Row].Length);

        public string Part1()
        {
            IEnumerable<VectorRC> getNeighbors(VectorRC node) =>
                getAllNeighbors(node).Where(next => map[next.Row][next.Col] - map[node.Row][node.Col] <= 1);
            var result = GraphAlgos.BfsToEnd(begin, getNeighbors, node => node == end);
            return result.distance.ToString();
        }

        public string Part2()
        {
            IEnumerable<VectorRC> getNeighbors(VectorRC node) =>
                getAllNeighbors(node).Where(next => map[node.Row][node.Col] - map[next.Row][next.Col] <= 1);
            var result = GraphAlgos.BfsToEnd(end, getNeighbors, node => map[node.Row][node.Col] == 'a');
            return result.distance.ToString();
        }
    }
}
