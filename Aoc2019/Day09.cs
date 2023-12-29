using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/9
    public class Day09(string input) : IAocDay
    {
        private readonly IntcodeInterpreter interpreter = new IntcodeInterpreter(input);

        public string Part1()
        {
            interpreter.Reset();
            BigInteger[] testInput = { 1 };
            var outputs = interpreter.RunToEnd(testInput).ToList();
            return string.Join("\n", outputs);
        }

        public string Part2()
        {
            interpreter.Reset();
            BigInteger[] testInput = { 2 };
            var outputs = interpreter.RunToEnd(testInput).ToList();
            return string.Join("\n", outputs);
        }
    }
}
