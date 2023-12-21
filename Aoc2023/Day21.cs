using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoc2023
{
    public class Day21(string input) : IAocDay
    {
        private Grid maze = new(input, '#');

        public long Part1()
        {
            VectorRC start = maze.Iterate().Where(x => x.Value == 'S').Select(x => x.Position).Single();
            IEnumerable<VectorRC> GetNeighbors(VectorRC position)
            {
                return position.NextFour().Where(p => maze.Get(p) != '#');
            }
            var bfsResult = GraphAlgos.BfsToAll(start, GetNeighbors);
            int count = bfsResult.Values.Count(x => x.distance <= 64 && x.distance % 2 == 0);
            return count;
        }

        public long Part2()
        {
            return -2;
        }
    }
}
