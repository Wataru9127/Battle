using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// どく
/// ・毎ターン終了時に最大HPの1/8ダメージ
/// </summary>
public class PoisonStatus : NonVolatileStatus
{
    public override string Name => "どく";

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        context.AddLog($"{condition}は {Name}を あびた！");
    }

    /// <summary>
    /// ターン終了時ダメージ
    /// </summary>
    public override void OnTurnEnd(PokemonCondition condition, BattleContext context)
    {
        int damage = Mathf.Max(1, condition.MaxHP / 8);
        condition.TakeDamage(damage);

        context.AddLog($"{condition}は {Name}の ダメージを うけた！");
    }
}
