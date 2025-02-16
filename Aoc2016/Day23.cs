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
        // The tgl instruction goes through the same sequence for 6-11; hopefully 12 too
        //tgl 8: inc->dec
        //tgl 6: inc->dec
        //tgl 4: jnz->cpy
        //tgl 2: jnz->cpy
        // inputs 6-11 execute in a reasonable time
        for (int i = 6; i <= 11; i++)
        {
            Console.WriteLine($"Input {i}");
            AssembunnyInterpreter tempInterpreter = new(input);
            tempInterpreter.Poke('a', i);
            tempInterpreter.Run();
            var output = tempInterpreter.Peek('a');
            Console.WriteLine(output);
            Console.WriteLine();
        }
        // Naive run
        AssembunnyInterpreter interpreter = new(input);
        interpreter.Poke('a', 12);
        interpreter.Run();
        var answer = interpreter.Peek('a');
        return answer.ToString();
    }
}
