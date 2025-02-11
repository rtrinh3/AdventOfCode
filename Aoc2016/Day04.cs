using AocCommon;
using System.Text.RegularExpressions;

namespace Aoc2016;

// https://adventofcode.com/2016/day/4
// --- Day 4: Security Through Obscurity ---
public class Day04(string input) : IAocDay
{
    private record Room(string Name, int SectorId, string Checksum);

    private readonly Room[] realRooms = Parsing.SplitLines(input).Select(line =>
    {
        var match = Regex.Match(line, @"(.+)-(\d+)\[(.+)\]");
        string name = match.Groups[1].Value;
        int sectorId = int.Parse(match.Groups[2].ValueSpan);
        string checksum = match.Groups[3].Value;
        return new Room(name, sectorId, checksum);
    }).Where(room =>
    {
        var calcChecksum = room.Name
            .Where(c => c != '-')
            .GroupBy(c => c)
            .Select(g => (g.Key, Count: g.Count()))
            .OrderByDescending(g => g.Count)
            .ThenBy(g => g.Key)
            .Select(g => g.Key)
            .Take(room.Checksum.Length);
        bool verified = calcChecksum.SequenceEqual(room.Checksum);
        return verified;
    }).ToArray();

    public string Part1()
    {
        var answer = realRooms.Sum(x => x.SectorId);
        return answer.ToString();
    }

    public string Part2()
    {
        foreach (var room in realRooms)
        {
            int shift = room.SectorId % 26;
            string decrypted = string.Join("", room.Name.Select(c =>
            {
                if (c == '-')
                {
                    return ' ';
                }
                else
                {
                    return (char)(((c - 'a' + shift) % 26) + 'a');
                }
            }));
            if (decrypted.Contains("north"))
            {
                return room.SectorId.ToString();
            }
        }
        throw new Exception("Answer not found");
    }
}
