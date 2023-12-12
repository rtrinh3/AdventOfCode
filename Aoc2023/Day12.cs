using System;
using System.Collections.Generic;
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
        private enum CellType { SPACE, BROKEN, FLEX };
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
            // Brute force
            long sum = 0;
            //foreach (var row in records)
            Parallel.ForEach(records, row =>
            {
                var maskPattern = new StringBuilder();
                maskPattern.Append(@"^");
                foreach (char c in row.mask)
                {
                    maskPattern.Append(c switch
                    {
                        '.' => @"\.",
                        '#' => @"#",
                        '?' => @".",
                        _ => throw new Exception("What is this pattern")
                    });
                }
                maskPattern.Append(@"$");
                //maskPattern.Dump();
                var maskValidator = new Regex(maskPattern.ToString());

                var runsPattern = new StringBuilder();
                runsPattern.Append(@"^\.*");
                foreach (int run in row.runs.SkipLast(1))
                {
                    runsPattern.Append("#{" + run.ToString() + "}");
                    runsPattern.Append(@"\.+");
                }
                runsPattern.Append("#{" + row.runs.Last().ToString() + "}");
                runsPattern.Append(@"\.*$");
                //runsPattern.Dump();
                var runsValidator = new Regex(runsPattern.ToString());

                int numberOfPatterns = 1 << row.mask.Length;
                //for (int pattern = 0; pattern < numberOfPatterns; pattern++)
                Parallel.For(0, numberOfPatterns, pattern =>
                {
                    var patternSB = new StringBuilder();
                    for (int b = 0; b < row.mask.Length; b++)
                    {
                        patternSB.Append(((pattern & (1 << b)) == 0) ? '#' : '.');
                    }
                    string patternString = patternSB.ToString();
                    if (maskValidator.IsMatch(patternString) && runsValidator.IsMatch(patternString))
                    {
                        Interlocked.Increment(ref sum);
                    }
                });
            });
            return sum;
        }

        public long Part1_Efficient()
        {
            return records.Sum(row => DoPuzzle(row));
        }

        private long DoPuzzle(Row row)
        {
            var initialRuns = new List<CellRun>();
            initialRuns.Add(new CellRun(CellType.FLEX, 0));
            for (int i = 0; i < row.runs.Length - 1; i++)
            {
                initialRuns.Add(new CellRun(CellType.BROKEN, row.runs[i]));
                initialRuns.Add(new CellRun(CellType.SPACE, 1));
                initialRuns.Add(new CellRun(CellType.FLEX, 0));
            }
            initialRuns.Add(new CellRun(CellType.BROKEN, row.runs.Last()));
            initialRuns.Add(new CellRun(CellType.FLEX, 0));

            long sum = -1;

            var validationPattern = new StringBuilder();
            validationPattern.Append(@"^");
            foreach (char c in row.mask)
            {
                validationPattern.Append(c switch
                {
                    '.' => @"\.",
                    '#' => @"#",
                    '?' => @".",
                    _ => throw new Exception("What is this pattern")
                });
            }
            validationPattern.Append(@"$");
            var validator = new Regex(validationPattern.ToString());

            void DoPuzzleRecurse(List<CellRun> runs, int index)
            {
                if (index >= runs.Count)
                {
                    // TODO
                    return;
                }
                var run = runs[index];
                if (run.type == CellType.SPACE || run.type == CellType.BROKEN)
                {
                    DoPuzzleRecurse(runs, index + 1);
                    return;
                }
                // run.type == CellType.FLEX
                int remainingSpace = row.mask.Length - runs.Sum(r => r.length);
                if (remainingSpace < 0)
                {
                    return;
                }
                if (index == runs.Count - 1)
                {
                    var nextRun = new List<CellRun>(runs);
                    nextRun[index] = new CellRun(CellType.SPACE, remainingSpace);
                    DoPuzzleRecurse(nextRun, index + 1);
                }
                else
                {
                    for (int s = 0; s <= remainingSpace; s++)
                    {
                        var nextRun = new List<CellRun>(runs);
                        nextRun[index] = new CellRun(CellType.SPACE, s);
                        DoPuzzleRecurse(nextRun, index + 1);
                    }
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
