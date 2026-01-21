using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HP・状態異常・能力ランク
/// </summary>
public class PokemonCondition
{
    //状態異常
    public PokemonStatus Status { get; private set; }

    //現在のHP
    public int currentHP { get; private set; }

    //現在の能力ランク
    private Dictionary<PokemonAbility, int> rank;

    /// <summary>
    /// 初期化
    /// </summary>
    public PokemonCondition(int maxHP)
    {
        currentHP = maxHP;
        Status = PokemonStatus.None;
        rank = new Dictionary<PokemonAbility, int>();
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
    }

    /// <summary>
    /// 状態異常を受ける
    /// </summary>
    public void TakeStatus(PokemonStatus status)
    {
        if (Status == PokemonStatus.None)
            Status = status;
    }

    /// <summary>
    /// 能力ランクの取得
    /// </summary>
    public int GetRank(PokemonAbility ability) => rank.ContainsKey(ability) ? rank[ability] : 0;
}
