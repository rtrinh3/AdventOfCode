using System.Collections.Immutable;

namespace AocCommon
{
    public class Grid
    {
        public ImmutableArray<string> Data { get; init; }
        public int Height { get => Data.Length; }
        public int Width { get; init; }

        private readonly char outsideChar;

        public Grid(string input, char outsideChar)
        {
            Data = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries).ToImmutableArray();
            this.outsideChar = outsideChar;
            Width = Data.Max(row => row.Length);
        }

        public Grid(string[] input, char outsideChar)
        {
            Data = input.ToImmutableArray();
            this.outsideChar = outsideChar;
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

        public IEnumerable<(VectorRC Position, char Value)> Iterate()
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    yield return (new VectorRC(row, col), Get(row, col));
                }
            }
        }
    }
}
