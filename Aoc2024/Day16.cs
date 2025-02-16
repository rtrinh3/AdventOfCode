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
        var scoreFromStart = GraphAlgos.DijkstraToAll(startNode, GetNext);
        var endNodes = scoreFromStart.Where(kvp => maze.Get(kvp.Key.Position) == 'E').ToList();
        var minimumScore = endNodes.Min(kvp => kvp.Value.distance);
        endNodes.RemoveAll(kvp => kvp.Value.distance > minimumScore);
        var scoreFromEnds = endNodes.Select(node =>
        {
            Node oppositeNode = new(node.Key.Position, -node.Key.Direction);
            var scoreFromThisEnd = GraphAlgos.DijkstraToAll(oppositeNode, GetNext);
            return scoreFromThisEnd.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.distance);
        }).ToArray();
        var onOptimalPath = scoreFromStart
            .Where(kvpFromStart => scoreFromEnds.Any(scoreFromEnd =>
            {
                Node nodeFromEnd = new(kvpFromStart.Key.Position, -kvpFromStart.Key.Direction);
                return scoreFromEnd.TryGetValue(nodeFromEnd, out var distanceFromEnd) &&
                distanceFromEnd + kvpFromStart.Value.distance == minimumScore;
            })
            ).Select(kvp => kvp.Key.Position)
            .Distinct()
            .ToArray();
        var answer = onOptimalPath.Length;
        return answer.ToString();
    }
}
