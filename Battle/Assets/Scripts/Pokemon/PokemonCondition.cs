using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HP・状態異常・能力ランク
/// </summary>
public class PokemonCondition
{
    //MaxHP
    public int MaxHP { get; }

    //現在のHP
    public int currentHP { get; private set; }

    //状態異常
    public PokemonStatus Status { get; private set; }

    //現在の能力ランク
    private Dictionary<PokemonStatType, int> rank;

    //外部出力用ログ
    public string Log;

    public bool IsFainted => currentHP <= 0;

    /// <summary>
    /// 初期化
    /// </summary>
    public PokemonCondition(int maxHP)
    {
        MaxHP = maxHP;
        currentHP = maxHP;
        Status = PokemonStatus.None;

        rank = new Dictionary<PokemonStatType, int>();
        foreach (PokemonStatType stat in System.Enum.GetValues(typeof(PokemonStatType)))
        {
            rank[stat] = 0;
        }
    }

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, MaxHP);
    }

    public void Heal(int heal)
    {
        currentHP = Mathf.Clamp(currentHP + heal, 0, MaxHP);
    }

    /// <summary>
    /// 状態異常を受ける
    /// ログも一緒に出力する
    /// </summary>
    public bool TakeStatus(PokemonStatus status)
    {
        if (Status != PokemonStatus.None)
        {
            return false;
        }
        Status = status;
        return true;
    }

    /// <summary>
    /// 能力ランクの取得
    /// </summary>
    public int GetRank(PokemonStatType stat) => rank[stat];

    /// <summary>
    /// 能力ランクの増減
    /// </summary>
    public void AddRank(PokemonStatType stat, int value)
    {
        rank[stat] = Mathf.Clamp(rank[stat] + value, -6, 6);
    }

    /// <summary>
    /// 能力ランクのリセット
    /// </summary>
    private void ResetRank()
    {
        foreach (var key in rank.Keys)
        {
            rank[key] = 0;
        }
    }

    /// <summary>
    /// 交代時に呼ぶ関数
    /// </summary>
    public void SwitchOut()
    {

    }
}
