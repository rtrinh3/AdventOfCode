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
    public class Day11(string input)
    {
        public long Part1()
        {
            string[] space = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int height = space.Length;
            int width = space.Max(r => r.Length);
            var emptyRows = new HashSet<int>(Enumerable.Range(0, height));
            var emptyCols = new HashSet<int>(Enumerable.Range(0, width));
            var galaxies = new List<VectorRC>();
            for (int row = 0; row < height; row++) {
                var matches = Regex.EnumerateMatches(space[row], @"#");
                foreach (var m in matches) {
                    Debug.Assert(m.Length == 1);
                    var coord = new VectorRC(row, m.Index);
                    galaxies.Add(coord);
                    emptyRows.Remove(row);
                    emptyCols.Remove(m.Index);
                }
            }
            int[] rowMapping = Enumerable.Range(0, height).Select(r => r + emptyRows.Count(e => e < r)).ToArray();
            int[] colMapping = Enumerable.Range(0, width).Select(c => c + emptyCols.Count(e => e < c)).ToArray();

            long sum = 0;
            for (int i = 0; i < galaxies.Count; i++) {
                int irow = rowMapping[galaxies[i].Row];
                int icol = colMapping[galaxies[i].Col];
                for (int j = i + 1; j < galaxies.Count; j++) {
                    int jrow = rowMapping[galaxies[j].Row];
                    int jcol = colMapping[galaxies[j].Col];
                    sum += Math.Abs(jrow - irow) + Math.Abs(jcol - icol);
                }
            }

            return sum;
        }

        public long Part2()
        {
            string[] space = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int height = space.Length;
            int width = space.Max(r => r.Length);
            var emptyRows = new HashSet<int>(Enumerable.Range(0, height));
            var emptyCols = new HashSet<int>(Enumerable.Range(0, width));
            var galaxies = new List<VectorRC>();
            for (int row = 0; row < height; row++) {
                var matches = Regex.EnumerateMatches(space[row], @"#");
                foreach (var m in matches) {
                    Debug.Assert(m.Length == 1);
                    var coord = new VectorRC(row, m.Index);
                    galaxies.Add(coord);
                    emptyRows.Remove(row);
                    emptyCols.Remove(m.Index);
                }
            }
            int[] rowMapping = Enumerable.Range(0, height).Select(r => r + 999_999 * emptyRows.Count(e => e < r)).ToArray();
            int[] colMapping = Enumerable.Range(0, width).Select(c => c + 999_999 * emptyCols.Count(e => e < c)).ToArray();

            long sum = 0;
            for (int i = 0; i < galaxies.Count; i++) {
                int irow = rowMapping[galaxies[i].Row];
                int icol = colMapping[galaxies[i].Col];
                for (int j = i + 1; j < galaxies.Count; j++) {
                    int jrow = rowMapping[galaxies[j].Row];
                    int jcol = colMapping[galaxies[j].Col];
                    sum += Math.Abs(jrow - irow) + Math.Abs(jcol - icol);
                }
            }

            return sum;
        }
    }
}
