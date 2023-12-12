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
            //return DoBruteForce(records);
            return records.Sum(row => DoPuzzle(row));
        }

        public long Part2()
        {
            var unfoldedRecords = this.records.Select(row =>
            {
                string unfoldedMask = row.mask + "?" + row.mask + "?" + row.mask + "?" + row.mask + "?" + row.mask;
                int[] unfoldedRuns = row.runs.Concat(row.runs).Concat(row.runs).Concat(row.runs).Concat(row.runs).ToArray();
                return new Row(unfoldedMask, unfoldedRuns);
            }).OrderBy(r => r.mask.Length + r.runs.Length).ToList();
            //return DoBruteForce(unfoldedRecords);
            //return unfoldedRecords.Sum(row => DoPuzzle(row));

            long sum = 0;
            int done = 0;
            var unfoldedRecordsCount = unfoldedRecords.Count;
            Parallel.ForEach(unfoldedRecords, row =>
            {
                var qty = DoPuzzle(row);
                //sum += qty;
                var mySum = Interlocked.Add(ref sum, qty);
                var myDone = Interlocked.Increment(ref done);
                Console.WriteLine($"{DateTime.Now} Done {myDone}/{unfoldedRecordsCount}, sum at {mySum}");
            });
            return sum;
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

            Regex[] maskValidators = new Regex[row.mask.Length];
            var maskPattern = new StringBuilder();
            maskPattern.Append(@"^");
            for (int i = 0; i < row.mask.Length; i++)
            {
                char c = row.mask[i];
                maskPattern.Append(c switch
                {
                    '.' => @"\.",
                    '#' => @"#",
                    '?' => @".",
                    _ => throw new Exception("What is this pattern")
                });
                maskValidators[i] = new Regex(maskPattern.ToString());
            }
            //maskPattern.Append(@"$");
            //var maskValidator = new Regex(maskPattern.ToString());

            long sum = 0;
            void DoPuzzleRecurse(List<CellRun> runs, int index, string prefix)
            {
                // Build the string
                var buf = new StringBuilder();
                if (index > 0)
                {
                    char c = runs[index - 1].type switch
                    {
                        CellType.SPACE => '.',
                        CellType.BROKEN => '#',
                        _ => throw new Exception() // FLEX too
                    };
                    buf.Append(c, runs[index - 1].length);
                }
                string pattern = prefix + buf.ToString();
                if (pattern.Length > row.mask.Length)
                {
                    return;
                }
                if (pattern.Length > 0 && !maskValidators[pattern.Length - 1].IsMatch(pattern))
                {
                    return;
                }
                if (pattern.Length == row.mask.Length)
                {
                    sum++;
                    //Interlocked.Increment(ref sum);
                    return;
                }

                var run = runs[index];
                if (run.type == CellType.SPACE || run.type == CellType.BROKEN)
                {
                    DoPuzzleRecurse(runs, index + 1, pattern);
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
                    DoPuzzleRecurse(nextRun, index + 1, pattern);
                }
                else
                {
                    for (int s = 0; s <= remainingSpace; s++)
                    //Parallel.For(0, remainingSpace + 1, s =>
                    {
                        var nextRun = new List<CellRun>(runs);
                        nextRun[index] = new CellRun(CellType.SPACE, s);
                        DoPuzzleRecurse(nextRun, index + 1, pattern);
                    }//);
                }
            }
            DoPuzzleRecurse(initialRuns, 0, "");
            return sum;
        }

        private long DoBruteForce(IEnumerable<Row> theseRecords)
        {
            // Brute force
            long sum = 0;
            //foreach (var row in records)
            Parallel.ForEach(theseRecords, row =>
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

                //Debug.Assert(row.mask.Length < 64);
                if (row.mask.Length > 62)
                {
                    Console.WriteLine($"Skip {row.mask} {string.Join(',', row.runs)}");
                    return;
                }
                long numberOfPatterns = 1L << row.mask.Length;
                //for (int pattern = 0; pattern < numberOfPatterns; pattern++)
                long localSum = 0;
                Parallel.For(0L, numberOfPatterns, pattern =>
                {
                    var patternSB = new StringBuilder();
                    for (int b = 0; b < row.mask.Length; b++)
                    {
                        patternSB.Append(((pattern & (1L << b)) == 0) ? '#' : '.');
                    }
                    string patternString = patternSB.ToString();
                    if (maskValidator.IsMatch(patternString) && runsValidator.IsMatch(patternString))
                    {
                        Interlocked.Increment(ref localSum);
                    }
                });
                Console.WriteLine($"Done {row.mask} {string.Join(',', row.runs)} - {localSum}");
                Interlocked.Add(ref sum, localSum);
            });
            return sum;
        }
    }
}
