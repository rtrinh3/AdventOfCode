using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/16
// --- Day 16: Reindeer Maze ---
public class Day16(string input) : IAocDay
{
    private readonly Grid maze = new(input, '#');

    private record Node(VectorRC Position, VectorRC Direction);

    List<(Node, int)> GetNext(Node node)
    {
        if (maze.Get(node.Position) == 'E')
        {
            return [];
        }
        var left = node with { Direction = node.Direction.RotatedLeft() };
        var right = node with { Direction = node.Direction.RotatedRight() };
        List<(Node, int)> next = [(left, 1000), (right, 1000)];
        var forwardPosition = node.Position + node.Direction;
        if (maze.Get(forwardPosition) != '#')
        {
            var forward = node with { Position = forwardPosition };
            next.Add(new(forward, 1));
        }
        return next;
    }

    public string Part1()
    {
        VectorRC startPos = maze.Iterate().Single(x => x.Value == 'S').Position;
        Node startNode = new(startPos, VectorRC.Right);
        var result = GraphAlgos.DijkstraToEnd(startNode, GetNext, node => maze.Get(node.Position) == 'E');
        return result.distance.ToString();
    }

    public string Part2()
    {
        VectorRC startPos = maze.Iterate().Single(x => x.Value == 'S').Position;
        Node startNode = new(startPos, VectorRC.Right);
        var paths = DijkstraMultipath(startNode, GetNext);
        VectorRC endPos = maze.Iterate().Single(x => x.Value == 'E').Position;
        var endNodes = paths.Where(kvp => kvp.Key.Position == endPos).ToList();
        var minimumScore = endNodes.Min(kvp => kvp.Value.Distance);
        endNodes.RemoveAll(kvp => kvp.Value.Distance != minimumScore);
        HashSet<Node> visited = new();
        void Visit(Node node)
        {
            // Add() returns false if already present
            if (!visited.Add(node))
            {
                return;
            }
            foreach (var parent in paths[node].Parents)
            {
                Visit(parent);
            }
        }
        foreach (var kvp in endNodes)
        {
            Visit(kvp.Key);
        }
        var positions = visited.Select(x => x.Position).Distinct().ToList();
        return positions.Count.ToString();
    }

    public static Dictionary<T, (List<T> Parents, int Distance)> DijkstraMultipath<T>(T start, Func<T, IEnumerable<(T, int)>> getNeighbors)
            where T : notnull
    {
        PriorityQueue<T, int> queue = new();
        queue.Enqueue(start, 0);
        Dictionary<T, int> distances = new();
        distances[start] = 0;
        DefaultDict<T, List<T>> parents = new();
        while (queue.TryDequeue(out var current, out var currentDistance))
        {
            if (distances[current] < currentDistance)
            {
                continue;
            }
            if (distances[current] > currentDistance)
            {
                throw new Exception("Not supposed to happen");
            }
            foreach (var (neighbor, distanceToNext) in getNeighbors(current))
            {
                var nextDistance = currentDistance + distanceToNext;
                if (!distances.TryGetValue(neighbor, out var recordedDistance) || nextDistance <= recordedDistance)
                {
                    if (nextDistance == recordedDistance)
                    {
                        parents[neighbor].Add(current);
                    }
                    else
                    {
                        distances[neighbor] = nextDistance;
                        parents[neighbor].Clear();
                        parents[neighbor].Add(current);
                        queue.Enqueue(neighbor, nextDistance);
                    }
                }
            }
        }
        Dictionary<T, (List<T>, int)> parentsDistances = new();
        foreach (var key in distances.Keys)
        {
            parentsDistances[key] = (parents[key], distances[key]);
        }
        return parentsDistances;
    }
}
