using AocCommon;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Aoc2024;

// https://adventofcode.com/2024/day/24
// --- Day 24: Crossed Wires ---
public class Day24 : IAocDay
{
    private readonly (string, bool)[] inputs;
    private readonly Dictionary<string, (string lhs, string op, string rhs)> initialWires;
    private readonly string[] wireList;

    public Day24(string input)
    {
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        Debug.Assert(paragraphs.Length == 2);

        var inputLines = paragraphs[0].Split('\n', StringSplitOptions.TrimEntries);
        List<(string, bool)> inputs = new();
        foreach (var line in inputLines)
        {
            var parts = line.Split(':', StringSplitOptions.TrimEntries);
            Debug.Assert(parts.Length == 2);
            string name = parts[0];
            bool value = (parts[1] != "0");
            inputs.Add((name, value));
        }
        this.inputs = inputs.ToArray();

        var gates = paragraphs[1].Split('\n', StringSplitOptions.TrimEntries);
        initialWires = new();
        foreach (var line in gates)
        {
            var parse = Regex.Match(line, @"^(\w+) (\w+) (\w+) -> (\w+)$");
            Debug.Assert(parse.Success);
            string lhs = parse.Groups[1].Value;
            string op = parse.Groups[2].Value;
            string rhs = parse.Groups[3].Value;
            string target = parse.Groups[4].Value;
            initialWires[target] = (lhs, op, rhs);
        }
        wireList = initialWires.Keys.Order().ToArray();
    }

    public string Part1()
    {
        Dictionary<string, Lazy<bool>> wires = new();
        foreach (var input in inputs)
        {
            wires[input.Item1] = new Lazy<bool>(() => input.Item2);
        }
        foreach (var wire in initialWires)
        {
            wires[wire.Key] = new Lazy<bool>(() =>
            {
                var (lhs, op, rhs) = wire.Value;
                return op switch
                {
                    "AND" => wires[lhs].Value && wires[rhs].Value,
                    "OR" => wires[lhs].Value || wires[rhs].Value,
                    "XOR" => wires[lhs].Value ^ wires[rhs].Value,
                    _ => throw new NotImplementedException(op)
                };
            });
        }
        ulong answer = 0;
        foreach (var zWire in wires.Keys.Where(k => k.StartsWith('z')))
        {
            int position = int.Parse(zWire[1..]);
            ulong value = wires[zWire].Value ? 1UL : 0UL;
            answer |= (value << position);
        }

        return answer.ToString();
    }

    public string Part2()
    {
        var answer = DoPart2(4, EvaluateErrorAddition);
        return answer;
    }

    public string DoPart2(int numberOfSwaps, Func<Func<ulong, ulong, ulong>, int> evaluateError)
    {
        var timer = Stopwatch.StartNew();
        PriorityQueue<(int, int)[], int> queue = new();
        queue.Enqueue([], int.MaxValue);
        while (queue.TryDequeue(out var swaps, out var distance))
        {
            Console.WriteLine($"{timer.Elapsed} Queue size {queue.Count} Dequeue {string.Join("", swaps)} {distance}");
            if (swaps.Length >= numberOfSwaps)
            {
                if (distance == 0)
                {
                    List<string> answer = new();
                    foreach (var s in swaps)
                    {
                        answer.Add(wireList[s.Item1]);
                        answer.Add(wireList[s.Item2]);
                    }
                    answer.Sort();
                    return string.Join(',', answer);
                }
                // Else nothing
            }
            else
            {
                int leftMax = swaps.Length > 0 ? swaps.Last().Item1 : -1;
                int[] rights = swaps.Select(s => s.Item2).Order().ToArray();
                for (int i = leftMax + 1; i < wireList.Length; i++)
                {
                    for (int j = i + 1; j < wireList.Length; j++)
                    {
                        if (!rights.Contains(j))
                        {
                            (int, int)[] newSwaps = [.. swaps, (i, j)];
                            try
                            {
                                int newDistance = evaluateError(MakeSystemWithSwaps(newSwaps));
                                if (newSwaps.Length < numberOfSwaps || newDistance == 0)
                                {
                                    queue.Enqueue(newSwaps, newDistance);
                                }
                            }
                            catch (Exception ex)
                            {
                                // Dead-end
                            }
                        }
                    }
                }
            }
        }
        throw new Exception("Not found");
    }

    private Func<ulong, ulong, ulong> MakeSystemWithSwaps((int, int)[] swaps)
    {
        Dictionary<string, (string lhs, string op, string rhs)> wires = new(initialWires);
        foreach (var s in swaps)
        {
            // tuple swap
            (wires[wireList[s.Item2]], wires[wireList[s.Item1]]) = (wires[wireList[s.Item1]], wires[wireList[s.Item2]]);
        }
        ulong systemEvaluator(ulong x, ulong y)
        {
            Dictionary<string, Lazy<bool>> wiresWithInputs = new();
            foreach (var input in inputs)
            {
                ulong source = input.Item1[0] switch
                {
                    'x' => x,
                    'y' => y,
                    _ => throw new Exception("what source " + input.Item1[0])
                };
                int bit = int.Parse(input.Item1[1..]);
                wiresWithInputs[input.Item1] = new Lazy<bool>(() => (source & (1UL << bit)) != 0);
            }
            foreach (var wire in wires)
            {
                wiresWithInputs[wire.Key] = new Lazy<bool>(() =>
                {
                    var (lhs, op, rhs) = wire.Value;
                    return op switch
                    {
                        "AND" => wiresWithInputs[lhs].Value && wiresWithInputs[rhs].Value,
                        "OR" => wiresWithInputs[lhs].Value || wiresWithInputs[rhs].Value,
                        "XOR" => wiresWithInputs[lhs].Value ^ wiresWithInputs[rhs].Value,
                        _ => throw new NotImplementedException(op)
                    };
                });
            }
            ulong answer = 0;
            foreach (var zWire in wires.Keys.Where(k => k.StartsWith('z')))
            {
                int position = int.Parse(zWire[1..]);
                ulong value = wiresWithInputs[zWire].Value ? 1UL : 0UL;
                answer |= (value << position);
            }
            return answer;
        }
        return systemEvaluator;
    }

    private int EvaluateErrorAddition(Func<ulong, ulong, ulong> system)
    {
        int inputSize = inputs.Length / 2;
        int errors = 0;
        for (int i = 1; i <= inputSize; i++)
        {
            ulong expected = 1UL << i;
            ulong x = expected >> 1;
            ulong actual = system(x, x);
            ulong difference = actual ^ expected;
            int error = BitOperations.PopCount(difference);
            errors += error;
        }
        return errors;
    }
}
