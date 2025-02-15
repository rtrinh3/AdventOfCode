using AocCommon;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/20
// --- Day 20: Firewall Rules ---
public class Day20(string input, uint maxAddress) : IAocDay
{
    public Day20(string input) :
        this(input, 4294967295)
    {
    }

    private record Range(uint Min, uint Max);

    private SortedSet<Range> ApplyFilters()
    {
        SortedSet<Range> addressSpace = new(Comparer<Range>.Create((a, b) => a.Min.CompareTo(b.Min)))
        {
            new(0, maxAddress)
        };
        var lines = Parsing.SplitLines(input);
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            var blockedLow = uint.Parse(parts[0]);
            var blockedHigh = uint.Parse(parts[1]);
            Range zero = new(0, 0);
            Range blockedRange = new(blockedLow, blockedHigh);
            Range blockedMax = new(blockedHigh, blockedHigh);
            var toRemove = addressSpace.GetViewBetween(blockedRange, blockedMax);
            List<Range> toAdd = new();
            foreach (var range in toRemove)
            {
                Debug.Assert(blockedLow <= range.Min && range.Min <= blockedHigh); // by definition of GetViewBetween
                if (range.Max > blockedHigh)
                {
                    toAdd.Add(new(blockedHigh + 1, range.Max));
                }
            }
            toRemove.Clear();
            var overlapStart = addressSpace.GetViewBetween(zero, blockedRange).Reverse().FirstOrDefault();
            if (overlapStart is not null)
            {
                Debug.Assert(overlapStart.Min <= blockedLow); // by definition of GetViewBetween
                if (blockedLow <= overlapStart.Max)
                {
                    addressSpace.Remove(overlapStart);
                    toAdd.Add(new(overlapStart.Min, blockedLow - 1));
                    if (blockedHigh <= overlapStart.Max)
                    {
                        toAdd.Add(new(blockedHigh + 1, overlapStart.Max));
                    }
                }
            }
            toAdd.RemoveAll(r => r.Max < r.Min);
            addressSpace.UnionWith(toAdd);
        }
        return addressSpace;
    }

    public string Part1()
    {
        var addressSpace = ApplyFilters();
        if (addressSpace.Count > 0)
        {
            var answer = addressSpace.First().Min;
            return answer.ToString();
        }
        else
        {
            throw new Exception("No answer found");
        }
    }

    public string Part2()
    {
        var addressSpace = ApplyFilters();
        uint answer = 0;
        foreach (var range in addressSpace)
        {
            answer += (range.Max - range.Min + 1);
        }
        return answer.ToString();
    }
}
