using UnityEngine;

/// <summary>
/// Ží‘°’l
/// </summary>
public class BaseStats
{
    public int HP { get; }
    public int Attack { get; }
    public int Defense { get; }
    public int SpecialAttack { get; }
    public int SpecialDefense { get; }
    public int Speed { get; }

    public BaseStats(
            int hp,
            int attack,
            int defense,
            int spAttack,
            int spDefense,
            int speed)
    {
        HP = hp;
        Attack = attack;
        Defense = defense;
        SpecialAttack = spAttack;
        SpecialDefense = spDefense;
        Speed = speed;
    }

    /// <summary>
    /// Ží‘°’l‚ðŽæ“¾‚·‚é
    /// </summary>
    public int GetBaseStats(PokemonStatType ability)
    {
        return ability switch
        {
            PokemonStatType.HP => HP,
            PokemonStatType.Attack => Attack,
            PokemonStatType.Defense => Defense,
            PokemonStatType.SpecialAttack => SpecialAttack,
            PokemonStatType.SpecialDefense => SpecialDefense,
            PokemonStatType.Speed => Speed,
            _ => throw new System.ArgumentOutOfRangeException(nameof(ability))
        };
    }
}
