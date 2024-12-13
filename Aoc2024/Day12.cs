﻿using AocCommon;
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
                var thisFence = regionData.Fences;
                UnionFind<Fence> sides = new();                
                for (int i = 0; i < thisFence.Length; i++)
                {
                    Fence fenceI = thisFence[i];
                    for (int j = i + 1; j < thisFence.Length; j++)
                    {
                        Fence fenceJ = thisFence[j];
                        if (fenceI.IsAdjacentAlignedWith(fenceJ))
                        {
                            sides.Union(fenceI, fenceJ);
                        }
                    }
                }
                HashSet<Fence> sidesSet = new();
                foreach (var f in thisFence)
                {
                    sidesSet.Add(sides.Find(f));
                }
                var sideCount = sidesSet.Count;
                var thisPrice = Math.BigMul(regionData.Area, sideCount);
                total += thisPrice;
            }

            return total.ToString();
        }

        private Dictionary<VectorRC, (int Area, Fence[] Fences)> FindRegions()
        {
            // Partition the map and assign fences to plots
            UnionFind<VectorRC> regions = new();
            DefaultDict<VectorRC, List<Fence>> plotFence = new();
            void ProcessPlots(VectorRC a, VectorRC b)
            {
                if (map.Get(a) == map.Get(b))
                {
                    regions.Union(a, b);
                }
                else
                {
                    // Order of arguments to Fence is (Inside, Outside)
                    plotFence[a].Add(new Fence(a, b));
                    plotFence[b].Add(new Fence(b, a));
                }
            }
            // Scan one more plot to the left and top
            for (int row = -1; row < map.Height; row++)
            {
                for (int col = -1; col < map.Height; col++)
                {
                    // Check the right and bottom
                    VectorRC pos = new(row, col);
                    VectorRC right = new(row, col + 1);
                    ProcessPlots(pos, right);
                    VectorRC down = new(row + 1, col);
                    ProcessPlots(pos, down);
                }
            }

            // Calculate area and fence of regions
            DefaultDict<VectorRC, int> regionArea = new();
            DefaultDict<VectorRC, List<Fence>> regionFence = new();
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Height; col++)
                {
                    VectorRC pos = new(row, col);
                    regionArea[regions.Find(pos)]++;
                    regionFence[regions.Find(pos)].AddRange(plotFence[pos]);
                }
            }

            Dictionary<VectorRC, (int, Fence[])> result = new();
            foreach (var (Region, Area) in regionArea)
            {
                //Debug.Assert(regionFence.ContainsKey(Region));
                result[Region] = (Area, regionFence[Region].ToArray());
            }
            return result;
        }

        // Fence is defined by the two plots separated by the fence.
        private record class Fence(VectorRC Inside, VectorRC Outside)
        {
            private bool IsValid()
            {
                return (Outside - Inside).ManhattanMetric() == 1;
            }

            internal bool IsAdjacentAlignedWith(Fence other)
            {
                if (this == other)
                {
                    return false;
                }
                //Debug.Assert(IsValid());
                //Debug.Assert(other.IsValid());
                var thisOrientation = Outside - Inside;
                var otherOrientation = other.Outside - other.Inside;
                return thisOrientation == otherOrientation && (Inside + thisOrientation.RotatedLeft() == other.Inside || Inside + thisOrientation.RotatedRight() == other.Inside);
            }
        }
    }
}
