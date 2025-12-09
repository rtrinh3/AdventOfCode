using AocCommon;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/9
// --- Day 9: Movie Theater ---
public class Day09 : IAocDay
{
    private readonly VectorRC[] Points;

    public Day09(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        List<VectorRC> points = new();
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var point = new VectorRC(int.Parse(parts[0]), int.Parse(parts[1]));
            points.Add(point);
        }
        this.Points = points.ToArray();
    }

    public string Part1()
    {
        long maxRectangle = 0;
        for (int a = 0; a < Points.Length; a++)
        {
            for (int b = a + 1; b < Points.Length; b++)
            {
                int height = Math.Abs(Points[b].Row - Points[a].Row) + 1;
                int width = Math.Abs(Points[b].Col - Points[a].Col) + 1;
                long area = Math.BigMul(height, width);
                if (area > maxRectangle)
                {
                    maxRectangle = area;
                }
            }
        }
        return maxRectangle.ToString();
    }

    public string Part2()
    {
        // Rasterize polygon
        Console.WriteLine("Rasterization");
        HashSet<VectorRC> perimeter = new();
        HashSet<VectorRC> leftPoints = new();
        HashSet<VectorRC> rightPoints = new();
        for (int x = 0; x < Points.Length; x++)
        {
            var pointA = Points[x];
            var pointB = Points[(x + 1) % Points.Length];
            var diff = pointB - pointA;
            var diffLen = diff.ManhattanMetric();
            var diffDir = new VectorRC(Math.Sign(diff.Row), Math.Sign(diff.Col));
            Debug.Assert(diffDir.Row == 0 || diffDir.Col == 0);
            for (int i = 0; i <= diffLen; i++)
            {
                VectorRC pos = pointA + diffDir.Scale(i);
                perimeter.Add(pos);
                leftPoints.Add(pos + diffDir.RotatedLeft());
                rightPoints.Add(pos + diffDir.RotatedRight());
            }
        }
        var leftMost = Points.MinBy(p => p.Col);
        var leftProbe = leftMost.NextLeft();
        HashSet<VectorRC> inside;
        HashSet<VectorRC> outside;
        if (leftPoints.Contains(leftProbe))
        {
            outside = new(leftPoints.Except(perimeter));
            inside = new(rightPoints.Except(perimeter));
        }
        else if (rightPoints.Contains(leftProbe))
        {
            outside = new(rightPoints.Except(perimeter));
            inside = new(leftPoints.Except(perimeter));
        }
        else
        {
            throw new Exception("??");
        }

        //// Debug
        //int minRow = perimeter.Min(x => x.Row);
        //int maxRow = perimeter.Max(x => x.Row);
        //int minCol = perimeter.Min(x => x.Col);
        //int maxCol = perimeter.Max(x => x.Col);
        //for (int row = minRow - 1; row <= maxRow + 1; row++)
        //{
        //    for (int col = minCol - 1; col <= maxCol + 1; col++)
        //    {
        //        if (inside.Contains(new(row, col)))
        //        {
        //            Console.Write('I');
        //        }
        //        else if (outside.Contains(new(row, col)))
        //        {
        //            Console.Write('O');
        //        }
        //        else if (perimeter.Contains(new(row, col)))
        //        {
        //            Console.Write('P');
        //        }
        //        else
        //        {
        //            Console.Write(' ');
        //        }
        //    }
        //    Console.WriteLine();
        //}

        long maxRectangle = 0;
        int a = 0;
        int b = 0;
        var report = Task.Run(() =>
        {
            while (a < Points.Length)
            {
                Console.WriteLine($"Testing {a}-{b} / {Points.Length} -- Max {maxRectangle}");
                Thread.Sleep(1000);
            }
        });
        for (a = 0; a < Points.Length; a++)
        {
            for (b = a + 1; b < Points.Length; b++)
            {                
                var cornerA = Points[a];
                var cornerB = Points[b];
                // Test border doesn't leave polygon
                var top = Math.Min(cornerA.Row, cornerB.Row);
                var bottom = Math.Max(cornerA.Row, cornerB.Row);
                var left = Math.Min(cornerA.Col, cornerB.Col);
                var right = Math.Max(cornerA.Col, cornerB.Col);
                bool inPolygon = true;
                for (int row = top; row <= bottom && inPolygon; row++)
                {
                    inPolygon = !outside.Contains(new(row, left)) && !outside.Contains(new(row, right));
                }
                for (int col = left; col <= right && inPolygon; col++)
                {
                    inPolygon = !outside.Contains(new(top, col)) && !outside.Contains(new(bottom, col));
                }

                if (inPolygon)
                {
                    int height = Math.Abs(Points[b].Row - Points[a].Row) + 1;
                    int width = Math.Abs(Points[b].Col - Points[a].Col) + 1;
                    long area = Math.BigMul(height, width);
                    if (area > maxRectangle)
                    {
                        maxRectangle = area;
                    }
                }
            }
        }
        return maxRectangle.ToString();
    }
}