namespace Aoc2020
{
    // https://adventofcode.com/2020/day/19
    // --- Day 19: Monster Messages ---
    public class Day19 : IAocDay
    {
        private readonly Dictionary<string, Func<string, int, int>> rules;
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
                var alternatives = alternativesSplit.Select(alt => alt.Trim().Split(' ')).ToArray();
                rules[ruleNumber] = (string message, int index) =>
                {
                    foreach (var alt in alternatives)
                    {
                        int current = index;
                        foreach (var subrule in alt)
                        {
                            current = rules[subrule](message, current);
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
            rules["\"a\""] = (string message, int index) => message[index] == 'a' ? index + 1 : -1;
            rules["\"b\""] = (string message, int index) => message[index] == 'b' ? index + 1 : -1;

            string messagesPart = parts[1];
            messages = messagesPart.Split('\n');
        }

        public long Part1()
        {
            int answer = 0;
            foreach (string message in messages)
            {
                var test = rules["0"](message, 0);
                if (test == message.Length)
                {
                    answer++;
                }
            }
            return answer;
        }

        public long Part2()
        {
            throw new NotImplementedException();
        }
    }
}
