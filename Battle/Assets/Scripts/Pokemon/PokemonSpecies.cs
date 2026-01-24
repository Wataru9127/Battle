using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 図鑑情報
/// </summary>
public class PokemonSpecies
{
    //図鑑番号
    public int DexNumber { get; }

    //種族ID
    public int SpeciesID { get; }

    //種族名
    public string Name { get; }

    //タイプ1
    public PokemonType PrimaryType { get; }

    //タイプ2
    public PokemonType? SecondaryType { get; }

    //種族値
    public IReadOnlyDictionary<PokemonStatType, int> BaseStats => baseStats;
    private readonly Dictionary<PokemonStatType, int> baseStats;

    //とくせい
}
