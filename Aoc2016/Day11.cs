using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/11
// --- Day 11: Radioisotope Thermoelectric Generators ---
public class Day11 : IAocDay
{
    // Let's represent the contents of a floor as a bitmap
    // Position 0: alpha-compatible microchip
    // Position 1: alpha generator
    // Position 2: bravo-compatible microchip
    // Position 3: bravo generator
    // ...

    private record State(int ElevatorFloor, EquatableArray<uint> Floors);

    private (string[] Chips, string[] Generators)[] initialDescription;

    public Day11(string input)
    {
        var lines = Parsing.SplitLines(input);
        initialDescription = lines.Select(line =>
        {
            var chips = Regex.Matches(line, @"(\w+)-compatible microchip").Select(m => m.Groups[1].Value).ToArray();
            var generators = Regex.Matches(line, @"(\w+) generator").Select(m => m.Groups[1].Value).ToArray();
            return (chips, generators);
        }).ToArray();
    }

    private static int DoPuzzle((string[] Chips, string[] Generators)[] initialDescription)
    {
        var elements = initialDescription.SelectMany(f => f.Chips).ToHashSet();
        var elementsFromGenerators = initialDescription.SelectMany(f => f.Generators);
        Debug.Assert(elements.SetEquals(elementsFromGenerators));
        // initialState
        Dictionary<string, int> elementIndices = new();
        var elementsSorted = elements.Order().ToArray();
        for (int el = 0; el < elementsSorted.Length; el++)
        {
            elementIndices[elementsSorted[el]] = el;
        }
        uint[] initialFloors = new uint[initialDescription.Length];
        for (int f = 0; f < initialDescription.Length; f++)
        {
            uint floor = 0;
            foreach (var el in initialDescription[f].Chips)
            {
                floor |= 1u << (2 * elementIndices[el]);
            }
            foreach (var el in initialDescription[f].Generators)
            {
                floor |= 1u << (2 * elementIndices[el] + 1);
            }
            initialFloors[f] = floor;
        }
        var initialState = new State(0, new(initialFloors));
        // finalState
        var finalFloors = new uint[initialDescription.Length];
        for (int i = 0; i < finalFloors.Length - 1; i++)
        {
            finalFloors[i] = 0u;
        }
        uint allChipsAndGens = (1U << (2 * elementsSorted.Length)) - 1;
        finalFloors[^1] = allChipsAndGens; // others = 0        
        var finalState = new State(initialDescription.Length - 1, new(finalFloors));
        var traversalResult = GraphAlgos.BfsToEnd(initialState, GetMoves, state => state == finalState);
        return traversalResult.distance;
    }

    private static List<State> GetMoves(State state)
    {
        var result = new List<State>();
        List<int> targetFloors = new();
        if (state.ElevatorFloor > 0)
        {
            targetFloors.Add(state.ElevatorFloor - 1);
        }
        if (state.ElevatorFloor < state.Floors.Count - 1)
        {
            targetFloors.Add(state.ElevatorFloor + 1);
        }
        var currentFloor = state.Floors[state.ElevatorFloor];
        Debug.Assert(CheckFloor(currentFloor));
        // Represent what we take onto the elevator as a "Floor"
        List<uint> takeOneOrTwoThings = new();
        for (uint i = 1u; i <= currentFloor; i <<= 1)
        {
            if ((currentFloor & i) == 0)
            {
                continue;
            }
            for (uint j = 1u; j <= i; j <<= 1)
            {
                if ((currentFloor & j) == 0)
                {
                    continue;
                }
                uint combo = i | j;
                takeOneOrTwoThings.Add(combo);
            }
        }
        foreach (var intoElevator in takeOneOrTwoThings)
        {
            if (!CheckFloor(intoElevator))
            {
                continue;
            }
            var floorLeftovers = currentFloor - intoElevator;
            if (!CheckFloor(floorLeftovers))
            {
                continue;
            }
            foreach (var targetFloorNumber in targetFloors)
            {
                var newFloors = state.Floors.ToArray();
                var newTargetFloor = state.Floors[targetFloorNumber] | intoElevator;
                if (CheckFloor(newTargetFloor))
                {
                    newFloors[state.ElevatorFloor] = floorLeftovers;
                    newFloors[targetFloorNumber] = newTargetFloor;
                    State newState = new(targetFloorNumber, new(newFloors));
                    result.Add(newState);
                }
            }
        }
        return result;
    }

    private static bool CheckFloor(uint floor)
    {
        uint chips = floor & 0x55555555;
        uint gens = floor & 0xaaaaaaaa;
        return (gens == 0) || ((gens & (chips << 1)) == (chips << 1));
    }

    public string Part1()
    {
        var answer = DoPuzzle(initialDescription);
        return answer.ToString();
    }

    public string Part2()
    {
        var part2Description = initialDescription.ToArray();
        string[] floorZeroChips = [.. initialDescription[0].Chips, "elerium", "dilithium"];
        string[] floorZeroGens = [.. initialDescription[0].Generators, "elerium", "dilithium"];
        part2Description[0] = (floorZeroChips, floorZeroGens);
        var answer = DoPuzzle(part2Description);
        return answer.ToString();
    }
}
