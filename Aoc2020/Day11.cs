using System.Text;
using AocCommon;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/11
    // --- Day 11: Seating System ---
    public class Day11(string input) : IAocDay
    {
        public long Part1()
        {
            string state = input.TrimEnd().ReplaceLineEndings("\n") + '\n';
            while (true)
            {
                Grid grid = new(state, '.');
                StringBuilder newStateBuilder = new();
                for (int row = 0; row < grid.Height; row++)
                {
                    for (int col = 0; col < grid.Width; col++)
                    {
                        VectorRC pos = new(row, col);
                        char current = grid.Get(pos);
                        if (current == 'L')
                        {
                            if (pos.NextEight().All(p => grid.Get(p) != '#'))
                            {
                                newStateBuilder.Append('#');
                            }
                            else
                            {
                                newStateBuilder.Append('L');
                            }
                        }
                        else if (current == '#')
                        {
                            if (pos.NextEight().Count(p => grid.Get(p) == '#') >= 4)
                            {
                                newStateBuilder.Append('L');
                            }
                            else
                            {
                                newStateBuilder.Append('#');
                            }
                        }
                        else
                        {
                            newStateBuilder.Append(current);
                        }
                    }
                    newStateBuilder.Append('\n');
                }
                string newState = newStateBuilder.ToString();
                if (newState == state)
                {
                    break;
                }
                else
                {
                    state = newState;
                }
            }

            var answer = state.Count(c => c == '#');
            return answer;
        }

        public long Part2()
        {
            string state = input.TrimEnd().ReplaceLineEndings("\n") + '\n';
            while (true)
            {
                Grid grid = new(state, '.');
                StringBuilder newStateBuilder = new();
                for (int row = 0; row < grid.Height; row++)
                {
                    for (int col = 0; col < grid.Width; col++)
                    {
                        VectorRC pos = new(row, col);
                        char current = grid.Get(pos);
                        if (current == 'L')
                        {
                            int occupied = CountOccupiedSeatsSightLine(grid, pos);
                            if (occupied == 0)
                            {
                                newStateBuilder.Append('#');
                            }
                            else
                            {
                                newStateBuilder.Append('L');
                            }
                        }
                        else if (current == '#')
                        {
                            int occupied = CountOccupiedSeatsSightLine(grid, pos);
                            if (occupied >= 5)
                            {
                                newStateBuilder.Append('L');
                            }
                            else
                            {
                                newStateBuilder.Append('#');
                            }
                        }
                        else
                        {
                            newStateBuilder.Append(current);
                        }
                    }
                    newStateBuilder.Append('\n');
                }
                string newState = newStateBuilder.ToString();
                if (newState == state)
                {
                    break;
                }
                else
                {
                    state = newState;
                }
            }

            var answer = state.Count(c => c == '#');
            return answer;
        }

        private static int CountOccupiedSeatsSightLine(Grid grid, VectorRC pos)
        {
            int occupied = 0;
            foreach (var dir in VectorRC.Zero.NextEight())
            {
                char firstSeat = FindFirstSeat(grid, pos, dir);
                if (firstSeat == '#')
                {
                    occupied++;
                }
            }
            return occupied;
        }

        private static char FindFirstSeat(Grid grid, VectorRC pos, VectorRC dir)
        {
            while (true)
            {
                pos += dir;
                if (!(0 <= pos.Row && pos.Row < grid.Height && 0 <= pos.Col && pos.Col < grid.Width))
                {
                    return '.';
                }
                char current = grid.Get(pos);
                if (current == '#' || current == 'L')
                {
                    return current;
                }
                // else continue
            }
        }
    }
}
