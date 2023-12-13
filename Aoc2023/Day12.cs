using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/12
    public class Day12
    {
        private record Row(string mask, int[] runs);
        private enum CellType { DOT, POUND, FLEX };
        private record CellRun(CellType type, int length);

        private readonly Row[] records;

        public Day12(string input)
        {
            records = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries).Select(line =>
            {
                var parts = line.Split(' ');
                var runs = parts[1].Split(',').Select(int.Parse).ToArray();
                return new Row(parts[0], runs);
            }).ToArray();
        }

        public long Part1()
        {
            return records.Sum(row => DoPuzzle(row));
        }

        public long Part2()
        {
            var unfoldedRecords = records.Select(row =>
            {
                string unfoldedMask = row.mask + "?" + row.mask + "?" + row.mask + "?" + row.mask + "?" + row.mask;
                int[] unfoldedRuns = row.runs.Concat(row.runs).Concat(row.runs).Concat(row.runs).Concat(row.runs).ToArray();
                return new Row(unfoldedMask, unfoldedRuns);
            }).ToArray();
            return unfoldedRecords.Sum(row => DoPuzzle(row));
        }

        private long DoPuzzle(Row row)
        {
            var expandedRuns = new List<CellRun>();
            expandedRuns.Add(new CellRun(CellType.FLEX, 0));
            for (int i = 0; i < row.runs.Length - 1; i++)
            {
                expandedRuns.Add(new CellRun(CellType.POUND, row.runs[i]));
                expandedRuns.Add(new CellRun(CellType.DOT, 1));
                expandedRuns.Add(new CellRun(CellType.FLEX, 0));
            }
            expandedRuns.Add(new CellRun(CellType.POUND, row.runs.Last()));
            expandedRuns.Add(new CellRun(CellType.FLEX, 0));

            Func<int, int, Regex> GetValidator = Memoization.Make((int startIndex, int length) =>
            {
                var patternBuilder = new StringBuilder(@"^");
                string substr = row.mask.Substring(startIndex, length);
                var escapes = substr.Select(c =>
                {
                    return c switch
                    {
                        '.' => @"\.",
                        '#' => @"#",
                        '?' => @".",
                        _ => throw new Exception("What is this mask")
                    };
                });
                patternBuilder.AppendJoin("", escapes);
                patternBuilder.Append('$');
                string pattern = patternBuilder.ToString();
                return new Regex(pattern);
            });

            Func<int, int, long> DoPuzzleRecurse = (x, y) => throw new NotImplementedException("Stub for recursive function");
            DoPuzzleRecurse = Memoization.Make((int maskIndex, int runIndex) =>
            {
                Debug.Assert(maskIndex <= row.mask.Length);
                Debug.Assert(runIndex <= expandedRuns.Count);
                if (runIndex == expandedRuns.Count)
                {
                    return (maskIndex == row.mask.Length) ? 1 : 0;
                }
                var run = expandedRuns[runIndex];
                char runChar = run.type switch
                {
                    CellType.DOT => '.',
                    CellType.POUND => '#',
                    CellType.FLEX => '.',
                    _ => throw new Exception("What is this cell")
                };
                IEnumerable<int> runLengths = run.type switch
                {
                    CellType.DOT => [run.length],
                    CellType.POUND => [run.length],
                    CellType.FLEX => Enumerable.Range(0, 1 + row.mask.Length - maskIndex - expandedRuns.Skip(runIndex).Sum(r => r.length)),
                    _ => throw new Exception("What is this cell")
                };
                long sum = 0;
                foreach (var len in runLengths)
                {
                    string runString = string.Join("", Enumerable.Repeat(runChar, len));
                    var validator = GetValidator(maskIndex, len);
                    if (validator.IsMatch(runString))
                    {
                        sum += DoPuzzleRecurse(maskIndex + len, runIndex + 1);
                    }
                }
                return sum;
            });

            var answer = DoPuzzleRecurse(0, 0);
            return answer;
        }
    }
}
