using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2024;

// https://adventofcode.com/2024/day/17
// --- Day 17: Chronospatial Computer ---
public class Day17 : IAocDay
{
    private int initialRegisterA;
    private int initialRegisterB;
    private int initialRegisterC;
    private int[] program;

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
        //long answer = long.MaxValue;
        //long done = 0;
        //Parallel.For(0, long.MaxValue, (i, loopState) =>
        //{
        //    var outputs = Run(i, initialRegisterB, initialRegisterC);
        //    if (program.SequenceEqual(outputs))
        //    {
        //        lock (this)
        //        {
        //            answer = Math.Min(i, answer);
        //        }
        //        loopState.Break();
        //    }
        //    var myDone = Interlocked.Increment(ref done);
        //    if ((myDone & 0xffffff) == 0)
        //    {
        //        Console.WriteLine(myDone);
        //    }
        //});
        //return answer.ToString();
        //long FindNumber(long a, int programIndex)
        long[] seed = [0];
        Func<long, int, IEnumerable<long>> FindNumber = (_, _) => throw new Exception();
        FindNumber = Memoization.Make((long a, int programIndex) =>
        {
            if (programIndex >= program.Length)
            {
                return seed.AsEnumerable();
            }
            const int bitsToCheck = 3 + 8; // 3 bits because of instruction (0,3) (A >>= 3), 8 because of instruction (7,5) (C = A >> B)
            HashSet<long> results = new();
            for (long i = a; i < (1 << bitsToCheck); i++)
            {
                int output = Run(i, initialRegisterB, initialRegisterC).First();
                if (output == program[programIndex])
                {
                    var nextA = i >> 3;
                    var tails = FindNumber(nextA, programIndex + 1);
                    foreach (var tail in tails)
                    {
                        var combined = (tail << 3) | (i & 7);
                        var secondOutput = Run(combined, initialRegisterB, initialRegisterC);
                        if (secondOutput.SequenceEqual(program.Skip(programIndex)))
                        {
                            results.Add(combined);
                        }
                    }
                }
            }
            return results.AsEnumerable();
        });
        var answers = FindNumber(0, 0).ToList();
        Debug.Assert(answers.Count >= 1, "Answer not found");
        var answer = answers.Min();
        var test = Run(answer, initialRegisterB, initialRegisterC).ToList();
        Debug.Assert(test.SequenceEqual(program), "Answer does not fit");
        return answer.ToString();
    }
}
