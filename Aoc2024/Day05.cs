using System.Diagnostics;
using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/5
    // --- Day 5: Print Queue ---
    public class Day05 : IAocDay
    {
        private readonly Dictionary<long, HashSet<long>> beforeAfter;
        private readonly long[][] prints;
        private readonly long[][] correctPrints;
        private readonly long[][] incorrectPrints;

        public Day05(string input)
        {
            var paragraphs = input.ReplaceLineEndings("\n").TrimEnd().Split("\n\n");
            var orderLines = paragraphs[0].Split('\n');
            this.beforeAfter = orderLines
                .Select(line => line.Split('|').Select(long.Parse).ToArray())
                .GroupBy(line => line[0])
                .ToDictionary(g => g.Key, g => g.Select(line => line[1]).ToHashSet());

            var printLines = paragraphs[1].Split('\n');
            this.prints = printLines.Select(line => line.Split(',').Select(long.Parse).ToArray()).ToArray();

            List<long[]> correctPrints = new();
            List<long[]> incorrectPrints = new();
            foreach (var print in prints)
            {
                bool correct = true;
                for (int i = 0; i < print.Length - 1; i++) 
                {
                    for (int j = i + 1; j < print.Length; j++)
                    {
                        if (beforeAfter.ContainsKey(print[j]) && beforeAfter[print[j]].Contains(print[i])) 
                        {
                            correct = false;
                            goto VALIDATED;
                        }
                    }
                }
                VALIDATED:
                if (correct) 
                {
                    correctPrints.Add(print);
                }
                else
                {
                    incorrectPrints.Add(print);
                }
            }
            this.correctPrints = correctPrints.ToArray();
            this.incorrectPrints = incorrectPrints.ToArray();
        }

        public string Part1()
        {
            long sum = 0;
            foreach (var print in correctPrints)
            {
                Debug.Assert(print.Length % 2 == 1);
                var middle = print[print.Length / 2];
                sum += middle;
            }

            return sum.ToString();
        }

        public string Part2()
        {
            long sum = 0;
            foreach (var print in incorrectPrints)
            {
                // The complete rules have loops, so we only keep the relevant rules to do a topological sort on them.
                Dictionary<long, List<long>> relevantRules = new();
                foreach (var kvp in beforeAfter)
                {
                    if (print.Contains(kvp.Key))
                    {
                        relevantRules[kvp.Key] = kvp.Value.Where(v => print.Contains(v)).ToList();
                    }
                }
                var topologicalSort = TopologicalSort(relevantRules.Keys, n => relevantRules.GetValueOrDefault(n, []));
                var correct = print.OrderBy(page => topologicalSort.IndexOf(page)).ToList();
                Debug.Assert(correct.Count % 2 == 1);
                var middle = correct[correct.Count / 2];
                sum += middle;
            }

            return sum.ToString();
        }

        // https://en.wikipedia.org/wiki/Topological_sorting#Depth-first_search
        private static List<T> TopologicalSort<T>(IEnumerable<T> nodes, Func<T, IEnumerable<T>> children)
        {
            HashSet<T> unmarked = nodes.ToHashSet();
            HashSet<T> permanentMark = new();
            HashSet<T> temporaryMark = new();
            List<T> result = new();

            void Visit(T n)
            {
                if (permanentMark.Contains(n))
                {
                    return;
                }
                if (temporaryMark.Contains(n))
                {
                    throw new Exception("Graph has cycle");
                }
                temporaryMark.Add(n);
                foreach (var m in children(n))
                {
                    Visit(m);
                }
                temporaryMark.Remove(n);
                unmarked.Remove(n);
                permanentMark.Add(n);
                result.Add(n);
            }

            while (unmarked.Any())
            {
                var pick = unmarked.First();
                Visit(pick);
            }
            result.Reverse();
            return result;
        }
    }
}
