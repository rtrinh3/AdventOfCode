using AocCommon;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/24
// --- Day 24: Air Duct Spelunking ---
public class Day24 : IAocDay
{
    private readonly int[,] distances;

    public Day24(string input)
    {
        var maze = new Grid(input, '#');
        Dictionary<int, VectorRC> keyPositions = new();
        uint allKeys = 0u;
        foreach (var x in maze.Iterate())
        {
            if (char.IsAsciiDigit(x.Value))
            {
                int value = x.Value - '0';
                keyPositions[value] = x.Position;
                allKeys |= 1u << value;
            }
        }
        int numberOfKeys = keyPositions.Count;
        Debug.Assert((1u << numberOfKeys) - 1u == allKeys);
        IEnumerable<VectorRC> GetNextInMaze(VectorRC pos)
        {
            return pos.NextFour().Where(x => maze.Get(x) != '#');
        }
        this.distances = new int[keyPositions.Count, keyPositions.Count];
        for (int i = 0; i < numberOfKeys; i++)
        {
            var traversal = GraphAlgos.BfsToAll(keyPositions[i], GetNextInMaze);
            for (int j = 0; j < numberOfKeys; j++)
            {
                distances[i, j] = traversal[keyPositions[j]].distance;
            }
        }
    }

    private record TraversalState(int Position, uint Keys);
    List<(TraversalState, int)> GetNextKeys(TraversalState state)
    {
        int numberOfKeys = distances.GetLength(0);
        List<(TraversalState, int)> results = new();
        for (int i = 0; i < numberOfKeys; i++)
        {
            TraversalState newState = new(i, state.Keys | (1u << i));
            results.Add((newState, distances[state.Position, i]));
        }
        return results;
    }

    private int DoPuzzle(Predicate<TraversalState> endPredicate)
    {
        TraversalState initialState = new(0, 1u);
        uint allKeys = (1u << distances.GetLength(0)) - 1u;
        var traversal = GraphAlgos.DijkstraToEnd(initialState, GetNextKeys, endPredicate);
        return traversal.distance;
    }

    public string Part1()
    {
        uint allKeys = (1u << distances.GetLength(0)) - 1u;
        var answer = DoPuzzle(s => s.Keys == allKeys);
        return answer.ToString();
    }

    public string Part2()
    {
        uint allKeys = (1u << distances.GetLength(0)) - 1u;
        TraversalState target = new(0, allKeys);
        var answer = DoPuzzle(s => s == target);
        return answer.ToString();
    }
}
