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
        // TODO Factorize common code out of GetArrowMoves, GetArrowSequenceLength, GetNumberMoves, GetNumberSequenceLength?
        Func<char, char, string> GetArrowMoves = Memoization.Make((char origin, char destination) =>
        {
            VectorRC originCoord = directionPad.Iterate().Single(x => x.Value == origin).Position;
            VectorRC destinationCoord = directionPad.Iterate().Single(x => x.Value == destination).Position;
            VectorRC movement = destinationCoord - originCoord;
            StringBuilder answer = new();
            // Avoid the gap in the corner
            if (originCoord.Col == 0 && destinationCoord.Row == 0)
            {
                Debug.Assert(movement.Row < 0);
                Debug.Assert(movement.Col > 0);
                for (int i = 0; i < movement.Col; i++)
                {
                    answer.Append('>');
                }
                for (int i = 0; i < -movement.Row; i++)
                {
                    answer.Append('^');
                }
            }
            else if (originCoord.Row == 0 && destinationCoord.Col == 0)
            {
                Debug.Assert(movement.Row > 0);
                Debug.Assert(movement.Col < 0);
                for (int i = 0; i < movement.Row; i++)
                {
                    answer.Append('v');
                }
                for (int i = 0; i < -movement.Col; i++)
                {
                    answer.Append('<');
                }
            }
            else
            {
                // Move in the order <v^> -- furthest to closest to A
                for (int i = 0; i < -movement.Col; i++)
                {
                    answer.Append('<');
                }
                for (int i = 0; i < movement.Row; i++)
                {
                    answer.Append('v');
                }
                for (int i = 0; i < -movement.Row; i++)
                {
                    answer.Append('^');
                }
                for (int i = 0; i < movement.Col; i++)
                {
                    answer.Append('>');
                }
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
            var moves = GetArrowMoves(origin, destination);
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
        Func<char, char, string> GetNumberMoves = Memoization.Make((char origin, char destination) =>
        {
            VectorRC originCoord = numberPad.Iterate().Single(x => x.Value == origin).Position;
            VectorRC destinationCoord = numberPad.Iterate().Single(x => x.Value == destination).Position;
            VectorRC movement = destinationCoord - originCoord;
            StringBuilder answer = new();
            // Avoid the gap in the corner
            if (originCoord.Col == 0 && destinationCoord.Row == 3)
            {
                Debug.Assert(movement.Row > 0);
                Debug.Assert(movement.Col > 0);
                for (int i = 0; i < movement.Col; i++)
                {
                    answer.Append('>');
                }
                for (int i = 0; i < movement.Row; i++)
                {
                    answer.Append('v');
                }
            }
            else if (originCoord.Row == 3 && destinationCoord.Col == 0)
            {
                Debug.Assert(movement.Row < 0);
                Debug.Assert(movement.Col < 0);
                for (int i = 0; i < -movement.Row; i++)
                {
                    answer.Append('^');
                }
                for (int i = 0; i < -movement.Col; i++)
                {
                    answer.Append('<');
                }
            }
            else
            {
                // Move in the order <v^> -- furthest to closest to A
                for (int i = 0; i < -movement.Col; i++)
                {
                    answer.Append('<');
                }
                for (int i = 0; i < movement.Row; i++)
                {
                    answer.Append('v');
                }
                for (int i = 0; i < -movement.Row; i++)
                {
                    answer.Append('^');
                }
                for (int i = 0; i < movement.Col; i++)
                {
                    answer.Append('>');
                }
            }
            answer.Append('A');
            return answer.ToString();
        });
        Func<char, char, long> GetNumberSequenceLength = Memoization.Make((char origin, char destination) =>
        {
            var moves = GetNumberMoves(origin, destination);
            long length = 0;
            for (int i = 0; i < moves.Length; i++)
            {
                char moveOrigin = i == 0 ? 'A' : moves[i - 1];
                char moveDestination = moves[i];
                long x = GetArrowSequenceLength(moveOrigin, moveDestination, directionRobots);
                length += x;
            }
            return length;
        });

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
