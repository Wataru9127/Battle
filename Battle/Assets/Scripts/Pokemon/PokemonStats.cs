using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

/// <summary>
/// 実数値
/// </summary>
public sealed class PokemonStats
{
    //実数値
    private readonly Dictionary<PokemonAbility, int> stats;

    public PokemonStats(Dictionary<PokemonAbility, int> stats)
    {
        this.stats = stats;
    }

    /// <summary>
    /// 指定の実数値を取得する関数
    /// </summary>
    public int GetStats(PokemonAbility ability)
    {
        return stats.TryGetValue(ability, out var value) ? value : 0;
    }
}
