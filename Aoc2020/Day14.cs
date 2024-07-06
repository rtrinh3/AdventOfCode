using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/14
    // --- Day 14: Docking Data ---
    public class Day14(string input) : IAocDay
    {
        private readonly string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

        public long Part1()
        {
            Dictionary<long, long> memory = new();
            long onesMask = 0L;
            long zeroesMask = 0xF_FFFF_FFFF;
            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var parse = line.Split('=');
                    string mask = parse[1].Trim();
                    Debug.Assert(mask.Length == 36);
                    onesMask = 0L;
                    zeroesMask = 0xF_FFFF_FFFF;
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] == '0')
                        {
                            zeroesMask &= ~(1L << (mask.Length - i - 1));
                        }
                        else if (mask[i] == '1')
                        {
                            onesMask |= (1L << (mask.Length - i - 1));
                        }
                    }
                }
                else if (line.StartsWith("mem"))
                {
                    var parse = Regex.Match(line, @"^mem\[(\d+)\] = (\d+)$");
                    if (!parse.Success)
                    {
                        throw new Exception("What is this instruction " + line);
                    }
                    long address = long.Parse(parse.Groups[1].Value);
                    long value = long.Parse(parse.Groups[2].Value);
                    memory[address] = ((value & zeroesMask) | onesMask);
                }
                else
                {
                    throw new Exception("What is this instruction " + line);
                }
            }
            var sum = memory.Values.Sum();
            return sum;
        }

        public long Part2()
        {
            Dictionary<long, long> memory = new();
            long onesMask = 0L;
            long wildMask = 0L;
            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var parse = line.Split('=');
                    string newMask = parse[1].Trim();
                    Debug.Assert(newMask.Length == 36);
                    onesMask = 0L;
                    wildMask = 0L;
                    foreach (char bit in newMask)
                    {
                        onesMask <<= 1;
                        wildMask <<= 1;
                        if (bit == '1')
                        {
                            onesMask |= 1;
                        }
                        if (bit == 'X')
                        {
                            wildMask |= 1;
                        }
                    }
                }
                else if (line.StartsWith("mem"))
                {
                    var parse = Regex.Match(line, @"^mem\[(\d+)\] = (\d+)$");
                    if (!parse.Success)
                    {
                        throw new Exception("What is this instruction " + line);
                    }
                    long address = long.Parse(parse.Groups[1].Value);
                    long value = long.Parse(parse.Groups[2].Value);

                    long addressBase = address | onesMask;
                    var adresses = GenerateAddresses(addressBase, wildMask);
                    foreach (long finalAddress in adresses)
                    {
                        memory[finalAddress] = value;
                    }
                }
                else
                {
                    throw new Exception("What is this instruction " + line);
                }
            }
            var sum = memory.Values.Sum();
            return sum;
        }

        private static IEnumerable<long> GenerateAddresses(long baseNumber, long mask)
        {
            if (mask == 0)
            {
                return [baseNumber];
            }
            long highBitMask = (long)BitOperations.RoundUpToPowerOf2((ulong)mask);
            if (highBitMask > mask)
            {
                highBitMask >>= 1;
            }
            long nextBitMask = mask & ~highBitMask;
            long baseUnset = baseNumber & ~highBitMask;
            var unsetAddresses = GenerateAddresses(baseUnset, nextBitMask);
            long baseSet = baseNumber | highBitMask;
            var setAddresses = GenerateAddresses(baseSet, nextBitMask);
            return unsetAddresses.Concat(setAddresses);
        }
    }
}
