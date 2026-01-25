using Battle;

/// <summary>
/// ひるみ
/// ・そのターン行動不能
/// ・ターン終了時に解除
/// </summary>
public class FlinchStatus : VolatileStatus
{
    public override string Name => "ひるみ";

    //行動判定を1回使ったか
    private bool consumed = false;

    /// <summary>
    /// 行動可否
    /// </summary>
    public override bool CanAction(BattlePokemon target, BattleContext context)
    {
        if (!consumed)
        {
            consumed = true;
            context.AddLog($"{target.Name}は ひるんで うごけない！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// ターン終了時に必ず解除
    /// </summary>
    public override bool IsExpired(BattlePokemon target, BattleContext context)
    {
        return consumed;
    }
}
