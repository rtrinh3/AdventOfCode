using AocCommon;
using System.Diagnostics;
using System.Numerics;

namespace Aoc2024
{
    // https://adventofcode.com/2024/day/12
    // --- Day 12: Garden Groups ---
    public class Day12(string input) : IAocDay
    {
        private const char OUTSIDE = '\0';
        private readonly Grid map = new(input, OUTSIDE);

        public string Part1()
        {
            // Partition the map and assign fences to plots
            UnionFind<VectorRC> regions = new();
            DefaultDict<VectorRC, int> plotFence = new(() => 0);
            void ProcessPlots(VectorRC a, VectorRC b)
            {
                if (map.Get(a) == map.Get(b))
                {
                    regions.Union(a, b);
                }
                else
                {
                    plotFence[a]++;
                    plotFence[b]++;
                }
            }
            // Scan one more plot to the left and top
            for (int row = -1; row < map.Height; row++)
            {
                for (int col = -1; col < map.Height; col++)
                {
                    // Check the right and bottom
                    VectorRC pos = new(row, col);
                    VectorRC right = new(row, col + 1);
                    ProcessPlots(pos, right);
                    VectorRC down = new(row + 1, col);
                    ProcessPlots(pos, down);
                }
            }

            // Calculate area and fence of regions
            DefaultDict<VectorRC, int> regionArea = new();
            DefaultDict<VectorRC, int> regionFence = new();
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Height; col++)
                {
                    VectorRC pos = new(row, col);
                    regionArea[regions.Find(pos)] += 1;
                    regionFence[regions.Find(pos)] += plotFence[pos];
                }
            }

            // Calculate prices
            long total = 0;
            Debug.Assert(regionArea.Count == regionFence.Count);
            foreach (var region in regionArea.Keys)
            {
                var type = map.Get(region); // for debugging
                var thisArea = regionArea[region];
                var thisFence = regionFence[region];
                var thisPrice = Math.BigMul(thisArea, thisFence);
                total += thisPrice;
            }

            return total.ToString();
        }

        public string Part2()
        {
            // Partition the map and assign fences to plots
            UnionFind<VectorRC> regions = new();
            DefaultDict<VectorRC, List<Fence>> plotFence = new();
            void ProcessPlots(VectorRC a, VectorRC b)
            {
                if (map.Get(a) == map.Get(b))
                {
                    regions.Union(a, b);
                }
                else
                {
                    plotFence[a].Add(new Fence(a, b));
                    plotFence[b].Add(new Fence(a, b));
                }
            }
            // Scan one more plot to the left and top
            for (int row = -1; row < map.Height; row++)
            {
                for (int col = -1; col < map.Height; col++)
                {
                    // Check the right and bottom
                    VectorRC pos = new(row, col);
                    VectorRC right = new(row, col + 1);
                    ProcessPlots(pos, right);
                    VectorRC down = new(row + 1, col);
                    ProcessPlots(pos, down);
                }
            }

            // Calculate area and fence of regions
            DefaultDict<VectorRC, int> regionArea = new();
            DefaultDict<VectorRC, List<Fence>> regionFence = new();
            for (int row = 0; row < map.Height; row++)
            {
                for (int col = 0; col < map.Height; col++)
                {
                    VectorRC pos = new(row, col);
                    regionArea[regions.Find(pos)] += 1;
                    regionFence[regions.Find(pos)].AddRange(plotFence[pos]);
                }
            }

            // Calculate prices
            long total = 0;
            Debug.Assert(regionArea.Count == regionFence.Count);
            foreach (var region in regionArea.Keys)
            {
                var type = map.Get(region); // for debugging
                var thisArea = regionArea[region];
                var thisFence = regionFence[region];
                var orientedFence = thisFence.Select(f => regions.AreMerged(region, f.A) ? f : new Fence(f.B, f.A)).ToList();
                // Find number of sides
                UnionFind<Fence> sides = new();
                for (int i = 0; i < orientedFence.Count; i++)
                {
                    Fence fenceI = orientedFence[i];
                    for (int j = i + 1; j < orientedFence.Count; j++)
                    {
                        // TODO something with thisFence[i] and thisFence[j]
                        // sides.Find(thisFence[i]);
                        // sides.Find(thisFence[j]);
                                            
                        Fence fenceJ = orientedFence[j];
                        if (fenceI.AreAdjacentAligned(fenceJ))
                        {
                            sides.Union(fenceI, fenceJ);
                        }
                    }
                }
                var sideCount = orientedFence.Select(sides.Find).Distinct().Count();
                var thisPrice = Math.BigMul(thisArea, sideCount);
                total += thisPrice;
            }

            return total.ToString();
        }

        // Fence is defined by the two plots separated by the fence.
        // Order matters. Let's say A is inside and B is outside.
        public class Fence : IEquatable<Fence>
        {
            public VectorRC A { get; init; }
            public VectorRC B { get; init; }

            internal Fence(VectorRC X, VectorRC Y)
            {
                if (X == Y)
                {
                    throw new Exception("Must be distinct!");
                }
                if (Math.Abs(X.Row - Y.Row) >= 2 || Math.Abs(X.Col - Y.Col) >= 2)
                {
                    throw new Exception("Unaligned!");
                }
                if (Math.Abs(X.Row - Y.Row) == 1 && Math.Abs(X.Col - Y.Col) == 1)
                {
                    throw new Exception("Unaligned!");
                }
                Debug.Assert(Math.Abs(X.Row - Y.Row) == 0 || Math.Abs(X.Col - Y.Col) == 0);
                if (Math.Abs(X.Row - Y.Row) == 0)
                {
                    Debug.Assert(Math.Abs(X.Col - Y.Col) == 1);
                    A = X;
                    B = Y;
                    // if (X.Col < Y.Col)
                    // {
                    //     A = X;
                    //     B = Y;
                    // }
                    // else
                    // {
                    //     A = Y;
                    //     B = X;
                    // }
                }
                else
                {
                    Debug.Assert(Math.Abs(X.Col - Y.Col) == 0);
                    Debug.Assert(Math.Abs(X.Row - Y.Row) == 1);
                    A = X;
                    B = Y;
                    // if (X.Row < Y.Row)
                    // {
                    //     A = X;
                    //     B = Y;
                    // }
                    // else
                    // {
                    //     A = Y;
                    //     B = X;
                    // }
                }
            }

            internal bool IsHorizontal => A.Col == B.Col;
            internal bool IsVertical => A.Row == B.Row;

            internal bool AreAdjacentAligned(Fence other)
            {
                if (this == other)
                {
                    return false;
                }
                if (this.IsHorizontal)
                {
                    if (!other.IsHorizontal)
                    {
                        return false;
                    }
                    return this.A.Row == other.A.Row && Math.Abs(this.A.Col - other.A.Col) == 1 && this.B.Row == other.B.Row;
                }
                else if (this.IsVertical)
                {
                    if (!other.IsVertical)
                    {
                        return false;
                    }
                    return this.A.Col == other.A.Col && Math.Abs(this.A.Row - other.A.Row) == 1 && this.B.Col == other.B.Col;
                }
                else
                {
                    throw new Exception("What orientation?");
                }
            }

            // IEquatable
            public bool Equals(Fence? other)
            {
                return ReferenceEquals(this, other) || (other is not null && this.A == other.A && this.B == other.B);
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Fence);
            }

            public override int GetHashCode()
            {
                var hasher = new HashCode();
                hasher.Add(A);
                hasher.Add(B);
                return hasher.ToHashCode();
            }

            public override string ToString()
            {
                return $"Fence({A}|{B})";
            }

            public static bool operator ==(Fence? lhs, Fence? rhs)
            {
                return lhs?.Equals(rhs) ?? (lhs is null && rhs is null);
            }
            public static bool operator !=(Fence? lhs, Fence? rhs)
            {
                return !(lhs == rhs);
            }
        }
    }
}
