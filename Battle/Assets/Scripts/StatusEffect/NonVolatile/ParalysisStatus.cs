using Battle;
using Common;

/// <summary>
/// まひ
/// ・25%の確率で行動不能
/// ・素早さが1/2になる
/// </summary>
public class ParalysisStatus : NonVolatileStatus
{
    public override string Name => "まひ";

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        context.AddLog($"{condition}は {Name} してしまった！");
    }

    /// <summary>
    /// 行動可否判定
    /// </summary>
    public override bool CanAction(PokemonCondition condition, BattleContext context)
    {
        //25% で行動の可否を判断する
        if (context.Random.NextFloat() < 0.25f)
        {
            context.AddLog($"{condition}は からだが しびれて うごけない！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 素早さ補正
    /// </summary>
    public override float GetSpeedModifier()
    {
        return 0.5f;
    }
}
