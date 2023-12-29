using System.Numerics;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/5
    public class Day05(string input) : IAocDay
    {
        private readonly IntcodeInterpreter interpreter = new(input);

        public string Part1()
        {
            interpreter.Reset();
            BigInteger[] inputs = { 1 };
            var outputs = interpreter.RunToEnd(inputs);
            string answer = string.Join("\n", outputs);
            return answer;
        }

        public string Part2()
        {
            interpreter.Reset();
            BigInteger[] inputs = { 5 };
            var outputs = interpreter.RunToEnd(inputs);
            string answer = string.Join("\n", outputs);
            return answer;
        }
    }
}
