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
            //return DoBruteForce(records);
            //return records.Sum(row => DoPuzzle(row));
            return records.Sum(row => DoPuzzle2(row));
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
                var qty = DoPuzzle2(row);
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
                initialRuns.Add(new CellRun(CellType.POUND, row.runs[i]));
                initialRuns.Add(new CellRun(CellType.DOT, 1));
                initialRuns.Add(new CellRun(CellType.FLEX, 0));
            }
            initialRuns.Add(new CellRun(CellType.POUND, row.runs.Last()));
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
                        CellType.DOT => '.',
                        CellType.POUND => '#',
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
                if (run.type == CellType.DOT || run.type == CellType.POUND)
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
                    nextRun[index] = new CellRun(CellType.DOT, remainingSpace);
                    DoPuzzleRecurse(nextRun, index + 1, pattern);
                }
                else
                {
                    for (int s = 0; s <= remainingSpace; s++)
                    //Parallel.For(0, remainingSpace + 1, s =>
                    {
                        var nextRun = new List<CellRun>(runs);
                        nextRun[index] = new CellRun(CellType.DOT, s);
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

        private long DoPuzzle2(Row row)
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

            Func<int, int, Regex> GetValidator = (int startIndex, int length) =>
            {
                var patternBuilder = new StringBuilder(@"^");
                var substrLen = Math.Min(row.mask.Length - startIndex, length);
                string substr = row.mask.Substring(startIndex, substrLen);
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
            };
            GetValidator = Memoize(GetValidator);

            Func<int, int, long> DoPuzzleRecurse = (x, y) => throw new Exception("Not initialized");
            DoPuzzleRecurse = Memoize((int maskIndex, int runIndex) =>
            {
                Debug.Assert(maskIndex <= row.mask.Length);
                Debug.Assert(runIndex <= expandedRuns.Count);
                if (runIndex == expandedRuns.Count)
                {
                    return (maskIndex == row.mask.Length) ? 1 : 0;
                }
                var run = expandedRuns[runIndex];
                char c = run.type switch
                {
                    CellType.DOT => '.',
                    CellType.POUND => '#',
                    CellType.FLEX => '.',
                    _ => throw new Exception("What is this cell")
                };
                IEnumerable<int> lengths = run.type switch
                {
                    CellType.DOT => [run.length],
                    CellType.POUND => [run.length],
                    CellType.FLEX => Enumerable.Range(0, 1 + row.mask.Length - maskIndex - expandedRuns.Skip(runIndex).Sum(r => r.length)),
                    _ => throw new Exception("What is this cell")
                };
                long sum = 0;
                foreach (var len in lengths)
                {
                    string runString = string.Join("", Enumerable.Repeat(c, len));
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

        private static Func<T1, TR> Memoize<T1, TR>(Func<T1, TR> fn)
            where T1: notnull
        {
            Dictionary<T1, TR> memo = new();
            return x1 =>
            {
                if (memo.TryGetValue(x1, out var result))
                {
                    return result;
                }
                return memo[x1] = fn(x1);
            };
        }
        private static Func<T1, T2, TR> Memoize<T1, T2, TR>(Func<T1, T2, TR> fn)
        {
            Dictionary<(T1, T2), TR> memo = new();
            return (x1, x2) =>
            {
                if (memo.TryGetValue((x1, x2), out var result))
                {
                    return result;
                }
                return memo[(x1, x2)] = fn(x1, x2);
            };
        }
        private static Func<T1, T2, T3, TR> Memoize<T1, T2, T3, TR>(Func<T1, T2, T3, TR> fn)
        {
            Dictionary<(T1, T2, T3), TR> memo = new();
            return (x1, x2, x3) =>
            {
                if (memo.TryGetValue((x1, x2, x3), out var result))
                {
                    return result;
                }
                return memo[(x1, x2, x3)] = fn(x1, x2, x3);
            };
        }
        private static Func<T1, T2, T3, T4, TR> Memoize<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> fn)
        {
            Dictionary<(T1, T2, T3, T4), TR> memo = new();
            return (x1, x2, x3, x4) =>
            {
                if (memo.TryGetValue((x1, x2, x3, x4), out var result))
                {
                    return result;
                }
                return memo[(x1, x2, x3, x4)] = fn(x1, x2, x3, x4);
            };
        }
    }
}
