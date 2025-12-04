using AocCommon;

namespace Aoc2025;

// https://adventofcode.com/2025/day/4
// --- Day 4: Printing Department ---
public class Day04(string input) : IAocDay
{
    private readonly Grid grid = new(input, '.');

    public string Part1()
    {
        int accumulator = 0;
        foreach (var cell in grid.Iterate())
        {
            if (cell.Value == '@')
            {
                int adjacentRolls = cell.Position.NextEight().Count(p => grid.Get(p) == '@');
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
        throw new NotImplementedException();
    }
}