using System.Diagnostics;
using System.Text;
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
            string mask = "000000000000000000000000000000000000";
            foreach (string line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    var parse = line.Split('=');
                    string newMask = parse[1].Trim();
                    Debug.Assert(newMask.Length == 36);
                    mask = newMask;
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
                    
                    StringBuilder addressBuilder = new();
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] == '0')
                        {
                            long bitExtractor = 1L << (mask.Length - i - 1);
                            char bit = (address & bitExtractor) == 0 ? '0' : '1';
                            addressBuilder.Append(bit);
                        }
                        else
                        {
                            addressBuilder.Append(mask[i]);
                        }
                    }
                    string addressPattern = addressBuilder.ToString();
                    var addresses = GenerateAddresses(addressPattern, 0).Select(BinaryToNumber);
                    foreach (long finalAddress in addresses)
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

        private static IEnumerable<string> GenerateAddresses(string pattern, int index)
        {
            if (index >= pattern.Length)
            {
                return [""];
            }
            char current = pattern[index];
            if (current == '0' || current == '1')
            {
                return GenerateAddresses(pattern, index + 1).Select(a => current + a);
            }
            if (current == 'X')
            {
                var zeroes = GenerateAddresses(pattern, index + 1).Select(a => '0' + a);
                var ones = GenerateAddresses(pattern, index + 1).Select(a => '1' + a);
                return zeroes.Concat(ones);
            }
            throw new Exception("Unknown char " + current + " at position " + index);
        }

        private static long BinaryToNumber(string bitString)
        {
            long number = 0L;
            foreach (char c in bitString)
            {
                number <<= 1;
                number |= (c == '1') ? 1L : 0L;
            }
            return number;
        }
    }
}
