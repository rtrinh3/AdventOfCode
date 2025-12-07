namespace Aoc2025;

// https://adventofcode.com/2025/day/7
// --- Day 7: Laboratories ---
public class Day07(string input) : AocCommon.IAocDay
{
    public string Part1()
    {
        var (Part1Answer, _) = DoPuzzle();
        return Part1Answer.ToString();
    }

    public string Part2()
    {
        var (_, Part2Answer) = DoPuzzle();
        return Part2Answer.ToString();
    }

    private (int Part1Answer, long Part2Answer) DoPuzzle()
    {
        string[] rows = input.ReplaceLineEndings("\n").Split('\n', StringSplitOptions.RemoveEmptyEntries);
        AocCommon.DefaultDict<int, long> beams = new();
        int splitCount = 0;
        bool firstRow = true;
        foreach (string row in rows)
        {
            AocCommon.DefaultDict<int, long> newBeams = new();
            if (firstRow)
            {
                for (int col = 0; col < row.Length; col++)
                {
                    if (row[col] == 'S')
                    {
                        newBeams[col]++;
                    }
                }
                firstRow = false;
            }
            foreach (var (beam, count) in beams)
            {
                if (row[beam] == '^')
                {
                    newBeams[beam - 1] += count;
                    newBeams[beam + 1] += count;
                    splitCount++;
                }
                else
                {
                    newBeams[beam] += count;
                }
            }
            beams = newBeams;
        }
        return (splitCount, beams.Values.Sum());
    }
}