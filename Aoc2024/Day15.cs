using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2024;

// https://adventofcode.com/2024/day/15
// --- Day 15: Warehouse Woes ---
public class Day15(string input) : IAocDay
{
    public string Part1()
    {
        // Parse
        var paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");

        var inputMap = paragraphs[0].Split('\n');
        Dictionary<VectorRC, char> map = new();
        VectorRC robotPos = VectorRC.Zero;
        for (int row = 0; row < inputMap.Length; row++)
        {
            for (int col = 0; col < inputMap[row].Length; col++)
            {
                VectorRC pos = new VectorRC(row, col);
                char tile = inputMap[row][col];
                if (tile == '#' || tile == 'O')
                {
                    map[pos] = tile;
                }
                else if (tile == '@')
                {
                    robotPos = pos;
                }
            }
        }
        Debug.Assert(robotPos != VectorRC.Zero);

        char[] moves = paragraphs[1].Where(c => !char.IsWhiteSpace(c)).ToArray();

        // Execute moves
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

    private record class Obstacle(char Tile, VectorRC Left, VectorRC Right);

    public string Part2()
    {
        // Parse
        var paragraphs = input.ReplaceLineEndings("\n").Split("\n\n");

        var inputMap = paragraphs[0].Split('\n');
        Dictionary<VectorRC, Obstacle> map = new();
        VectorRC robotPos = VectorRC.Zero;
        for (int row = 0; row < inputMap.Length; row++)
        {
            for (int col = 0; col < inputMap[row].Length; col++)
            {
                VectorRC pos = new VectorRC(row, col * 2);
                char tile = inputMap[row][col];
                if (tile == '#' || tile == 'O')
                {
                    VectorRC right = pos + VectorRC.Right;
                    Obstacle obstacle = new(tile, pos, right);
                    map[pos] = obstacle;
                    map[right] = obstacle;
                }
                else if (tile == '@')
                {
                    robotPos = pos;
                }
            }
        }
        Debug.Assert(robotPos != VectorRC.Zero);

        char[] moves = paragraphs[1].Where(c => !char.IsWhiteSpace(c)).ToArray();

        Console.WriteLine("Initial state:");
        //VisualizePart2(robotPos, map);

        // Execute moves
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
            // Memoization doesn't improve performance here
            HashSet<Obstacle> GetThingsToPush(VectorRC pos)
            {
                if (map.TryGetValue(pos, out var obstacle))
                {
                    HashSet<Obstacle> results = [obstacle];
                    if (obstacle.Tile == 'O')
                    {
                        if (dir == VectorRC.Left)
                        {
                            results.UnionWith(GetThingsToPush(obstacle.Left + VectorRC.Left));
                        }
                        else if (dir == VectorRC.Right)
                        {
                            results.UnionWith(GetThingsToPush(obstacle.Right + VectorRC.Right));
                        }
                        else
                        {
                            results.UnionWith(GetThingsToPush(obstacle.Left + dir));
                            results.UnionWith(GetThingsToPush(obstacle.Right + dir));
                        }
                    }
                    return results;
                }
                else
                {
                    return new HashSet<Obstacle>();
                }
            }
            HashSet<Obstacle> thingsToPush = GetThingsToPush(robotPos + dir);
            if (thingsToPush.Count == 0)
            {
                robotPos += dir;
            }
            else if (thingsToPush.Any(obstacle => obstacle.Tile == '#'))
            {
                // No move
            }
            else
            {
                Debug.Assert(thingsToPush.All(obstacle => obstacle.Tile == 'O'));
                foreach (var obstacle in thingsToPush)
                {
                    map.Remove(obstacle.Left);
                    map.Remove(obstacle.Right);
                }
                foreach (var obstacle in thingsToPush)
                {
                    Obstacle newObstacle = new(obstacle.Tile, obstacle.Left + dir, obstacle.Right + dir);
                    map[newObstacle.Left] = newObstacle;
                    map[newObstacle.Right] = newObstacle;
                }
                robotPos += dir;
            }
            Console.WriteLine($"Move {move}:");
            //VisualizePart2(robotPos, map);
        }

        // Sum of coordinates
        HashSet<VectorRC> coordinates = new();
        foreach (var kvp in map)
        {
            if (kvp.Value.Tile == 'O')
            {
                Debug.Assert(kvp.Key == kvp.Value.Left || kvp.Key == kvp.Value.Right);
                coordinates.Add(kvp.Value.Left);
            }
        }
        var total = coordinates.Sum(c => 100L * c.Row + c.Col);
        return total.ToString();
    }

    private void VisualizePart2(VectorRC robot, Dictionary<VectorRC, Obstacle> map)
    {
        List<List<char>> grid = new();
        foreach (var kvp in map)
        {
            while (grid.Count <= kvp.Key.Row)
            {
                grid.Add(new List<char>());
            }
            var row = grid[kvp.Key.Row];
            while (row.Count <= kvp.Key.Col)
            {
                row.Add('.');
            }
            if (kvp.Value.Tile == 'O')
            {
                if (kvp.Key == kvp.Value.Left)
                {
                    row[kvp.Key.Col] = '[';
                }
                else if (kvp.Key == kvp.Value.Right)
                {
                    row[kvp.Key.Col] = ']';
                }
                else
                {
                    throw new Exception("What");
                }
            }
            else
            {
                row[kvp.Key.Col] = kvp.Value.Tile;
            }
        }
        grid[robot.Row][robot.Col] = '@';
        StringBuilder sb = new();
        foreach (var row in grid)
        {
            foreach (var c in row)
            {
                sb.Append(c);
            }
            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }
}
