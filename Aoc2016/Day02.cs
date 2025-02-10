using AocCommon;
using System.Text;

namespace Aoc2016;

// https://adventofcode.com/2016/day/2
// --- Day 2: Bathroom Security ---
public class Day02(string input) : IAocDay
{
    private const char OUTSIDE = ' ';
    private readonly string[] lines = Parsing.SplitLines(input);

    public string Part1()
    {
        string[] keypad = [
            "123",
            "456",
            "789"
            ];
        return DoPuzzle(keypad);
    }

    public string Part2()
    {
        string[] keypad = [
            "  1",
            " 234",
            "56789",
            " ABC",
            "  D"
            ];
        return DoPuzzle(keypad);
    }

    private string DoPuzzle(string[] keypad)
    {
        Grid grid = new(keypad, OUTSIDE);
        VectorRC position = grid.Iterate().First(x => x.Value == '5').Position;
        StringBuilder answer = new();
        foreach (var line in lines)
        {
            foreach (var step in line)
            {
                VectorRC direction = step switch
                {
                    'U' => VectorRC.Up,
                    'D' => VectorRC.Down,
                    'L' => VectorRC.Left,
                    'R' => VectorRC.Right,
                    _ => throw new Exception("Unrecognized step " + step)
                };
                var next = position + direction;
                if (grid.Get(next) != OUTSIDE)
                {
                    position = next;
                }
            }
            answer.Append(grid.Get(position));
        }
        return answer.ToString();
    }
}
