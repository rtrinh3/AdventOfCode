namespace Aoc2019
{
    // https://adventofcode.com/2019/day/1
    public class Day01(string input) : IAocDay
    {
        public string Part1()
        {
            var answer = input.TrimEnd().Split('\n').Select(s => int.Parse(s) / 3 - 2).Sum();
            return answer.ToString();
        }

        private static int FuelForMass(int mass)
        {
            int fuel = mass / 3 - 2;
            if (fuel <= 0)
            {
                return 0;
            }
            else
            {
                return fuel + FuelForMass(fuel);
            }
        }

        public string Part2()
        {
            var answer = input.TrimEnd().Split('\n').Select(s => FuelForMass(int.Parse(s))).Sum();
            return answer.ToString();
        }
    }
}
