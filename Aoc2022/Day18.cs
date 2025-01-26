using AocCommon;
using System.Linq;
using System.Numerics;

namespace Aoc2022
{
    public class Day18 : IAocDay
    {
        private static readonly VectorXYZ[] directions = {
            new(+1, 0, 0),
            new(-1, 0, 0),
            new(0, +1, 0),
            new(0, -1, 0),
            new(0, 0, +1),
            new(0, 0, -1)
        };

        private HashSet<VectorXYZ> droplets = new();
        public Day18(string input)
        {
            foreach (string line in Parsing.SplitLines(input))
            {
                int[] parts = line.Split(',').Select(int.Parse).ToArray();
                VectorXYZ coords = new(parts[0], parts[1], parts[2]);
                droplets.Add(coords);
            }
        }

        public string Part1()
        {
            int exposed = 0;
            foreach (var drop in droplets)
            {
                foreach (var dir in directions)
                {
                    var neighbor = drop + dir;
                    if (!droplets.Contains(neighbor))
                    {
                        ++exposed;
                    }
                }
            }
            return exposed.ToString();
        }

        public string Part2()
        {
            int minX = droplets.Select(d => d.X).Min();
            int maxX = droplets.Select(d => d.X).Max();
            int minY = droplets.Select(d => d.Y).Min();
            int maxY = droplets.Select(d => d.Y).Max();
            int minZ = droplets.Select(d => d.Z).Min();
            int maxZ = droplets.Select(d => d.Z).Max();
            IEnumerable<VectorXYZ> GetNeighbors(VectorXYZ coords)
            {
                return directions.Select(dir => coords + dir)
                    .Where(next => minX - 1 <= next.X && next.X <= maxX + 1 &&
                    minY - 1 <= next.Y && next.Y <= maxY + 1 &&
                    minZ - 1 <= next.Z && next.Z <= maxZ + 1 &&
                    !droplets.Contains(next));
            }
            VectorXYZ outsideRoot = new(minX - 1, minY - 1, minZ - 1);
            var floodFillResult = GraphAlgos.BfsToAll(outsideRoot, GetNeighbors);
            var outside = floodFillResult.Keys;
            int exposedOutside = 0;
            foreach (var drop in droplets)
            {
                foreach (var dir in directions)
                {
                    var neighbor = drop + dir;
                    if (outside.Contains(neighbor))
                    {
                        ++exposedOutside;
                    }
                }
            }
            return exposedOutside.ToString();
        }
    }
}
