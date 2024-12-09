using System.Diagnostics;
using System.Numerics;
using AocCommon;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/9
    // --- Day 9: Disk Fragmenter ---
    public class Day09(string input) : IAocDay
    {
        public string Part1()
        {
            string trimmedInput = input.Trim();
            // Build map
            const int EMPTY = -1;
            List<int> map = new();
            int fileIndex = 0;
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
                    map.AddRange(Enumerable.Repeat(EMPTY, length));
                }
            }

            // Defragment
            int spaceBlockIndex = 0;
            while (map[spaceBlockIndex] != EMPTY)
            {
                spaceBlockIndex++;
            }
            int fileBlockIndex = map.Count - 1;
            while (map[fileBlockIndex] == EMPTY)
            {
                fileBlockIndex--;
            }
            while (spaceBlockIndex < fileBlockIndex)
            {
                map[spaceBlockIndex] = map[fileBlockIndex];
                map[fileBlockIndex] = EMPTY;
                while (map[spaceBlockIndex] != EMPTY)
                {
                    spaceBlockIndex++;
                }
                while (map[fileBlockIndex] == EMPTY)
                {
                    fileBlockIndex--;
                }
            }

            // Checksum
            BigInteger checksum = BigInteger.Zero;
            for (int i = 0; i < map.Count && map[i] != EMPTY; i++)
            {
                checksum += i * map[i];
            }

            return checksum.ToString();
        }

        private record class Part2Space(int Start, int End)
        {
            public int Length => End - Start + 1;
        }

        public string Part2()
        {
            string trimmedInput = input.Trim();
            // Build map
            SortedDictionary<int, Part2Space> spaceStarts = new();
            SortedDictionary<int, Part2Space> spaceEnds = new();
            Part2Space MergeSpaces(Part2Space left, Part2Space right)
            {
                Debug.Assert(left.End + 1 == right.Start);
                Debug.Assert(spaceStarts[left.Start] == left);
                Debug.Assert(spaceEnds[left.End] == left);
                Debug.Assert(spaceStarts[right.Start] == right);
                Debug.Assert(spaceEnds[right.End] == right);
                
                var newSpace = new Part2Space(left.Start, right.End);
                spaceStarts[left.Start] = newSpace;
                spaceStarts.Remove(right.Start);
                spaceEnds.Remove(left.End);
                spaceEnds[right.End] = newSpace;
                
                return newSpace;
            }
            void AddSpace(int start, int end)
            {
                if (end < start)
                {
                    return;
                }
                Part2Space newSpace = new(start, end);
                spaceStarts[start] = newSpace;
                spaceEnds[end] = newSpace;
                if (spaceEnds.TryGetValue(start - 1, out var left))
                {
                    newSpace = MergeSpaces(left, newSpace);
                }
                if (spaceStarts.TryGetValue(end + 1, out var right))
                {
                    newSpace = MergeSpaces(newSpace, right);
                }
            }
            List<int> fileStarts = new();
            List<int> fileLengths = new();
            int mapIndex = 0;
            for (int i = 0; i < trimmedInput.Length; i++)
            {
                Debug.Assert(char.IsAsciiDigit(trimmedInput[i]));
                int length = trimmedInput[i] - '0';
                if (i % 2 == 0)
                {
                    // File
                    fileStarts.Add(mapIndex);
                    fileLengths.Add(length);
                }
                else
                {
                    // Space
                    AddSpace(mapIndex, mapIndex + length - 1);
                }
                mapIndex += length;
            }

            // Defragment
            //throw new NotImplementedException("TODO");
            for (int file = fileStarts.Count - 1; file >= 0; file--)
            {
                // spaceStarts is sorted by key, ie start index
                var space = spaceStarts.FirstOrDefault(kvp => kvp.Value.Length >= fileLengths[file]).Value;
                if (space == null)
                {
                    continue;
                }
                if (space.Start > fileStarts[file])
                {
                    continue;
                }
                AddSpace(fileStarts[file], fileStarts[file] + fileLengths[file] - 1);
                fileStarts[file] = space.Start;
                spaceStarts.Remove(space.Start);
                spaceEnds.Remove(space.End);
                var remainingSpace = space.Length - fileLengths[file];
                if (remainingSpace > 0)
                {
                    AddSpace(space.Start + fileLengths[file], space.End);
                }
            }

            // Checksum
            BigInteger checksum = BigInteger.Zero;
            for (int file = 0; file < fileStarts.Count; file++)
            {
                for (int i = fileStarts[file]; i < fileStarts[file] + fileLengths[file]; i++)
                {
                    checksum += i * file;
                }
            }
            return checksum.ToString();
        }
    }
}
