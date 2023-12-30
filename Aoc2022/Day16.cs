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
        readonly Func<int, ulong, int, int> evaluateMaxRelease;

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
                    neighbors[i] |= 1UL << valveNameIndexMap[next];
                }
            }
            memoBfsToAll = Memoization.MakeInt((int pos) =>
            {
                var parentsDistances = GraphAlgos.BfsToAll(pos, p => Enumerable.Range(0, nbValves).Where(i => (neighbors[p] & (1UL << i)) != 0));
                int[] distances = new int[nbValves];
                for (int i = 0; i < nbValves; i++)
                {
                    distances[i] = parentsDistances[i].Item2;
                }
                return distances;
            });
            evaluateMaxRelease = Memoization.Make((int pos, ulong myValves, int timeLimit) =>
            {
                if (timeLimit <= 0 || myValves == 0)
                {
                    return 0;
                }
                int maxRelease = 0;
                var distances = memoBfsToAll(pos);
                for (int i = 0; i < nbValves; i++)
                {
                    if ((myValves & (1UL << i)) != 0)
                    {
                        int distance = distances[i];
                        int remainingTime = (timeLimit - distance - 1);
                        if (remainingTime > 0)
                        {
                            int flowReleased = remainingTime * flows[i];
                            ulong oneLessValve = myValves & ~(1UL << i);
                            int remainingPotential = evaluateMaxRelease(i, oneLessValve, remainingTime);
                            int totalFlow = flowReleased + remainingPotential;
                            if (totalFlow > maxRelease)
                            {
                                maxRelease = totalFlow;
                            }
                        }
                    }
                }
                return maxRelease;
            });
        }

        private int EvaluateValveCombination(ulong myValves, int timeLimit)
        {
            return evaluateMaxRelease(valveAApos, myValves, timeLimit);
        }

        public string Part1()
        {
            ulong allWorkingValvesMask = 0UL;
            foreach (int i in allWorkingValves)
            {
                allWorkingValvesMask |= 1UL << i;
            }
            int partOneMaxRelease = EvaluateValveCombination(allWorkingValvesMask, 30);
            return partOneMaxRelease.ToString();
        }
        public string Part2()
        {
            int maxIteration = 1 << (allWorkingValves.Length - 1);
            ConcurrentBag<int> maxReleases = new();
            int completed = 0;
            Parallel.For(0, maxIteration, partTwoIteration =>
            {
                ulong humanValves = 0UL;
                ulong elephantValves = 0UL;
                for (int j = 0; j < allWorkingValves.Length; j++)
                {
                    if ((partTwoIteration & (1 << j)) != 0)
                    {
                        humanValves |= 1UL << allWorkingValves[j];
                    }
                    else
                    {
                        elephantValves |= 1UL << allWorkingValves[j];
                    }
                }
                int humanRelease = EvaluateValveCombination(humanValves, 26);
                int elephantRelease = EvaluateValveCombination(elephantValves, 26);
                int totalRelease = humanRelease + elephantRelease;
                maxReleases.Add(totalRelease);
                var localCompleted = Interlocked.Increment(ref completed);
                if ((localCompleted & 0x3FF) == 0)
                {
                    Console.WriteLine($"Completed {localCompleted}/{maxIteration}");
                }
            });
            var partTwoMaxRelease = maxReleases.Max();
            return partTwoMaxRelease.ToString();
        }
    }
}
