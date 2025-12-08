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
        var distances = GetDistanceQueue();
        UnionFindInt connections = new();
        for (int c = 0; c < connectionsToMake; c++)
        {
            distances.TryDequeue(out var pair, out var distance);
            var (boxA, boxB) = pair;
            connections.Union(boxA, boxB);
        }

        // Count
        var groups = Enumerable.Range(0, boxes.Length).Select(connections.Find).GroupBy(x => x);
        var orderedSizes = groups.Select(g => g.Count()).OrderByDescending(count => count);
        var answer = orderedSizes.Take(3).Aggregate((a, b) => a * b);

        return answer;
    }

    public string Part2()
    {
        var distances = GetDistanceQueue();
        int numberOfCircuits = boxes.Length;
        UnionFindInt connections = new();
        while (distances.TryDequeue(out var pair, out var distance))
        {
            var (boxA, boxB) = pair;
            if (!connections.AreMerged(boxA, boxB))
            {
                connections.Union(boxA, boxB);
                numberOfCircuits--;
            }
            if (numberOfCircuits <= 1)
            {
                var answer = boxes[boxA].X * boxes[boxB].X;
                return answer.ToString();
            }
        }
        throw new Exception("No answer found!?");
    }

    private PriorityQueue<(int, int), long> GetDistanceQueue()
    {
        IEnumerable<((int, int), long)> distances =
            from a in Enumerable.Range(0, boxes.Length)
            from b in Enumerable.Range(a + 1, boxes.Length - a - 1)
            select ((a, b), (boxes[a] - boxes[b]).EuclideanSquared());
        PriorityQueue<(int, int), long> queue = new(distances); // Construct the heap using the heapify operation
        return queue;
    }
}