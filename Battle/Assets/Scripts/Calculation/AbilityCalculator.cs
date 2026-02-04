using Battle;
using UnityEngine;

/// <summary>
/// とくせいの計算時に使用
/// </summary>
public class AbilityCalculator
{
    public static int ApplyAbilityModifier(int value, BattleContext context, BattlePokemon pokemon, PokemonStatType statType)
    {
        //とくせい側が倍率を持っている想定
        //value = Mathf.FloorToInt(
        //    value * pokemon.PokemonData.Ability.GetStatModifier(
        //        context, pokemon, statType)
        //);

        return value;
    }
}
