namespace Aoc2020
{
    // https://adventofcode.com/2020/day/19
    // --- Day 19: Monster Messages ---
    public class Day19 : IAocDay
    {
        private readonly Dictionary<string, string[][]> rules;
        private readonly string[] messages;

        public Day19(string input)
        {
            var parts = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            string rulesPart = parts[0];
            var rulesLines = rulesPart.Split('\n');
            rules = new();
            foreach (var line in rulesLines)
            {
                var ruleNumberSplit = line.Split(':');
                string ruleNumber = ruleNumberSplit[0].Trim();
                var alternativesSplit = ruleNumberSplit[1].Split('|');
                rules[ruleNumber] = alternativesSplit.Select(alt => alt.Trim().Split(' ')).ToArray();
            }
            string messagesPart = parts[1];
            messages = messagesPart.Split('\n');
        }

        public string Part1()
        {
            rules["8"] = [["42"]];
            rules["11"] = [["42", "31"]];
            int answer = 0;
            foreach (string message in messages)
            {
                var test = EvaluateRule("0", message, 0);
                if (test.Contains(message.Length))
                {
                    answer++;
                }
            }
            return answer.ToString();
        }

        public string Part2()
        {
            rules["8"] = [["42"], ["42", "8"]];
            rules["11"] = [["42", "31"], ["42", "11", "31"]];
            int answer = 0;
            foreach (string message in messages)
            {
                var test = EvaluateRule("0", message, 0);
                if (test.Contains(message.Length))
                {
                    answer++;
                }
            }
            return answer.ToString();
        }

        private HashSet<int> EvaluateRule(string ruleNumber, string message, int index)
        {
            HashSet<int> results = new();
            if (ruleNumber.Length == 3 && ruleNumber.StartsWith('"') && ruleNumber.EndsWith('"'))
            {
                if (index < message.Length && message[index] == ruleNumber[1])
                {
                    results.Add(index + 1);
                }
            }
            else if (index < message.Length)
            {
                foreach (var alt in rules[ruleNumber])
                {
                    IEnumerable<int> altResults = [index];
                    for (int i = 0; i < alt.Length; i++)
                    {
                        var subrule = alt[i];
                        altResults = altResults.SelectMany(intermediate => EvaluateRule(subrule, message, intermediate));
                    }
                    results.UnionWith(altResults);
                }
            }
            return results;
        }
    }
}
