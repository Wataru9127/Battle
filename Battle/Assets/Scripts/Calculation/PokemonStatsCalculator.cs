using System;
using System.Collections.Generic;
using UnityEngine;

public static class PokemonStatsCalculator
{
    public static Dictionary<PokemonAbility, int> Calculate(PokemonGrowth growth)
    {
        var result = new Dictionary<PokemonAbility, int>();

        foreach (PokemonAbility ability in Enum.GetValues(typeof(PokemonAbility)))
        {
            int baseStat = growth.BaseStats.GetBaseStats(ability);
            int iv = growth.IV.GetIndividual(ability);
            int ev = growth.EV.GetEffort(ability);
            int level = growth.Level;

            if (ability == PokemonAbility.HP)
            {
                result[ability] = CalculateHP(baseStat, iv, ev, level);
            }
            else
            {
                float natureModifier = growth.Nature.GetModifier(ability);
                result[ability] = CalculateOther(
                    baseStat,
                    iv,
                    ev,
                    level,
                    natureModifier
                );
            }
        }

        return result;
    }

    /// <summary>
    /// HP‚ÌÀ”’lŒvZ
    /// H = floor(((í‘°’l~2 + ŒÂ‘Ì’l + “w—Í’l/4) ~ ƒŒƒxƒ‹ / 100) + ƒŒƒxƒ‹ + 10)
    /// </summary>
    private static int CalculateHP(int baseStat, int iv, int ev, int level)
    {
        float value =
            ((baseStat * 2f + iv + ev / 4f) * level / 100f)
            + level
            + 10f;

        return Mathf.FloorToInt(value);
    }

    /// <summary>
    /// HPˆÈŠO‚ÌÀ”’lŒvZ
    /// ABDSC = floor((floor((í‘°’l~2 + ŒÂ‘Ì’l + “w—Í’l/4) ~ ƒŒƒxƒ‹ / 100) + 5) ~ «Ši•â³)
    /// </summary>
    private static int CalculateOther(
        int baseStat,
        int iv,
        int ev,
        int level,
        float natureModifier)
    {
        // ¦ float ‚Å³‚µ‚­ŒvZ‚·‚é
        float baseValue =
            (baseStat * 2f + iv + ev / 4f) * level / 100f;

        float value = Mathf.Floor(baseValue + 5f) * natureModifier;

        return Mathf.FloorToInt(value);
    }
}
