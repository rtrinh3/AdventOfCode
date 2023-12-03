namespace Aoc2023
{
    internal class Aoc2023Main
    {
        static void Main(string[] args)
        {
            string? day = args.ElementAtOrDefault(0);
            string? input = args.ElementAtOrDefault(1);
#if DEBUG
            day ??= "01";
            input ??= @"PLACEHOLDER";
#else
            if (args.Length < 1)
            {
                throw new Exception($"Missing day");
            }
            if (args.Length < 2)
            {
                throw new Exception($"Missing input");
            }
#endif
            if (File.Exists(input))
            {
                input = File.ReadAllText(input);
            }
            if (!int.TryParse(day, out int dayValue) || dayValue < 1 || dayValue > 25)
            {
                throw new Exception($"Bad day: {day}");
            }
            string dayClassName = "Aoc2023.Day" + dayValue.ToString("00");
            var dayClass = typeof(Aoc2023Main).Assembly.GetType(dayClassName);
            var dayConstructor = dayClass.GetConstructor([typeof(string)]);
            var dayInstance = dayConstructor.Invoke([input]);

            Console.WriteLine("Part 1");
            var partOneMethod = dayClass.GetMethod("Part1");
            var partOneAnswer = partOneMethod.Invoke(dayInstance, []);
            Console.WriteLine(partOneAnswer);

            Console.WriteLine("Part 2");
            var partTwoMethod = dayClass.GetMethod("Part2");
            var partTwoAnswer = partTwoMethod.Invoke(dayInstance, []);
            Console.WriteLine(partTwoAnswer);
        }
    }
}
