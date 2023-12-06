using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aoc2023
{
    public class Day05
    {
        private readonly long[] seeds;
        private readonly (long[] rangeKeys, (long destinationRangeStart, long sourceRangeStart, long rangeLength)[] ranges)[] maps;

        public Day05(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            Debug.Assert(paragraphs[0].StartsWith("seeds:"));
            seeds = paragraphs[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(long.Parse).ToArray();
            string previousTarget = "seed";
            maps = new (long[], (long, long, long)[])[paragraphs.Length - 1];
            for (int i = 1; i < paragraphs.Length; i++)
            {
                string map = paragraphs[i];
                string[] lines = map.Split('\n');
                var parseHeader = Regex.Match(lines[0], @"([a-z]+)-to-([a-z]+) map:");
                string mapFrom = parseHeader.Groups[1].Value;
                string mapTo = parseHeader.Groups[2].Value;
                Debug.Assert(previousTarget == mapFrom); // Assert the mappings are in order
                var ranges = lines.Skip(1).Select(range =>
                {
                    var numbers = range.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var destinationRangeStart = long.Parse(numbers[0]);
                    var sourceRangeStart = long.Parse(numbers[1]);
                    var rangeLength = long.Parse(numbers[2]);
                    return (destinationRangeStart, sourceRangeStart, rangeLength);
                }).OrderBy(r => r.sourceRangeStart).ToArray();
                var rangeKeys = ranges.Select(r => r.sourceRangeStart).ToArray();
                maps[i - 1] = (rangeKeys, ranges);
                previousTarget = mapTo;
            }
        }

        private long ConvertFromSeedToLocation(long seed)
        {
            long n = seed;
            foreach (var mapping in maps)
            {
                var binarySearchResult = Array.BinarySearch(mapping.rangeKeys, n);
                var index = (binarySearchResult < 0) ? (~binarySearchResult - 1) : binarySearchResult;
                if (index >= 0)
                {
                    var range = mapping.ranges[index];
                    if (range.sourceRangeStart <= n && n < range.sourceRangeStart + range.rangeLength)
                    {
                        n = n - range.sourceRangeStart + range.destinationRangeStart;
                    }
                    // else no adjustment
                }
                // else no adjustment
            }
            return n;
        }

        public long Part1()
        {
            var locations = seeds.Select(ConvertFromSeedToLocation);
            return locations.Min();
        }

        public long Part2()
        {
            object minLocationLock = new();
            long minLocation = long.MaxValue;
            int seedRangeCount = seeds.Length / 2;
            int done = 0;
            Parallel.ForEach(seeds.Chunk(2), seedRange =>
            {
                long localMinLocation = long.MaxValue;
                for (long seed = seedRange[0]; seed < seedRange[0] + seedRange[1]; seed++)
                {
                    long location = ConvertFromSeedToLocation(seed);
                    if (location < localMinLocation)
                    {
                        localMinLocation = location;
                    }
                }
                lock (minLocationLock)
                {
                    if (localMinLocation < minLocation)
                    {
                        minLocation = localMinLocation;
                    }
                    ++done;
                }
                Console.WriteLine($"Done range {seedRange[0]} length {seedRange[1]}, {done}/{seedRangeCount}");
            });
            Console.WriteLine(minLocation);
            return minLocation;
        }
    }
}
