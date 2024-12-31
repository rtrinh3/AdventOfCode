using AocCommon;
using System.Diagnostics;
using System.Text;

namespace Aoc2024;

// https://adventofcode.com/2024/day/21
// --- Day 21: Keypad Conundrum ---
public class Day21(string input) : IAocDay
{
    private const char OUTSIDE = ' ';
    private static readonly Grid directionPad = new([
        " ^A",
        "<v>"
        ], OUTSIDE);
    private static readonly Grid numberPad = new([
        "789",
        "456",
        "123",
        " 0A"
        ], OUTSIDE);

    private readonly string[] lines = input.TrimEnd().ReplaceLineEndings("\n").Split('\n');

    public string Part1()
    {
        long answer = DoPuzzle(2);
        return answer.ToString();
    }

    private long DoPuzzle(int directionRobots)
    {
        Func<Grid, char, char, string> GetMoves = Memoization.Make((Grid pad, char origin, char destination) =>
        {
            VectorRC originCoord = pad.Iterate().Single(x => x.Value == origin).Position;
            VectorRC destinationCoord = pad.Iterate().Single(x => x.Value == destination).Position;
            VectorRC gapCoord = pad.Iterate().Single(x => x.Value == OUTSIDE).Position;
            VectorRC movement = destinationCoord - originCoord;
            StringBuilder answer = new();
            // Move in the order <v^> -- furthest to closest to A
            if (movement.Col < 0)
            {
                answer.Append('<', -movement.Col);
            }
            if (movement.Row > 0)
            {
                answer.Append('v', movement.Row);
            }
            if (movement.Row < 0)
            {
                answer.Append('^', -movement.Row);
            }
            if (movement.Col > 0)
            {
                answer.Append('>', movement.Col);
            }
            // Reverse path to avoid gap
            if ((originCoord.Col == gapCoord.Col && destinationCoord.Row == gapCoord.Row) || (originCoord.Row == gapCoord.Row && destinationCoord.Col == gapCoord.Col))
            {
                var reversed = answer.ToString().Reverse().ToArray();
                answer.Clear();
                answer.Append(reversed);
            }
            answer.Append('A');
            return answer.ToString();
        });
        Func<char, char, int, long> GetArrowSequenceLength = (_, _, _) => throw new Exception("Stub for memoized recursive function");
        GetArrowSequenceLength = Memoization.Make((char origin, char destination, int depth) =>
        {
            if (depth <= 0)
            {
                return 1L;
            }
            var moves = GetMoves(directionPad, origin, destination);
            long length = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                char moveOrigin = i == 0 ? 'A' : moves[i - 1];
                char moveDestination = moves[i];
                long x = GetArrowSequenceLength(moveOrigin, moveDestination, depth - 1);
                length += x;
            }
            return length;
        });
        long GetNumberSequenceLength(char origin, char destination)
        {
            var moves = GetMoves(numberPad, origin, destination);
            long length = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                char moveOrigin = i == 0 ? 'A' : moves[i - 1];
                char moveDestination = moves[i];
                long x = GetArrowSequenceLength(moveOrigin, moveDestination, directionRobots);
                length += x;
            }
            return length;
        }

        long puzzleAnswer = 0;
        foreach (var code in lines)
        {
            long buttons = 0;
            for (int i = 0; i < code.Length; i++)
            {
                char origin = i == 0 ? 'A' : code[i - 1];
                char destination = code[i];
                long x = GetNumberSequenceLength(origin, destination);
                buttons += x;
            }
            long complexity = buttons * int.Parse(code[..^1]);
            puzzleAnswer += complexity;
        }
        return puzzleAnswer;
    }

    public string Part2()
    {
        long answer = DoPuzzle(25);
        return answer.ToString();
    }
}
