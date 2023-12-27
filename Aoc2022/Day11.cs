using System.Diagnostics;
using System.Text.RegularExpressions;
using MathNet.Symbolics;

namespace Aoc2022
{
    // https://adventofcode.com/2022/day/11
    // I seem to have lost my original solution for this day, so let's reimplement it!
    public class Day11 : IAocDay
    {
        private readonly Monkey[] initialMonkeys;

        public Day11(string input)
        {
            var matches = Regex.Matches(input, @"Monkey (\d+):\s+Starting items: (.*)\s+Operation: new = (.*)\s+Test: divisible by (\d+)\s+If true: throw to monkey (\d+)\s+If false: throw to monkey (\d+)");
            initialMonkeys = new Monkey[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                string matchNumber = i.ToString();
                var match = matches[i];
                Debug.Assert(match.Groups[1].Value == matchNumber);
                initialMonkeys[i] = new Monkey(match);
            }
        }

        public string Part1()
        {
            static long worryManagement(long x) => x / 3;
            var answer = CalculateMonkeyBusiness(20, worryManagement);
            return answer.ToString();
        }

        public string Part2()
        {
            // https://en.wikipedia.org/wiki/Chinese_remainder_theorem
            // If we're looking for a number N knowing the remainders of its division by some divisors which are coprime,
            // this theorem states that the number N is unique modulo the product of the divisors.
            // Since the monkeys only check for divisibility and their divisors are all different prime numbers,
            // we only need to keep track of our worries modulo the product of these divisors.
            var commonMultiple = initialMonkeys.Select(m => m.divisibilityTest).Aggregate((a, b) => a * b);
            long worryManagement(long x) => x % commonMultiple;
            var answer = CalculateMonkeyBusiness(10000, worryManagement);
            return answer.ToString();
        }

        private long CalculateMonkeyBusiness(int rounds, Func<long, long> worryManagement)
        {
            Monkey[] monkeys = initialMonkeys.Select(m => new Monkey(m)).ToArray();
            long[] inspections = new long[monkeys.Length];
            for (int i = 0; i < rounds; i++)
            {
                for (int monkeyIndex = 0; monkeyIndex < monkeys.Length; monkeyIndex++)
                {
                    var thrownItems = monkeys[monkeyIndex].InspectItems(worryManagement);
                    inspections[monkeyIndex] += thrownItems.Count;
                    foreach (var (targetMonkey, item) in thrownItems)
                    {
                        monkeys[targetMonkey].Add(item);
                    }
                }
            }
            long monkeyBusiness = inspections.OrderDescending().Take(2).Aggregate((a, b) => a * b);
            return monkeyBusiness;
        }

        private class Monkey
        {
            private readonly Queue<long> items;
            private readonly SymbolicExpression operation;
            public readonly int divisibilityTest;
            private readonly int goodMonkey;
            private readonly int badMonkey;

            public Monkey(Match match)
            {
                items = new(match.Groups[2].Value.Split(',').Select(long.Parse));
                operation = SymbolicExpression.Parse(match.Groups[3].Value);
                divisibilityTest = int.Parse(match.Groups[4].Value);
                goodMonkey = int.Parse(match.Groups[5].Value);
                badMonkey = int.Parse(match.Groups[6].Value);
            }

            public Monkey(Monkey other)
            {
                items = new(other.items);
                operation = other.operation;
                divisibilityTest = other.divisibilityTest;
                goodMonkey = other.goodMonkey;
                badMonkey = other.badMonkey;
            }

            private long DoOperation(long item)
            {
                var values = new Dictionary<string, FloatingPoint>
                {
                    ["old"] = item
                };
                var newValue = operation.Evaluate(values).RealValue;
                var newValueLong = (long)Math.Round(newValue);
                return newValueLong;
            }

            public List<(int monkey, long item)> InspectItems(Func<long, long> worryManagement)
            {
                List<(int monkey, long item)> thrownItems = new();
                while (items.TryDequeue(out var item))
                {
                    var initialWorry = DoOperation(item);
                    var worry = worryManagement(initialWorry);
                    if (worry % divisibilityTest == 0)
                    {
                        thrownItems.Add((goodMonkey, worry));
                    }
                    else
                    {
                        thrownItems.Add((badMonkey, worry));
                    }
                }
                return thrownItems;
            }

            public void Add(long item)
            {
                items.Enqueue(item);
            }
        }
    }
}
