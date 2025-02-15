using AocCommon;

namespace Aoc2016;

// https://adventofcode.com/2016/day/15
// --- Day 15: Timing is Everything ---
public class Day15(string input) : IAocDay
{
    private record Disc(int Positions, int InitialPosition);

    private readonly Disc[] discs = Parsing.SplitLines(input).Select(line =>
    {
        var numbers = Parsing.IntsPositive(line);
        return new Disc(numbers[1], numbers[3]);
    }).ToArray();

    private static int DoPuzzle(Disc[] discs)
    {
        int maxTime = discs.Select(d => d.Positions).Aggregate((a, b) => a * b);
        for (int buttonTime = 0; buttonTime < maxTime; buttonTime++)
        {
            bool pass = true;
            for (int discIndex = 0; pass && discIndex < discs.Length; discIndex++)
            {
                int simulationTime = buttonTime + 1 + discIndex;
                int discPosition = (discs[discIndex].InitialPosition + simulationTime) % discs[discIndex].Positions;
                pass = (discPosition == 0);
            }
            if (pass)
            {
                return buttonTime;
            }
        }
        throw new Exception("No answer found");
    }

    public string Part1()
    {
        var answer = DoPuzzle(discs);
        return answer.ToString();
    }

    public string Part2()
    {
        Disc[] augmentedDiscs = [.. discs, new(11, 0)];
        var answer = DoPuzzle(augmentedDiscs);
        return answer.ToString();
    }
}
