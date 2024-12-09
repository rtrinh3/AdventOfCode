using System.Diagnostics;
using System.Numerics;
using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/9
    // --- Day 9: Disk Fragmenter ---
    public class Day09(string input) : IAocDay
    {
        private const long SPACE = -1;

        public string Part1()
        {
            string trimmedInput = input.Trim();
            // Build map
            List<long> map = new();
            long fileIndex = 0;
            for (int i = 0; i < trimmedInput.Length; i++)
            {
                Debug.Assert(char.IsAsciiDigit(trimmedInput[i]));
                int length = trimmedInput[i] - '0';
                if (i % 2 == 0)
                {
                    // File
                    map.AddRange(Enumerable.Repeat(fileIndex, length));
                    fileIndex++;
                }
                else
                {
                    // Space
                    map.AddRange(Enumerable.Repeat(SPACE, length));
                }
            }

            // Defragment
            int spaceBlockIndex = 0;
            while (map[spaceBlockIndex] != SPACE)
            {
                spaceBlockIndex++;
            }
            int fileBlockIndex = map.Count - 1;
            while (map[fileBlockIndex] == SPACE)
            {
                fileBlockIndex--;
            }
            while (spaceBlockIndex < fileBlockIndex)
            {
                map[spaceBlockIndex] = map[fileBlockIndex];
                map[fileBlockIndex] = SPACE;
                while (map[spaceBlockIndex] != SPACE)
                {
                    spaceBlockIndex++;
                }
                while (map[fileBlockIndex] == SPACE)
                {
                    fileBlockIndex--;
                }
            }

            // Checksum
            BigInteger checksum = new(0);
            for (int i = 0; i < map.Count && map[i] != SPACE; i++)
            {
                checksum += i * map[i];
            }

            return checksum.ToString();
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }
    }
}
