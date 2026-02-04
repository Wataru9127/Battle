using UnityEngine;
using Battle;

/// <summary>
/// ‚Æ‚­‚¹‚¢Šî’êƒNƒ‰ƒX
/// </summary>
public abstract class AbilityBase
{
    public virtual void OnBattleEvent(BattleEvent battleEvent, BattleContext context, BattlePokemon owner)
    {
        //if (battleEvent is StatCalculatedEvent e)
        //{
        //    if (e.Pokemon == owner && e.StatType == PokemonStatType.Attack)
        //    {
        //        e.Value = Mathf.FloorToInt(e.Value * 2f);
        //    }
        //}
    }
}
