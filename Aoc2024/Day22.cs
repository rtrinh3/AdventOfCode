using AocCommon;
using System.Collections.Concurrent;

namespace Aoc2024;

// https://adventofcode.com/2024/day/22
// --- Day 22: Monkey Market ---
public class Day22(string input) : IAocDay
{
    private readonly long[] initialSecretNumbers = input.TrimEnd().ReplaceLineEndings("\n").Split('\n').Select(long.Parse).ToArray();

    public string Part1()
    {
        long answer = 0;
        foreach (var secret in initialSecretNumbers)
        {
            var number = GenerateSecretNumbers(secret).ElementAt(2000);
            answer += number;
        }
        return answer.ToString();
    }

    private static IEnumerable<long> GenerateSecretNumbers(long initialSecret)
    {
        var number = initialSecret;
        while (true)
        {
            yield return number;
            number = ((number * 64) ^ number) % 16777216;
            number = ((number / 32) ^ number) % 16777216;
            number = ((number * 2048) ^ number) % 16777216;
        }
    }

    public string Part2()
    {
        // Get sequences of changes
        const int OFFSET = 9;
        const int BASE = 19;
        static int FlattenChanges(int a, int b, int c, int d) => ((a + OFFSET) * BASE * BASE * BASE) + ((b + OFFSET) * BASE * BASE) + ((c + OFFSET) * BASE) + (d + OFFSET);
        sbyte[,] monkeySales = new sbyte[initialSecretNumbers.Length, BASE * BASE * BASE * BASE]; // initialized to 0
        bool[,] monkeySalesSet = new bool[initialSecretNumbers.Length, BASE * BASE * BASE * BASE]; // initialized to false
        ConcurrentDictionary<int, bool> allSequencesBag = new();
        Parallel.For(0, initialSecretNumbers.Length, monkey =>
        {
            var numbers = GenerateSecretNumbers(initialSecretNumbers[monkey]).Take(2000 + 1).Select(n => (sbyte)(n % 10)).ToList();
            for (int i = 0; i < 2000 + 1 - 4; i++)
            {
                var changes = FlattenChanges(
                    numbers[i + 1] - numbers[i + 0],
                    numbers[i + 2] - numbers[i + 1],
                    numbers[i + 3] - numbers[i + 2],
                    numbers[i + 4] - numbers[i + 3]
                    );
                if (!monkeySalesSet[monkey, changes])
                {
                    monkeySales[monkey, changes] = numbers[i + 4];
                    monkeySalesSet[monkey, changes] = true;
                    allSequencesBag.TryAdd(changes, true);
                }
            }
        });

        // Find max haul
        var allSequences = allSequencesBag.Keys;
        long maxHaul = 0;
        Parallel.ForEach(allSequences, sequence =>
        {
            long haul = 0;
            for (int monkey = 0; monkey < initialSecretNumbers.Length; monkey++)
            {
                haul += monkeySales[monkey, sequence];
            }
            lock (this)
            {
                maxHaul = Math.Max(maxHaul, haul);
            }
        });
        return maxHaul.ToString();
    }
}
