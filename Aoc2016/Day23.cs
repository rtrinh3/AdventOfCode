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
        // As far as I can tell, the program calculates the factorial of the input and adds a constant to it.
        // Let's deduce that offset from a few easy inputs and apply it to Input 12.
        List<long> offsets = new();
        for (int i = 7; i <= 10; i++)
        {
            AssembunnyInterpreter interpreter = new(input);
            interpreter.Poke('a', i);
            interpreter.Run();
            var output = interpreter.Peek('a');

            long expectedFactorial = Enumerable.Range(1, i).Aggregate((a, b) => a * b);
            long offset = output - expectedFactorial;
            offsets.Add(offset);
        }
        var distinctOffsets = offsets.Distinct().ToArray();
        var finalOffset = distinctOffsets.Single();
        long factorialTwelve = Enumerable.Range(1, 12).Aggregate((a, b) => a * b);
        long answer = factorialTwelve + finalOffset;
        return answer.ToString();
    }
}
