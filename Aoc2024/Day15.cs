using AocCommon;
using System.Diagnostics;

namespace Aoc2024;

// https://adventofcode.com/2024/day/15
// --- Day 15: Warehouse Woes ---
public class Day15 : IAocDay
{
    private readonly Dictionary<VectorRC, char> initialMap;
    private readonly VectorRC initialRobotPosition;
    private readonly string moves;

    public Day15(string input)
    {
        var paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");

        var inputMap = paragraphs[0].Split('\n');
        initialMap = new();
        for (int row = 0; row < inputMap.Length; row++)
        {
            for (int col = 0; col < inputMap[row].Length; col++)
            {
                VectorRC pos = new VectorRC(row, col);
                char tile = inputMap[row][col];
                if (tile == '#' || tile == 'O')
                {
                    initialMap[pos] = tile;
                }
                else if (tile == '@')
                {
                    initialRobotPosition = pos;
                }
            }
        }

        string inputMoves = paragraphs[1];
        moves = string.Join("", inputMoves.Where(c => !char.IsWhiteSpace(c)));
    }

    public string Part1()
    {
        // Execute moves
        Dictionary<VectorRC, char> map = new(initialMap);
        VectorRC robotPos = initialRobotPosition;
        foreach (char move in moves)
        {
            VectorRC dir = move switch
            {
                '>' => VectorRC.Right,
                'v' => VectorRC.Down,
                '<' => VectorRC.Left,
                '^' => VectorRC.Up,
                _ => throw new Exception("Unrecognized direction")
            };
            List<char> thingsToPush = new();
            VectorRC next = robotPos + dir;
            while (true)
            {
                if (map.TryGetValue(next, out var tile))
                {
                    thingsToPush.Add(tile);
                    if (tile == '#')
                    {
                        break;
                    }
                    else
                    {
                        next += dir;
                    }
                }
                else
                {
                    break;
                }
            }
            if (thingsToPush.Count == 0)
            {
                robotPos += dir;
            }
            else if (thingsToPush.Last() == 'O')
            {
                Debug.Assert(thingsToPush.All(c => c == 'O'));
                for (int i = 0; i < thingsToPush.Count; i++)
                {
                    map.Remove(robotPos + dir.Scale(1 + i));
                }
                for (int i = 0; i < thingsToPush.Count; i++)
                {
                    map[robotPos + dir.Scale(2 + i)] = thingsToPush[i];
                }
                robotPos += dir;
            }
            else
            {
                // No move
            }
        }
        // Sum of coordinates
        long total = 0;
        foreach (var (Position, Tile) in map)
        {
            if (Tile == 'O')
            {
                var coordinate = 100 * Position.Row + Position.Col;
                total += coordinate;
            }
        }
        return total.ToString();
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}
