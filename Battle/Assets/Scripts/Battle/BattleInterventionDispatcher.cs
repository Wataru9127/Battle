using System.Linq;
using static PokemonStatType;
using Battle;

/// <summary>
/// ‰î“ü—pDispatcher
/// </summary>
public class BattleInterventionDispatcher
{
    private readonly BattleContext context;

    public void Dispatch(MutableBattleEvent e)
    {
        var pokemons = context.GetAllBattlePokemon()
            .Where(p => !p.IsFainted)
            .OrderByDescending(p => PokemonStatsCalculator.CalculateStat(context, p, Speed));

        foreach (var pokemon in pokemons)
        {
            //pokemon.Ability?.OnBattleEvent(e, context);

            if (e.IsCancelled)
                break;
        }
    }
}
