using AocCommon;
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

            Func<EquatableArray<string>, string[]?> FindFieldOrderImpl = _ => throw new NotImplementedException();
            FindFieldOrderImpl = Memoization.Make((EquatableArray<string> unassignedFields) =>
            {
                if (unassignedFields.Count == 0)
                {
                    return Array.Empty<string>();
                }
                int fieldIndex = fields.Length - unassignedFields.Count;
                var values = validTicketsTranspose[fieldIndex];
                foreach (string field in unassignedFields)
                {
                    var ranges = fields.First(f => f.Name == field).Ranges;
                    if (values.All(v => ranges.Any(r => r.Min <= v && v <= r.Max)))
                    {
                        var nextFields = unassignedFields.Remove(field);
                        var nextOrder = FindFieldOrderImpl(nextFields);
                        if (nextOrder != null)
                        {
                            return nextOrder.Prepend(field).ToArray();
                        }
                    }
                }
                return null;
            });
            return FindFieldOrderImpl(new EquatableArray<string>(fields.Select(f => f.Name)));
        }
    }
}
