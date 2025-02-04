using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AocCommon;

namespace Aoc2015;

// https://adventofcode.com/2015/day/19
// --- Day 19: Medicine for Rudolph ---
public class Day19 : IAocDay
{
    private readonly (string InToken, string OutToken)[] rules;
    private readonly string givenMolecule;

    public Day19(string input)
    {
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        Debug.Assert(paragraphs.Length == 2);
        rules = paragraphs[0].Split('\n').Select(line =>
        {
            var parts = line.Split("=>", Parsing.TrimAndDiscard);
            Debug.Assert(parts.Length == 2);
            return (parts[0], parts[1]);
        }).ToArray();
        givenMolecule = paragraphs[1];
    }

    public string Part1()
    {
        var rulesLookup = rules
            .GroupBy(r => r.InToken)
            .ToDictionary(g => g.Key, g => g.Select(r => r.OutToken).ToArray());
        // Replacements
        string pattern = string.Join("|", rulesLookup.Keys);
        var matches = Regex.EnumerateMatches(givenMolecule, pattern);
        HashSet<string> newMolecules = new();
        foreach (var match in matches)
        {
            var prefix = givenMolecule.AsSpan(0, match.Index);
            var suffix = givenMolecule.AsSpan(match.Index + match.Length);
            var initial = givenMolecule.Substring(match.Index, match.Length);
            var replacements = rulesLookup[initial];
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
        // Work backwards from the given molecule towards the initial state, "e"
        // Treat the "e => ..." rules separately because there are no "... => e" rules
        // DFS, only works if the first solution has the best length
        var initialRules = rules.Where(r => r.InToken == "e").ToArray();
        var otherRules = rules.Where(r => r.InToken != "e").ToArray();
        Stack<(string molecule, int replacements)> stack = new();
        stack.Push((givenMolecule, 0));
        while (stack.TryPop(out var state))
        {
            var (molecule, replacements) = state;
            if (molecule == "e")
            {
                return replacements.ToString();
            }
            foreach (var r in initialRules)
            {
                if (molecule == r.OutToken)
                {
                    stack.Push(("e", replacements + 1)); // by definition of initialRules
                }
            }
            for (int i = 0; i < molecule.Length; i++)
            {
                foreach (var r in otherRules)
                {
                    if (molecule.AsSpan(i).StartsWith(r.OutToken))
                    {
                        StringBuilder replaced = new();
                        replaced.Append(molecule.AsSpan(0, i));
                        replaced.Append(r.InToken);
                        replaced.Append(molecule.AsSpan(i + r.OutToken.Length));
                        stack.Push((replaced.ToString(), replacements + 1));
                    }
                }
            }
        }
        throw new Exception("Answer not found");
    }
}
