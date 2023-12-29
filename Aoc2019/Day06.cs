using AocCommon;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/6
    public class Day06(string input) : IAocDay
    {
        private readonly Dictionary<string, string> parents = input.TrimEnd().Split('\n').Select(s => s.TrimEnd().Split(')')).ToDictionary(ss => ss[1], ss => ss[0]);

        public string Part1()
        {
            int orbits = 0;
            foreach (string planet in parents.Keys)
            {
                string cursor = planet;
                while (cursor != "COM")
                {
                    orbits++;
                    cursor = parents[cursor];
                }
            }
            return orbits.ToString();
        }

        public string Part2()
        {
            DefaultDict<string, HashSet<string>> adjacency = new();
            foreach (var kvp in parents)
            {
                adjacency[kvp.Key].Add(kvp.Value);
                adjacency[kvp.Value].Add(kvp.Key);
            }
            string start = parents["YOU"];
            string end = parents["SAN"];
            Dictionary<string, int> hops = new();
            Queue<(string, int)> queue = new();
            queue.Enqueue((start, 0));
            while (queue.Count > 0)
            {
                (string planet, int queueHops) = queue.Dequeue();
                if (planet == end)
                {
                    return queueHops.ToString();
                }
                if (!hops.ContainsKey(planet) || queueHops < hops[planet])
                {
                    hops[planet] = queueHops;
                    foreach (var other in adjacency[planet])
                    {
                        queue.Enqueue((other, queueHops + 1));
                    }
                }
            }
            throw new Exception("No answer found");
        }
    }
}
