using AocCommon;

namespace Aoc2025;

// https://adventofcode.com/2025/day/8
// --- Day 8: Playground ---
public class Day08(string input) : IAocDay
{
    private readonly VectorXYZ[] boxes = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
        .Select(line =>
        {
            var parts = line.Split(',');
            var coords = parts.Select(int.Parse).ToArray();
            return new VectorXYZ(coords[0], coords[1], coords[2]);
        })
        .ToArray();

    public string Part1()
    {
        var answer = DoPart1(1000);
        return answer.ToString();
    }

    public int DoPart1(int connectionsToMake)
    {
        // Distances
        List<(long, VectorXYZ, VectorXYZ)> distances = new();
        for (int a = 0; a < boxes.Length; a++)
        {
            for (int b = a + 1; b < boxes.Length; b++)
            {
                var distance = (boxes[b] - boxes[a]).EuclideanSquared();
                distances.Add((distance, boxes[a], boxes[b]));
            }
        }
        //distances.Sort((a, b) => a.Item1.CompareTo(b.Item1));
        distances = distances.OrderBy(x => x.Item1).ToList();

        // Connections
        UnionFind<VectorXYZ> connections = new();
        for (int c = 0; c < connectionsToMake; c++)
        {
            connections.Union(distances[c].Item2, distances[c].Item3);
        }

        // Count
        var groups = boxes.Select(connections.Find).GroupBy(x => x);
        var orderedSizes = groups.Select(g => g.Count()).OrderByDescending(count => count);
        var answer = orderedSizes.Take(3).Aggregate((a, b) => a * b);

        return answer;
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}