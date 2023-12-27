using AocCommon;

namespace Aoc2022
{
    public class Day23 : IAocDay
    {
        private readonly VectorXY[] initialElves;
        public Day23(string input)
        {
            var inputLines = input.ReplaceLineEndings("\n").Split('\n');
            List<VectorXY> elves = [];
            for (int y = 0; y < inputLines.Length; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    if (inputLines[y][x] == '#')
                    {
                        elves.Add(new VectorXY(x, -y));
                    }
                }
            }
            initialElves = [.. elves];
        }

        public string Part1()
        {
            var elves = initialElves;
            for (int round = 0; round < 10; round++)
            {
                elves = DoRound(round, elves);
            }

            var xmin = elves.Select(e => e.X).Min();
            var xmax = elves.Select(e => e.X).Max();
            var ymin = elves.Select(e => e.Y).Min();
            var ymax = elves.Select(e => e.Y).Max();
            var emptySpaces = (xmax - xmin + 1) * (ymax - ymin + 1) - elves.Length;
            return emptySpaces.ToString();
        }

        public string Part2()
        {
            var elves = initialElves;
            int round = 0;
            while (true)
            {
                var newElves = DoRound(round, elves);
                if (newElves.SequenceEqual(elves))
                {
                    var answer = round + 1;
                    return answer.ToString();
                }
                elves = newElves;
                round++;
            }
        }

        private static readonly VectorXY DIR_N = new(0, +1);
        private static readonly VectorXY DIR_NE = new(+1, +1);
        private static readonly VectorXY DIR_E = new(+1, 0);
        private static readonly VectorXY DIR_SE = new(+1, -1);
        private static readonly VectorXY DIR_S = new(0, -1);
        private static readonly VectorXY DIR_SW = new(-1, -1);
        private static readonly VectorXY DIR_W = new(-1, 0);
        private static readonly VectorXY DIR_NW = new(-1, +1);
        private static readonly VectorXY[] DIRECTIONS = [DIR_N, DIR_NE, DIR_E, DIR_SE, DIR_S, DIR_SW, DIR_W, DIR_NW];

        private static VectorXY[] DoRound(int round, VectorXY[] elves)
        {
            // First half: proposals
            HashSet<VectorXY> elfSet = [.. elves];
            VectorXY[] proposals = new VectorXY[elves.Length];
            for (int elfIndex = 0; elfIndex < elves.Length; elfIndex++)
            {
                var elf = elves[elfIndex];
                if (DIRECTIONS.Select(d => elf + d).All(pos => !elfSet.Contains(pos)))
                {
                    proposals[elfIndex] = elf;
                }
                else
                {
                    int checkOffset = round % 4;
                    bool moved = false;
                    for (int check = 0; check < 4 && !moved; check++)
                    {
                        switch ((checkOffset + check) % 4)
                        {
                            case 0:
                                if (!elfSet.Contains(elf + DIR_N) && !elfSet.Contains(elf + DIR_NE) && !elfSet.Contains(elf + DIR_NW))
                                {
                                    proposals[elfIndex] = elf + DIR_N;
                                    moved = true;
                                }
                                break;
                            case 1:
                                if (!elfSet.Contains(elf + DIR_S) && !elfSet.Contains(elf + DIR_SE) && !elfSet.Contains(elf + DIR_SW))
                                {
                                    proposals[elfIndex] = elf + DIR_S;
                                    moved = true;
                                }
                                break;
                            case 2:
                                if (!elfSet.Contains(elf + DIR_W) && !elfSet.Contains(elf + DIR_NW) && !elfSet.Contains(elf + DIR_SW))
                                {
                                    proposals[elfIndex] = elf + DIR_W;
                                    moved = true;
                                }
                                break;
                            case 3:
                                if (!elfSet.Contains(elf + DIR_E) && !elfSet.Contains(elf + DIR_NE) && !elfSet.Contains(elf + DIR_SE))
                                {
                                    proposals[elfIndex] = elf + DIR_E;
                                    moved = true;
                                }
                                break;
                            default:
                                throw new Exception("???");
                        }
                    }
                    if (!moved)
                    {
                        proposals[elfIndex] = elf;
                    }
                }
            }
            // Second half: moves
            var proposalCounts = proposals.GroupBy(p => p).ToDictionary(g => g.Key, g => g.Count());
            for (int i = 0; i < elves.Length; i++)
            {
                if (proposalCounts[proposals[i]] > 1)
                {
                    proposals[i] = elves[i];
                }
            }
            return proposals;
        }
    }
}
