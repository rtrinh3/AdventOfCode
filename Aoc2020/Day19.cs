using AocCommon;

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

        public long Part1()
        {
            Dictionary<string, Func<string, int, int>> partOneRules = new();
            foreach (var (ruleNumber, alternatives) in rules)
            {
                partOneRules[ruleNumber] = (string message, int index) =>
                {
                    foreach (var alt in alternatives)
                    {
                        int current = index;
                        foreach (var subrule in alt)
                        {
                            current = partOneRules[subrule](message, current);
                            if (current < 0)
                            {
                                break;
                            }
                        }
                        if (current >= index)
                        {
                            return current;
                        }
                    }
                    return -1;
                };
            }
            partOneRules["\"a\""] = (string message, int index) => message[index] == 'a' ? index + 1 : -1;
            partOneRules["\"b\""] = (string message, int index) => message[index] == 'b' ? index + 1 : -1;

            int answer = 0;
            foreach (string message in messages)
            {
                var test = partOneRules["0"](message, 0);
                if (test == message.Length)
                {
                    answer++;
                }
            }
            return answer;
        }

        public long Part2()
        {
            Dictionary<string, Func<string, int, HashSet<int>>> partTwoRules = new();
            foreach (var (ruleNumber, alternatives) in rules)
            {
                partTwoRules[ruleNumber] = (string message, int index) =>
                {
                    HashSet<int> results = new();
                    foreach (var alt in alternatives)
                    {
                        IEnumerable<int> altResults = [index];
                        for (int i = 0; i < alt.Length; i++)
                        {
                            var subrule = alt[i];
                            altResults = altResults.SelectMany(intermediate => partTwoRules[subrule](message, intermediate)).ToHashSet();
                        }
                        results.UnionWith(altResults);
                    }
                    return results;
                };
            }
            partTwoRules["\"a\""] = (string message, int index) => (index < message.Length && message[index] == 'a') ? [index + 1] : [];
            partTwoRules["\"b\""] = (string message, int index) => (index < message.Length && message[index] == 'b') ? [index + 1] : [];
            partTwoRules["8"] = (string message, int index) =>
            {
                HashSet<int> results = new();
                HashSet<int> intermediates = [index];
                while (true)
                {
                    var newIntermediates = intermediates.SelectMany(intermediate => partTwoRules["42"](message, intermediate)).ToHashSet();
                    int oldSize = results.Count;
                    results.UnionWith(newIntermediates);
                    if (results.Count == oldSize)
                    {
                        break;
                    }
                    intermediates = newIntermediates;
                }
                return results;
            };
            partTwoRules["11"] = (string message, int index) =>
            {
                if (index >= message.Length)
                {
                    return [];
                }
                HashSet<int> results = new();
                int repetitions = 1;
                while (repetitions < 10) // TODO better handle repetitions
                {
                    HashSet<int> intermediates = [index];
                    for (int i = 0; i < repetitions; i++)
                    {
                        intermediates = intermediates.SelectMany(intermediate => partTwoRules["42"](message, intermediate)).ToHashSet();
                    }
                    if (intermediates.Count > 0)
                    {
                        for (int i = 0; i < repetitions; i++)
                        {
                            intermediates = intermediates.SelectMany(intermediate => partTwoRules["31"](message, intermediate)).ToHashSet();
                        }
                        results.UnionWith(intermediates);
                    }
                    repetitions++;
                }
                return results;
            };

            foreach (var ruleNumber in partTwoRules.Keys.ToList())
            {
                partTwoRules[ruleNumber] = Memoization.Make(partTwoRules[ruleNumber]);
            }

            int answer = 0;
            foreach (string message in messages)
            {
                var test = partTwoRules["0"](message, 0);
                if (test.Contains(message.Length))
                {
                    //Console.WriteLine(message);
                    answer++;
                }
            }
            return answer;
        }
    }
}
