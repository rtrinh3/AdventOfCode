using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/2
// --- Day 2: Gift Shop ---
public class Day02(string input) : AocCommon.IAocDay
{
    private readonly (long Lower, long Upper)[] Ranges =
        input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        .Select(pair =>
        {
            var parts = pair.Split('-', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(parts.Length == 2);
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        }).ToArray();

    public string Part1()
    {
        long accumulator = 0L;
        foreach (var (Lower, Upper) in Ranges)
        {
            for (long i = Lower; i <= Upper; i++)
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
        long accumulator = 0L;
        foreach (var (Lower, Upper) in Ranges)
        {
            for (long idn = Lower; idn <= Upper; idn++)
            {
                string id = idn.ToString();
                for (int segmentLength = 1; segmentLength * 2 <= id.Length; segmentLength++)
                {
                    if (id.Length % segmentLength != 0)
                    {
                        continue;
                    }
                    int repeats = id.Length / segmentLength;
                    var segments = Enumerable.Range(0, repeats).Select(n => id.Substring(n * segmentLength, segmentLength)).ToArray();
                    bool isRepetitive = segments.All(s => s == segments[0]);
                    if (isRepetitive)
                    {
                        accumulator += idn;
                        break;
                    }
                }
            }
        }
        return accumulator.ToString();
    }
}