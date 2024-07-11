using System.Diagnostics;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/20
    // --- Day 20: Jurassic Jigsaw ---
    public class Day20 : IAocDay
    {
        private readonly List<Tile> tiles;

        public Day20(string input)
        {
            tiles = new();
            string[] tileData = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            foreach (var tile in tileData)
            {
                var split = tile.Split(":\n");
                int id = int.Parse(split[0].Split(' ')[1]);
                string[] data = split[1].Split('\n');
                tiles.Add(new Tile(id, data));
            }

            for (int i = 0; i < tiles.Count; i++)
            {
                for (int j = i + 1; j < tiles.Count; j++)
                {
                    if (tiles[i].Edges.Intersect(tiles[j].AllEdges).Any())
                    {
                        tiles[i].Matches.Add(tiles[j].ID);
                        tiles[j].Matches.Add(tiles[i].ID);
                    }
                }
            }
        }

        public long Part1()
        {
            var corners = tiles.Where(t => t.Matches.Count == 2).ToList();
            Debug.Assert(corners.Count == 4);
            long product = (long)corners[0].ID * corners[1].ID * corners[2].ID * corners[3].ID;
            return product;
        }

        public long Part2()
        {
            throw new NotImplementedException();
        }

        private class Tile
        {
            public int ID { get; init; }
            public string[] Image { get; init; }
            public List<int> Matches { get; init; }
            public IEnumerable<ushort> Edges => [_edges.Top, _edges.Right, _edges.Bottom, _edges.Left];
            public IEnumerable<ushort> AllEdges => [_edges.Top, _edges.Right, _edges.Bottom, _edges.Left, FlipEdge(_edges.Top), FlipEdge(_edges.Left), FlipEdge(_edges.Bottom), FlipEdge(_edges.Right)];
            private readonly (ushort Top, ushort Right, ushort Bottom, ushort Left) _edges;

            public Tile(int id, string[] image)
            {
                ID = id;
                Image = image;
                Matches = new();
                ushort top = ConvertToEdge(image[0]);
                ushort right = ConvertToEdge(image.Select(line => line.Last()));
                ushort bottom = FlipEdge(ConvertToEdge(image.Last()));
                ushort left = ConvertToEdge(image.Reverse().Select(line => line[0]));
                _edges = (top, right, bottom, left);
            }

            public static ushort FlipEdge(ushort edge)
            {
                ushort answer = 0;
                for (int i = 0; i < 10; i++)
                {
                    if ((edge & (1U << i)) != 0)
                    {
                        answer |= (ushort)(1U << (10 - 1 - i));
                    }
                }
                return answer;
            }

            private static ushort ConvertToEdge(IEnumerable<char> edgeChars)
            {
                ushort answer = 0;
                int i = 0;
                foreach (char c in edgeChars)
                {
                    answer <<= 1;
                    if (c == '#')
                    {
                        answer |= 1;
                    }
                    i++;
                }
                return answer;
            }
        }
    }
}
