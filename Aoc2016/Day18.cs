namespace Aoc2016;

// https://adventofcode.com/2016/day/18
// --- Day 18: Like a Rogue ---
public class Day18(string input) : AocCommon.IAocDay
{
    public int DoPuzzle(int rows)
    {
        bool[] row = input.Trim().Select(c => c == '^').ToArray();
        int answer = row.Count(t => !t);
        for (int i = 1; i < rows; i++)
        {
            var newRow = new bool[row.Length];
            for (int j = 0; j < newRow.Length; j++)
            {
                bool left = row.ElementAtOrDefault(j - 1);
                bool center = row[j];
                bool right = row.ElementAtOrDefault(j + 1);
                bool newTile = (left && center && !right) || (center && right && !left) || (left && !center && !right) || (right && !left && !center);
                newRow[j] = newTile;
                if (!newTile)
                {
                    answer++;
                }
            }
            row = newRow;
        }
        return answer;
    }

    public string Part1()
    {
        var answer = DoPuzzle(40);
        return answer.ToString();
    }

    public string Part2()
    {
        var answer = DoPuzzle(400000);
        return answer.ToString();
    }
}
