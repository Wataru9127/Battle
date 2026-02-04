using System.Collections.Generic;

/// <summary>
/// バトルイベントを全ポケモンに通知する
/// </summary>
namespace Battle
{
    public class BattleEventDispatcher
    {
        private readonly BattleContext context;

        public BattleEventDispatcher(BattleContext context)
        {
            this.context = context;
        }

        public void Dispatch(BattleEvent battleEvent)
        {
            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                if (pokemon.IsFainted) continue;

                if (pokemon.PokemonData.Ability == AbilityID.None) continue;

                //pokemon.PokemonData.Ability?
                //    .OnBattleEvent(battleEvent, context, pokemon);
            }
        }
    }
}
