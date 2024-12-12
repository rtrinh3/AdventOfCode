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
            // Partition the map and assign fences to plots
            UnionFind<VectorRC> regions = new();
            DefaultDict<VectorRC, int> plotFence = new(() => 0);
            // Scan one more plot to the left and top
            for (int row = -1; row < map.Height; row++)
            {
                for (int col = -1; col < map.Height; col++)
                {
                    // Check the right and bottom
                    VectorRC pos = new(row, col);
                    {
                        VectorRC right = new(row, col + 1);
                        if (map.Get(pos) == map.Get(right))
                        {
                            regions.Union(pos, right);
                        }
                        else
                        {
                            plotFence[pos]++;
                            plotFence[right]++;
                        }
                    }
                    {
                        VectorRC down = new(row + 1, col);
                        if (map.Get(pos) == map.Get(down))
                        {
                            regions.Union(pos, down);
                        }
                        else
                        {
                            plotFence[pos]++;
                            plotFence[down]++;
                        }
                    }
                }
            }

            // Calculate area and fence of regions
            DefaultDict<VectorRC, int> regionArea = new();
            DefaultDict<VectorRC, int> regionFence = new();
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Height; col++)
                {
                    VectorRC pos = new(row, col);
                    regionArea[regions.Find(pos)] += 1;
                    regionFence[regions.Find(pos)] += plotFence[pos];
                }
            }

            // Calculate prices
            long total = 0;
            Debug.Assert(regionArea.Count == regionFence.Count);
            foreach (var region in regionArea.Keys)
            {
                var type = map.Get(region); // for debugging
                var thisArea = regionArea[region];
                var thisFence = regionFence[region];
                var thisPrice = Math.BigMul(thisArea, thisFence);
                total += thisPrice;
            }

            return total.ToString();
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
