using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/25
// --- Day 25: Clock Signal ---
public class Day25(string input) : IAocDay
{
    public string Part1()
    {
        const int SAMPLE_LENGTH = 8;
        long i = 0;
        Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
            }
        });
        while (true)
        {
            AssembunnyInterpreter interpreter = new(input);
            interpreter.Poke('a', i);
            var outputs = interpreter.RunLazyOutput().Take(SAMPLE_LENGTH).ToArray();
            if (outputs.Length == SAMPLE_LENGTH)
            {
                bool isClock = true;
                for (int s = 0; s < SAMPLE_LENGTH && isClock; s++)
                {
                    isClock = ((s % 2) == outputs[s]);
                }
                if (isClock)
                {
                    return i.ToString();
                }
            }
            i++;
        }
        throw new Exception("No answer found");
    }

    public string Part2()
    {
        return "Merry Christmas!";
    }
}
