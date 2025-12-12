using AocCommon;
using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/12
// --- Day 12: Christmas Tree Farm ---
public class Day12(string input) : AocCommon.IAocDay
{
    private record Block(VectorRC[] Tiles, int TileCount, int Height, int Width);

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
                    tiles.Add(new(row, col));
                }
            }
            blocks.Add(new(tiles.ToArray(), tiles.Count, blockLines.Length, width));
        }
        int dimension = 0;
        var distinctHeights = blocks.Select(b => b.Height).Distinct().ToList();
        var distinctWidths = blocks.Select(b => b.Width).Distinct().ToList();
        if (distinctHeights.Count==1 && distinctWidths.Count == 1 && distinctHeights[0] == distinctWidths[0])
        {
            dimension = distinctHeights[0];
        }

        var treesText = paragraphs[^1];
        var treeLines = treesText.Split('\n');
        int accumulator = 0;
        foreach (var line in treeLines)
        {
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
            
            if (dimension != 0)
            {
                var boundingBoxPacking = (height / dimension) * (width / dimension);
                if (boundingBoxPacking >= requirements.Sum())
                {
                    accumulator++;
                    continue;
                }
            }

            // TODO More accurate criteria
        }

        return accumulator.ToString();
    }

    public string Part2()
    {
        return "Merry Christmas!";
    }
}