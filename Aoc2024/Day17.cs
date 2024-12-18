using AocCommon;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Aoc2024;

// https://adventofcode.com/2024/day/17
// --- Day 17: Chronospatial Computer ---
public class Day17 : IAocDay
{
    private readonly int initialRegisterA;
    private readonly int initialRegisterB;
    private readonly int initialRegisterC;
    private readonly int[] program;

    public Day17(string input)
    {
        var ints = Regex.Matches(input, @"\d+").Select(m => int.Parse(m.ValueSpan)).ToList();
        initialRegisterA = ints[0];
        initialRegisterB = ints[1];
        initialRegisterC = ints[2];
        program = ints.Skip(3).ToArray();
    }

    public string Part1()
    {
        var outputs = Run(initialRegisterA, initialRegisterB, initialRegisterC);
        var answer = string.Join(',', outputs);
        return answer;
    }

    public IEnumerable<int> Run(long regA, long regB, long regC)
    {
        int ip = 0;
        long LiteralOperand() => program[ip + 1];
        long ComboOperand()
        {
            var operand = program[ip + 1];
            return operand switch
            {
                >= 0 and <= 3 => operand,
                4 => regA,
                5 => regB,
                6 => regC,
                _ => throw new Exception("Combo operand?" + operand)
            };
        }
        while (ip < program.Length)
        {
            switch (program[ip])
            {
                case 0:
                    // adv
                    regA = regA >> (int)ComboOperand();
                    ip += 2;
                    break;
                case 1:
                    // bxl
                    regB = regB ^ LiteralOperand();
                    ip += 2;
                    break;
                case 2:
                    // bst
                    regB = ComboOperand() & 7;
                    ip += 2;
                    break;
                case 3:
                    // jnz
                    if (regA == 0)
                    {
                        ip += 2;
                    }
                    else
                    {
                        ip = (int)LiteralOperand();
                    }
                    break;
                case 4:
                    // bxc
                    regB = regB ^ regC;
                    ip += 2;
                    break;
                case 5:
                    // out
                    yield return (int)(ComboOperand() & 7);
                    ip += 2;
                    break;
                case 6:
                    // bdv
                    regB = regA >> (int)ComboOperand();
                    ip += 2;
                    break;
                case 7:
                    // cdv
                    regC = regA >> (int)ComboOperand();
                    ip += 2;
                    break;
            }
        }
    }

    public string Part2()
    {
        // The input program has a structure like this:
        // Run some calculations on the lower bits of A and output that (...,5,X);
        // A >>= X (0,X);
        // Repeat until A is 0 (3,0).
        // So each number in the output is affected by at most bitsToCheck (= X + 8) in A [X bits because of instruction (0,X) (A >>= X), 8 because of instruction (7,5) (C = A >> B)]
        // So for each number K in the output:
        // Consider a seed number from a previous step;
        // Iterate i over numbers 0..2^bitsToCheck where the low bits are equal to the seed number;
        // If running the input program with i yields a number K then:
        // Look recursively at the next number of the output with the high bits of i as a seed.
        HashSet<long> emptySeed = [0];
        int shiftPerCycle = 0;
        for (int i = 0; i < program.Length; i += 2)
        {
            if (program[i] == 0)
            {
                shiftPerCycle = program[i + 1];
            }
        }
        Debug.Assert(shiftPerCycle != 0);
        int bitsToCheck = shiftPerCycle + 8; // C bits because of instruction (0,C) (A >>= C), 8 because of instruction (7,5) (C = A >> B)
        Func<long, int, HashSet<long>> FindNumber = (_, _) => throw new Exception("Stub for memoized recursive function");
        FindNumber = Memoization.Make((long a, int programIndex) =>
        {
            if (programIndex >= program.Length)
            {
                return emptySeed;
            }
            HashSet<long> results = new();
            var increment = (long)BitOperations.RoundUpToPowerOf2(1ul + (ulong)a); // Given a number with bits 00aa, we only want to change the bits 00.
            for (long i = a; i < (1 << bitsToCheck); i += increment)
            {
                int output = Run(i, initialRegisterB, initialRegisterC).First();
                if (output == program[programIndex])
                {
                    var nextA = i >> shiftPerCycle;
                    var tails = FindNumber(nextA, programIndex + 1);
                    foreach (var tail in tails)
                    {
                        var combined = (tail << shiftPerCycle) | i;
                        var secondOutput = Run(combined, initialRegisterB, initialRegisterC);
                        if (secondOutput.SequenceEqual(program.Skip(programIndex)))
                        {
                            results.Add(combined);
                        }
                    }
                }
            }
            return results;
        });
        var answers = FindNumber(0, 0);
        Debug.Assert(answers.Count >= 1, "Answer not found");
        var answer = answers.Min();
        var test = Run(answer, initialRegisterB, initialRegisterC).ToList();
        Debug.Assert(test.SequenceEqual(program), "Answer does not fit");
        return answer.ToString();
    }
}
