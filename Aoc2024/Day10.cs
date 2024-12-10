using AocCommon;

namespace Aoc2024
{
    public class Day10(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';
        private readonly Grid map = new(input, OUTSIDE);

        public string Part1()
        {
            Dictionary<VectorRC, int> trailheadScores = new();
            foreach (var (Position, Value) in map.Iterate())
            {
                if (Value != '0')
                {
                    continue;
                }
                HashSet<VectorRC> summits = new();
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
                trailheadScores[Position] = summits.Count;
            }
            var answer = trailheadScores.Values.Sum();
            return answer.ToString();
        }

        public string Part2()
        {
            Dictionary<VectorRC, int> trailheadRatings = new();
            foreach (var (Position, Value) in map.Iterate())
            {
                if (Value != '0')
                {
                    continue;
                }
                int trails = 0;
                void Visit(VectorRC step)
                {
                    var current = map.Get(step);
                    if (current == '9')
                    {
                        trails++;
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
                trailheadRatings[Position] = trails;
            }
            var answer = trailheadRatings.Values.Sum();
            return answer.ToString();
        }
    }
}
