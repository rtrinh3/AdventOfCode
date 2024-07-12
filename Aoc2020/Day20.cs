using AocCommon;
using System.Diagnostics;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
                    if (tiles[i].Edges.Enumerate().Any(e => tiles[j].Edges.EnumerateAll().Contains(e)))
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
            long product = corners.Select(c => c.ID).Aggregate(1L, (acc, id) => acc * id);
            return product;
        }

        public long Part2()
        {
            // Step 1: Assemble the puzzle
            DefaultDict<short, List<Tile>> edgeToTileMap = new();
            foreach (var t in tiles)
            {
                foreach (var e in t.Edges.EnumerateAll())
                {
                    edgeToTileMap[e].Add(t);
                }
            }

            var corners = tiles.Where(t => t.Matches.Count == 2).ToList();
            Tile cornerStone = corners.First();
            Transformation cornerOrientation = Transformation.Original;
            foreach (var t in Enum.GetValues<Transformation>())
            {
                var e = cornerStone.Edges.Transform(t);
                if (edgeToTileMap[e.Top].Count == 1 && edgeToTileMap[e.Left].Count == 1)
                {
                    cornerOrientation = t;
                    break;
                }
            }

            int sideLength = (int)Math.Sqrt(tiles.Count);
            Debug.Assert(sideLength * sideLength == tiles.Count);
            Tile[,] tileLayout = new Tile[sideLength, sideLength];
            Transformation[,] tileOrientations = new Transformation[sideLength, sideLength];
            tileLayout[0, 0] = cornerStone;
            tileOrientations[0, 0] = cornerOrientation;

            for (int row = 0; row < sideLength; row++)
            {
                for (int col = 0; col < sideLength; col++)
                {
                    if (row == 0 && col == 0)
                    {
                        continue;
                    }
                    short topEdgeToMatch = (row > 0) ? Edges.FlipEdge(tileLayout[row - 1, col].Edges.Transform(tileOrientations[row - 1, col]).Bottom) : (short)-1;
                    Tile? expectedTileFromTop = (topEdgeToMatch >= 0) ? edgeToTileMap[topEdgeToMatch].Where(t => t != tileLayout[row - 1, col]).Single() : null;
                    short leftEdgeToMatch = (col > 0) ? Edges.FlipEdge(tileLayout[row, col - 1].Edges.Transform(tileOrientations[row, col - 1]).Right) : (short)-1;
                    Tile? expectedTileFromLeft = (leftEdgeToMatch >= 0) ? edgeToTileMap[leftEdgeToMatch].Where(t => t != tileLayout[row, col - 1]).Single() : null;
                    Debug.Assert(expectedTileFromTop == null || expectedTileFromLeft == null || expectedTileFromTop == expectedTileFromLeft);
                    Debug.Assert(expectedTileFromTop == null || tileLayout[row - 1, col].Matches.Contains(expectedTileFromTop.ID));
                    Debug.Assert(expectedTileFromLeft == null || tileLayout[row, col - 1].Matches.Contains(expectedTileFromLeft.ID));
                    Tile expectedTile = expectedTileFromTop ?? expectedTileFromLeft!;
                    tileLayout[row, col] = expectedTile;
                    bool orientationFound = false;
                    foreach (var t in Enum.GetValues<Transformation>())
                    {
                        var e = expectedTile.Edges.Transform(t);
                        if ((leftEdgeToMatch < 0 || leftEdgeToMatch == e.Left) && (topEdgeToMatch < 0 || topEdgeToMatch == e.Top))
                        {
                            tileOrientations[row, col] = t;
                            orientationFound = true;
                            break;
                        }
                    }
                    Debug.Assert(orientationFound);
                }
            }

            Debug.Assert(corners.Contains(tileLayout[0, sideLength - 1]));
            Debug.Assert(corners.Contains(tileLayout[sideLength - 1, 0]));
            Debug.Assert(corners.Contains(tileLayout[sideLength - 1, sideLength - 1]));

            // Step 2: Stitch the tiles into the image
            int rowsInTile = tileLayout[0, 0].Image.Length;
            StringBuilder[] imageBuilder = Enumerable.Range(0, sideLength * (rowsInTile - 2)).Select(_ => new StringBuilder()).ToArray();
            for (int macroRow = 0; macroRow < sideLength; macroRow++)
            {
                for (int macroCol = 0; macroCol < sideLength; macroCol++)
                {
                    var tileImageWithBorder = tileLayout[macroRow, macroCol].Image;
                    var tileImage = tileImageWithBorder[1..^1].Select(row => row[1..^1]).ToArray();
                    var tileImageTransformed = Transform(tileImage, tileOrientations[macroRow, macroCol]);
                    for (int tileRow = 0; tileRow < tileImageTransformed.Length; tileRow++)
                    {
                        int imageRow = macroRow * (rowsInTile - 2) + tileRow;
                        imageBuilder[imageRow].Append(tileImageTransformed[tileRow]);
                    }
                }
            }
            string[] image = imageBuilder.Select(x => x.ToString()).ToArray();
            var imageLengths = image.Select(row => row.Length).ToArray();
            Debug.Assert(imageLengths.All(l => l == image.Length));

            // WIP The image doesn't seem right

            // Step 3: Find the monsters
            const string MonsterPattern = "                  # \n#    ##    ##    ###\n #  #  #  #  #  #   ";
            Grid MonsterPatternGrid = new(MonsterPattern, ' ');
            VectorRC[] MonsterPatternPoints = MonsterPatternGrid.Iterate().Where(x => x.Value == '#').Select(x => x.Position).ToArray();
            Grid maxMonsterGrid = new(image, '.');
            List<VectorRC> maxMonsterPositions = new();
            foreach (var t in Enum.GetValues<Transformation>())
            {
                List<VectorRC> monsters = new();
                Grid transformedGrid = new(Transform(image, t), '.');
                for (int row = 0; row < transformedGrid.Height; row++)
                {
                    for (int col = 0; col < transformedGrid.Width; col++)
                    {
                        VectorRC pos = new(row, col);
                        var monsterTest = MonsterPatternPoints.Select(x => x + pos);
                        if (monsterTest.All(p => transformedGrid.Get(p) == '#'))
                        {
                            monsters.Add(pos);
                        }
                    }
                }
                if (monsters.Count > maxMonsterPositions.Count)
                {
                    maxMonsterPositions = monsters;
                    maxMonsterGrid = transformedGrid;
                }
            }

            // Step 4: Determine roughness
            var monsterPixels = maxMonsterPositions.SelectMany(x => MonsterPatternPoints.Select(p => x + p)).ToHashSet();
            int countWaves = maxMonsterGrid.Iterate().Where(x => x.Value == '#' && !monsterPixels.Contains(x.Position)).Count();
            return countWaves;
        }

        protected static string[] Transform(string[] image, Transformation t)
        {
            return t switch
            {
                Transformation.Original => image,
                Transformation.Spin1 => Spin1(image),
                Transformation.Spin2 => Spin2(image),
                Transformation.Spin3 => Spin1(Spin2(image)),
                Transformation.Flip => Flip(image),
                Transformation.FlipSpin1 => Spin1(Flip(image)),
                Transformation.FlipSpin2 => Spin2(Flip(image)),
                Transformation.FlipSpin3 => Spin1(Spin2(Flip(image))),
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

        protected static string[] Spin2(string[] image)
        {
            return image.Reverse().Select(r => string.Join("", r.Reverse())).ToArray();
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
            public List<int> Matches { get; init; }
            public Edges Edges { get; init; }

            public Tile(int id, string[] image)
            {
                ID = id;
                Image = image;
                Matches = new();
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
        }

        protected readonly record struct Edges(short Top, short Right, short Bottom, short Left)
        {
            public static short FlipEdge(short edge)
            {
                short answer = 0;
                for (int i = 0; i < 10; i++)
                {
                    if ((edge & (1 << i)) != 0)
                    {
                        answer |= (short)(1 << (10 - 1 - i));
                    }
                }
                return answer;
            }

            public Edges Transform(Transformation t)
            {
                return t switch
                {
                    Transformation.Original => this,
                    Transformation.Spin1 => new(Right, Bottom, Left, Top),
                    Transformation.Spin2 => new(Bottom, Left, Top, Right),
                    Transformation.Spin3 => new(Left, Top, Right, Bottom),
                    Transformation.Flip => new(FlipEdge(Top), FlipEdge(Left), FlipEdge(Bottom), FlipEdge(Right)),
                    Transformation.FlipSpin1 => new(FlipEdge(Left), FlipEdge(Bottom), FlipEdge(Right), FlipEdge(Top)),
                    Transformation.FlipSpin2 => new(FlipEdge(Bottom), FlipEdge(Right), FlipEdge(Top), FlipEdge(Left)),
                    Transformation.FlipSpin3 => new(FlipEdge(Right), FlipEdge(Top), FlipEdge(Left), FlipEdge(Bottom)),
                    _ => throw new ArgumentOutOfRangeException(nameof(Transformation))
                };
            }

            public IEnumerable<short> Enumerate() => [Top, Right, Bottom, Left];
            public IEnumerable<short> EnumerateAll() => [Top, Right, Bottom, Left, FlipEdge(Top), FlipEdge(Left), FlipEdge(Bottom), FlipEdge(Right)];
        }
    }
}
