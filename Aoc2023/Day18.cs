﻿using Aoc2023;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    // https://adventofcode.com/2023/day/18
    public class Day18(string input) : IAocDay
    {
        string[] lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        public long Part1()
        {
            // Parse
            List<(VectorRC start, VectorRC end)> edges = new();
            VectorRC position = VectorRC.Zero;
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                VectorRC direction = parts[0] switch
                {
                    "U" => VectorRC.Up,
                    "D" => VectorRC.Down,
                    "L" => VectorRC.Left,
                    "R" => VectorRC.Right,
                    _ => throw new Exception("What is this direction " + line)
                };
                int length = int.Parse(parts[1]);
                VectorRC newPosition = position + direction.Scale(length);
                edges.Add((position, newPosition));
                position = newPosition;
            }
            Debug.Assert(edges.First().start == edges.Last().end);
            // Pick's theorem ( https://en.wikipedia.org/wiki/Pick%27s_theorem ) states that:
            // Area = Interior + Boundary/2 - 1
            // We're looking for Interior + Boundary
            // We can calculate Area and Boundary, so the sum we're looking for is:
            // Interior + Boundary = Area + Boundary/2 + 1
            var boundary = edges.Sum(e => (e.end - e.start).ManhattanMetric());
            var area = PolygonArea(edges);
            decimal sum = area + (decimal)boundary / 2 + 1;
            return (long)sum;
        }

        public long Part2()
        {
            // Parse
            List<(VectorRC start, VectorRC end)> edges = new();
            VectorRC position = VectorRC.Zero;
            decimal boundary = 0;
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                string word = parts[2];
                VectorRC direction = word[7] switch
                {
                    '0' => VectorRC.Right,
                    '1' => VectorRC.Down,
                    '2' => VectorRC.Left,
                    '3' => VectorRC.Up,
                    _ => throw new Exception("What is this direction " + line)
                };
                int length = int.Parse(word[2..^2], System.Globalization.NumberStyles.HexNumber);
                Debug.Assert(length > 0);
                boundary += length;
                VectorRC newPosition = position + direction.Scale(length);
                edges.Add((position, newPosition));
                position = newPosition;
            }
            Debug.Assert(edges.First().start == edges.Last().end);
            // Pick's theorem ( https://en.wikipedia.org/wiki/Pick%27s_theorem ) states that:
            // Area = Interior + Boundary/2 - 1
            // We're looking for Interior + Boundary
            // We can calculate Area and Boundary, so the sum we're looking for is:
            // Interior + Boundary = Area + Boundary/2 + 1
            //var boundary = edges.Sum(e => (e.end - e.start).ManhattanMetric());
            var area = PolygonArea(edges);
            decimal sum = area + boundary / 2 + 1;
            return (long)sum;
        }

        private decimal PolygonArea(IEnumerable<(VectorRC start, VectorRC end)> edges)
        {
            // https://en.wikipedia.org/wiki/Shoelace_formula
            decimal doubleArea = 0;
            foreach (var edge in edges)
            {
                doubleArea += (decimal)edge.start.Row * edge.end.Col - (decimal)edge.start.Col * edge.end.Row;
            }
            return Math.Abs(doubleArea / 2);
        }
    }
}
