using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/22
// --- Day 22: Grid Computing ---
public class Day22(string input) : IAocDay
{
    public string Part1()
    {
        List<(int Used, int Avail)> nodes = new();
        var matches = Regex.Matches(input, @"\/dev\/grid\/node-x(\d+)-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+\d+%");
        foreach (var parse in matches.Cast<Match>())
        {
            nodes.Add((int.Parse(parse.Groups[4].ValueSpan), int.Parse(parse.Groups[5].ValueSpan)));
        }
        int answer = 0;
        for (int a = 0; a < nodes.Count; a++)
        {
            for (int b = 0; b < nodes.Count; b++)
            {
                if (a != b && nodes[a].Used > 0 && nodes[a].Used <= nodes[b].Avail)
                {
                    answer++;
                }
            }
        }
        return answer.ToString();
    }

    private record GridState(VectorXY Hole, VectorXY Wanted);

    public string Part2()
    {
        // Solution based on moving the empty node ("hole") like the example
        // Parse
        Dictionary<VectorXY, (int Size, int Used)> parseGrid = new();
        int maxX = 0;
        int maxY = 0;
        var matches = Regex.Matches(input, @"\/dev\/grid\/node-x(\d+)-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+\d+%");
        foreach (var parse in matches.Cast<Match>())
        {
            VectorXY coords = new(int.Parse(parse.Groups[1].ValueSpan), int.Parse(parse.Groups[2].ValueSpan));
            maxX = Math.Max(maxX, coords.X);
            maxY = Math.Max(maxY, coords.Y);
            int size = int.Parse(parse.Groups[3].ValueSpan);
            int used = int.Parse(parse.Groups[4].ValueSpan);
            parseGrid[coords] = (size, used);
        }
        // Initialization
        VectorXY initialWanted = new(maxX, 0);
        var holes = parseGrid.Where(kvp => kvp.Value.Used == 0);
        var initialHole = holes.Single().Key;
        GridState initialState = new(initialHole, initialWanted);
        // BFS
        List<GridState> GetNext(GridState state)
        {
            List<GridState> results = new();
            foreach (VectorXY newHole in state.Hole.NextFour())
            {
                if (0 <= newHole.X && newHole.X <= maxX && 0 <= newHole.Y && newHole.Y <= maxY)
                {
                    if (parseGrid[newHole].Size >= parseGrid[state.Hole].Used)
                    {
                        VectorXY newWanted = (newHole == state.Wanted) ? state.Hole : state.Wanted;
                        results.Add(new(newHole, newWanted));
                    }
                }
            }
            return results;
        }
        var traversal = GraphAlgos.BfsToEnd(initialState, GetNext, s => s.Wanted == VectorXY.Zero);
        return traversal.distance.ToString();
    }
}
