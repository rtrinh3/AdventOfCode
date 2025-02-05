using AocCommon;
using System.Collections.Generic;

namespace Aoc2015;

// https://adventofcode.com/2015/day/22
// --- Day 22: Wizard Simulator 20XX ---
public class Day22 : IAocDay
{
    private readonly int initialBossHp;
    private readonly int bossDamage;

    public Day22(string input)
    {
        var bossData = Parsing.IntsPositive(input);
        initialBossHp = bossData[0];
        bossDamage = bossData[1];
    }

    private record BattleState(
        int PlayerHp,
        int Mana,
        int ShieldEffect,
        int PoisonEffect,
        int RechargeEffect,
        int BossHp)
    {
        internal BattleState ApplyEffects()
        {
            var (_, mana, shieldEffect, poisonEffect, rechargeEffect, bossHp) = this;
            if (shieldEffect > 0)
            {
                shieldEffect--;
            }
            if (poisonEffect > 0)
            {
                bossHp -= 3;
                poisonEffect--;
            }
            if (rechargeEffect > 0)
            {
                mana += 101;
                rechargeEffect--;
            }
            return new BattleState(this.PlayerHp, mana, shieldEffect, poisonEffect, rechargeEffect, bossHp);
        }

        internal BattleState BossAttack(int damage)
        {
            var afterBossEffects = ApplyEffects();
            if (afterBossEffects.BossHp <= 0)
            {
                return afterBossEffects;
            }
            else
            {
                int finalDamage = damage - (ShieldEffect > 0 ? 7 : 0);
                return afterBossEffects with
                {
                    PlayerHp = afterBossEffects.PlayerHp
                    - Math.Max(0, finalDamage)
                };
            }
        }
    }

    private List<(BattleState, int)> BattleChoices(BattleState state)
    {
        List<(BattleState, int)> choices = new();
        var afterPlayerEffects = state.ApplyEffects();
        if (afterPlayerEffects.BossHp <= 0)
        {
            choices.Add((afterPlayerEffects, 0));
            return choices;
        }
        // Magic Missile
        const int MAGIC_MISSILE_MANA = 53;
        if (afterPlayerEffects.Mana >= MAGIC_MISSILE_MANA)
        {
            var afterAttack = afterPlayerEffects with
            {
                Mana = afterPlayerEffects.Mana - MAGIC_MISSILE_MANA,
                BossHp = afterPlayerEffects.BossHp - 4
            };
            if (afterAttack.BossHp <= 0)
            {
                choices.Add((afterAttack, MAGIC_MISSILE_MANA));
            }
            else
            {
                var afterBoss = afterAttack.BossAttack(bossDamage);
                choices.Add((afterBoss, MAGIC_MISSILE_MANA));
            }
        }
        // Drain
        const int DRAIN_MANA = 73;
        if (afterPlayerEffects.Mana >= DRAIN_MANA)
        {
            var afterAttack = afterPlayerEffects with
            {
                Mana = afterPlayerEffects.Mana - DRAIN_MANA,
                PlayerHp = afterPlayerEffects.PlayerHp + 2,
                BossHp = afterPlayerEffects.BossHp - 2
            };
            if (afterAttack.BossHp <= 0)
            {
                choices.Add((afterAttack, DRAIN_MANA));
            }
            else
            {
                var afterBoss = afterAttack.BossAttack(bossDamage);
                choices.Add((afterBoss, DRAIN_MANA));
            }
        }
        // Shield
        const int SHIELD_MANA = 113;
        if (afterPlayerEffects.Mana >= SHIELD_MANA && afterPlayerEffects.ShieldEffect <= 0)
        {
            var afterSpell = afterPlayerEffects with
            {
                Mana = afterPlayerEffects.Mana - SHIELD_MANA,
                ShieldEffect = 6
            };
            var afterBoss = afterSpell.BossAttack(bossDamage);
            choices.Add((afterBoss, SHIELD_MANA));
        }
        // Poison
        const int POISON_MANA = 173;
        if (afterPlayerEffects.Mana >= POISON_MANA && afterPlayerEffects.PoisonEffect <= 0)
        {
            var afterSpell = afterPlayerEffects with
            {
                Mana = afterPlayerEffects.Mana - POISON_MANA,
                PoisonEffect = 6
            };
            var afterBoss = afterSpell.BossAttack(bossDamage);
            choices.Add((afterBoss, POISON_MANA));
        }
        // Recharge
        const int RECHARGE_MANA = 229;
        if (afterPlayerEffects.Mana >= RECHARGE_MANA && afterPlayerEffects.RechargeEffect <= 0)
        {
            var afterSpell = afterPlayerEffects with
            {
                Mana = afterPlayerEffects.Mana - RECHARGE_MANA,
                RechargeEffect = 5
            };
            var afterBoss = afterSpell.BossAttack(bossDamage);
            choices.Add((afterBoss, RECHARGE_MANA));
        }

        choices.RemoveAll(s => s.Item1.PlayerHp <= 0);
        return choices;
    }

    public int CostToWinBattle(int initialPlayerHp, int initialMana)
    {
        BattleState initialState = new(initialPlayerHp, initialMana, 0, 0, 0, initialBossHp);
        var result = GraphAlgos.DijkstraToEnd(initialState, BattleChoices, s => s.BossHp <= 0);
        return result.distance;
    }

    public string Part1()
    {
        var manaCost = CostToWinBattle(50, 500);
        return manaCost.ToString();
    }

    private IEnumerable<(BattleState, int)> HardBattleChoices(BattleState state)
    {
        var hardMode = state with { PlayerHp = state.PlayerHp - 1 };
        if (hardMode.PlayerHp <= 0)
        {
            return [];
        }
        else
        {
            return BattleChoices(hardMode);
        }
    }
    private int CostToWinHardBattle(int initialPlayerHp, int initialMana)
    {
        BattleState initialState = new(initialPlayerHp, initialMana, 0, 0, 0, initialBossHp);
        var result = GraphAlgos.DijkstraToEnd(initialState, HardBattleChoices, s => s.BossHp <= 0);
        return result.distance;
    }

    public string Part2()
    {
        var manaCost = CostToWinHardBattle(50, 500);
        return manaCost.ToString();
    }
}
