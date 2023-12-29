namespace Aoc2019
{
    // https://adventofcode.com/2019/day/2
    public class Day02(string input) : IAocDay
    {
        private IntcodeInterpreter interpreter = new(input);

        public string Part1()
        {
            interpreter.Reset();
            interpreter.Poke(1, 12);
            interpreter.Poke(2, 2);
            _ = interpreter.RunToEnd().ToList();
            return interpreter.Peek(0).ToString();
        }

        public string Part2()
        {
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    interpreter.Reset();
                    interpreter.Poke(1, noun);
                    interpreter.Poke(2, verb);
                    _ = interpreter.RunToEnd().ToList();
                    if (interpreter.Peek(0) == 19690720)
                    {
                        var answer = 100 * noun + verb;
                        return answer.ToString();
                    }
                }
            }
            throw new Exception("No answer found");
        }
    }
}
