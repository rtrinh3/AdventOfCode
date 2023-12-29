using AocCommon;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/19
    public class Day19(string input) : IAocDay
    {
        IntcodeInterpreter interpreter = new(input);

        public string Part1()
        {
            int count = 0;
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    interpreter.Reset();
                    var output = interpreter.RunUntilOutput(x, y);
                    if (output == 1)
                    {
                        count++;
                    }
                    //Console.Write(output);
                }
                //Console.WriteLine();
            }
            return count.ToString();
        }

        public string Part2()
        {
            // I know the closest 10*10 square is at (94,67) -> (103,76)
            // Multiplying by 10 should land me at (940,670) -> (1030,760)
            // Let's add a bit of a safety margin
            const int xMin = 900;
            const int xMax = 1200;
            const int yMin = 600;
            const int yMax = 900;
            const int width = 100;
            Func<int, int, int> scan = Memoization.Make((int x, int y) =>
            {
                interpreter.Reset();
                var output = interpreter.RunUntilOutput(x, y);
                int ret = (int)output.Value;
                return ret;
            });
            for (int x = xMin; x < xMax - width; x++)
            {
                for (int y = yMin; y < yMax - width; y++)
                {
                    if (scan(x, y) == 1 && scan(x + width - 1, y) == 1 && scan(x, y + width - 1) == 1 && scan(x + width - 1, y + width - 1) == 1)
                    {
                        var answer = x * 10000 + y;
                        return answer.ToString();
                    }
                }
            }
            throw new Exception("No answer found");
        }
    }
}
