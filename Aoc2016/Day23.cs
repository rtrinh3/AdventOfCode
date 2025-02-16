using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/23
// --- Day 23: Safe Cracking ---
public class Day23(string input) : IAocDay
{
    public string Part1()
    {
        AssembunnyInterpreter interpreter = new(input);
        interpreter.Poke('a', 7);
        interpreter.Run();
        var answer = interpreter.Peek('a');
        return answer.ToString();
    }

    public string Part2()
    {
        AssembunnyInterpreter interpreter = new(input);
        interpreter.Poke('a', 12);
        interpreter.Run();
        var answer = interpreter.Peek('a');
        return answer.ToString();
    }
}
