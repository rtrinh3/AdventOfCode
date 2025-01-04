using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/18
    // --- Day 18: Many-Worlds Interpretation ---
    public class Day18(string input) : IAocDay
    {
        private readonly Grid originalMaze = new(input, '#');

        public string Part1()
        {
            var solver = new Solver(originalMaze);
            var answer = solver.Solve();
            return answer.ToString();
        }

        public string Part2()
        {
            StringBuilder[] tempMaze = originalMaze.Data.Select(row => new StringBuilder(row)).ToArray();
            VectorRC start = originalMaze.Iterate().Single(x => x.Value == '@').Position;
            tempMaze[start.Row - 1][start.Col - 1] = '@';
            tempMaze[start.Row - 1][start.Col] = '#';
            tempMaze[start.Row - 1][start.Col + 1] = '@';
            tempMaze[start.Row][start.Col - 1] = '#';
            tempMaze[start.Row][start.Col] = '#';
            tempMaze[start.Row][start.Col + 1] = '#';
            tempMaze[start.Row + 1][start.Col - 1] = '@';
            tempMaze[start.Row + 1][start.Col] = '#';
            tempMaze[start.Row + 1][start.Col + 1] = '@';
            Grid newMaze = new(tempMaze.Select(row => row.ToString()).ToArray(), '#');

            var solver = new Solver(newMaze);
            var answer = solver.Solve();
            return answer.ToString();
        }

        private class Solver
        {
            private readonly Grid maze;
            private readonly VectorRC[] robots;
            private readonly uint allKeys = 0u;
            private readonly VectorRC[] keyLocations = new VectorRC[26];
            private readonly VectorRC[] doorLocations = new VectorRC[26];
            private readonly Dictionary<(VectorRC, uint), int[]> GetReachableDistancesMemo = new();
            private readonly Dictionary<VectorRC, Dictionary<VectorRC, int>> compressedMaze;

            private record class ExplorationState(EquatableArray<VectorRC> Robots, uint Keys);

            internal Solver(Grid originalMaze)
            {
                maze = originalMaze;
                List<VectorRC> starts = new();
                foreach (var (pos, tile) in maze.Iterate())
                {
                    if (tile == '@')
                    {
                        starts.Add(pos);
                    }
                    else if (char.IsLower(tile))
                    {
                        int keyIndex = tile - 'a';
                        allKeys |= 1u << keyIndex;
                        keyLocations[keyIndex] = pos;
                    }
                    else if (char.IsUpper(tile))
                    {
                        int keyIndex = tile - 'A';
                        allKeys |= 1u << keyIndex;
                        doorLocations[keyIndex] = pos;
                    }
                }
                Debug.Assert(allKeys != 0u);
                Debug.Assert(starts.Count > 0);
                robots = starts.ToArray();

                compressedMaze = CompressPaths();
            }

            private bool IsTraversable(VectorRC position, uint keys)
            {
                char tile = maze.Get(position);
                if (tile == '#')
                {
                    return false;
                }
                else if (char.IsUpper(tile))
                {
                    int keyIndex = tile - 'A';
                    return 0 != (keys & (1u << keyIndex));
                }
                else
                {
                    return true;
                }
            }

            private Dictionary<VectorRC, Dictionary<VectorRC, int>> CompressPaths()
            {
                // Initialize
                Dictionary<VectorRC, Dictionary<VectorRC, int>> paths = new();
                foreach (var (pos, tile) in maze.Iterate())
                {
                    if (tile == '#')
                    {
                        continue;
                    }
                    Dictionary<VectorRC, int> connections = new();
                    foreach (var next in pos.NextFour())
                    {
                        var nextTile = maze.Get(next);
                        if (nextTile == '#')
                        {
                            continue;
                        }
                        connections.Add(next, 1);
                    }
                    paths[pos] = connections;
                }

                // Eliminate dead-ends
                Stack<VectorRC> deadEnds = new(paths.Where(kvp => kvp.Value.Count == 1 && maze.Get(kvp.Key) == '.').Select(kvp => kvp.Key));
                while (deadEnds.TryPop(out var deadEnd))
                {
                    var nextPosition = paths[deadEnd].Keys.Single();
                    var nextNext = paths[nextPosition];
                    nextNext.Remove(deadEnd);
                    if (nextNext.Count == 1 && maze.Get(nextPosition) == '.')
                    {
                        deadEnds.Push(nextPosition);
                    }
                    paths.Remove(deadEnd);
                }

                // Eliminate hallways
                var hallways = paths.Where(kvp => kvp.Value.Count == 2 && maze.Get(kvp.Key) == '.').ToList();
                foreach (var (pos, connections) in hallways)
                {
                    var connectionsList = connections.ToList();
                    var newDistance = connectionsList[0].Value + connectionsList[1].Value;
                    paths[connectionsList[0].Key].Add(connectionsList[1].Key, newDistance);
                    paths[connectionsList[1].Key].Add(connectionsList[0].Key, newDistance);
                    paths[connectionsList[0].Key].Remove(pos);
                    paths[connectionsList[1].Key].Remove(pos);
                    paths.Remove(pos);
                }

                return paths;
            }

            private int[] GetReachableDistances(VectorRC start, uint keys)
            {
                if (GetReachableDistancesMemo.TryGetValue((start, keys), out var value))
                {
                    return value;
                }
                List<(VectorRC, int)> GetNextPositions(VectorRC position)
                {
                    List<(VectorRC, int)> results = new();
                    foreach (var next in compressedMaze[position])
                    {
                        if (IsTraversable(next.Key, keys))
                        {
                            results.Add((next.Key, next.Value));
                        }
                    }
                    return results;
                }
                var dijkstraResult = GraphAlgos.DijkstraToAll(start, GetNextPositions);
                int[] keyDistances = new int[26];
                for (int i = 0; i < 26; i++)
                {
                    if (dijkstraResult.TryGetValue(keyLocations[i], out var keyDist))
                    {
                        keyDistances[i] = keyDist.distance;
                    }
                    else
                    {
                        keyDistances[i] = -1;
                    }
                }
                return GetReachableDistancesMemo[(start, keys)] = keyDistances;
            }

            private List<(ExplorationState, int)> GetNextStates(ExplorationState current)
            {
                List<(ExplorationState, int)> nextStates = new();
                VectorRC[] nextRobots = current.Robots.ToArray();
                for (int i = 0; i < current.Robots.Count; i++)
                {
                    var robot = current.Robots[i];
                    var reachable = GetReachableDistances(robot, current.Keys);
                    for (int key = 0; key < reachable.Length; key++)
                    {
                        // if the key is reachable and we don't have the key
                        if (reachable[key] >= 0 && (current.Keys & (1u << key)) == 0)
                        {
                            var nextKeys = current.Keys | (1u << key);
                            nextRobots[i] = keyLocations[key];
                            ExplorationState nextState = new(new(nextRobots), nextKeys);
                            nextStates.Add((nextState, reachable[key]));
                        }
                    }
                    nextRobots[i] = current.Robots[i]; // Reset
                }
                return nextStates;
            }

            internal int Solve()
            {
                ExplorationState startNode = new(new(robots), 0u);
                var pathResult = GraphAlgos.DijkstraToEnd(startNode, GetNextStates, node => node.Keys == allKeys);
                return pathResult.distance;
            }
        }
    }
}
