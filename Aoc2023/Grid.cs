using System.Collections.Immutable;

namespace Aoc2023
{
    internal class Grid
    {
        public readonly ImmutableArray<string> Data;
        private readonly char outsideChar;
        public readonly int Height;
        public readonly int Width;

        public Grid(string input, char outsideChar)
        {
            Data = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries).ToImmutableArray();
            this.outsideChar = outsideChar;
            Height = Data.Length;
            Width = Data.Max(row => row.Length);
        }

        public char Get(int row, int col)
        {
            if (row < 0 || row >= Data.Length || col < 0 || col >= Data[row].Length)
            {
                return outsideChar;
            }
            return Data[row][col];
        }

        public char Get(VectorRC coord)
        {
            return Get(coord.Row, coord.Col);
        }
    }
}
