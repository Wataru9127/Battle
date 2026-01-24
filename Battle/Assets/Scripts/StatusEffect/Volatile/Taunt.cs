/// <summary>
/// ちょうはつ
/// </summary>
public class Taunt : VolatileStatus
{
    //残りターン数
    private int remaining = 3;

    public override string Name => "ちょうはつ";

    /// <summary>
    /// ちょうはつ付与時
    /// </summary>
    public override void OnApply(BattlePokemon target, BattleContext context)
    {
        context.AddLog($"{target.Name}は {Name} された！");
    }

    /// <summary>
    /// ターン終了時
    /// </summary>
    public override void OnTurnEnd(BattlePokemon target, BattleContext context)
    {
        //残りターン数減少
        remaining--;
    }

    /// <summary>
    /// 解除
    /// </summary>
    public override bool Remove(PokemonCondition condition, BattleContext context)
    {
        if (remaining <= 0)
        {
            context.AddLog($"{condition}の {Name} が とけた！");
            return true;
        }
        return false;
    }
}
