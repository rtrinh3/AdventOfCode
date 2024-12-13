using AocCommon;
using System.Linq;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/10
    // --- Day 10: Hoof It ---
    public class Day10(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';
        private readonly Grid map = new(input, OUTSIDE);

        private Dictionary<VectorRC, List<VectorRC>> FindTrails()
        {
            Dictionary<VectorRC, List<VectorRC>> trails = new();
            foreach (var (Position, Value) in map.Iterate())
            {
                if (Value != '0')
                {
                    continue;
                }
                List<VectorRC> summits = new();
                void Visit(VectorRC step)
                {
                    var current = map.Get(step);
                    if (current == '9')
                    {
                        summits.Add(step);
                        return;
                    }
                    foreach (var next in step.NextFour())
                    {
                        if (map.Get(next) == current + 1)
                        {
                            Visit(next);
                        }
                    }
                }
                Visit(Position);
                trails[Position] = summits;
            }
            return trails;
        }

        public string Part1()
        {
            var trails = FindTrails();
            var answer = trails.Values.Select(l => l.Distinct().Count()).Sum();
            return answer.ToString();
        }

        public string Part2()
        {
            var trails = FindTrails();
            var answer = trails.Values.Select(l => l.Count()).Sum();
            return answer.ToString();
        }
    }
}
