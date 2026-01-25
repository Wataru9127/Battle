using UnityEngine;
using Battle;

/// <summary>
/// やどりぎ
/// ・毎ターン終了時に最大HPの1/8吸収（SV仕様）
/// ・交代で解除
/// ・くさタイプには無効
/// </summary>
public class MistletoeSeed : VolatileStatus
{
    //植えた側
    private BattlePokemon source;

    public override string Name => "やどりぎのタネ";

    public MistletoeSeed(BattlePokemon source)
    {
        this.source = source;
    }

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(BattlePokemon target, BattleContext context)
    {
        context.AddLog($"{target.Name}に やどりぎの タネが うえつけられた！");
    }

    /// <summary>
    /// ターン終了時 吸収処理
    /// </summary>
    public override void OnTurnEnd(BattlePokemon target, BattleContext context)
    {
        // 植えた側が場にいない or ひんしなら回復なし
        if (source == null || source.Condition.IsFainted)
        {
            return;
        }

        int damage = Mathf.Max(1, target.Condition.MaxHP / 8);
        target.Condition.TakeDamage(damage);
        source.Condition.Heal(damage);

        context.AddLog($"{target.Name}の たいりょくが うばわれた！");
    }

    /// <summary>
    /// 解除判定（交代時に Condition 側で消えるので常に false）
    /// </summary>
    public override bool IsExpired(BattlePokemon target, BattleContext context)
    {
        return false;
    }
}
