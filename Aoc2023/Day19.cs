using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/19
    public class Day19 : IAocDay
    {
        private record Rule(string Param, string Op, int Comparand, string Target);
        private record Workflow(Rule[] Rules, string Fallback);

        private readonly Dictionary<string, Workflow> workflows;
        private readonly string partsString;

        public Day19(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            partsString = paragraphs[1];

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
                    return new Rule(parseRule.Groups[1].Value, parseRule.Groups[2].Value, int.Parse(parseRule.Groups[3].ValueSpan), parseRule.Groups[4].Value);
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
                            throw new NotImplementedException("Unimplemented criterion " + rule.Op);
                    }
                }
                return workflow.Fallback;
            }

            long sum = 0;
            foreach (string part in partsString.Split('\n'))
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

        private record Range(int Start, int Length);

        public long Part2()
        {
            Dictionary<string, Range> initialRange = new();
            initialRange["x"] = new Range(1, 4000);
            initialRange["m"] = new Range(1, 4000);
            initialRange["a"] = new Range(1, 4000);
            initialRange["s"] = new Range(1, 4000);
            List<Dictionary<string, Range>> acceptedRanges = new();
            List<Dictionary<string, Range>> rejectedRanges = new();

            Stack<(Dictionary<string, Range> range, string workflowName, int ruleIndex)> rangeQueue = new();
            rangeQueue.Push((initialRange, "in", 0));
            while (rangeQueue.TryPop(out var range))
            {
                if (range.workflowName == "A")
                {
                    acceptedRanges.Add(range.range);
                    continue;
                }
                else if (range.workflowName == "R")
                {
                    rejectedRanges.Add(range.range);
                    continue;
                }
                var workflow = workflows[range.workflowName];
                if (workflow.Rules.Length <= range.ruleIndex)
                {
                    rangeQueue.Push((range.range, workflow.Fallback, 0));
                    continue;
                }
                var rule = workflow.Rules[range.ruleIndex];
                var targetedRange = range.range[rule.Param];
                switch (rule.Op)
                {
                    case ">":
                        if (targetedRange.Start > rule.Comparand)
                        {
                            // Entirely > than Comparand, goto target
                            rangeQueue.Push((range.range, rule.Target, 0));
                        }
                        else if (targetedRange.Start + targetedRange.Length - 1 <= rule.Comparand)
                        {
                            // Entirely <= than Comparand, goto next rule
                            rangeQueue.Push((range.range, range.workflowName, range.ruleIndex + 1));
                        }
                        else
                        {
                            // The overlap goes to the target workflow
                            Range overlap = new Range(rule.Comparand + 1, targetedRange.Start + targetedRange.Length - rule.Comparand - 1);
                            Dictionary<string, Range> overlapRanges = new Dictionary<string, Range>(range.range);
                            overlapRanges[rule.Param] = overlap;
                            rangeQueue.Push((overlapRanges, rule.Target, 0));
                            // The leftover goes to the next rule
                            Range nonOverlap = new Range(targetedRange.Start, rule.Comparand - targetedRange.Start + 1);
                            Dictionary<string, Range> nonOverlapRanges = new Dictionary<string, Range>(range.range);
                            nonOverlapRanges[rule.Param] = nonOverlap;
                            rangeQueue.Push((nonOverlapRanges, range.workflowName, range.ruleIndex + 1));

                        }
                        break;
                    case "<":
                        if (targetedRange.Start < rule.Comparand)
                        {
                            if (targetedRange.Start + targetedRange.Length <= rule.Comparand)
                            {
                                // Entirely < than Comparand, goto target
                                rangeQueue.Push((range.range, rule.Target, 0));
                            }
                            else
                            {
                                // The overlap goes to the target workflow
                                Range overlap = new Range(targetedRange.Start, rule.Comparand - targetedRange.Start);
                                Dictionary<string, Range> overlapRanges = new Dictionary<string, Range>(range.range);
                                overlapRanges[rule.Param] = overlap;
                                rangeQueue.Push((overlapRanges, rule.Target, 0));
                                // The leftover goes to the next rule
                                Range nonOverlap = new Range(rule.Comparand, targetedRange.Start + targetedRange.Length - rule.Comparand);
                                Dictionary<string, Range> nonOverlapRanges = new Dictionary<string, Range>(range.range);
                                nonOverlapRanges[rule.Param] = nonOverlap;
                                rangeQueue.Push((nonOverlapRanges, range.workflowName, range.ruleIndex + 1));
                            }
                        }
                        else
                        {
                            // Entirely >= than Comparand, goto next rule
                            rangeQueue.Push((range.range, range.workflowName, range.ruleIndex + 1));
                        }
                        break;
                    default:
                        throw new NotImplementedException("Unimplemented criterion " + rule.Op);
                }
            }

            long sum = 0;
            foreach (var ranges in acceptedRanges)
            {
                long product = ranges.Values.Select(r => r.Length).Aggregate(1L, (acc, val) => acc * val);
                sum += product;
            }
            // Sanity check
            //long rejectedSum = rejectedRanges.Sum(ranges => ranges.Values.Select(r => r.Length).Aggregate(1L, (acc, val) => acc * val));
            //Console.WriteLine($"Accepted {sum}, Rejected {rejectedSum}, total {sum + rejectedSum}");

            return sum;
        }
    }
}
