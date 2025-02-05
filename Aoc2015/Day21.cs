using AocCommon;

namespace Aoc2015;

// https://adventofcode.com/2015/day/21
// --- Day 21: RPG Simulator 20XX ---
public class Day21(string input) : IAocDay
{
    public record Combatant(int HitPoints, int Damage, int Armor);

    private record Equipment(int Cost, int Damage, int Armor)
    {
        public Equipment Combine(Equipment other) => new Equipment(Cost + other.Cost, Damage + other.Damage, Armor + other.Armor);
    }

    public string Part1()
    {
        var stats = Parsing.IntsPositive(input);
        var boss = new Combatant(stats[0], stats[1], stats[2]);

        Equipment[] weaponShop =
        {
            new(8, 4, 0), // Dagger
            new(10, 5, 0), // Shortsword
            new(25, 6, 0), // Warhammer
            new(40, 7, 0), // Longsword
            new(74, 8, 0), // Greataxe
        };
        Equipment[] armorShop =
        {
            new(13, 0, 1), // Leather
            new(31, 0, 2), // Chainmail
            new(53, 0, 3), // Splintmail
            new(75, 0, 4), // Bandedmail
            new(102, 0, 5), // Platemail
        };
        Equipment[] ringShop =
        {
            new(25, 1, 0), // Damage +1
            new(50, 2, 0), // Damage +2
            new(100, 3, 0), // Damage +3
            new(20, 0, 1), // Defense +1
            new(40, 0, 2), // Defense +2
            new(80, 0, 3), // Defense +3
        };

        var weaponArmor =
            from w in weaponShop
            from a in armorShop
            select w.Combine(a);
        var weaponArmorChoices = weaponShop.Concat(weaponArmor);
        var oneRing =
            from wa in weaponArmorChoices
            from r in ringShop
            select wa.Combine(r);
        var ringPairs =
            from a in ringShop
            from b in ringShop
            where a != b
            select a.Combine(b);
        var twoRings =
            from wa in weaponArmorChoices
            from rp in ringPairs
            select wa.Combine(rp);
        var loadouts =
            weaponArmorChoices
            .Concat(oneRing)
            .Concat(twoRings)
            .OrderBy(x => x.Cost)
            .ToArray();
        foreach (var l in loadouts)
        {
            var player = new Combatant(100, l.Damage, l.Armor);
            var fight = SimulateCombat(player, boss);
            if (fight == 0)
            {
                return l.Cost.ToString();
            }
        }
        throw new Exception("No answer found");
    }

    public static int SimulateCombat(Combatant player, Combatant boss)
    {
        int playerHp = player.HitPoints;
        int bossHp = boss.HitPoints;
        while (true)
        {
            // Player attacks!
            bossHp -= player.Damage - boss.Armor;
            if (bossHp <= 0)
            {
                return 0;
            }
            // Boss attacks!
            playerHp -= boss.Damage - player.Armor;
            if (playerHp <= 0)
            {
                return 1;
            }
        }
    }

    public string Part2()
    {
        throw new NotImplementedException();
    }
}
