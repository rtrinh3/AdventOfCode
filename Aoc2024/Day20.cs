using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/20
// --- Day 20: Race Condition ---
public class Day20(string input) : IAocDay
{
    private readonly Grid map = new(input, '#');

    public string Part1()
    {
        var cheats = FindCheats();
        var answer = cheats.Count(c => c.Saved >= 100);
        return answer.ToString();
    }

    public List<(VectorRC Start, VectorRC End, int Saved)> FindCheats()
    {
        VectorRC raceStart = map.Iterate().Single(x => x.Value == 'S').Position;
        IEnumerable<VectorRC> originalNeighbors(VectorRC pos)
        {
            return pos.NextFour().Where(next => map.Get(next) != '#');
        }
        var originalRace = GraphAlgos.BfsToEnd(raceStart, originalNeighbors, pos => map.Get(pos) == 'E');
        var originalTime = originalRace.distance;
        List<VectorRC> cheatOffsets = new();
        for (int i = 0; i <= 2; i++)
        {
            VectorRC offset = new(i, 2 - i);
            cheatOffsets.Add(offset);
            cheatOffsets.Add(-offset);
            cheatOffsets.Add(offset.RotatedLeft());
            cheatOffsets.Add(offset.RotatedRight());
        }
        var cheats = map.Iterate()
            .Where(x => x.Value != '#')
            .SelectMany(x => cheatOffsets.Select(offset => (Start: x.Position, End: x.Position + offset)))
            .Distinct() // Why Distinct? Where are the duplicates coming from?
            .Where(cheat => map.Get(cheat.End) != '#')
            .Select(cheat =>
            {
                List<(VectorRC, int)> getNeighbors(VectorRC pos)
                {
                    List<(VectorRC, int)> results = pos.NextFour()
                    .Where(next => map.Get(next) != '#')
                    .Select(next => (next, 1))
                    .ToList();
                    if (pos == cheat.Start)
                    {
                        results.Add((cheat.End, 2));
                    }
                    return results;
                }
                var race = GraphAlgos.DijkstraToEnd(raceStart, getNeighbors, pos => map.Get(pos) == 'E');
                var answer = (cheat.Start, cheat.End, originalTime - race.distance);
                //Console.WriteLine(answer);
                return answer;
            })
            .ToList();
        return cheats;
    }

    public string Part2()
    {
        return nameof(Part2);
    }
}
