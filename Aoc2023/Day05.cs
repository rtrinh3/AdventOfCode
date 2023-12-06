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
                }).OrderBy(r => r.sourceRangeStart).ToList();

                // Fill gaps in ranges with trivial mappings, inspired by https://reddit.com/r/adventofcode/comments/18b4b0r/2023_day_5_solutions/kc67vzu/
                int rangesInText = ranges.Count;
                if (ranges[0].sourceRangeStart > 0) {
                    ranges.Add((0, 0, ranges[0].sourceRangeStart));
                }
                for (int r = 0; r < rangesInText - 1; r++) {
                    if (ranges[r].sourceRangeStart + ranges[r].rangeLength < ranges[r + 1].sourceRangeStart) {
                        var gapStart = ranges[r].sourceRangeStart + ranges[r].rangeLength;
                        var gapLength = ranges[r + 1].sourceRangeStart - gapStart;
                        ranges.Add((gapStart, gapStart, gapLength));
                    }
                }
                var lastRangeStart = ranges[rangesInText - 1].sourceRangeStart + ranges[rangesInText - 1].rangeLength;
                ranges.Add((lastRangeStart, lastRangeStart, long.MaxValue - lastRangeStart));
                ranges.Sort((a, b) => a.sourceRangeStart.CompareTo(b.sourceRangeStart));

                var rangeKeys = ranges.Select(r => r.sourceRangeStart).ToArray();
                maps[i - 1] = (rangeKeys, ranges.ToArray());
                previousTarget = mapTo;
            }
        }

        private long ConvertFromSeedToLocation(long seed)
        {
            long n = seed;
            foreach (var mapping in maps)
            {
                // The keys are sorted, so we can search through them with a binary search
                // The result of Array.BinarySearch is either positive -- the index of an exact match --
                // or negative -- the bitwise complement of the index of the element > our item.
                // We want the index of the element <= our item.
                var binarySearchResult = Array.BinarySearch(mapping.rangeKeys, n);
                var index = (binarySearchResult < 0) ? (~binarySearchResult - 1) : binarySearchResult;
                Debug.Assert(0 <= index && index < mapping.rangeKeys.Length, "Did I mess up the range fillers?");
                var range = mapping.ranges[index];
                Debug.Assert(range.sourceRangeStart <= n && n < range.sourceRangeStart + range.rangeLength, "Did I mess up the range fillers?");
                n = n - range.sourceRangeStart + range.destinationRangeStart;
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
            (long start, long length)[] initialSeedRanges = seeds.Chunk(2).Select(c => (c[0], c[1])).ToArray();
            Stack<(long start, long length)> incomingRanges = new(initialSeedRanges);
            foreach (var mapping in maps) {
                Stack<(long start, long length)> mappedRanges = new();
                while (incomingRanges.Count > 0) {
                    var thisRange = incomingRanges.Pop();
                    // See comment about binary search in ConvertFromSeedToLocation
                    var binarySearchResult = Array.BinarySearch(mapping.rangeKeys, thisRange.start);
                    var index = (binarySearchResult < 0) ? (~binarySearchResult - 1) : binarySearchResult;
                    Debug.Assert(0 <= index && index < mapping.rangeKeys.Length, "Did I mess up the range fillers?");
                    var range = mapping.ranges[index];
                    if (thisRange.start + thisRange.length > range.sourceRangeStart + range.rangeLength) {
                        // If thisRange goes beyond range
                        var overlap = range.sourceRangeStart + range.rangeLength - thisRange.start;
                        mappedRanges.Push((thisRange.start - range.sourceRangeStart + range.destinationRangeStart, overlap));
                        // Put the cut-off part back in the queue for processing
                        incomingRanges.Push((thisRange.start + overlap, thisRange.length - overlap));
                    } else {
                        // thisRange fits within range
                        mappedRanges.Push((thisRange.start - range.sourceRangeStart + range.destinationRangeStart, thisRange.length));
                    }
                }
                incomingRanges = mappedRanges;
            }
            return incomingRanges.Min(r => r.start);
        }
    }
}
