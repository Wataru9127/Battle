using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

/// <summary>
/// 実数値
/// </summary>
public sealed class PokemonStats
{
    //実数値
    public Dictionary<PokemonStatType, int> Stats { get; private set; }

    public PokemonStats(Dictionary<PokemonStatType, int> dictionary)
    {
        Stats = dictionary;
    }

    /// <summary>
    /// 指定の実数値を取得する関数
    /// </summary>
    public int GetStats(PokemonStatType stats)
    {
        return Stats[stats];
    }
}
