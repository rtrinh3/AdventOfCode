using AocCommon;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Aoc2024;

// https://adventofcode.com/2024/day/23
// --- Day 23: LAN Party ---
public class Day23 : IAocDay
{
    private readonly DefaultDict<string, List<string>> connections;

    public Day23(string input)
    {
        connections = new();
        var lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            Debug.Assert(parts.Length == 2);
            connections[parts[0]].Add(parts[1]);
            connections[parts[1]].Add(parts[0]);
        }
    }

    public string Part1()
    {
        var chiefComputers = connections.Keys.Where(k => k.StartsWith('t')).ToList();
        HashSet<EquatableArray<string>> trios = new();
        foreach (var computer in chiefComputers)
        {
            var neighbors = connections[computer];
            for (int i = 0; i < neighbors.Count; i++)
            {
                for (int j = i + 1; j < neighbors.Count; j++)
                {
                    if (connections[neighbors[i]].Contains(neighbors[j]))
                    {
                        Span<string> trio = [computer, neighbors[i], neighbors[j]];
                        trio.Sort();
                        trios.Add(new EquatableArray<string>(trio.ToImmutableArray()));
                    }
                }
            }
        }

        var answer = trios.Count;
        return answer.ToString();
    }

    public string Part2()
    {
        var computers = connections.Keys.Order().ToList();
        string[] maxNetwork = [];
        void Visit(string[] network)
        {
            // Check if max complete network
            if (network.Length > maxNetwork.Length)
            {
                maxNetwork = network;
            }
            // Recurse
            var tails = (network.Length == 0) ? computers : computers.SkipWhile(c => c.CompareTo(network.Last()) <= 0).ToList();
            foreach (var computer in tails)
            {
                if (network.All(c => connections[c].Contains(computer)))
                {
                    string[] newNetwork = [.. network, computer];
                    Visit(newNetwork);
                }
            }
        }
        Visit([]);
        var answer = string.Join(',', maxNetwork);
        return answer;
    }
}
