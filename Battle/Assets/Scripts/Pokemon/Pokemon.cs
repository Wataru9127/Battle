using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポケモン本体の情報
/// </summary>
public class Pokemon : IPokemon
{
    //名前
    public string Name { get; private set; }

    //レベル
    public int Level { get; private set; }

    //タイプ1
    public PokemonType FirstType { get; private set; }

    //タイプ2
    public PokemonType SecondType { get; private set; }

    //状態異常
    public PokemonStatus Status { get; private set; }

    //せいかく
    public Nature Nature { get; private set; }

    //種族値
    private Dictionary<PokemonAbility, int> baseStats;

    //個体値
    private Dictionary<PokemonAbility, int> individual;

    //努力値
    private Dictionary<PokemonAbility, int> effort;

    //実数値
    private Dictionary<PokemonAbility, int> current;

    //現在のHP
    private int hp;

    //ステータス計算
    public Pokemon(string name, int level, PokemonType primary, PokemonType secondary, Nature nature, Dictionary<PokemonAbility, int> baseStats, Dictionary<PokemonAbility, int> individual = null, Dictionary<PokemonAbility, int> effort = null)
    {
        Name = name;
        Level = level;
        FirstType = primary;
        SecondType = secondary;
        Nature = nature;
        this.baseStats = baseStats;
        this.individual = individual ?? new Dictionary<PokemonAbility, int>();
        this.effort = effort ?? new Dictionary<PokemonAbility, int>();
        current = new Dictionary<PokemonAbility, int>();

        foreach (PokemonAbility stat in System.Enum.GetValues(typeof(PokemonAbility)))
        {
            int baseValue = baseStats.ContainsKey(stat) ? baseStats[stat] : 0;
            int indi = this.individual.ContainsKey(stat) ? this.individual[stat] : 0;
            int effo = this.effort.ContainsKey(stat) ? this.effort[stat] : 0;

            current[stat] = CalculateStat(stat, baseValue, indi, effo, level, nature);
        }

        hp = current[PokemonAbility.HP];
        Status = PokemonStatus.None;
    }

    /// <summary>
    /// 実数値計算関数
    /// </summary>
    private int CalculateStat(PokemonAbility ability, int baseValue, int indi, int effo, int level, Nature nature)
    {
        //H =（種族値*2 ＋ 個体値 ＋ 努力値/4）* レベル/100 ＋ レベル＋10
        if (ability == PokemonAbility.HP)
        {
            return Mathf.FloorToInt(((baseValue * 2 + indi + (effo / 4f)) * (level / 100) + level + 10));
        }
        //ABCDS = ｛（種族値*2 ＋ 個体値 ＋ 努力値/4）* レベル/100 ＋ 5｝* 性格補正(1.1 or 0.9 or 1.0)
        else
        {
            float value = ((baseValue * 2 + indi + (effo / 4)) * (level / 100)) + 5;
            return Mathf.FloorToInt(value * nature.GetModifier(ability));
        }
    }

    /// <summary>
    /// 実数値の取得
    /// </summary>
    public int GetActual(PokemonAbility ability)
    {
        return current[ability];
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
    }

    /// <summary>
    /// 状態異常を受ける
    /// </summary>
    public void TakeStatus(PokemonStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// きぜつ判定
    /// </summary>
    public bool IsFainted() => hp <= 0;
}
