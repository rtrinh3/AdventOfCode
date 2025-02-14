using AocCommon;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/11
// --- Day 11: Radioisotope Thermoelectric Generators ---
public class Day11 : IAocDay
{
    private record Floor(EquatableSet<string> Chips, EquatableSet<string> Generators)
    {
        public Floor Union(Floor other)
        {
            return new Floor(Chips.Union(other.Chips), Generators.Union(other.Generators));
        }
        public Floor Except(Floor other)
        {
            return new Floor(Chips.Except(other.Chips), Generators.Except(other.Generators));
        }
    }
    private record State(int ElevatorFloor, EquatableArray<Floor> Floors);

    private readonly State initialState;

    public Day11(string input)
    {
        var lines = Parsing.SplitLines(input);
        var initialLayout = lines.Select(line =>
        {
            var chips = Regex.Matches(line, @"(\w+)-compatible microchip").Select(m => m.Groups[1].Value);
            var generators = Regex.Matches(line, @"(\w+) generator").Select(m => m.Groups[1].Value);
            return new Floor(new(chips), new(generators));
        });
        initialState = new State(0, new(initialLayout));
    }

    private int DoPuzzle(State initialState)
    {
        var elements = initialState.Floors.SelectMany(f => f.Chips);
        var elementsFromGenerators = initialState.Floors.SelectMany(f => f.Generators);
        bool elementsAreEqual = elements.ToHashSet().SetEquals(elementsFromGenerators);
        Debug.Assert(elementsAreEqual);
        var finalFloors = new Floor[initialState.Floors.Count];
        Floor emptyFloor = new(new([]), new([]));
        for (int i = 0; i < finalFloors.Length - 1; i++)
        {
            finalFloors[i] = emptyFloor;
        }
        finalFloors[^1] = new(new(elements), new(elements));
        var finalState = new State(initialState.Floors.Count - 1, new(finalFloors));
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
        var takeOneChip = currentFloor.Chips.Select(c => new Floor(new([c]), new([])));
        var takeOneGen = currentFloor.Generators.Select(g => new Floor(new([]), new([g])));
        var takeOneThing = takeOneChip.Concat(takeOneGen);
        var takeOneOrTwoThings =
            from a in takeOneThing
            from b in takeOneThing
            select a.Union(b);
        foreach (var intoElevator in takeOneOrTwoThings)
        {
            if (!CheckFloor(intoElevator))
            {
                continue;
            }
            var floorLeftovers = currentFloor.Except(intoElevator);
            if (!CheckFloor(floorLeftovers))
            {
                continue;
            }
            foreach (var targetFloorNumber in targetFloors)
            {
                var newFloors = state.Floors.ToArray();
                var newTargetFloor = state.Floors[targetFloorNumber].Union(intoElevator);
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

    private static bool CheckFloor(Floor floor)
    {
        return floor.Chips.All(c => floor.Generators.Contains(c) || floor.Generators.Count == 0);
    }

    public string Part1()
    {
        var answer = DoPuzzle(initialState);
        return answer.ToString();
    }

    public string Part2()
    {
        var floors = initialState.Floors.ToArray();
        var floorZeroChips = floors[0].Chips.Add("elerium").Add("dilithium");
        var floorZeroGens = floors[0].Generators.Add("elerium").Add("dilithium");
        floors[0] = new(floorZeroChips, floorZeroGens);
        var part2InitialState = new State(0, new(floors));
        var answer = DoPuzzle(part2InitialState);
        return answer.ToString();
    }
}
