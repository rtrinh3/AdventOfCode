using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024;

// https://adventofcode.com/2024/day/24
// --- Day 24: Crossed Wires ---
public class Day24(string input) : IAocDay
{
    public string Part1()
    {
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        Debug.Assert(paragraphs.Length == 2);
        Dictionary<string, Lazy<bool>> wires = new();

        var initials = paragraphs[0].Split('\n', StringSplitOptions.TrimEntries);
        foreach (var line in initials)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);
            Debug.Assert(parts.Length == 2);
            string name = parts[0];
            bool value = (parts[1] != "0");
            wires[name] = new Lazy<bool>(() => value);
        }

        var gates = paragraphs[1].Split('\n', StringSplitOptions.TrimEntries);
        foreach (var line in gates)
        {
            var parse = Regex.Match(line, @"^(\w+) (\w+) (\w+) -> (\w+)$");
            Debug.Assert(parse.Success);
            string lhs = parse.Groups[1].Value;
            string op = parse.Groups[2].Value;
            string rhs = parse.Groups[3].Value;
            string target = parse.Groups[4].Value;
            wires[target] = new Lazy<bool>(() =>
            {
                return op switch
                {
                    "AND" => wires[lhs].Value && wires[rhs].Value,
                    "OR" => wires[lhs].Value || wires[rhs].Value,
                    "XOR" => wires[lhs].Value ^ wires[rhs].Value,
                    _ => throw new NotImplementedException(op)
                };
            });
        }

        ulong answer = 0;
        foreach (var zWire in wires.Keys.Where(k => k.StartsWith('z')))
        {
            int position = int.Parse(zWire[1..]);
            ulong value = wires[zWire].Value ? 1UL : 0UL;
            answer |= (value << position);
        }

        return answer.ToString();
    }

    public string Part2()
    {
        return nameof(Day24);
    }
}
