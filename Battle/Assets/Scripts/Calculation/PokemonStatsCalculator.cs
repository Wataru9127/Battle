using Battle;
using Field;
using System.Collections.Generic;
using UnityEngine;
using static PokemonStatType;

/// <summary>
/// 実数値計算専用クラス
/// </summary>
public static class PokemonStatsCalculator
{
    /// <summary>
    /// フォルム・IV・EV・性格・レベルを考慮した実数値
    /// </summary>
    public static int CalculateBaseStat(BattlePokemon pokemon, PokemonStatType statType)
    {
        var growth = pokemon.PokemonData.Growth;
        var baseStats = pokemon.GetBaseStats();
        int level = growth.Level;

        if (statType == HP)
        {
            return CalculateHP(
                baseStats.HP,
                growth.IV.individual[HP],
                growth.EV.effort[HP],
                level
            );
        }

        return CalculateOther(
            baseStats.GetBaseStats(statType),
            growth.IV.individual[statType],
            growth.EV.effort[statType],
            level,
            growth.Nature.GetModifier(statType)
        );
    }

    /// <summary>
    /// HPの実数値計算
    /// H = floor(((種族値×2 + 個体値 + 努力値/4) × レベル / 100) + レベル + 10)
    /// </summary>
    private static int CalculateHP(int baseStat, int iv, int ev, int level)
    {
        float value = ((baseStat * 2f + iv + ev / 4f) * level / 100f) + level + 10f;

        return Mathf.FloorToInt(value);
    }

    /// <summary>
    /// HP以外の実数値計算
    /// ABCDS = floor((floor((種族値×2 + 個体値 + 努力値/4) × レベル / 100) + 5) × 性格補正)
    /// </summary>
    private static int CalculateOther(int baseStat, int iv, int ev, int level, float natureModifier)
    {
        float baseValue = (baseStat * 2f + iv + ev / 4f) * level / 100f;

        float value = Mathf.Floor(baseValue + 5f) * natureModifier;

        return Mathf.FloorToInt(value);
    }

    /// <summary>
    /// 能力ランク補正
    /// ・ランク >= 0  => 実数値 × (2 + ランク) / 2
    /// ・ランク < 0   => 実数値 × 2 / (2 - ランク)
    /// ただし、HP・命中率はのぞく
    /// </summary>
    private static int ApplyRank(int stat, int rank)
    {
        if (rank >= 0) return Mathf.FloorToInt(stat * (2f + rank) / 2f);

        return Mathf.FloorToInt(stat * 2f / (2f - rank));
    }

    private static int ApplyStatsModifier(int value, BattleContext context, BattlePokemon pokemon, PokemonStatType statType)
    {
        // まひ
        if (statType == PokemonStatType.Speed)
        {
            value = Mathf.FloorToInt(
                value * pokemon.Condition.GetSpeedModifier()
            );
        }
        return value;
    }

    private static int ApplyFieldModifier(int value, BattleContext context, BattlePokemon pokemon, PokemonStatType statType)
    {
        // フィールドが無ければそのまま
        if (context.FieldManager == null ||
            context.FieldManager.Current == null)
        {
            return value;
        }

        float rate = context.FieldManager.Current.GetStatModifier(
            context,
            pokemon,
            statType
        );

        return Mathf.FloorToInt(value * rate);
    }

    /// <summary>
    /// バトル中の現在値の取得
    /// </summary>
    public static int CalculateStat(BattleContext context, BattlePokemon pokemon, PokemonStatType statType)
    {
        //1.フォルム考慮済み実数値
        int value = CalculateBaseStat(pokemon, statType);

        //2. ランク
        value = ApplyRank(value, pokemon.Condition.GetRank(statType));

        //3. 状態異常
        value = ApplyStatsModifier(value, context, pokemon, statType);

        //4. 特性
        value = AbilityCalculator.ApplyAbilityModifier(value, context, pokemon, statType);

        //5. フィールド・天候
        value = ApplyFieldModifier(value, context, pokemon, statType);

        //6. 最低値の保証
        return Mathf.Max(1, value);
    }
}
