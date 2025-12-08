using AocCommon;
using System.Diagnostics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/12
    // --- Day 12: Garden Groups ---
    public class Day12(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';
        private readonly Grid map = new(input, OUTSIDE);

        public string Part1()
        {
            var regions = FindRegions();

            // Calculate prices
            long total = 0;
            foreach (var (region, regionData) in regions)
            {
                //var type = map.Get(region); // for debugging
                var thisPrice = Math.BigMul(regionData.Area, regionData.Fences.Length);
                total += thisPrice;
            }

            return total.ToString();
        }

        public string Part2()
        {
            var regions = FindRegions();

            // Calculate prices
            long total = 0;
            foreach (var (region, regionData) in regions)
            {
                //var type = map.Get(region); // for debugging
                // Find number of sides
                int sideCount = 0;
                // Group fences by direction and distance when projected on that direction
                var fenceGroups = regionData.Fences.GroupBy(f => (f.Direction, f.Direction.Dot(f.Inside)));
                foreach (var group in fenceGroups)
                {
                    // Within each group, project each fence onto the other direction,
                    // then sort them: adjacent fences become adjacent numbers
                    VectorRC perpendicular = group.Key.Direction.RotatedLeft();
                    var projectedFences = group.Select(f => perpendicular.Dot(f.Inside)).Order().ToList();
                    int runCount = 1;
                    for (int i = 1; i < projectedFences.Count; i++)
                    {
                        if (projectedFences[i] - projectedFences[i - 1] > 1)
                        {
                            runCount++;
                        }
                    }
                    sideCount += runCount;
                }
                var thisPrice = Math.BigMul(regionData.Area, sideCount);
                total += thisPrice;
            }

            return total.ToString();
        }

        private Dictionary<int, (int Area, Fence[] Fences)> FindRegions()
        {
            // Our vectors are small enough for this
            Debug.Assert(map.Width <= 0xFF && map.Height <= 0xFF);
            static int Linearize(VectorRC x) => (x.Row << 8) | x.Col;

            // Partition the map and assign fences to plots
            ReadOnlySpan<VectorRC> scanDirections = [VectorRC.Right, VectorRC.Down];
            UnionFindInt regions = new();
            DefaultDict<VectorRC, List<Fence>> plotFence = new();
            foreach (var (pos, _) in map.Iterate())
            {
                if (pos.Row == 0)
                {
                    plotFence[pos].Add(new Fence(pos, VectorRC.Up));
                }
                if (pos.Col == 0)
                {
                    plotFence[pos].Add(new Fence(pos, VectorRC.Left));
                }
                foreach (var dir in scanDirections)
                {
                    var neighbor = pos + dir;
                    if (map.Get(pos) == map.Get(neighbor))
                    {
                        regions.Union(Linearize(pos), Linearize(neighbor));
                    }
                    else
                    {
                        plotFence[pos].Add(new Fence(pos, dir));
                        plotFence[neighbor].Add(new Fence(neighbor, -dir));
                    }
                }
            }

            // Calculate area and fence of regions
            DefaultDict<int, int> regionArea = new();
            DefaultDict<int, List<Fence>> regionFence = new();
            foreach (var (pos, _) in map.Iterate())
            {
                regionArea[regions.Find(Linearize(pos))]++;
                regionFence[regions.Find(Linearize(pos))].AddRange(plotFence[pos]);
            }

            Dictionary<int, (int, Fence[])> result = new();
            foreach (var (Region, Area) in regionArea)
            {
                //Debug.Assert(regionFence.ContainsKey(Region));
                result[Region] = (Area, regionFence[Region].ToArray());
            }
            return result;
        }

        private record class Fence(VectorRC Inside, VectorRC Direction)
        {
            private bool IsValid()
            {
                return Direction.ManhattanMetric() == 1;
            }
        }
    }
}
