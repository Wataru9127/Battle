using Common;
using System.Collections.Generic;

/// <summary>
/// 実数値
/// </summary>
public sealed class PokemonStats
{
    //実数値
    public IReadOnlyDictionary<PokemonStatType, int> Stats { get; private set; }

    public PokemonStats(Dictionary<PokemonStatType, int> dictionary)
    {
        Stats = dictionary;
    }

    /// <summary>
    /// 実数値の取得
    /// </summary>
    /// <param name="pokemon"></param>
    /// <param name="statType"></param>
    /// <returns></returns>
    public int GetStatsValue(PokemonStatType statType)
    {
        return Stats.TryGetValue(statType, out var value)
            ? value
            : throw new DomainException("実数値が存在しません");
    }
}
