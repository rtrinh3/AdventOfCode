using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/16
// --- Day 16: Reindeer Maze ---
public class Day16(string input) : IAocDay
{
    private readonly Grid maze = new(input, '#');

    private record Node(VectorRC Position, VectorRC Direction);

    public string Part1()
    {
        VectorRC startPos = maze.Iterate().Single(x => x.Value == 'S').Position;
        Node startNode = new(startPos, VectorRC.Right);
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
        var result = GraphAlgos.DijkstraToEnd(startNode, GetNext, node => maze.Get(node.Position) == 'E');
        return result.distance.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}
