using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 種族
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
    public IReadOnlyDictionary<PokemonAbility, int> BaseStats => baseStats;
    private readonly Dictionary<PokemonAbility, int> baseStats;

    //とくせい
}
