using Battle;

/// <summary>
/// 「各ポケモンが何をするか」を決める役
/// </summary>
public interface IActionProvider
{
    BattleAction DecideAction(BattlePokemon pokemon, BattleContext context);
}
