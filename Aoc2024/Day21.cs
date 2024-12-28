using AocCommon;

namespace Aoc2024;

// https://adventofcode.com/2024/day/21
// --- Day 21: Keypad Conundrum ---
public class Day21(string input) : IAocDay
{
    private const char OUTSIDE = ' ';
    private static readonly Grid directionPad = new([" ^A", "<v>"], OUTSIDE);
    private static readonly Grid numberPad = new(["789", "456", "123", " 0A"], OUTSIDE);

    private readonly string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

    public string Part1()
    {
        long answer = 0;
        IRobot initialState = new PadRobot(new VectorRC(0, 2), directionPad,
            new PadRobot(new VectorRC(0, 2), directionPad,
            new PadRobot(new VectorRC(3, 2), numberPad,
            new OutputCode(""))));
        foreach (var code in lines)
        {
            var path = GraphAlgos.BfsToEnd(initialState, state => GetNextStates(state, code), state => (string)state.GetState().Last() == code);
            long buttons = path.distance;
            long complexity = buttons * int.Parse(code[..^1]);
            answer += complexity;
        }
        return answer.ToString();
    }

    private static List<IRobot> GetNextStates(IRobot state, string targetCode)
    {
        List<IRobot> results = new();
        foreach (char button in "^v<>A")
        {
            var next = state.PushButton(button);
            if (next is not null && targetCode.StartsWith((string)next.GetState().Last()))
            {
                results.Add(next);
            }
        }
        return results;
    }

    private interface IRobot
    {
        IEnumerable<object> GetState();
        IRobot? PushButton(char button);
    }

    private record class PadRobot(VectorRC Position, Grid Pad, IRobot Next) : IRobot
    {
        public IEnumerable<object> GetState() => [Position, .. Next.GetState()];
        public IRobot? PushButton(char button)
        {
            if (button == 'A')
            {
                var newNext = Next.PushButton(Pad.Get(Position));
                if (newNext is not null)
                {
                    return new PadRobot(Position, Pad, newNext);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                VectorRC move = button switch
                {
                    '^' => VectorRC.Up,
                    'v' => VectorRC.Down,
                    '<' => VectorRC.Left,
                    '>' => VectorRC.Right,
                    _ => throw new Exception("what button")
                };
                var newPos = Position + move;
                if (Pad.Get(newPos) != OUTSIDE)
                {
                    return new PadRobot(newPos, Pad, Next);
                }
                else
                {
                    return null;
                }
            }
            throw new NotImplementedException(nameof(PadRobot));
        }
    }

    private record class OutputCode(string Code) : IRobot
    {
        public IEnumerable<object> GetState() => [Code];
        public IRobot PushButton(char button)
        {
            return new OutputCode(Code + button);
        }
    }

    public string Part2()
    {
        return nameof(Part2);
    }
}
