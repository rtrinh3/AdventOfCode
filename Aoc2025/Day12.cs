using AocCommon;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/12
// --- Day 12: Christmas Tree Farm ---
public class Day12(string input) : AocCommon.IAocDay
{
    private record Block(VectorRC[][] Variants, int TileCount, int Height, int Width);

    public string Part1()
    {
        var paragraphs = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
        var blocksText = paragraphs[..^1];
        List<Block> blocks = new List<Block>();
        for (int b = 0; b < blocksText.Length; b++)
        {
            var blockAllLines = blocksText[b].Split('\n');
            int index = int.Parse(blockAllLines[0][..^1]);
            Debug.Assert(index == b);
            var blockLines = blockAllLines[1..];
            List<VectorRC> tiles = new();
            int width = 0;
            for (int row = 0; row < blockLines.Length; row++)
            {
                width = Math.Max(width, blockLines[row].Length);
                for (int col = 0; col < blockLines[row].Length; col++)
                {
                    if (blockLines[row][col] == '#')
                    {
                        tiles.Add(new(row, col));
                    }
                }
            }
            HashSet<EquatableSet<VectorRC>> variants = new();
            VectorRC[] tempTiles = tiles.ToArray();
            // Non-mirrored
            for (int i = 0; i < 4; i++)
            {
                variants.Add(new(tempTiles));
                for (int t = 0; t < tempTiles.Length; t++)
                {
                    tempTiles[t] = tempTiles[t].RotatedRight();
                }
                tempTiles = RecenterTiles(tempTiles).ToArray();
            }
            // Mirrored
            for (int t = 0; t < tempTiles.Length; t++)
            {
                tempTiles[t] = new(tempTiles[t].Row, -tempTiles[t].Col);
            }
            tempTiles = RecenterTiles(tempTiles).ToArray();
            for (int i = 0; i < 4; i++)
            {
                variants.Add(new(tempTiles));
                for (int t = 0; t < tempTiles.Length; t++)
                {
                    tempTiles[t] = tempTiles[t].RotatedRight();
                }
                tempTiles = RecenterTiles(tempTiles).ToArray();
            }
            blocks.Add(new(variants.Select(v => v.ToArray()).ToArray(), tiles.Count, blockLines.Length, width));
        }

        int simpleDim = 0;
        var distinctHeights = blocks.Select(b => b.Height).Distinct().ToList();
        var distinctWidths = blocks.Select(b => b.Width).Distinct().ToList();
        if (distinctHeights.Count == 1 && distinctWidths.Count == 1 && distinctHeights[0] == distinctWidths[0])
        {
            simpleDim = distinctHeights[0];
        }

        var treesText = paragraphs[^1];
        var treeLines = treesText.Split('\n');
        int accumulator = 0;
        foreach (var line in treeLines)
        {
            Console.WriteLine(line);
            var splitColon = line.Split(':');
            var dimsText = splitColon[0].Split('x');
            int height = int.Parse(dimsText[0]);
            int width = int.Parse(dimsText[1]);
            var requirementsText = splitColon[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] requirements = requirementsText.Select(int.Parse).ToArray();
            Debug.Assert(requirements.Length == blocks.Count);

            var requiredTiles = requirements.Zip(blocks, (r, b) => r * b.TileCount).Sum();
            if (requiredTiles > height * width)
            {
                continue;
            }

            if (simpleDim != 0)
            {
                var boundingBoxPacking = (height / simpleDim) * (width / simpleDim);
                if (boundingBoxPacking >= requirements.Sum())
                {
                    accumulator++;
                    continue;
                }
            }

            Console.WriteLine("Non trivial");
            Func<EquatableSet<VectorRC>, EquatableArray<int>, bool> FindArrangement = null;
            FindArrangement = Memoization.Make((EquatableSet<VectorRC> arrangement, EquatableArray<int> remaining) =>
            {
                int arrangementTop = arrangement.Count > 0 ? arrangement.Min(x => x.Row) : 0;
                int arrangementBottom = arrangement.Count > 0 ? arrangement.Max(x => x.Row) : 0;
                if (arrangementBottom - arrangementTop + 1 > height)
                {
                    return false;
                }
                int arrangementLeft = arrangement.Count > 0 ? arrangement.Min(x => x.Col) : 0;
                int arrangementRight = arrangement.Count > 0 ? arrangement.Max(x => x.Col) : 0;
                if (arrangementRight - arrangementLeft + 1 > width)
                {
                    return false;
                }
                if (remaining.All(x => x == 0))
                {
                    return true;
                }
                for (int t = 0; t < remaining.Count; t++)
                {
                    if (remaining[t] == 0)
                    {
                        continue;
                    }
                    var nextRemaining = remaining.ToArray();
                    nextRemaining[t]--;
                    int expansion = Math.Max(blocks[t].Width, blocks[t].Height);
                    foreach (var blockVariant in blocks[t].Variants)
                    {
                        for (int row = arrangementTop - expansion - 1; row < arrangementBottom + expansion + 1; row++)
                        {
                            for (int col = arrangementLeft - expansion - 1; col < arrangementRight + expansion + 1; col++)
                            {
                                VectorRC offset = new(row, col);
                                HashSet<VectorRC> nextArrangement = new(arrangement);
                                foreach (var tile in blockVariant)
                                {
                                    var offsetTile = tile + offset;
                                    if (!nextArrangement.Add(offsetTile))
                                    {
                                        goto NEXT_VARIANT;
                                    }
                                }
                                var nextArrangementRecentered = RecenterTiles(nextArrangement);
                                var foundNext = FindArrangement(new(nextArrangementRecentered), new(nextRemaining));
                                if (foundNext)
                                {
                                    return true;
                                }
                            NEXT_VARIANT:
                                ;
                            }
                        }
                    }
                }
                return false;
            });
            var arrangementFound = FindArrangement(new([]), new(requirements));
            if (arrangementFound)
            {
                Console.WriteLine("OK");
                accumulator++;
            }
            else
            {
                Console.WriteLine("No");
            }
        }

        return accumulator.ToString();
    }

    private static IEnumerable<VectorRC> RecenterTiles(IEnumerable<VectorRC> tiles)
    {
        int minRow = tiles.Min(t => t.Row);
        int minCol = tiles.Min(t => t.Col);
        var minVec = new VectorRC(minRow, minCol);
        return tiles.Select(t => t - minVec);
    }

    public string Part2()
    {
        return "Merry Christmas!";
    }
}