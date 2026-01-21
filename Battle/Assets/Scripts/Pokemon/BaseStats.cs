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
    public int GetBaseStats(PokemonAbility ability)
    {
        return ability switch
        {
            PokemonAbility.HP => HP,
            PokemonAbility.Attack => Attack,
            PokemonAbility.Defense => Defense,
            PokemonAbility.SpecialAttack => SpecialAttack,
            PokemonAbility.SpecialDefense => SpecialDefense,
            PokemonAbility.Speed => Speed,
            _ => throw new System.ArgumentOutOfRangeException(nameof(ability))
        };
    }
}
