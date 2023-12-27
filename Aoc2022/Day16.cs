using AocCommon;
using System.Collections.Concurrent;

namespace Aoc2022
{
    public class Day16 : IAocDay
    {
        readonly int valveAApos;
        readonly int[] flows;
        readonly int[] allWorkingValves;
        readonly Func<int, int[]> memoBfsToAll;
        readonly Func<int, EquatableArray<int>, int, int> evaluateMaxRelease;

        public Day16(string input)
        {
            char[] separators = { ' ', '=', ';', ',' };
            string[][] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(s => s.Split(separators, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            int nbValves = lines.Length;
            Dictionary<string, int> valveNameIndexMap = new();
            for (int i = 0; i < nbValves; i++)
            {
                valveNameIndexMap[lines[i][1]] = i;
            }
            valveAApos = valveNameIndexMap["AA"];
            flows = lines.Select(ss => int.Parse(ss[5])).ToArray();
            allWorkingValves = Enumerable.Range(0, nbValves).Where(i => flows[i] > 0).ToArray();
            ulong[] neighbors = new ulong[nbValves];
            for (int i = 0; i < nbValves; i++)
            {
                foreach (string next in lines[i][10..])
                {
                    neighbors[i] = neighbors[i] | 1UL << valveNameIndexMap[next];
                }
            }
            memoBfsToAll = Memoization.Make((int pos) =>
            {
                var parentsDistances = GraphAlgos.BfsToAll(pos, p => Enumerable.Range(0, nbValves).Where(i => (neighbors[p] & 1UL << i) != 0));
                int[] distances = new int[nbValves];
                for (int i = 0; i < nbValves; i++)
                {
                    distances[i] = parentsDistances[i].Item2;
                }
                return distances;
            });
            evaluateMaxRelease = Memoization.Make((int pos, EquatableArray<int> myValves, int timeLimit) =>
            {
                if (timeLimit <= 0 || myValves.Data.Length == 0)
                {
                    return 0;
                }
                int maxRelease = 0;
                var distances = memoBfsToAll(pos);
                foreach (int i in myValves.Data)
                {
                    int distance = distances[i];
                    int remainingTime = (timeLimit - distance - 1);
                    if (remainingTime > 0)
                    {
                        int flowReleased = remainingTime * flows[i];
                        int remainingPotential = evaluateMaxRelease(i, new EquatableArray<int>(myValves.Data.Remove(i)), remainingTime);
                        int totalFlow = flowReleased + remainingPotential;
                        if (totalFlow > maxRelease)
                        {
                            maxRelease = totalFlow;
                        }
                    }
                }
                return maxRelease;
            });
        }

        private int EvaluateValveCombination(IEnumerable<int> myValves, int timeLimit)
        {
            return evaluateMaxRelease(valveAApos, new EquatableArray<int>(myValves), timeLimit);
        }

        public string Part1()
        {
            int partOneMaxRelease = EvaluateValveCombination(allWorkingValves, 30);
            return partOneMaxRelease.ToString();
        }
        public string Part2()
        {
            int maxIteration = 1 << allWorkingValves.Length - 1;
            ConcurrentBag<int> maxReleases = new();
            int completed = 0;
            Parallel.For(0, maxIteration, partTwoIteration =>
            {
                List<int> humanValves = new();
                List<int> elephantValves = new();
                for (int j = 0; j < allWorkingValves.Length; j++)
                {
                    if ((partTwoIteration & 1 << j) != 0)
                    {
                        humanValves.Add(allWorkingValves[j]);
                    }
                    else
                    {
                        elephantValves.Add(allWorkingValves[j]);
                    }
                }
                int humanRelease = EvaluateValveCombination(humanValves, 26);
                int elephantRelease = EvaluateValveCombination(elephantValves, 26);
                int totalRelease = humanRelease + elephantRelease;
                //Console.WriteLine($"Human {string.Join("-", humanValves.Select(n => lines[n][1]))} Elephant {string.Join("-", elephantValves.Select(n => lines[n][1]))} released {totalRelease}");
                maxReleases.Add(totalRelease);
                var localCompleted = Interlocked.Increment(ref completed);
                if (localCompleted % 500 == 0)
                {
                    Console.WriteLine($"Completed {localCompleted}/{maxIteration}");
                }
            });
            var partTwoMaxRelease = maxReleases.Max();
            return partTwoMaxRelease.ToString();
        }
    }
}
