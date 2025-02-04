using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2015;

// https://adventofcode.com/2015/day/13
// --- Day 13: Knights of the Dinner Table ---
public class Day13 : IAocDay
{
    private readonly Dictionary<(string, string), int> relationships = new();

    public Day13(string input)
    {
        foreach (var line in input.TrimEnd().Split('\n'))
        {
            var m = Regex.Match(line, @"(\w+) would (\w+) (\d+) happiness units by sitting next to (\w+).");
            string subject = m.Groups[1].Value;
            string feeling = m.Groups[2].Value;
            int value = int.Parse(m.Groups[3].ValueSpan);
            string target = m.Groups[4].Value;
            if (feeling == "lose") value = -value;
            relationships[(subject, target)] = value;
        }
    }

    int EvaluateArrangement(string[] arrangement)
    {
        int score = 0;
        for (int i = 0; i < arrangement.Length; i++)
        {
            string me = arrangement[i];
            string left = (i == 0) ? arrangement[^1] : arrangement[i - 1];
            string right = (i == arrangement.Length - 1) ? arrangement[0] : arrangement[i + 1];
            score += relationships.GetValueOrDefault((me, left), 0);
            score += relationships.GetValueOrDefault((me, right), 0);
        }
        return score;
    }

    public string Part1()
    {
        string[] names = relationships.Keys.Select(x => x.Item1).Distinct().Order().ToArray();
        var permutations = MoreMath.IteratePermutations(names);
        int maxScore = int.MinValue;
        foreach (var p in permutations)
        {
            maxScore = Math.Max(maxScore, EvaluateArrangement(p));
        }
        return maxScore.ToString();
    }

    public string Part2()
    {
        string[] names = relationships.Keys.Select(x => x.Item1).Distinct().Order().ToArray();
        var permutations = MoreMath.IteratePermutations(names);
        int maxScore = int.MinValue;
        foreach (var p in permutations)
        {
            var augmented = p.Append("").ToArray();
            maxScore = Math.Max(maxScore, EvaluateArrangement(augmented));
        }
        return maxScore.ToString();
    }
}
