using System.Collections.Specialized;
using System.Text;

namespace Aoc2019
{
    // https://adventofcode.com/2019/day/24
    public class Day24 : IAocDay
    {
        private const int sideLength = 5;
        private readonly BitVector32 initialState;

        public Day24(string input)
        {
            string[] lines = input.Split('\n', AocCommon.Constants.TrimAndDiscard);
            initialState = new BitVector32();
            for (int row = 0; row < sideLength; row++)
            {
                for (int col = 0; col < sideLength; col++)
                {
                    int index = row * sideLength + col;
                    initialState[1 << index] = (lines[row][col] == '#');
                }
            }
        }

        public string Part1()
        {
            static string ShowState(BitVector32 state)
            {
                StringBuilder sb = new();
                for (int row = 0; row < sideLength; row++)
                {
                    for (int col = 0; col < sideLength; col++)
                    {
                        int index = row * sideLength + col;
                        sb.Append(state[1 << index] ? '#' : '.');
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            static bool GetCell(BitVector32 state, int row, int col)
            {
                if (row < 0 || row >= sideLength)
                {
                    return false;
                }
                if (col < 0 || col >= sideLength)
                {
                    return false;
                }
                int index = row * sideLength + col;
                return state[1 << index];
            }
            static int CountNeighbors(BitVector32 state, int row, int col)
            {
                int count = 0;
                if (GetCell(state, row + 1, col)) { count++; }
                if (GetCell(state, row - 1, col)) { count++; }
                if (GetCell(state, row, col + 1)) { count++; }
                if (GetCell(state, row, col - 1)) { count++; }
                return count;
            }

            BitVector32 state = initialState;
            HashSet<BitVector32> previousStates = new();
            while (true)
            {
                //Console.WriteLine(ShowState(state));
                if (previousStates.Contains(state))
                {
                    break;
                }
                previousStates.Add(state);
                BitVector32 newState = new();
                for (int row = 0; row < sideLength; row++)
                {
                    for (int col = 0; col < sideLength; col++)
                    {
                        int index = row * sideLength + col;
                        int neighbors = CountNeighbors(state, row, col);
                        newState[1 << index] = (state[1 << index])
                            ? (neighbors == 1)
                            : (neighbors == 1 || neighbors == 2);
                    }
                }
                state = newState;
            }
            //Console.WriteLine(ShowState(state));
            //Console.WriteLine(state.Data);
            return state.Data.ToString();
        }

        public string Part2()
        {
            return DoPart2(200).ToString();
        }

        public int DoPart2(int timeLimit)
        {
            static string ShowState(BitVector32 state)
            {
                StringBuilder sb = new();
                for (int row = 0; row < sideLength; row++)
                {
                    for (int col = 0; col < sideLength; col++)
                    {
                        if (row == 2 && col == 2)
                        {
                            sb.Append('?');
                        }
                        else
                        {
                            int index = row * sideLength + col;
                            sb.Append(state[1 << index] ? '#' : '.');
                        }
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            SortedDictionary<int, BitVector32> state = new()
            {
                [0] = initialState
            };
            static (int, int)[] GetNeighbors(int index, int depth)
            {
                if (index < 0 || index >= 25 || index == 12)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }
                (int, int)[][] lookupTable =
                [
                    [(7, -1), (1, 0), (5, 0), (11, -1)],
                    [(7, -1), (2, 0), (6, 0), (0, 0)],
                    [(7, -1), (3, 0), (7, 0), (1, 0)],
                    [(7, -1), (4, 0), (8, 0), (2, 0)],
                    [(7, -1), (13, -1), (9, 0), (3, 0)],

                    [(0, 0), (6, 0), (10, 0), (11, -1)],
                    [(1, 0), (7, 0), (11, 0), (5, 0)],
                    [(2, 0), (8, 0), (0, +1), (1, +1), (2, +1), (3, +1), (4, +1), (6, 0)],
                    [(3, 0), (9, 0), (13, 0), (7, 0)],
                    [(4, 0), (13, -1), (14, 0), (8, 0)],

                    [(5, 0), (11, 0), (15, 0), (11, -1)],
                    [(6, 0), (0, +1), (5, +1), (10, +1), (15, +1), (20, +1), (16, 0), (10, 0)],
                    [],
                    [(8, 0), (14, 0), (18, 0), (4, +1), (9, +1), (14, +1), (19, +1), (24, +1)],
                    [(9, 0), (13, -1), (19, 0), (13, 0)],

                    [(10, 0), (16, 0), (20, 0), (11, -1)],
                    [(11, 0), (17, 0), (21, 0), (15, 0)],
                    [(20, +1), (21, +1), (22, +1), (23, +1), (24, +1), (18, 0), (22, 0), (16, 0)],
                    [(13, 0), (19, 0), (23, 0), (17, 0)],
                    [(14, 0), (13, -1), (24, 0), (18, 0)],

                    [(15, 0), (21, 0), (17, -1), (11, -1)],
                    [(16, 0), (22, 0), (17, -1), (20, 0)],
                    [(17, 0), (23, 0), (17, -1), (21, 0)],
                    [(18, 0), (24, 0), (17, -1), (22, 0)],
                    [(19, 0), (13, -1), (17, -1), (23, 0)],
                ];
                (int, int)[] ret = lookupTable[index].Select(p => (p.Item1, p.Item2 + depth)).ToArray();
                return ret;
            }
            for (int minute = 0; minute < timeLimit; minute++)
            {
                SortedDictionary<int, BitVector32> newState = new();
                var keys = state.Keys.ToList();
                keys.Add(keys[^1] + 1);
                keys.Insert(0, keys[0] - 1);
                foreach (var depth in keys)
                {
                    BitVector32 currentLevel = state.GetValueOrDefault(depth);
                    BitVector32 newLevel = new();
                    for (int index = 0; index < (sideLength * sideLength); index++)
                    {
                        if (index == 12)
                        {
                            continue;
                        }
                        var neighborCoords = GetNeighbors(index, depth);
                        var neighbors = neighborCoords.Count(c => state.GetValueOrDefault(c.Item2)[1 << c.Item1]);
                        newLevel[1 << index] = (currentLevel[1 << index])
                            ? (neighbors == 1)
                            : (neighbors == 1 || neighbors == 2);
                    }
                    newState[depth] = newLevel;
                }
                // Cleanup
                foreach (var depth in keys.TakeWhile(k => newState[k].Data == 0))
                {
                    newState.Remove(depth);
                }
                foreach (var depth in keys.AsEnumerable().Reverse().TakeWhile(k => newState[k].Data == 0))
                {
                    newState.Remove(depth);
                }
                state = newState;
            }
            //Console.WriteLine("Debug");
            //foreach (var kvp in state)
            //{
            //    Console.WriteLine($"Depth {kvp.Key}");
            //    Console.WriteLine(ShowState(kvp.Value));
            //}
            static int CountBits(BitVector32 val) => Enumerable.Range(0, 32).Count(i => val[1 << i]);
            var countBugs = state.Values.Select(level => CountBits(level)).Sum();
            //Console.WriteLine($"Bugs: {countBugs}");
            return countBugs;
        }
    }
}
