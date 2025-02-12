using AocCommon;
using System.Text;

namespace Aoc2016;

// https://adventofcode.com/2016/day/8
// --- Day 8: Two-Factor Authentication ---
public class Day08(string input) : IAocDay
{
    public string Part1()
    {
        var answer = DoPart1(50, 6);
        return answer.ToString();
    }

    public int DoPart1(int width, int height)
    {
        var screen = RunProgram(width, height);
        int answer = 0;
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                if (screen[col, row])
                {
                    answer++;
                }
            }
        }
        return answer;
    }

    public bool[,] RunProgram(int width, int height)
    {
        bool[,] screen = new bool[width, height];
        bool[] rowBuffer = new bool[width];
        bool[] colBuffer = new bool[height];
        var instructions = Parsing.SplitLines(input);
        foreach (var instruction in instructions)
        {
            var args = Parsing.IntsPositive(instruction);
            if (instruction.StartsWith("rect "))
            {
                for (int col = 0; col < args[0]; col++)
                {
                    for (int row = 0; row < args[1]; row++)
                    {
                        screen[col, row] = true;
                    }
                }
            }
            else if (instruction.StartsWith("rotate row "))
            {
                var row = args[0];
                var shift = args[1];
                for (int col = 0; col < width; col++)
                {
                    rowBuffer[col] = screen[col, row];
                }
                for (int col = 0; col < width; col++)
                {
                    screen[col, row] = rowBuffer[(col + width - shift) % width];
                }
            }
            else if (instruction.StartsWith("rotate column "))
            {
                var col = args[0];
                var shift = args[1];
                for (int row = 0; row < height; row++)
                {
                    colBuffer[row] = screen[col, row];
                }
                for (int row = 0; row < height; row++)
                {
                    screen[col, row] = colBuffer[(row + height - shift) % height];
                }
            }
            else
            {
                throw new Exception("Parse error: " + instruction);
            }
            //// Visualization
            //Console.WriteLine(instruction);
            //Console.WriteLine(FormatScreen(screen));
        }
        return screen;
    }

    private static string FormatScreen(bool[,] screen)
    {
        var width = screen.GetLength(0);
        var height = screen.GetLength(1);
        var sb = new StringBuilder();
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                sb.Append(screen[col, row] ? '█' : '.');
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public string Part2()
    {
        var screen = RunProgram(50, 6);
        var display = FormatScreen(screen);
        return display;
    }
}
