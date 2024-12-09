using System.Diagnostics;
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
            long checksum = 0;
            for (int i = 0; i < map.Count && map[i] != EMPTY; i++)
            {
                checksum += i * map[i];
            }

            return checksum.ToString();
        }

        private record class Part2Span(int Start, int Length)
        {
            public int End => Start + Length; // End excluded            
        }

        public string Part2()
        {
            string trimmedInput = input.Trim();
            // Build map
            SortedDictionary<int, Part2Span> spaceStarts = new();
            SortedDictionary<int, Part2Span> spaceEnds = new();
            Part2Span MergeSpaces(Part2Span left, Part2Span right)
            {
                Debug.Assert(left.End == right.Start);
                Debug.Assert(spaceStarts[left.Start] == left);
                Debug.Assert(spaceEnds[left.End] == left);
                Debug.Assert(spaceStarts[right.Start] == right);
                Debug.Assert(spaceEnds[right.End] == right);

                var newSpace = new Part2Span(left.Start, left.Length + right.Length);
                spaceStarts[left.Start] = newSpace;
                spaceStarts.Remove(right.Start);
                spaceEnds.Remove(left.End);
                spaceEnds[right.End] = newSpace;

                return newSpace;
            }
            void AddSpace(int start, int length)
            {
                if (length <= 0)
                {
                    return;
                }
                Part2Span newSpace = new(start, length);
                spaceStarts[start] = newSpace;
                spaceEnds[start + length] = newSpace;
                if (spaceEnds.TryGetValue(start, out var left))
                {
                    newSpace = MergeSpaces(left, newSpace);
                }
                if (spaceStarts.TryGetValue(start + length, out var right))
                {
                    newSpace = MergeSpaces(newSpace, right);
                }
            }
            List<Part2Span> files = new();
            int mapIndex = 0;
            for (int i = 0; i < trimmedInput.Length; i++)
            {
                Debug.Assert(char.IsAsciiDigit(trimmedInput[i]));
                int length = trimmedInput[i] - '0';
                if (i % 2 == 0)
                {
                    // File
                    files.Add(new(mapIndex, length));
                }
                else
                {
                    // Space
                    AddSpace(mapIndex, length);
                }
                mapIndex += length;
            }

            // Defragment
            for (int file = files.Count - 1; file >= 0; file--)
            {
                // spaceStarts is sorted by key, ie start index
                var space = spaceStarts.FirstOrDefault(kvp => kvp.Value.Length >= files[file].Length).Value;
                if (space == null || space.Start > files[file].Start)
                {
                    continue;
                }
                AddSpace(files[file].Start, files[file].Length);
                files[file] = files[file] with { Start = space.Start };
                spaceStarts.Remove(space.Start);
                spaceEnds.Remove(space.End);
                var remainingSpace = space.Length - files[file].Length;
                if (remainingSpace > 0)
                {
                    AddSpace(space.Start + files[file].Length, remainingSpace);
                }
            }

            // Checksum
            long checksum = 0;
            for (int file = 0; file < files.Count; file++)
            {
                for (int i = files[file].Start; i < files[file].End; i++)
                {
                    checksum += i * file;
                }
            }
            return checksum.ToString();
        }
    }
}
