using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/2
// --- Day 2: Gift Shop ---
public class Day02(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        var pairs = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        long accumulator = 0L;
        foreach (var pair in pairs)
        {
            var parts = pair.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(parts.Length == 2);
            long lower = long.Parse(parts[0]);
            long upper = long.Parse(parts[1]);
            for (long i = lower; i <= upper; i++)
            {
                string id = i.ToString();
                if (id.Length % 2 != 0)
                {
                    continue;
                }
                int halfLength = id.Length / 2;
                if (id.Take(halfLength).SequenceEqual(id.Skip(halfLength)))
                {
                    accumulator += i;
                }
            }
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}