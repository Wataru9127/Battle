using Calculator;
using System.Collections.Generic;
using UnityEngine;
using static PokemonStatType;

/// <summary>
/// 実数値計算専用クラス
/// </summary>
public static class PokemonStatsCalculator
{
    /// <summary>
    /// 実数値の計算
    /// </summary>
    public static PokemonStats Calculate(PokemonGrowth growth)
    {
        //引数用のDictionary
        Dictionary< PokemonStatType, int> keyValues = new Dictionary< PokemonStatType, int>();

        int level = growth.Level;

        //それぞれ値を取得
        int hp = CalculateHP(
            growth.BaseStats.HP,
            growth.IV.individual[PokemonStatType.HP],
            growth.EV.effort[PokemonStatType.HP],
            level
        );

        int attack = CalculateOther(
            growth.BaseStats.Attack,
            growth.IV.individual[PokemonStatType.Attack],
            growth.EV.effort[PokemonStatType.Attack],
            level,
            growth.Nature.GetModifier(PokemonStatType.Attack)
        );

        int defense = CalculateOther(
            growth.BaseStats.Defense,
            growth.IV.individual[PokemonStatType.Defense],
            growth.EV.effort[PokemonStatType.Defense],
            level,
            growth.Nature.GetModifier(PokemonStatType.Defense)
        );

        int specialAttack = CalculateOther(
            growth.BaseStats.SpecialAttack,
            growth.IV.individual[PokemonStatType.SpecialAttack],
            growth.EV.effort[PokemonStatType.SpecialAttack],
            level,
            growth.Nature.GetModifier(PokemonStatType.SpecialAttack)
        );

        int specialDefense = CalculateOther(
            growth.BaseStats.SpecialDefense,
            growth.IV.individual[PokemonStatType.SpecialDefense],
            growth.EV.effort[PokemonStatType.SpecialDefense],
            level,
            growth.Nature.GetModifier(PokemonStatType.SpecialDefense)
        );

        int speed = CalculateOther(
            growth.BaseStats.Speed,
            growth.IV.individual[PokemonStatType.Speed],
            growth.EV.effort[PokemonStatType.Speed],
            level,
            growth.Nature.GetModifier(PokemonStatType.Speed)
        );

        //追加
        keyValues.Add(PokemonStatType.HP, hp);
        keyValues.Add(PokemonStatType.Attack, attack);
        keyValues.Add(PokemonStatType.Defense, defense);
        keyValues.Add(PokemonStatType.SpecialAttack, specialAttack);
        keyValues.Add(PokemonStatType.SpecialDefense, specialDefense);
        keyValues.Add(PokemonStatType.Speed, speed);

        return new PokemonStats(keyValues);
    }

    /// <summary>
    /// HPの実数値計算
    /// H = floor(((種族値×2 + 個体値 + 努力値/4) × レベル / 100) + レベル + 10)
    /// </summary>
    private static int CalculateHP(int baseStat, int iv, int ev, int level)
    {
        float value =
            ((baseStat * 2f + iv + ev / 4f) * level / 100f) + level + 10f;

        return Mathf.FloorToInt(value);
    }

    /// <summary>
    /// HP以外の実数値計算
    /// ABDSC = floor((floor((種族値×2 + 個体値 + 努力値/4) × レベル / 100) + 5) × 性格補正)
    /// </summary>
    private static int CalculateOther(int baseStat, int iv, int ev, int level, float natureModifier)
    {
        float baseValue =
            (baseStat * 2f + iv + ev / 4f) * level / 100f;

        float value = Mathf.Floor(baseValue + 5f) * natureModifier;

        return Mathf.FloorToInt(value);
    }

    /// <summary>
    /// 能力ランク込みの実数値の取得
    /// ・ランク >= 0  => 実数値 × (2 + ランク) / 2
    /// ・ランク < 0   => 実数値 × 2 / (2 - ランク)
    /// ただし、HP・命中率はのぞく
    /// </summary>
    public static int CalculateInRank(BattlePokemon pokemon, PokemonStatType statType)
    {
        //現在の能力ランクを取得
        int rank = pokemon.Condition.GetRank(statType);

        //実数値を取得
        int stat = pokemon.PokemonData.Stats.GetStatsValue(statType);

        //ランク補正をかける
        if (rank >= 0)
        {
            return Mathf.FloorToInt(stat * (2f + rank) / 2f);
        }
        else
        {
            return Mathf.FloorToInt(stat * 2f / (2f - rank));
        }
    }
}
