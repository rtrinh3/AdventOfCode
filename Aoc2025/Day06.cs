using System.Diagnostics;

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
        long accumulator = 0L;
        for (int col = 0; col < cells[0].Length; col++)
        {
            var numbers = cells[0..^1].Select(row => long.Parse(row[col]));
            string op = cells[^1][col];
            long columnAnswer = op switch
            {
                "+" => numbers.Aggregate((a, b) => a + b),
                "*" => numbers.Aggregate((a, b) => a * b),
                _ => throw new Exception("Unrecognized operation " + op)
            };
            accumulator += columnAnswer;
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        // Parse
        var originalLines = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int originalWidth = originalLines.Max(line => line.Length);
        string[] rotatedLines = Enumerable.Range(0, originalWidth)
            .Select(index =>
                string.Join("", originalLines.Select(line => line[originalWidth - index - 1]))
            ).ToArray();
        var paragraphs = SplitOnBlank(rotatedLines).ToArray();

        // Homework
        long accumulator = 0L;
        foreach (var p in paragraphs)
        {
            char op = '\0';
            string[] paragraphCopy = p.ToArray();
            for (int i = 0; i < paragraphCopy.Length; i++)
            {
                char last = paragraphCopy[i][^1];
                if (!char.IsDigit(last) && !char.IsWhiteSpace(last))
                {
                    Debug.Assert(op == '\0');
                    op = last;
                    paragraphCopy[i] = paragraphCopy[i][..^1];
                }
            }
            var numbers = paragraphCopy.Select(long.Parse);
            long columnAnswer = op switch
            {
                '+' => numbers.Aggregate((a, b) => a + b),
                '*' => numbers.Aggregate((a, b) => a * b),
                _ => throw new Exception("Unrecognized operation " + op)
            };
            accumulator += columnAnswer;
        }
        return accumulator.ToString();
    }

    private static IEnumerable<string[]> SplitOnBlank(IEnumerable<string> stream)
    {
        List<string> buffer = new();
        foreach (var x in stream)
        {
            if (string.IsNullOrWhiteSpace(x))
            {
                yield return buffer.ToArray();
                buffer.Clear();
            }
            else
            {
                buffer.Add(x);
            }
        }
        yield return buffer.ToArray();
    }
}