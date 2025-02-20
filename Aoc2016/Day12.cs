﻿using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/12
// --- Day 12: Leonardo's Monorail ---
public class Day12(string input) : IAocDay
{
    public string Part1()
    {
        AssembunnyInterpreter interpreter = new(input);
        interpreter.Run();
        var answer = interpreter.Peek('a');
        return answer.ToString();
    }

    public string Part2()
    {
        AssembunnyInterpreter interpreter = new(input);
        interpreter.Poke('c', 1);
        interpreter.Run();
        var answer = interpreter.Peek('a');
        return answer.ToString();
    }
}
