using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/20
    // --- Day 20: Jurassic Jigsaw ---
    public class Day20 : IAocDay
    {
        private const int TILE_WIDTH = 10;
        private readonly Tile[] tiles;
        private readonly int[][] connections;
        private readonly int[] corners;
        private readonly int arrangementSide;

        public Day20(string input)
        {
            List<Tile> tiles = new();
            string[] tileData = input.TrimEnd().ReplaceLineEndings("\n").Split("\n\n");
            foreach (var tile in tileData)
            {
                var split = tile.Split(":\n");
                int id = int.Parse(split[0].Split(' ')[1]);
                string[] data = split[1].Split('\n');
                tiles.Add(new Tile(id, data));
            }
            this.tiles = tiles.ToArray();

            short[][] allEdges = new short[tiles.Count][];
            for (int i = 0; i < tiles.Count; i++)
            {
                short[] edges = [.. tiles[i].Edges.Enumerate(), .. tiles[i].Transform(Transformation.Flip).Edges.Enumerate()];
                allEdges[i] = edges;
            }
            List<int>[] connections = new List<int>[tiles.Count];
            for (int i = 0; i < tiles.Count; i++)
            {
                List<int> connection = new();
                for (int j = 0; j < tiles.Count; j++)
                {
                    if (i != j && allEdges[i].Intersect(allEdges[j]).Any())
                    {
                        connection.Add(j);
                    }
                }
                connections[i] = connection;
            }
            this.connections = connections.Select(c => c.ToArray()).ToArray();

            List<int> corners = new();
            for (int i = 0; i < connections.Length; i++)
            {
                if (connections[i].Count == 2)
                {
                    corners.Add(i);
                }
            }
            this.corners = corners.ToArray();

            arrangementSide = (int)Math.Sqrt(tiles.Count);
            Debug.Assert(arrangementSide * arrangementSide == tiles.Count);
        }

        public string Part1()
        {
            Debug.Assert(corners.Length == 4);
            long product = corners.Select(i => tiles[i].ID).Aggregate(1L, (acc, id) => acc * id);
            return product.ToString();
        }

        public string Part2()
        {
            // Step 1: Put tiles in position
            var tileArrangement = FindArrangement();
            //// Visualization
            //Console.WriteLine("Tile indices");
            //for (int i = 0; i < arrangementSide; i++)
            //{
            //    for (int j = 0; j < arrangementSide; j++)
            //    {
            //        Console.Write(tileArrangement[i * arrangementSide + j]);
            //        Console.Write(' ');
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("Tile IDs");
            //for (int i = 0; i < arrangementSide; i++)
            //{
            //    for (int j = 0; j < arrangementSide; j++)
            //    {
            //        Console.Write(tiles[tileArrangement[i * arrangementSide + j]].ID);
            //        Console.Write(' ');
            //    }
            //    Console.WriteLine();
            //}

            // Step 2: Spin tiles to match neighbors
            var tileSpins = SpinTiles(tileArrangement);

            // Step 3: Assemble data
            List<List<char>> completeGrid = Enumerable.Range(0, arrangementSide * TILE_WIDTH).Select(x => new List<char>()).ToList();
            for (int i = 0; i < tileSpins.Length; i++)
            {
                int macroRow = i / arrangementSide;
                int macroCol = i % arrangementSide;
                for (int imageRow = 0; imageRow < TILE_WIDTH; imageRow++)
                {
                    completeGrid[macroRow * TILE_WIDTH + imageRow].AddRange(tileSpins[i].Image[imageRow]);
                }
            }
            //// Visualization
            //Console.WriteLine("With borders");
            //foreach (var row in completeGrid)
            //{
            //    foreach (var c in row)
            //    {
            //        Console.Write(c);
            //    }
            //    Console.WriteLine();
            //}

            // Step 4: Remove borders
            List<List<char>> withoutBorders = new();
            for (int row = 0; row < completeGrid.Count; row++)
            {
                int microRow = row % TILE_WIDTH;
                if (microRow == 0 || microRow == TILE_WIDTH - 1)
                {
                    continue;
                }
                List<char> newRow = new();
                for (int col = 0; col < completeGrid[row].Count; col++)
                {
                    int microCol = col % TILE_WIDTH;
                    if (microCol == 0 || microCol == TILE_WIDTH - 1)
                    {
                        continue;
                    }
                    newRow.Add(completeGrid[row][col]);
                }
                withoutBorders.Add(newRow);
            }
            //// Visualization
            //Console.WriteLine("Without borders");
            //foreach (var row in cleanedGrid)
            //{
            //    foreach (var c in row)
            //    {
            //        Console.Write(c);
            //    }
            //    Console.WriteLine();
            //}

            // Step 5: Find sea monsters
            string[] cleanedGrid = withoutBorders.Select(row => string.Join("", row)).ToArray();
            Grid seaMonsterGrid = new(@"                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ", ' ');
            VectorRC[] seaMonsterPoints = seaMonsterGrid.Iterate().Where(x => x.Value == '#').Select(x => x.Position).ToArray();
            int roughness = int.MaxValue;
            foreach (var t in Enum.GetValues<Transformation>())
            {
                var transformed = Transform(cleanedGrid, t);
                var grid = new Grid(transformed, '.');
                var waves = grid.Iterate().Where(x => x.Value == '#').Select(x => x.Position).ToHashSet();
                foreach (var position in grid.Iterate().Select(x => x.Position))
                {
                    var translatedMonster = seaMonsterPoints.Select(s => s + position).ToList();
                    if (translatedMonster.All(x => grid.Get(x) == '#'))
                    {
                        foreach (var x in translatedMonster)
                        {
                            waves.Remove(x);
                        }
                    }
                }
                if (waves.Count < roughness)
                {
                    roughness = waves.Count;
                }
            }

            return roughness.ToString();
        }

        private int[] FindArrangement()
        {
            Stack<int[]> stack = new();
            stack.Push([]);
            while (stack.TryPop(out var prefix))
            {
                if (prefix.Length == tiles.Length)
                {
                    return prefix;
                }
                HashSet<int> candidates = Enumerable.Range(0, tiles.Length).ToHashSet();
                candidates.ExceptWith(prefix);
                int currentIndex = prefix.Length;
                int row = currentIndex / arrangementSide;
                int col = currentIndex % arrangementSide;
                if ((row == 0 || row == arrangementSide - 1) && (col == 0 || col == arrangementSide - 1))
                {
                    candidates.IntersectWith(corners);
                }
                if (row > 0)
                {
                    int up = prefix[currentIndex - arrangementSide];
                    candidates.IntersectWith(connections[up]);
                }
                if (col > 0)
                {
                    int left = prefix[currentIndex - 1];
                    candidates.IntersectWith(connections[left]);
                }
                foreach (var candidate in candidates.Reverse())
                {
                    int[] newPrefix = [.. prefix, candidate];
                    stack.Push(newPrefix);
                }
            }
            throw new Exception("Empty stack!");
        }

        private Tile[] SpinTiles(int[] arrangement)
        {
            Stack<Tile[]> stack = new();
            stack.Push([]);
            while (stack.TryPop(out var prefix))
            {
                if (prefix.Length == tiles.Length)
                {
                    return prefix;
                }
                int currentIndex = prefix.Length;
                Tile currentTile = tiles[arrangement[currentIndex]];
                int row = currentIndex / arrangementSide;
                int col = currentIndex % arrangementSide;
                foreach (var t in Enum.GetValues<Transformation>())
                {
                    var transformed = currentTile.Transform(t);
                    bool ok = true;
                    if (ok && row > 0)
                    {
                        var up = prefix[currentIndex - arrangementSide];
                        ok &= transformed.Image[0] == up.Image[^1];
                    }
                    if (ok && col > 0)
                    {
                        var left = prefix[currentIndex - 1];
                        ok &= transformed.Image.Select(r => r[0]).SequenceEqual(left.Image.Select(r => r[^1]));
                    }
                    if (ok)
                    {
                        Tile[] newPrefix = [.. prefix, transformed];
                        stack.Push(newPrefix);
                    }
                }
            }
            throw new Exception("Empty stack!");
        }

        protected static string[] Transform(string[] image, Transformation t)
        {
            return t switch
            {
                Transformation.Original => image,
                Transformation.Spin1 => Spin1(image),
                Transformation.Spin2 => Spin1(Spin1(image)),
                Transformation.Spin3 => Spin1(Spin1(Spin1(image))),
                Transformation.Flip => Flip(image),
                Transformation.FlipSpin1 => Spin1(Flip(image)),
                Transformation.FlipSpin2 => Spin1(Spin1(Flip(image))),
                Transformation.FlipSpin3 => Spin1(Spin1(Spin1(Flip(image)))),
                _ => throw new ArgumentOutOfRangeException(nameof(Transformation)),
            };
        }

        protected static string[] Spin1(string[] image)
        {
            StringBuilder[] lines = new StringBuilder[image[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == null)
                {
                    lines[i] = new();
                }
                for (int j = 0; j < image.Length; j++)
                {
                    lines[i].Append(image[image.Length - 1 - j][i]);
                }
            }
            return lines.Select(l => l.ToString()).ToArray();
        }

        protected static string[] Flip(string[] image)
        {
            return image.Select(r => string.Join("", r.Reverse())).ToArray();
        }

        protected enum Transformation
        {
            Original,
            Spin1,
            Spin2,
            Spin3,
            Flip,
            FlipSpin1,
            FlipSpin2,
            FlipSpin3
        }

        protected class Tile
        {
            public int ID { get; init; }
            public string[] Image { get; init; }
            public Edges Edges { get; init; }

            public Tile(int id, string[] image)
            {
                ID = id;
                Image = image;
                short top = ConvertToEdge(image[0]);
                short right = ConvertToEdge(image.Select(line => line.Last()));
                short bottom = ConvertToEdge(image.Last().Reverse());
                short left = ConvertToEdge(image.Reverse().Select(line => line[0]));
                this.Edges = new(top, right, bottom, left);
            }

            private static short ConvertToEdge(IEnumerable<char> edgeChars)
            {
                short answer = 0;
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

            public Tile Transform(Transformation t)
            {
                var transformedData = Day20.Transform(Image, t);
                var result = new Tile(ID, transformedData);
                return result;
            }
        }

        protected readonly record struct Edges(short Top, short Right, short Bottom, short Left)
        {
            public IEnumerable<short> Enumerate() => [Top, Right, Bottom, Left];
        }
    }
}
