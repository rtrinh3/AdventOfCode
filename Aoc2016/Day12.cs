using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/12
// --- Day 12: Leonardo's Monorail ---
public class Day12(string input) : IAocDay
{
    public string Part1()
    {
        long[] registers = new long[4];
        var answer = RunProgram(registers);
        return answer.ToString();
    }

    public string Part2()
    {
        long[] registers = new long[4];
        registers['c' - 'a'] = 1;
        var answer = RunProgram(registers);
        return answer.ToString();
    }

    private long RunProgram(ReadOnlySpan<long> initialRegisters)
    {
        var lines = Parsing.SplitLines(input);
        var instructions = lines.Select(l => l.Split(' ')).ToArray();
        int ip = 0;
        long[] registers = initialRegisters.ToArray();
        while (ip < lines.Length)
        {
            var instr = instructions[ip];
            if (instr[0] == "cpy")
            {
                long x = char.IsAsciiLetter(instr[1][0]) ? registers[instr[1][0] - 'a'] : long.Parse(instr[1]);
                int y = instr[2][0] - 'a';
                registers[y] = x;
                ip++;
            }
            else if (instr[0] == "inc")
            {
                int x = instr[1][0] - 'a';
                registers[x]++;
                ip++;
            }
            else if (instr[0] == "dec")
            {
                int x = instr[1][0] - 'a';
                registers[x]--;
                ip++;
            }
            if (instr[0] == "jnz")
            {
                long x = char.IsAsciiLetter(instr[1][0]) ? registers[instr[1][0] - 'a'] : long.Parse(instr[1]);
                if (x != 0)
                {
                    long y = char.IsAsciiLetter(instr[2][0]) ? registers[instr[2][0] - 'a'] : long.Parse(instr[2]);
                    ip += (int)y;
                }
                else
                {
                    ip++;
                }
            }
        }
        var answer = registers[0];
        return answer;
    }
}
