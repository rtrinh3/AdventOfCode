using System.Diagnostics;

namespace Aoc2016;

// https://adventofcode.com/2016/day/19
// --- Day 19: An Elephant Named Joseph ---
public class Day19(string input) : AocCommon.IAocDay
{
    private class Elf
    {
        public int Number;
        public Elf? Next;
    }

    public string Part1()
    {
        // Build circle of elves
        int numberOfElves = int.Parse(input);
        Elf firstElf = new Elf() { Number = 1 };
        Elf elf = firstElf;
        for (int i = 2; i <= numberOfElves; i++)
        {
            Elf newElf = new Elf() { Number = i };
            elf.Next = newElf;
            elf = newElf;
        }
        elf.Next = firstElf;
        // Play game
        elf = firstElf;
        while (numberOfElves > 1)
        {
            elf.Next = elf.Next.Next;
            elf = elf.Next;
            numberOfElves--;
        }
        var answer = elf.Number;
        return answer.ToString();
    }

    public string Part2()
    {
        // Build circle of elves
        int initialNumberOfElves = int.Parse(input);
        int numberOfElves = initialNumberOfElves;
        List<int> elves = Enumerable.Range(1, initialNumberOfElves).ToList();
        // Play game
        Task.Run(() =>
        {
            var timer = Stopwatch.StartNew();
            Thread.Sleep(1000);
            while (numberOfElves > 1)
            {
                var current = numberOfElves;
                int done = initialNumberOfElves - current;
                Console.WriteLine($"Done {done}, Remaining {current}, ETA {timer.Elapsed * current / done}");
                Thread.Sleep(1000);
            }
        });
        int index = 0;
        while (numberOfElves > 1)
        {
            int oppositeIndex = (index + numberOfElves / 2) % numberOfElves;
            elves.RemoveAt(oppositeIndex);
            numberOfElves--;
            if (oppositeIndex < index)
            {
                index--;
            }
            index = (index + 1) % numberOfElves;
        }
        return elves.Single().ToString();
    }
}
