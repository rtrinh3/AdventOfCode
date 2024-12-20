using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/20
// --- Day 20: Race Condition ---
public class Day20(string input) : IAocDay
{
    private readonly Grid map = new(input, '#');

    public string Part1()
    {
        var cheats = FindCheats(2);
        var answer = cheats.Count(c => c.Saved >= 100);
        return answer.ToString();
    }

    public List<(VectorRC Start, VectorRC End, int Saved)> FindCheats(int cheatTime)
    {
        VectorRC raceStart = map.Iterate().Single(x => x.Value == 'S').Position;
        VectorRC raceEnd = map.Iterate().Single(x => x.Value == 'E').Position;
        IEnumerable<VectorRC> originalNeighbors(VectorRC pos)
        {
            return pos.NextFour().Where(next => map.Get(next) != '#');
        }
        var originalRace = GraphAlgos.BfsToAll(raceStart, originalNeighbors);
        var originalTime = originalRace[raceEnd].distance;

        List<VectorRC> cheatOffsets = new();
        for (int row = -cheatTime; row <= cheatTime; row++)
        {
            for (int col = -cheatTime; col <= cheatTime; col++)
            {
                VectorRC offset = new(row, col);
                if (offset.ManhattanMetric() <= cheatTime)
                {
                    cheatOffsets.Add(offset);
                }
            }
        }

        var cheats = map.Iterate()
            .Where(x => x.Value != '#')
            .SelectMany(x => cheatOffsets.Select(offset => (Start: x.Position, End: x.Position + offset)))
            .Where(cheat => map.Get(cheat.End) != '#')
            .ToList();

        int done = 0;
        Task.Run(() =>
        {
            int prev = 0;
            while (done < cheats.Count)
            {
                int doneNow = done;
                int doneLastSec = doneNow - prev;
                int eta = 0;
                if (doneLastSec > 0)
                {
                    eta = (cheats.Count - doneNow) / doneLastSec;
                }
                Console.WriteLine($"Done {doneNow}/{cheats.Count}, ETA {eta}s");
                prev = doneNow;
                Thread.Sleep(1000);
            }
        });
        var cheatTimes = cheats.AsParallel()
            .Select(cheat =>
            {
                var shortcutTime = (cheat.End - cheat.Start).ManhattanMetric();
                var remaining = GraphAlgos.BfsToEnd(cheat.End, originalNeighbors, pos => pos == raceEnd);
                var totalTime = originalRace[cheat.Start].distance + shortcutTime + remaining.distance;
                var saved = originalTime - totalTime;
                var answer = (cheat.Start, cheat.End, saved);
                Interlocked.Increment(ref done);
                return answer;
            })
            .ToList();
        return cheatTimes;
    }

    public string Part2()
    {
        var cheats = FindCheats(20);
        var answer = cheats.Count(c => c.Saved >= 100);
        return answer.ToString();
    }
}
