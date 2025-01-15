using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/17
    // --- Day 17: Pyroclastic Flow ---
    public class Day17(string inputRaw) : IAocDay
    {
        private const int wellWidth = 7;
        private static readonly VectorXY[][] blocks = [
            [new(0, 0), new(1, 0), new(2, 0), new(3, 0)], // -
            [new(1, 0), new(0, 1), new(1, 1), new(2, 1), new(1, 2)], // +
            [new(0, 0), new(1, 0), new(2, 0), new(2, 1), new(2, 2)], // J
            [new(0, 0), new(0, 1), new(0, 2), new(0, 3)], // I
            [new(0, 0), new(1, 0), new(1, 1), new(0, 1)] // O
        ];

        private readonly string input = inputRaw.TrimEnd();

        public string Part1()
        {
            var answer = DoPuzzle(2022);
            return answer.ToString();
        }

        public string Part2()
        {
            var answer = DoPuzzle(1000000000000L);
            return answer.ToString();
        }

        private long DoPuzzle(long blockIterations)
        {
            Dictionary<(string board, int blockIndex, int windIndex), (long blockNo, long height)> boardCache = new();
            HashSet<VectorXY> blocked = new();
            bool Move(ref IList<VectorXY> block, VectorXY move)
            {
                List<VectorXY> blockMoved = block.Select(tile => tile + move).ToList();
                bool hasMoved = blockMoved.All(tile => 0 <= tile.X && tile.X < wellWidth && 1 <= tile.Y && !blocked.Contains(tile));
                if (hasMoved)
                {
                    block = blockMoved;
                }
                return hasMoved;
            }
            int windIndex = 0;
            long blockNo = 0;
            while (blockNo < blockIterations)
            {
                int highest = blocked.Count > 0 ? blocked.Select(b => b.Y).Max() : 0;
                VectorXY spawn = new VectorXY(2, highest + 4);
                int blockIndex = (int)(blockNo % blocks.Length);
                IList<VectorXY> block = blocks[blockIndex];
                block = block.Select(tile => tile + spawn).ToList();
                while (true)
                {
                    // Wind phase
                    VectorXY wind = input[windIndex % input.Length] == '<' ? new VectorXY(-1, 0) : new VectorXY(+1, 0);
                    Move(ref block, wind);
                    //input[windIndex % input.Length].Dump();
                    windIndex = (windIndex + 1) % input.Length;
                    // Down phase
                    if (!Move(ref block, new VectorXY(0, -1)))
                    {
                        break;
                    }
                }
                foreach (var tile in block)
                {
                    blocked.Add(tile);
                }

                // Trim bottom rows
                long maxY = blocked.Select(b => b.Y).Max();
                const int heuristicBufferHeight = 100;
                blocked.RemoveWhere(b => b.Y < maxY - heuristicBufferHeight);
                // Check cache
                var height = blocked.Select(b => b.Y).Max();
                var boardSerialized = Visualize(blocked);
                if (boardCache.TryGetValue((boardSerialized, blockIndex, windIndex), out var previousData))
                {
                    //Console.WriteLine(boardSerialized);
                    //Console.WriteLine($"blockIndex {blockIndex}, windIndex {windIndex}, blockNo {blockNo}, previousData {previousData}");
                    var loopPeriod = blockNo - previousData.blockNo;
                    var loopHeight = height - previousData.height;
                    Debug.Assert(loopPeriod % blocks.Length == 0);
                    //var fullRepetitions = (BLOCK_ITERATIONS - previousData.blockNo) / loopPeriod;
                    //var remainder = (BLOCK_ITERATIONS - previousData.blockNo) % loopPeriod;
                    var (fullRepetitions, remainder) = Math.DivRem(blockIterations - previousData.blockNo, loopPeriod);
                    var remainderData = boardCache.First(kvp => kvp.Value.blockNo == previousData.blockNo + remainder - 1);
                    var totalHeight = fullRepetitions * loopHeight + remainderData.Value.height; // +previousData.height-previousData.height
                    return totalHeight;
                }
                else
                {
                    boardCache.Add((boardSerialized, blockIndex, windIndex), (blockNo, height));
                }
                blockNo++;
            }
            // Found out the hard way
            return blocked.Select(b => b.Y).Max();
        }

        private static string Visualize(HashSet<VectorXY> blocked, string label = "")
        {
            StringBuilder well = new();
            well.AppendLine(label);
            var minY = blocked.Select(b => b.Y).Min();
            var maxY = blocked.Select(b => b.Y).Max();
            for (var y = maxY; y >= minY; --y)
            {
                for (int x = 0; x < wellWidth; ++x)
                {
                    well.Append(blocked.Contains(new(x, y)) ? '#' : '.');
                }
                well.AppendLine();
            }
            string wellFormatted = string.Join("\n", well);
            return wellFormatted.ToString();
        }
    }
}
