using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/19
    public class Day19 : IAocDay
    {
        private record Rule(string Param, string Op, long Comparand, string Target);
        private record Workflow(Rule[] Rules, string Fallback);

        private readonly Dictionary<string, Workflow> workflows;
        private readonly string[] parts;

        public Day19(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            parts = paragraphs[1].Split('\n');

            string[] workflowsString = paragraphs[0].Split('\n');
            workflows = new Dictionary<string, Workflow>();
            foreach (string workflow in workflowsString)
            {
                string[] headerSplit = workflow.Replace("}", "").Split('{');
                string name = headerSplit[0];
                string[] rulesString = headerSplit[1].Split(',');
                Rule[] conditions = rulesString.Take(rulesString.Length - 1).Select(rule =>
                {
                    var parseRule = Regex.Match(rule, @"([xmas])([^\d]+)(\d+):(\w+)");
                    Debug.Assert(parseRule.Success);
                    return new Rule(parseRule.Groups[1].Value, parseRule.Groups[2].Value, long.Parse(parseRule.Groups[3].ValueSpan), parseRule.Groups[4].Value);
                }).ToArray();
                string fallback = rulesString.Last();
                workflows[name] = new Workflow(conditions, fallback);
            }
        }

        public long Part1()
        {
            string Evaluate(Dictionary<string, int> part, string workflowName)
            {
                var workflow = workflows[workflowName];
                foreach (var rule in workflow.Rules)
                {
                    switch (rule.Op)
                    {
                        case ">":
                            if (part[rule.Param] > rule.Comparand)
                            {
                                return rule.Target;
                            }
                            break;
                        case "<":
                            if (part[rule.Param] < rule.Comparand)
                            {
                                return rule.Target;
                            }
                            break;
                        default:
                            throw new Exception("Unrecognized criterion " + rule.Op);
                    }
                }
                return workflow.Fallback;
            }

            long sum = 0;
            foreach (string part in parts)
            {
                Dictionary<string, int> gear = new();
                foreach (var assignments in part.Replace("{", "").Replace("}", "").Split(','))
                {
                    var assignmentParsed = assignments.Split('=');
                    gear[assignmentParsed[0]] = int.Parse(assignmentParsed[1]);
                }
                string rule = "in";
                while (rule != "A" && rule != "R")
                {
                    rule = Evaluate(gear, rule);
                }
                if (rule == "A")
                {
                    sum += gear.Values.Sum();
                }
            }

            return sum;
        }
        public long Part2()
        {
            return -2;
        }
    }
}
