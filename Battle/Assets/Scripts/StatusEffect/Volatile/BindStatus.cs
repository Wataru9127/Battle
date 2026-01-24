using UnityEngine;

/// <summary>
/// バインド
/// ・4〜5ターン持続
/// ・毎ターン終了時に最大HPの1/8ダメージ
/// ・交代不可
/// </summary>
public class BindStatus : VolatileStatus
{
    //持続ターン数
    private int remianing;

    public override string Name => "バインド";

    public BindStatus()
    {
        //4〜5ターン
        remianing = Random.Range(4, 6);
    }

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(BattlePokemon target, BattleContext context)
    {
        context.AddLog($"{target.Name}は しめつけられた！");
    }

    /// <summary>
    /// ターン終了時ダメージ
    /// </summary>
    public override void OnTurnEnd(BattlePokemon target, BattleContext context)
    {
        int damage = Mathf.Max(1, target.Condition.MaxHP / 8);
        target.Condition.TakeDamage(damage);

        context.AddLog($"{target.Name}は バインドの ダメージを うけた！");

        remianing--;

        if (remianing <= 0)
        {
            context.AddLog($"{target.Name}は バインドから ぬけだした！");
        }
    }

    /// <summary>
    /// 解除判定
    /// </summary>
    public override bool IsExpired(BattlePokemon target, BattleContext context)
    {
        return remianing <= 0;
    }


    /// <summary>
    /// 交代可否
    /// </summary>
    /*すべての強制交代技を無効化する*/
}
