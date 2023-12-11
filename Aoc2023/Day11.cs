using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/11
    public class Day11
    {
        private readonly int height;
        private readonly int width;
        private readonly HashSet<int> emptyRows;
        private readonly HashSet<int> emptyCols;
        private readonly List<VectorRC> galaxies;

        public Day11(string input)
        {
            string[] space = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            height = space.Length;
            width = space.Max(r => r.Length);
            emptyRows = new HashSet<int>(Enumerable.Range(0, height));
            emptyCols = new HashSet<int>(Enumerable.Range(0, width));
            galaxies = new List<VectorRC>();
            for (int row = 0; row < height; row++)
            {
                var matches = Regex.EnumerateMatches(space[row], @"#");
                foreach (var m in matches)
                {
                    Debug.Assert(m.Length == 1);
                    var coord = new VectorRC(row, m.Index);
                    galaxies.Add(coord);
                    emptyRows.Remove(row);
                    emptyCols.Remove(m.Index);
                }
            }
        }

        public long Part1()
        {
            return DoPuzzle(2);
        }

        public long Part2()
        {
            return DoPuzzle(1_000_000);
        }

        public long DoPuzzle(int expansion)
        {
            int[] rowMapping = Enumerable.Range(0, height).Select(r => r + (expansion - 1) * emptyRows.Count(e => e < r)).ToArray();
            int[] colMapping = Enumerable.Range(0, width).Select(c => c + (expansion - 1) * emptyCols.Count(e => e < c)).ToArray();
            long sum = 0;
            for (int i = 0; i < galaxies.Count; i++)
            {
                int irow = rowMapping[galaxies[i].Row];
                int icol = colMapping[galaxies[i].Col];
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    int jrow = rowMapping[galaxies[j].Row];
                    int jcol = colMapping[galaxies[j].Col];
                    sum += Math.Abs(jrow - irow) + Math.Abs(jcol - icol);
                }
            }
            return sum;
        }
    }
}
