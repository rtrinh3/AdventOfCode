using AocCommon;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Aoc2024;

// https://adventofcode.com/2024/day/23
// --- Day 23: LAN Party ---
public class Day23 : IAocDay
{
    private readonly DefaultDict<string, HashSet<string>> connections;

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
            var neighbors = connections[computer].ToList();
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
        var computers = connections.Keys.Order().ToArray();
        int[] maxNetwork = [];
        void Visit(int[] network)
        {
            // Check if max complete network
            if (network.Length > maxNetwork.Length)
            {
                maxNetwork = network;
            }
            // Recurse
            int skip = network.LastOrDefault(-1) + 1;
            for (int i = skip; i < computers.Length; i++)
            {

                if (network.All(c => connections[computers[c]].Contains(computers[i])))
                {
                    int[] newNetwork = [.. network, i];
                    Visit(newNetwork);
                }
            }
        }
        Visit([]);
        var answer = string.Join(',', maxNetwork.Select(i => computers[i]));
        return answer;
    }
}
