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
        private long[] seeds;
        private Dictionary<string, (string mapTo, long[] rangeKeys, (long destinationRangeStart, long sourceRangeStart, long rangeLength)[] ranges)> maps = new();

        public Day05(string input)
        {
            string[] paragraphs = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);
            Debug.Assert(paragraphs[0].StartsWith("seeds:"));
            seeds = paragraphs[0].Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(s => long.Parse(s)).ToArray();
            //var maps = new Dictionary<string, (string mapTo, long[] rangeKeys, (long destinationRangeStart, long sourceRangeStart, long rangeLength)[] ranges)>();
            foreach (string map in paragraphs.Skip(1))
            {
                string[] lines = map.Split('\n');
                var parseHeader = Regex.Match(lines[0], @"([a-z]+)-to-([a-z]+) map:");
                string mapFrom = parseHeader.Groups[1].Value;
                string mapTo = parseHeader.Groups[2].Value;
                var ranges = lines.Skip(1).Select(range =>
                {
                    var numbers = range.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var destinationRangeStart = long.Parse(numbers[0]);
                    var sourceRangeStart = long.Parse(numbers[1]);
                    var rangeLength = long.Parse(numbers[2]);
                    return (destinationRangeStart, sourceRangeStart, rangeLength);
                }).OrderBy(r => r.sourceRangeStart).ToArray();
                var rangeKeys = ranges.Select(r => r.sourceRangeStart).ToArray();
                maps[mapFrom] = (mapTo, rangeKeys, ranges);
            }

            //// Sanity check: we can map from seed to location
            //string testMaps = "seed";
            //while (testMaps != "location")
            //{
            //    testMaps = maps[testMaps].mapTo;
            //}
        }

        private (long, string) Convert(long n, string t)
        {
            var mapping = maps[t];
            var binarySearchResult = Array.BinarySearch(mapping.rangeKeys, n);
            var index = (binarySearchResult < 0) ? (~binarySearchResult - 1) : binarySearchResult;
            if (index < 0)
            {
                return (n, mapping.mapTo);
            }
            var range = mapping.ranges[index];
            if (range.sourceRangeStart <= n && n < range.sourceRangeStart + range.rangeLength)
            {
                return (n - range.sourceRangeStart + range.destinationRangeStart, mapping.mapTo);
            }
            else
            {
                return (n, mapping.mapTo);
            }
        }

        public long Part1()
        {
            var locations = seeds.Select(seed =>
            {
                var item = (seed, "seed");
                while (item.Item2 != "location")
                {
                    item = Convert(item.Item1, item.Item2);
                }
                return item.Item1;
            });
            return locations.Min();
        }

        public long Part2()
        {
            object lockMinLocation = new();
            long minLocation = long.MaxValue;
            int seedRangeCount = seeds.Length / 2;
            Parallel.ForEach(seeds.Chunk(2), seedRange =>
            {
                long localMinLocation = long.MaxValue;
                for (long seed = seedRange[0]; seed < seedRange[0] + seedRange[1]; seed++)
                {
                    var item = (seed, "seed");
                    while (item.Item2 != "location")
                    {
                        item = Convert(item.Item1, item.Item2);
                    }
                    long location = item.Item1;
                    if (location < localMinLocation)
                    {
                        localMinLocation = location;
                    }
                }
                lock (lockMinLocation)
                {
                    if (localMinLocation < minLocation)
                    {
                        minLocation = localMinLocation;
                    }
                }
                Console.WriteLine($"Done range {seedRange[0]} length {seedRange[1]}");
            });
            Console.WriteLine(minLocation);
            return minLocation;
        }
    }
}
