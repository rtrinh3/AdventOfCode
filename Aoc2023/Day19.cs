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
    public class Day19(string input) : IAocDay
    {
        public long Part1()
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            string[] workflowsString = paragraphs[0].Split('\n');
            string[] partsString = paragraphs[1].Split('\n');

            Dictionary<string, string[]> workflows = new();
            foreach (string workflow in workflowsString)
            {
                string[] headerSplit = workflow.Replace("}", "").Split('{');
                string name = headerSplit[0];
                string[] rules = headerSplit[1].Split(',');
                workflows[name] = rules;
            }
            string Evaluate(Dictionary<string, int> part, string workflowName)
            {
                foreach (string rule in workflows[workflowName])
                {
                    string[] ruleParts = rule.Split(':');
                    if (ruleParts.Length == 1)
                    {
                        return ruleParts[0];
                    }
                    else if (ruleParts.Length == 2)
                    {
                        //Console.WriteLine($"if({ruleParts[0]}) goto Label_{ruleParts[1]};");
                        var parsed = Regex.Match(ruleParts[0], @"([xmas])([^\d]+)(\d+)");
                        Debug.Assert(parsed.Success, "Failed parse");
                        switch (parsed.Groups[2].Value)
                        {
                            case ">":
                                if (part[parsed.Groups[1].Value] > int.Parse(parsed.Groups[3].Value))
                                {
                                    return ruleParts[1];
                                }
                                break;
                            case "<":
                                if (part[parsed.Groups[1].Value] < int.Parse(parsed.Groups[3].Value))
                                {
                                    return ruleParts[1];
                                }
                                break;
                            default:
                                throw new Exception("Unrecognized criterion");
                        }
                    }
                    else
                    {
                        throw new Exception("Too many colons");
                    }
                }
                throw new Exception("No match");
            }

            long sum = 0;
            foreach (string part in partsString)
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
