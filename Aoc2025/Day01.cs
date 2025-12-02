using System.Diagnostics;

namespace Aoc2025;

// https://adventofcode.com/2025/day/1
// --- Day 1: Secret Entrance ---
public class Day01(string input) : AocCommon.IAocDay
{
    private const int DIAL_SIZE = 100;
    private readonly string[] lines = AocCommon.Parsing.SplitLines(input);

    public string Part1()
    {
        int dial = 50;
        int password = 0;
        foreach (string line in lines)
        {
            char direction = line[0];
            int amount = int.Parse(line.AsSpan(1));
            int reduced = amount % DIAL_SIZE;
            if (direction == 'R')
            {
                dial = (dial + reduced) % DIAL_SIZE;
            }
            else if (direction == 'L')
            {
                dial = (dial - reduced + DIAL_SIZE) % DIAL_SIZE;
            }
            else
            {
                throw new InvalidOperationException($"Invalid direction: {line}");
            }
            if (dial == 0)
            {
                password++;
            }
        }
        return password.ToString();
    }

    public string Part2()
    {
        int dial = 50;
        int password = 0;
        foreach (string line in lines)
        {
            char direction = line[0];
            int amount = int.Parse(line.AsSpan(1));
            Debug.Assert(amount >= 0);
            if (direction == 'R')
            {
                int fullTurns = amount / DIAL_SIZE;
                int reduced = amount % DIAL_SIZE;
                bool reducedCrossZero = dial + reduced >= DIAL_SIZE;
                int zeroes = fullTurns + (reducedCrossZero ? 1 : 0);
                password += zeroes;
                dial = (dial + reduced) % DIAL_SIZE;
            }
            else if (direction == 'L')
            {
                int fullTurns = amount / DIAL_SIZE;
                int reduced = amount % DIAL_SIZE;
                bool reducedCrossZero = (dial != 0) && (dial <= reduced);
                int zeroes = fullTurns + (reducedCrossZero ? 1 : 0);
                password += zeroes;
                dial = (dial - reduced + DIAL_SIZE) % DIAL_SIZE;
            }
            else
            {
                throw new InvalidOperationException($"Invalid direction: {line}");
            }
        }
        return password.ToString();
    }
}
