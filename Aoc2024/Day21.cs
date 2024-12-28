using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/21
// --- Day 21: Keypad Conundrum ---
public class Day21(string input) : IAocDay
{
    private const char OUTSIDE = ' ';
    private static readonly Grid numberPad = new(["789", "456", "123", " 0A"], OUTSIDE);
    private static readonly Grid directionPad = new([" ^A", "<v>"], OUTSIDE);
    private record class RobotState(VectorRC RobotA, VectorRC RobotB, VectorRC RobotC, string Code);

    private readonly string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

    public string Part1()
    {
        long answer = 0;
        RobotState initialState = new(new(3, 2), new(3, 2), new(0, 2), "");
        foreach (var code in lines)
        {
            var path = GraphAlgos.BfsToEnd(initialState, state => GetNextStates(state, code), state => state.Code == code);
            long buttons = path.distance;
            long complexity = buttons * int.Parse(code[..^1]);
            answer += complexity;
        }
        return answer.ToString();
    }

    private static IEnumerable<RobotState> GetNextStates(RobotState state, string targetCode)
    {
        throw new NotImplementedException("TODO");
    }

    public string Part2()
    {
        return nameof(Part2);
    }
}
