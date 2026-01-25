using Battle;

/// <summary>
/// メロメロ
/// ・50%で行動不能
/// ・相手が場からいなくなると解除
/// </summary>
public class AttractStatus : VolatileStatus
{
    public override string Name => "メロメロ";

    //メロメロをくらった相手
    private readonly BattlePokemon owner;

    public AttractStatus(BattlePokemon owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(BattlePokemon target, BattleContext context)
    {
        context.AddLog($"{target.Name}は {owner.Name} に メロメロに なった！");
    }

    /// <summary>
    /// 行動可否判定
    /// </summary>
    public override bool CanAction(BattlePokemon target, BattleContext context)
    {
        // 解除条件を満たしているなら影響しない
        if (IsExpired(target, context))
        {
            return true;
        }

        // 50%で行動不能
        if (Common.Probability.Check(context.Random, 0.5f))
        {
            context.AddLog($"{target.Name}は メロメロで うごけない！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 解除条件
    /// </summary>
    public override bool IsExpired(BattlePokemon target, BattleContext context)
    {
        // 付与元がいない／ひんし／場にいない
        return owner == null
            || owner.IsFainted
            || !owner.IsOnField;
    }
}
