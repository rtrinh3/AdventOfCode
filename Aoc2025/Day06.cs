using System.Diagnostics;
using System.Numerics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/6
// --- Day 6: Trash Compactor ---
public class Day06(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        // Parse
        var lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        var cells = lines.Select(l => l.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)).ToArray();
        Debug.Assert(cells.All(row => row.Length == cells[0].Length));

        // Homework
        BigInteger accumulator = BigInteger.Zero;
        for (int col = 0; col < cells[0].Length; col++)
        {
            var numbers = cells[0..^1].Select(row => BigInteger.Parse(row[col]));
            BigInteger columnAnswer = cells[^1][col] switch
            {
                "+" => numbers.Aggregate((a, b) => a + b),
                "*" => numbers.Aggregate((a, b) => a * b),
                _ => throw new Exception("Unrecognized operation " + cells[^1][col])
            };
            accumulator += columnAnswer;
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}