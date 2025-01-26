using AocCommon;
using System.Diagnostics;

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
        var ints = Parsing.IntsPositive(input);
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
        // So we can work through A 3 bits at a time, and the later outputs are affected by the higher bits of A.
        // So we work backwards through the outputs to figure out the high bits first.
        // At the Nth number of the output, given a prefix from a previous iteration (later output):
        // for each possible suffix 0 to 2^shiftPerCycle:
        // run the program with input (prefix | suffix)
        // if it generates the outputs (N..end):
        // recurse with prefix|suffix as the new suffix and N-1 as the new index
        int shiftPerCycle = int.MaxValue;
        for (int i = 0; i < program.Length; i += 2)
        {
            if (program[i] == 0)
            {
                shiftPerCycle = Math.Min(shiftPerCycle, program[i + 1]);
            }
        }
        Debug.Assert(shiftPerCycle != int.MaxValue);
        List<long> answers = new();
        void FindAnswers(long prefix, int index)
        {
            if (index < 0)
            {
                answers.Add(prefix);
                return;
            }
            for (long i = 0; i < (1 << shiftPerCycle); i++)
            {
                long combined = (prefix << shiftPerCycle) | i;
                var test = Run(combined, initialRegisterB, initialRegisterC);
                if (test.SequenceEqual(program.Skip(index)))
                {
                    FindAnswers(combined, index - 1);
                }
            }
        }
        FindAnswers(0, program.Length - 1);
        Debug.Assert(answers.Count >= 1, "Answer not found");
        var answer = answers.Min();
        var test = Run(answer, initialRegisterB, initialRegisterC).ToList();
        Debug.Assert(test.SequenceEqual(program), "Answer does not fit");
        return answer.ToString();
    }
}
