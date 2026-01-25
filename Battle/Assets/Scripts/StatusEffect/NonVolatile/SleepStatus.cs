using Battle;

/// <summary>
/// ねむり
/// ・1?3ターン行動不能
/// ・ターン開始時にカウント減少
/// ・交代で解除
/// </summary>
public class SleepStatus : NonVolatileStatus
{
    public override string Name => "ねむり";

    //残りターン数
    private int remaining;

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        //1?3ターン
        remaining = context.Random.Next(1, 4);

        context.AddLog($"{condition}は ねむってしまった！");
    }

    /// <summary>
    /// ターン開始時処理
    /// </summary>
    public override void OnTurnStart(PokemonCondition condition, BattleContext context)
    {
        remaining--;

        if (remaining <= 0)
        {
            context.AddLog($"{condition}は めを さました！");
        }
    }

    /// <summary>
    /// 行動可否
    /// </summary>
    public override bool CanAction(PokemonCondition condition, BattleContext context)
    {
        if (remaining > 0)
        {
            context.AddLog($"{condition}は ねむっていて うごけない！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 解除判定
    /// </summary>
    public override bool IsExpired => remaining <= 0;

    /// <summary>
    /// 交代時：解除
    /// </summary>
    public override NonVolatileStatus OnSwitchOut()
    {
        return null;
    }
}
