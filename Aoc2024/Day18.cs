using AocCommon;
using System.Diagnostics;

namespace Aoc2024;

public class Day18(string input) : IAocDay
{
    private readonly VectorRC[] incoming = input
        .TrimEnd()
        .Split('\n')
        .Select(line => line.Split(',').Select(int.Parse).ToList())
        .Select(numbers => new VectorRC(numbers[0], numbers[1]))
        .ToArray();

    public string Part1()
    {
        var answer = DoPart1(70, 70, 1024);
        return answer.ToString();
    }

    public int DoPart1(int width, int height, int falls)
    {
        HashSet<VectorRC> obstacles = new(incoming.Take(falls));
        VectorRC start = VectorRC.Zero;
        VectorRC end = new(width, height);
        IEnumerable<VectorRC> GetNext(VectorRC pos)
        {
            List<VectorRC> answer = new();
            foreach (var next in pos.NextFour())
            {
                if (0 <= next.Row && next.Row <= height && 0 <= next.Col && next.Col <= width && !obstacles.Contains(next))
                {
                    answer.Add(next);
                }
            }
            return answer;
        }
        var result = GraphAlgos.BfsToEnd(start, GetNext, x => x == end);
        return result.distance;
    }

    public string Part2()
    {
        var answer = DoPart2(70, 70, 1024);
        return $"{answer.Item1},{answer.Item2}";
    }

    public (int, int) DoPart2(int width, int height, int initialFalls)
    {
        for (int i = initialFalls; i <= incoming.Length; i++)
        {
            var path = DoPart1(width, height, i);
            if (path <= 0)
            {
                var answer = incoming[i - 1];
                return (answer.Row, answer.Col);
            }
        }
        throw new Exception("Not found");
    }
}
