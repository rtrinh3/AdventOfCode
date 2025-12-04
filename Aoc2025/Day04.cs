using AocCommon;

namespace Aoc2025;

// https://adventofcode.com/2025/day/4
// --- Day 4: Printing Department ---
public class Day04(string input) : IAocDay
{
    private readonly Grid initialGrid = new(input, '.');

    public string Part1()
    {
        int accumulator = 0;
        foreach (var cell in initialGrid.Iterate())
        {
            if (cell.Value == '@')
            {
                int adjacentRolls = cell.Position.NextEight().Count(p => initialGrid.Get(p) == '@');
                if (adjacentRolls < 4)
                {
                    accumulator++;
                }
            }
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        bool[,] grid = new bool[initialGrid.Height, initialGrid.Width];
        foreach (var cell in initialGrid.Iterate())
        {
            if (cell.Value == '@')
            {
                grid[cell.Position.Row, cell.Position.Col] = true;
            }
        }

        int accumulator = 0;
        bool removed = false;
        do
        {
            removed = false;
            for (int r = 0; r < initialGrid.Height; r++)
            {
                for (int c = 0; c < initialGrid.Width; c++)
                {
                    if (grid[r, c])
                    {
                        int adjacentRolls = 0;
                        foreach (var pos in new VectorRC(r, c).NextEight())
                        {
                            if (pos.Row >= 0 && pos.Row < initialGrid.Height && pos.Col >= 0 && pos.Col < initialGrid.Width)
                            {
                                if (grid[pos.Row, pos.Col])
                                {
                                    adjacentRolls++;
                                }
                            }
                        }
                        if (adjacentRolls < 4)
                        {
                            grid[r, c] = false;
                            removed = true;
                            accumulator++;
                        }
                    }
                }
            }
        }
        while (removed);
        return accumulator.ToString();
    }
}