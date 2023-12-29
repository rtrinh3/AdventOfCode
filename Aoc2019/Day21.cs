using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/21
    public class Day21(string input) : IAocDay
    {
        private readonly IntcodeInterpreter interpreter = new(input);

        public string Part1()
        {
            // (-A or -B or -C) and D
            // T and J start at false
            const string partOneScript = @"NOT A J
NOT B T
OR T J
NOT C T
OR T J
AND D J
WALK
";
            interpreter.Reset();
            var partOneOutput = interpreter.RunToEnd(partOneScript.ReplaceLineEndings("\n").Select(c => (BigInteger)c)).ToList();
            //foreach (var c in partOneOutput)
            //{
            //    if (c <= 0xFF)
            //    {
            //        Console.Write((char)c);
            //    }
            //    else
            //    {
            //        Console.WriteLine(c);
            //    }
            //}
            var answer = partOneOutput.Last();
            return answer.ToString();
        }
        public string Part2()
        {
            // -(A and B and C) and (E or H) and D
            const string partTwoScript = @"NOT T T
AND A T
AND B T
AND C T
NOT T T
OR E J
OR H J
AND T J
AND D J
RUN
";
            interpreter.Reset();
            var partTwoOutput = interpreter.RunToEnd(partTwoScript.ReplaceLineEndings("\n").Select(c => (BigInteger)c)).ToList();
            //foreach (var c in partTwoOutput)
            //{
            //    if (c <= 0xFF)
            //    {
            //        Console.Write((char)c);
            //    }
            //    else
            //    {
            //        Console.WriteLine(c);
            //    }
            //}
            var answer = partTwoOutput.Last();
            return answer.ToString();
        }
    }
}
