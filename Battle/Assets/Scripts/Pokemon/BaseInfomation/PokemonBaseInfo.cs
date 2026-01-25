using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基本情報
/// </summary>
public class PokemonBaseInfo
{
    //種族ID
    public int SpeciesID { get; }

    //表示用名前
    public string Name { get; }

    //タイプ1
    public PokemonType PrimaryType { get; }

    //タイプ2
    public PokemonType? SecondaryType { get; }

    //種族値
    public IReadOnlyDictionary<PokemonStatType, int> BaseStats {  get; }
}
