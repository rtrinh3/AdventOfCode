using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AocCommon;

namespace Aoc2015;

public class Day19(string input) : IAocDay
{
    public string Part1()
    {
        // Parse
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        Debug.Assert(paragraphs.Length == 2);
        var rules = paragraphs[0].Split('\n').Select(line =>
        {
            var parts = line.Split("=>", Parsing.TrimAndDiscard);
            Debug.Assert(parts.Length == 2);
            return (parts[0], parts[1]);
        }).GroupBy(r => r.Item1)
        .ToDictionary(g => g.Key, g => g.Select(r => r.Item2).ToArray());
        string molecule = paragraphs[1];

        // Replacements
        string pattern = string.Join("|", rules.Keys);
        var matches = Regex.EnumerateMatches(molecule, pattern);
        HashSet<string> newMolecules = new();
        foreach (var match in matches)
        {
            var prefix = molecule.AsSpan(0, match.Index);
            var suffix = molecule.AsSpan(match.Index + match.Length);
            var initial = molecule.Substring(match.Index, match.Length);
            var replacements = rules[initial];
            foreach (var replacement in replacements)
            {
                StringBuilder replaced = new();
                replaced.Append(prefix);
                replaced.Append(replacement);
                replaced.Append(suffix);
                newMolecules.Add(replaced.ToString());
            }
        }
        return newMolecules.Count.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}
