namespace Aoc2025;

// https://adventofcode.com/2025/day/7
// --- Day 7: Laboratories ---
public class Day07(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        string[] rows = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        HashSet<int> beams = new();
        int splitCount = 0;
        foreach (string row in rows)
        {
            HashSet<int> newBeams = new();
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == 'S')
                {
                    newBeams.Add(i);
                }
            }
            foreach (var beam in beams)
            {
                if (row[beam] == '^')
                {
                    newBeams.Add(beam - 1);
                    newBeams.Add(beam + 1);
                    splitCount++;
                }
                else
                {
                    newBeams.Add(beam);
                }
            }
            beams = newBeams;
        }
        return splitCount.ToString();
    }

    public string Part2()
    {
        string[] rows = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        AocCommon.DefaultDict<int, long> beams = new();
        foreach (string row in rows)
        {
            AocCommon.DefaultDict<int, long> newBeams = new();
            for (int i = 0; i < row.Length; i++)
            {
                if (row[i] == 'S')
                {
                    newBeams[i]++;
                }
            }
            foreach (var (beam, count) in beams)
            {
                if (row[beam] == '^')
                {
                    newBeams[beam - 1] += count;
                    newBeams[beam + 1] += count;
                }
                else
                {
                    newBeams[beam] += count;
                }
            }
            beams = newBeams;
        }
        var answer = beams.Values.Sum();
        return answer.ToString();
    }
}