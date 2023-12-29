using AocCommon;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/18
    public class Day18 : IAocDay
    {
        private readonly string[] originalMaze;
        private readonly VectorXY start = new();
        private readonly BitVector32 noKeys = new();
        private readonly BitVector32 allKeys = new();
        private readonly VectorXY[] keyLocations = new VectorXY[26];

        public Day18(string input)
        {
            originalMaze = input.Split('\n', Constants.TrimAndDiscard);
            for (int row = 0; row < originalMaze.Length; row++)
            {
                for (int col = 0; col < originalMaze[row].Length; col++)
                {
                    if (originalMaze[row][col] == '@')
                    {
                        start = new VectorXY(col, row);
                        //break;
                    }
                    if (char.IsLower(originalMaze[row][col]))
                    {
                        int keyIndex = originalMaze[row][col] - 'a';
                        allKeys[1 << keyIndex] = true;
                        keyLocations[keyIndex] = new VectorXY(col, row);
                    }
                }
            }
        }

        private record Part1Node(VectorXY Position, BitVector32 Keys);
        public string Part1()
        {
            char GetTileInMaze(VectorXY pos)
            {
                if (pos.Y < 0 || pos.Y >= originalMaze.Length)
                {
                    return '\0';
                }
                var row = originalMaze[pos.Y];
                if (pos.X < 0 || pos.X >= row.Length)
                {
                    return '\0';
                }
                return row[pos.X];
            }
            Part1Node startNode = new(start, noKeys);
            IEnumerable<Part1Node> GetNext(Part1Node current)
            {
                foreach (var nextPos in current.Position.NextFour())
                {
                    char nextTile = GetTileInMaze(nextPos);
                    if (nextTile == '#')
                    {
                        continue;
                    }
                    if (char.IsUpper(nextTile))
                    {
                        var key = char.ToLower(nextTile);
                        if (!current.Keys[1 << (key - 'a')])
                        {
                            continue;
                        }
                    }
                    BitVector32 nextKeys = current.Keys;
                    if (char.IsLower(nextTile))
                    {
                        nextKeys[1 << (nextTile - 'a')] = true;
                    }
                    Part1Node nextState = new(nextPos, nextKeys);
                    yield return nextState;
                }
            }
            var bfsResult = GraphAlgos.BfsToEnd(startNode, GetNext, node => node.Keys.Equals(allKeys));
            return bfsResult.distance.ToString();
        }

        private record Part2Node(VectorXY Robot0, VectorXY Robot1, VectorXY Robot2, VectorXY Robot3, BitVector32 Keys);
        public string Part2()
        {
            StringBuilder[] tempMaze = originalMaze.Select(row => new StringBuilder(row)).ToArray();
            tempMaze[start.Y - 1][start.X - 1] = '@';
            tempMaze[start.Y - 1][start.X] = '#';
            tempMaze[start.Y - 1][start.X + 1] = '@';
            tempMaze[start.Y][start.X - 1] = '#';
            tempMaze[start.Y][start.X] = '#';
            tempMaze[start.Y][start.X + 1] = '#';
            tempMaze[start.Y + 1][start.X - 1] = '@';
            tempMaze[start.Y + 1][start.X] = '#';
            tempMaze[start.Y + 1][start.X + 1] = '@';
            string[] maze = tempMaze.Select(row => row.ToString()).ToArray();

            char GetTileInMaze(VectorXY pos)
            {
                if (pos.Y < 0 || pos.Y >= maze.Length)
                {
                    return '\0';
                }
                var row = maze[pos.Y];
                if (pos.X < 0 || pos.X >= row.Length)
                {
                    return '\0';
                }
                return row[pos.X];
            }

            Func<VectorXY, BitVector32, ImmutableArray<int>> GetReachableDistances = Memoization.Make((VectorXY start, BitVector32 keys) =>
            {
                IEnumerable<VectorXY> GetNext(VectorXY current)
                {
                    foreach (var nextPos in current.NextFour())
                    {
                        char nextTile = GetTileInMaze(nextPos);
                        if (nextTile == '#')
                        {
                            continue;
                        }
                        if (char.IsUpper(nextTile))
                        {
                            var keyForDoor = char.ToLower(nextTile);
                            if (!keys[1 << (keyForDoor - 'a')])
                            {
                                continue;
                            }
                        }
                        yield return nextPos;
                    }
                }
                var bfsResult = GraphAlgos.BfsToAll(start, GetNext);
                int[] keyDistances = new int[26];
                for (int i = 0; i < keyDistances.Length; i++)
                {
                    if (bfsResult.TryGetValue(keyLocations[i], out var keyDist))
                    {
                        keyDistances[i] = keyDist.distance;
                    }
                    else
                    {
                        keyDistances[i] = -1;
                    }
                }
                return keyDistances.ToImmutableArray();
            });

            VectorXY startA = start + new VectorXY(-1, -1);
            VectorXY startB = start + new VectorXY(-1, +1);
            VectorXY startC = start + new VectorXY(+1, +1);
            VectorXY startD = start + new VectorXY(+1, -1);
            Part2Node startNode = new(startA, startB, startC, startD, noKeys);
            IEnumerable<(Part2Node, int)> GetNextState(Part2Node current)
            {
                VectorXY[] robots = { current.Robot0, current.Robot1, current.Robot2, current.Robot3 };
                for (int i = 0; i < 4; i++)
                {
                    var robot = robots[i];
                    var reachable = GetReachableDistances(robot, current.Keys);
                    for (int key = 0; key < reachable.Length; key++)
                    {
                        if (reachable[key] >= 0 && !current.Keys[1 << key])
                        {
                            BitVector32 nextKeys = current.Keys;
                            nextKeys[1 << key] = true;
                            var nextRobots = robots.ToArray();
                            nextRobots[i] = keyLocations[key];
                            var nextState = new Part2Node(nextRobots[0], nextRobots[1], nextRobots[2], nextRobots[3], nextKeys);
                            yield return (nextState, reachable[key]);
                        }
                    }
                }
            }
            var pathResult = GraphAlgos.DijkstraToEnd(startNode, GetNextState, node => node.Keys.Equals(allKeys));
            return pathResult.distance.ToString();
        }
    }
}
