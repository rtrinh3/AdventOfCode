﻿using AocCommon;

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
        Dictionary<(sbyte, sbyte, sbyte, sbyte), sbyte>[] monkeySales = new Dictionary<(sbyte, sbyte, sbyte, sbyte), sbyte>[initialSecretNumbers.Length];
        Parallel.For(0, initialSecretNumbers.Length, monkey =>
        {
            var numbers = GenerateSecretNumbers(initialSecretNumbers[monkey]).Take(2000 + 1).Select(n => (sbyte)(n % 10)).ToList();
            Dictionary<(sbyte, sbyte, sbyte, sbyte), sbyte> sales = new();
            for (int i = 0; i < 2000 + 1 - 4; i++)
            {
                var changes = (
                    (sbyte)(numbers[i + 1] - numbers[i + 0]),
                    (sbyte)(numbers[i + 2] - numbers[i + 1]),
                    (sbyte)(numbers[i + 3] - numbers[i + 2]),
                    (sbyte)(numbers[i + 4] - numbers[i + 3])
                    );
                sales.TryAdd(changes, numbers[i + 4]);
                // If the key already exists, TryAdd does nothing and returns false.
            }
            monkeySales[monkey] = sales;
        });

        // Find max haul
        var allSequences = monkeySales.SelectMany(s => s.Keys).Distinct().ToList();
        long maxHaul = 0;
        Parallel.ForEach(allSequences, sequence =>
        {
            long haul = 0;
            foreach (var monkey in monkeySales)
            {
                haul += monkey.GetValueOrDefault(sequence);
            }
            lock (this)
            {
                maxHaul = Math.Max(maxHaul, haul);
            }
        });
        return maxHaul.ToString();
    }
}