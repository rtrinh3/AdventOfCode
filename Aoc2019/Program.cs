using System.Diagnostics;

namespace Aoc2019
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? day = args.ElementAtOrDefault(0);
            string? input = args.ElementAtOrDefault(1);
#if DEBUG
            const bool debug = true;
#else
            const bool debug = false;
#endif
            if (debug)
            {
                day ??= "01";
                input ??= @"PLACEHOLDER";
            }
            if (day == null)
            {
                throw new Exception($"Missing day");
            }
            if (input == null)
            {
                throw new Exception($"Missing input");
            }
            if (File.Exists(input))
            {
                input = File.ReadAllText(input);
            }
            if (!int.TryParse(day, out int dayValue) || dayValue < 1 || dayValue > 25)
            {
                throw new Exception($"Bad day: {day}");
            }
            string dayClassName = typeof(Program).Namespace +  ".Day" + dayValue.ToString("00");
            Console.WriteLine($"Loading: {dayClassName}");
            var initTimer = Stopwatch.StartNew();
            var dayClass = typeof(Program).Assembly.GetType(dayClassName);
            var dayConstructor = dayClass.GetConstructor([typeof(string)]);
            IAocDay dayInstance = (IAocDay)dayConstructor.Invoke([input]);
            Console.WriteLine($"Time: {initTimer.Elapsed}");

            Console.WriteLine("\nPart 1");
            var partOneTimer = Stopwatch.StartNew();
            var partOneAnswer = dayInstance.Part1();
            Console.WriteLine($"Time: {partOneTimer.Elapsed}");
            Console.WriteLine(partOneAnswer);

            Console.WriteLine("\nPart 2");
            var partTwoTimer = Stopwatch.StartNew();
            var partTwoAnswer = dayInstance.Part2();
            Console.WriteLine($"Time: {partTwoTimer.Elapsed}");
            Console.WriteLine(partTwoAnswer);
        }
    }
}
