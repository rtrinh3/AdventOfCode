using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2025;

// https://adventofcode.com/2025/day/10
// --- Day 10: Factory ---
public class Day10 : IAocDay
{
    private record Machine(uint Lights, uint[] Buttons, int[] Joltages);

    private readonly Machine[] Machines;

    public Day10(string input)
    {
        var lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');
        List<Machine> machines = new();
        foreach (var line in lines)
        {
            var lightsText = Regex.Match(line, @"\[([^\]]*)\]");
            uint lights = 0;
            for (int i = 0; i < lightsText.Groups[1].Length; i++)
            {
                if (lightsText.Groups[1].ValueSpan[i] == '#')
                {
                    lights |= (1u << i);
                }
            }
            var buttonMatches = Regex.EnumerateMatches(line, @"\([^\)]*\)");
            List<uint> buttons = new();
            foreach (var match in buttonMatches)
            {
                var buttonText = line.AsSpan(match.Index, match.Length);
                var numbers = Regex.EnumerateMatches(buttonText, @"\d+");
                uint button = 0;
                foreach (var number in numbers)
                {
                    int index = int.Parse(buttonText.Slice(number.Index, number.Length));
                    button |= (1u << index);
                }
                buttons.Add(button);
            }
            var joltagesMatch = Regex.Match(line, @"{([^}]*)}");
            var joltagesNumbersText = joltagesMatch.Groups[1].ValueSpan;
            var joltagesNumbers = Regex.EnumerateMatches(joltagesNumbersText, @"\d+");
            List<int> joltages = new();
            foreach (var number in joltagesNumbers)
            {
                int joltage = int.Parse(joltagesNumbersText.Slice(number.Index, number.Length));
                joltages.Add(joltage);
            }
            machines.Add(new(lights, buttons.ToArray(), joltages.ToArray()));
        }
        Machines = machines.ToArray();
    }

    public string Part1()
    {
        int accumulator = 0;
        foreach (var machine in Machines)
        {
            var bfsResult = GraphAlgos.BfsToEnd(0U,
                lights => machine.Buttons.Select(button => lights ^ button),
                lights => lights == machine.Lights);
            accumulator += bfsResult.distance;
        }
        return accumulator.ToString();
    }

    public string Part2()
    {
        var z3 = new Microsoft.Z3.Context();
        var z3zero = z3.MkInt(0);
        int accumulator = 0;
        foreach (var machine in Machines)
        {
            var optimizer = z3.MkOptimize();
            var buttonPresses = new Microsoft.Z3.IntExpr[machine.Buttons.Length];
            for (int buttonIndex = 0; buttonIndex < machine.Buttons.Length; buttonIndex++)
            {
                var button = z3.MkIntConst("b" + buttonIndex);
                buttonPresses[buttonIndex] = button;
                var positiveConstraint = z3.MkGe(button, z3zero);
                optimizer.Add(positiveConstraint);
            }
            for (int joltIndex = 0; joltIndex < machine.Joltages.Length; joltIndex++)
            {
                List<Microsoft.Z3.ArithExpr> joltContributions = new();
                for (int buttonIndex = 0; buttonIndex < machine.Buttons.Length; buttonIndex++)
                {
                    if ((machine.Buttons[buttonIndex] & (1u << joltIndex)) != 0)
                    {
                        joltContributions.Add(buttonPresses[buttonIndex]);
                    }
                }
                var targetJolt = z3.MkInt(machine.Joltages[joltIndex]);
                var joltConstraint = z3.MkEq(z3.MkAdd(joltContributions), targetJolt);
                optimizer.Add(joltConstraint);
            }
            var sumToMinimize = z3.MkAdd(buttonPresses);
            var optimizationResult = optimizer.MkMinimize(sumToMinimize);
            Debug.Assert(optimizer.Check() == Microsoft.Z3.Status.SATISFIABLE);
            if (optimizationResult.Value is Microsoft.Z3.IntNum answerInt)
            {
                var value = answerInt.Int;
                accumulator += value;
            }
            else
            {
                throw new Exception("What is this result " + optimizationResult.Value);
            }
        }
        return accumulator.ToString();
    }
}