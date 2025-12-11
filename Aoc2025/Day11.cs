namespace Aoc2025;

// https://adventofcode.com/2025/day/11
// --- Day 11: Reactor ---
public class Day11 : AocCommon.IAocDay
{
    private readonly Dictionary<string, string[]> Connections;
    private readonly Func<string, string, long> GetPathsBetween;

    public Day11(string input)
    {
        Connections = new();
        var lines = input.TrimEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (var line in lines)
        {
            var splitColon = line.Split(':');
            string device = splitColon[0];
            var outputs = splitColon[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Connections.Add(device, outputs);
        }

        GetPathsBetween = AocCommon.Memoization.Make((string origin, string destination) =>
        {
            if (origin == destination)
            {
                return 1L;
            }
            if (Connections.TryGetValue(origin, out var outputs))
            {
                long accumulator = 0L;
                foreach (var output in outputs)
                {
                    accumulator += GetPathsBetween(output, destination);
                }
                return accumulator;
            }
            else
            {
                return 0L;
            }
        });
    }

    public string Part1()
    {        
        var answer = GetPathsBetween("you", "out");
        return answer.ToString();
    }

    public string Part2()
    {
        // Assume svr-fft-dac-out
        var orderA = GetPathsBetween("svr", "fft") * GetPathsBetween("fft", "dac") * GetPathsBetween("dac", "out");
        // Assume svr-dac-fft-out
        var orderB = GetPathsBetween("svr", "dac") * GetPathsBetween("dac", "fft") * GetPathsBetween("fft", "out");
        var answer = orderA + orderB;
        return answer.ToString();
    }
}