using AocCommon;

namespace Aoc2025;

// https://adventofcode.com/2025/day/11
// --- Day 11: Reactor ---
public class Day11(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        // Parse
        Dictionary<string, string[]> connections = new();
        var lines = input.TrimEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var line in lines)
        {
            var splitColon = line.Split(':');
            string device = splitColon[0];
            var outputs = splitColon[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            connections.Add(device, outputs);
        }

        // Traverse
        Func<string, long> GetPathsToOut = null;
        GetPathsToOut = Memoization.Make((string device) =>
        {
            if (device == "out")
            {
                return 1L;
            }
            if (connections.TryGetValue(device, out var outputs))
            {
                long accumulator = 0L;
                foreach (var output in outputs)
                {
                    accumulator += GetPathsToOut(output);
                }
                return accumulator;
            }
            else
            {
                return 0L;
            }
        });
        var answer = GetPathsToOut("you");
        return answer.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}