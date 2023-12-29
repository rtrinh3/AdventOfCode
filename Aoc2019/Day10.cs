using AocCommon;
using System.Windows.Markup;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/10
    public class Day10 : IAocDay
    {
        private readonly List<(int, int)> asteroids = new();

        public Day10(string input)
        {
            string[] rows = input.Split('\n');
            for (int row = 0; row < rows.Length; row++)
            {
                for (int col = 0; col < rows[row].Length; col++)
                {
                    if (rows[row][col] == '#')
                    {
                        asteroids.Add((col, row));
                    }
                }
            }
        }

        public string Part1()
        {
            return DoPart1().bestCoordValue.ToString();
        }

        private (int bestCoordValue, (int, int) bestCoord) DoPart1()
        {
            int bestCoordValue = 0;
            (int, int) bestCoord = (0, 0);
            foreach (var site in asteroids)
            {
                var visibleAsteroids = new HashSet<(int, int)>(asteroids.Where(a => a != site)
                .Select(a => NormalizeOrientation(site.Item1 - a.Item1, site.Item2 - a.Item2)));
                if (visibleAsteroids.Count > bestCoordValue)
                {
                    bestCoordValue = visibleAsteroids.Count;
                    bestCoord = site;
                }
            }
            return (bestCoordValue, bestCoord);
        }

        public string Part2()
        {
            const int TARGET = 200;
            (int, int) bestCoord = DoPart1().bestCoord;
            var asteroidsByOrientation = asteroids.Where(a => a != bestCoord)
                .GroupBy(a => NormalizeOrientation(bestCoord.Item1 - a.Item1, bestCoord.Item2 - a.Item2))
                .ToDictionary(g => g.Key, g => g.ToList());
            int distanceToBestCoord((int, int) other)
            {
                return Math.Abs(other.Item1 - bestCoord.Item1) + Math.Abs(other.Item2 - bestCoord.Item2);
            }
            foreach (var kvp in asteroidsByOrientation)
            {
                kvp.Value.Sort((a, b) => distanceToBestCoord(a).CompareTo(distanceToBestCoord(b)));
            }
            var listOfOrientations = asteroidsByOrientation.Keys.OrderBy(o => GetAngle(o.Item1, o.Item2)).ToList();
            int o = 0;
            List<(int, int)> destroyed = new();
            while (asteroidsByOrientation.Any(x => x.Value.Any()))
            {
                var asteroidsInOrientation = asteroidsByOrientation[listOfOrientations[o]];
                if (asteroidsInOrientation.Any())
                {
                    var asteroid = asteroidsInOrientation.First();
                    destroyed.Add(asteroid);
                    asteroidsInOrientation.Remove(asteroid);
                }
                o = (o + 1) % listOfOrientations.Count;
            }
            var targetAsteroid = destroyed[TARGET - 1];
            var answer = targetAsteroid.Item1 * 100 + targetAsteroid.Item2;
            return answer.ToString();
        }

        double GetAngle(int x, int y)
        {
            double angle = Math.Atan2(-x, y); // Rotation to match laser
            if (angle < 0)
            {
                angle += Math.Tau; // Normalize to [0, Tau]
            }
            return angle;
        }

        (int, int) NormalizeOrientation(int x, int y)
        {
            int gcd = Math.Abs(MoreMath.Gcd(x, y));
            x /= gcd;
            y /= gcd;
            return (x, y);
        }
    }
}
