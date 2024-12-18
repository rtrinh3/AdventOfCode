using AocCommon;

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
        var answer = DoPart2(70, 70);
        return $"{answer.Item1},{answer.Item2}";
    }

    public (int, int) DoPart2(int width, int height)
    {
        HashSet<VectorRC> obstacles = new(incoming);
        UnionFind<VectorRC> connected = new();
        // Connect all free space
        for (int row = 0; row <= height; row++)
        {
            for (int col = 0; col <= width; col++)
            {
                VectorRC pos = new(row, col);
                if (!obstacles.Contains(pos))
                {
                    foreach (var next in pos.NextFour())
                    {
                        if (0 <= next.Row && next.Row <= height && 0 <= next.Col && next.Col <= width && !obstacles.Contains(next))
                        {
                            connected.Union(pos, next);
                        }
                    }
                }
            }
        }
        // Remove obstacles
        VectorRC start = VectorRC.Zero;
        VectorRC end = new(width, height);
        for (int i = incoming.Length - 1; i >= 0; i--)
        {
            var obstacleToRemove = incoming[i];
            obstacles.Remove(obstacleToRemove);
            foreach (var next in obstacleToRemove.NextFour())
            {
                if (0 <= next.Row && next.Row <= height && 0 <= next.Col && next.Col <= width && !obstacles.Contains(next))
                {
                    connected.Union(obstacleToRemove, next);
                }
            }
            if (connected.AreMerged(start, end))
            {
                return (obstacleToRemove.Row, obstacleToRemove.Col);
            }
        }
        throw new Exception("Answer not found");
    }
}
