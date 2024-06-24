using System.Diagnostics;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/8
    // --- Day 8: Handheld Halting ---
    public class Day08(string input): IAocDay
    {
        private record Instruction(string Op, int Arg);
        private record Result(bool Terminated, int Accumulator);

        private readonly Instruction[] Program = input
            .TrimEnd().ReplaceLineEndings("\n").Split('\n')
            .Select(line => line.Split(' '))
            .Select(line => new Instruction(line[0], int.Parse(line[1]))).ToArray();

        public long Part1()
        {
            var result = Interpret(Program);
            Debug.Assert(!result.Terminated);
            return result.Accumulator;
        }

        public long Part2()
        {
            Instruction[] programCopy = [..Program];
            for (int i = 0; i < programCopy.Length; i++)
            {
                if (Program[i].Op == "jmp")
                {
                    programCopy[i] = new Instruction("nop", Program[i].Arg);
                    var result = Interpret(programCopy);
                    if (result.Terminated)
                    {
                        return result.Accumulator;
                    }
                    programCopy[i] = Program[i];
                }
                else if (Program[i].Op == "nop")
                {
                    programCopy[i] = new Instruction("jmp", Program[i].Arg);
                    var result = Interpret(programCopy);
                    if (result.Terminated)
                    {
                        return result.Accumulator;
                    }
                    programCopy[i] = Program[i];
                }
            }
            throw new Exception("No answer found");
        }

        private static Result Interpret(IReadOnlyList<Instruction> prgm)
        {
            int ip = 0;
            int acc = 0;
            HashSet<int> seen = [];
            while (true)
            {
                if (seen.Contains(ip))
                {
                    return new Result(false, acc);
                }
                if (ip == prgm.Count)
                {
                    return new Result(true, acc);
                }
                seen.Add(ip);
                var (Op, Arg) = prgm[ip];
                switch (Op)
                {
                    case "acc":
                        acc += Arg;
                        ip++;
                        break;
                    case "jmp":
                        ip += Arg;
                        break;
                    case "nop":
                        ip++;
                        break;
                    default:
                        throw new Exception("What is this instruction?! " + Op);
                }
            }
        }
    }
}
