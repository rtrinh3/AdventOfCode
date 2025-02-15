using AocCommon;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Aoc2016;

// https://adventofcode.com/2016/day/17
// --- Day 17: Two Steps Forward ---
public class Day17(string input) : IAocDay
{
    private record TraversalState(VectorRC Position, string Path);

    private static readonly VectorRC endPos = new(3, 3);
    
    private readonly TraversalState initialState = new(VectorRC.Zero, input.Trim());
    
    private static List<TraversalState> GetNext(TraversalState state)
    {
        List<TraversalState> results = new();
        if (state.Position == endPos)
        {
            return results;
        }
        var hashInputBytes = Encoding.ASCII.GetBytes(state.Path);
        var hashBytes = MD5.HashData(hashInputBytes);
        var up = (hashBytes[0] & 0xf0) >> 4;
        if (state.Position.Row > 0 && up > 0x0a)
        {
            results.Add(new(state.Position + VectorRC.Up, state.Path + "U"));
        }
        var down = hashBytes[0] & 0x0f;
        if (state.Position.Row < 3 && down > 0x0a)
        {
            results.Add(new(state.Position + VectorRC.Down, state.Path + "D"));
        }
        var left = (hashBytes[1] & 0xf0) >> 4;
        if (state.Position.Col > 0 && left > 0x0a)
        {
            results.Add(new(state.Position + VectorRC.Left, state.Path + "L"));
        }
        var right = hashBytes[1] & 0x0f;
        if (state.Position.Col < 3 && right > 0x0a)
        {
            results.Add(new(state.Position + VectorRC.Right, state.Path + "R"));
        }
        return results;
    }

    public string Part1()
    {
        var traversal = GraphAlgos.BfsToEnd(initialState, GetNext, s => s.Position == endPos);
        var finalHashInput = traversal.path.First().Path;
        Debug.Assert(finalHashInput.StartsWith(initialState.Path));
        var answer = finalHashInput[initialState.Path.Length..];
        return answer;
    }

    public string Part2()
    {
        var traversal = GraphAlgos.BfsToAll(initialState, GetNext);
        var answer = traversal.Where(kvp => kvp.Key.Position == endPos)
            .Select(kvp => kvp.Value.distance)
            .Max();
        return answer.ToString();
    }
}
