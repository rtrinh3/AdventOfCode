using System.Collections;
using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/20
// --- Day 20: Firewall Rules ---
public class Day20(string input, long addressSpaceLength) : AocCommon.IAocDay
{
    public Day20(string input) :
        this(input, 4294967295 + 1L)
    {
    }

    private SegmentedBoolArray ApplyFilters()
    {
        var addressSpace = new SegmentedBoolArray(addressSpaceLength);
        var lines = AocCommon.Parsing.SplitLines(input);
        int lineIndex = 0;
        Task.Run(() =>
        {
            Thread.Sleep(1000);
            while (lineIndex < lines.Length)
            {
                Console.WriteLine($"Line {lineIndex}/{lines.Length}");
                Thread.Sleep(1000);
            }
        });
        for (; lineIndex < lines.Length; lineIndex++)
        {
            var line = lines[lineIndex];
            var parts = line.Split('-');
            long lo = long.Parse(parts[0]);
            long hi = long.Parse(parts[1]);
            for (long i = lo; i <= hi; i++)
            {
                addressSpace[i] = true;
            }
        }
        return addressSpace;
    }

    public string Part1()
    {
        var addressSpace = ApplyFilters();
        for (long i = 0; i < addressSpaceLength; i++)
        {
            if (!addressSpace[i])
            {
                return i.ToString();
            }
        }
        throw new Exception("No answer found");
    }

    public string Part2()
    {
        var addressSpace = ApplyFilters();
        long i = 0;
        long answer = 0;
        Task.Run(() =>
        {
            Thread.Sleep(1000);
            while (i < addressSpaceLength)
            {
                Console.WriteLine($"Check {i}/{addressSpaceLength}");
                Thread.Sleep(1000);
            }
        });
        for (; i < addressSpaceLength; i++)
        {
            if (!addressSpace[i])
            {
                answer++;
            }
        }
        return answer.ToString();
    }

    private class SegmentedBoolArray
    {
        public long Length { get; init; }
        private readonly BitArray[] data;

        public SegmentedBoolArray(long length)
        {
            Length = length;
            // arbitrary length limit
            if (length > (1L << 40))
            {
                throw new Exception("Too big: " + length);
            }
            int segments = (int)(length >> 24);
            int remainder = (int)(length & 0xffffff);
            data = new BitArray[segments + ((remainder > 0) ? 1 : 0)];
            for (int i = 0; i < segments; i++)
            {
                data[i] = new BitArray(1 << 24);
            }
            if (remainder > 0)
            {
                data[^1] = new BitArray(remainder);
            }
            Debug.Assert(data.Select(segment => (long)segment.Length).Sum() == length);
        }

        public bool this[long index]
        {
            get
            {
                int segment = (int)(index >> 24);
                int remainder = (int)(index & 0xffffff);
                return data[segment][remainder];
            }
            set
            {
                int segment = (int)(index >> 24);
                int remainder = (int)(index & 0xffffff);
                data[segment][remainder] = value;
            }
        }
    }
}
