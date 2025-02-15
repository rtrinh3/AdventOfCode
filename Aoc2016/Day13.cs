using AocCommon;
using System.Numerics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/13
// --- Day 13: A Maze of Twisty Little Cubicles ---
public class Day13(int input, int targetX, int targetY) : IAocDay
{
    public Day13(string input) : this(int.Parse(input), 31, 39)
    {
    }

    private bool IsSpacePart1(VectorXY coord)
    {
        long x = coord.X;
        long y = coord.Y;
        if (x < 0 || y < 0)
        {
            return false;
        }
        ulong number = (ulong)(x * x + 3 * x + 2 * x * y + y + y * y + input);
        var popCount = BitOperations.PopCount(number);
        return popCount % 2 == 0;
    }

    private IEnumerable<VectorXY> GetNextPart1(VectorXY coord)
    {
        return coord.NextFour().Where(IsSpacePart1);
    }

    public string Part1()
    {
        VectorXY start = new(1, 1);
        VectorXY end = new(targetX, targetY);
        var answer = GraphAlgos.BfsToEnd(start, GetNextPart1, s => s == end);
        return answer.distance.ToString();
    }

    private bool IsSpacePart2(VectorXY coord)
    {
        return coord.ManhattanMetric() <= 51 && IsSpacePart1(coord);
    }

    private IEnumerable<VectorXY> GetNextPart2(VectorXY coord)
    {
        return coord.NextFour().Where(IsSpacePart2);
    }

    public string Part2()
    {
        VectorXY start = new(1, 1);
        var traversal = GraphAlgos.BfsToAll(start, GetNextPart2);
        var answer = traversal.Count(kvp => kvp.Value.distance <= 50);
        return answer.ToString();
    }
}
