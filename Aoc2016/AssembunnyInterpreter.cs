using AocCommon;
using System.Collections.Frozen;

namespace Aoc2016;

// See:
// https://adventofcode.com/2016/day/12 ,
// https://adventofcode.com/2016/day/23
public class AssembunnyInterpreter
{
    private static readonly FrozenSet<string> oneArg = FrozenSet.ToFrozenSet(["inc", "dec", "tgl"]);
    private static readonly FrozenSet<string> twoArgs = FrozenSet.ToFrozenSet(["cpy", "jnz"]);

    private readonly string[][] instructions;
    private int ip = 0;
    private readonly long[] registers = new long[4];

    public AssembunnyInterpreter(string programSource)
    {
        var lines = Parsing.SplitLines(programSource);
        instructions = lines.Select(l => l.Split(' ')).ToArray();
    }

    public long Peek(char register)
    {
        return registers[register - 'a'];
    }

    public void Poke(char register, long value)
    {
        registers[register - 'a'] = value;
    }

    private long Eval(string arg)
    {
        return char.IsAsciiLetterLower(arg[0]) ? registers[arg[0] - 'a'] : long.Parse(arg);
    }

    public void Run()
    {
        while (ip < instructions.Length)
        {
            var instr = instructions[ip];
            if (instr[0] == "cpy")
            {
                if (char.IsAsciiLetterLower(instr[2][0]))
                {
                    long x = Eval(instr[1]);
                    int y = instr[2][0] - 'a';
                    registers[y] = x;
                }
                ip++;
            }
            else if (instr[0] == "inc")
            {
                if (char.IsAsciiLetterLower(instr[1][0]))
                {
                    int x = instr[1][0] - 'a';
                    registers[x]++;
                }
                ip++;
            }
            else if (instr[0] == "dec")
            {
                if (char.IsAsciiLetterLower(instr[1][0]))
                {
                    int x = instr[1][0] - 'a';
                    registers[x]--;
                }
                ip++;
            }
            else if (instr[0] == "jnz")
            {
                long x = Eval(instr[1]);
                if (x != 0)
                {
                    long y = Eval(instr[2]);
                    ip += (int)y;
                }
                else
                {
                    ip++;
                }
            }
            else if (instr[0] == "tgl")
            {
                long x = Eval(instr[1]);
                int tglTarget = ip + (int)x;
                if (0 <= tglTarget && tglTarget < instructions.Length)
                {
                    if (instructions[tglTarget][0] == "inc")
                    {
                        instructions[tglTarget][0] = "dec";
                    }
                    else if (instructions[tglTarget][0] == "jnz")
                    {
                        instructions[tglTarget][0] = "cpy";
                    }
                    else if (oneArg.Contains(instructions[tglTarget][0]))
                    {
                        instructions[tglTarget][0] = "inc";
                    }
                    else if (twoArgs.Contains(instructions[tglTarget][0]))
                    {
                        instructions[tglTarget][0] = "jnz";
                    }
                    else
                    {
                        throw new Exception("Unrecognized instruction while toggling: " + instr);
                    }
                }
                ip++;
            }
            else
            {
                throw new Exception("Unrecognized instruction: " + instr);
            }
        }
    }
}
