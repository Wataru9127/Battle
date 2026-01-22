using UnityEngine;

/// <summary>
/// 1匹のポケモンの情報をすべて持つ
/// </summary>
public class PokemonData
{
    //名前・タイプ
    public PokemonIdentity Identity { get; }

    //種族値・個体値・努力値・せいかく・レベル
    public PokemonGrowth Growth { get; private set; }

    //とくせい
    public PokemonAbility Ability { get; }

    //実数値
    public PokemonStats Stats { get; private set; }

    public PokemonData(PokemonIdentity identity, PokemonGrowth growth, PokemonAbility ability)
    {
        Identity = identity;
        Growth = growth;
        Ability = ability;

        PokemonStatsCalculator.Calculate(Growth);
    }
}
