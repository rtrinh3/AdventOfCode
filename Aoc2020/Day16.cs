using AocCommon;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Aoc2020
{
    // https://adventofcode.com/2020/day/16
    // --- Day 16: Ticket Translation ---
    public class Day16 : IAocDay
    {
        private readonly (string Name, (long Min, long Max)[] Ranges)[] fields;
        private readonly long[] yourTicket;
        private readonly long[][] nearbyTickets;

        public Day16(string input)
        {
            var yourTicketSplit = input.ReplaceLineEndings("\n").Split("your ticket:");
            string inputFields = yourTicketSplit[0].Trim();
            var nearbyTicketsSplit = yourTicketSplit[1].Split("nearby tickets:");
            string inputYourTicket = nearbyTicketsSplit[0].Trim();
            string inputNearbyTickets = nearbyTicketsSplit[1].Trim();

            fields = inputFields.Split('\n')
                .Select(line =>
                {
                    var nameSplit = line.Split(':');
                    var name = nameSplit[0].Trim();
                    var ranges = Regex.Matches(nameSplit[1], @"(\d+)-(\d+)").Cast<Match>()
                        .Select(m => (long.Parse(m.Groups[1].ValueSpan), long.Parse(m.Groups[2].ValueSpan)))
                        .Order()
                        .ToArray();
                    return (name, ranges);
                }).ToArray();

            yourTicket = inputYourTicket.Split(',').Select(long.Parse).ToArray();

            nearbyTickets = inputNearbyTickets.Split('\n')
                .Select(line => line.Split(',').Select(long.Parse).ToArray())
                .ToArray();
        }

        public long Part1()
        {
            var fieldRanges = fields.SelectMany(f => f.Ranges).Order().ToList();
            var nearbyValues = nearbyTickets.SelectMany(x => x);
            long answer = nearbyValues.Where(v => !fieldRanges.Any(r => r.Min <= v && v <= r.Max)).Sum();

            return answer;
        }

        public long Part2()
        {
            var fieldOrder = FindFieldOrder();
            long answer = 1L;
            for (int i = 0; i < fieldOrder.Length; i++)
            {
                if (fieldOrder[i].StartsWith("departure"))
                {
                    answer *= yourTicket[i];
                }
            }
            return answer;
        }

        public string[] FindFieldOrder()
        {
            var fieldRanges = fields.SelectMany(f => f.Ranges).Order().ToList();
            List<long[]> validTickets = nearbyTickets
                .Where(ticket =>
                    ticket.All(value =>
                        fieldRanges.Any(range => range.Min <= value && value <= range.Max)
                    )
                )
                .Append(yourTicket)
                .ToList();
            long[][] validTicketsTranspose = Enumerable.Range(0, validTickets[0].Length)
                .Select(i => validTickets.Select(ticket => ticket[i]).ToArray())
                .ToArray();

            Func<int, int, bool> ValuesFitField = Memoization.Make((int candidateFieldIndex, int ticketFieldIndex) =>
            {
                var values = validTicketsTranspose[ticketFieldIndex];
                var ranges = fields[candidateFieldIndex].Ranges;
                return values.All(v => ranges.Any(r => r.Min <= v && v <= r.Max));
            });

            Func<ulong, int[]?> FindFieldOrderImpl = _ => throw new NotImplementedException();
            FindFieldOrderImpl = Memoization.Make((ulong unassignedFields) =>
            {
                int unassignedFieldsCount = BitOperations.PopCount(unassignedFields);
                if (unassignedFieldsCount == 0)
                {
                    return Array.Empty<int>();
                }
                int fieldIndex = fields.Length - unassignedFieldsCount;
                for (int candidateField = 0; candidateField < fields.Length; candidateField++)
                {
                    ulong candidateFieldMask = 1UL << candidateField;
                    if (0 != (unassignedFields & candidateFieldMask))
                    {
                        if (ValuesFitField(candidateField, fieldIndex))
                        {
                            var nextFields = unassignedFields & ~candidateFieldMask;
                            var nextOrder = FindFieldOrderImpl(nextFields);
                            if (nextOrder != null)
                            {
                                return nextOrder.Prepend(candidateField).ToArray();
                            }
                        }
                    }
                }
                return null;
            });
            var answerIndexes = FindFieldOrderImpl((1UL << fields.Length) - 1);
            return answerIndexes.Select(i => fields[i].Name).ToArray();
        }
    }
}
