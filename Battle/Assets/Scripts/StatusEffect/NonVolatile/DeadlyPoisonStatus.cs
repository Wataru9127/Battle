using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// もうどく
/// ・毎ターン終了時にダメージ増加
/// </summary>
public class DeadlyPoisonStatus : NonVolatileStatus
{
    public override string Name => "もうどく";

    //経過ターン数
    private int turnCount = 0;

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        turnCount = 0;
        context.AddLog($"{condition}は {Name}を あびた！");
    }

    /// <summary>
    /// ターン終了時ダメージ
    /// </summary>
    public override void OnTurnEnd(PokemonCondition condition, BattleContext context)
    {
        turnCount++;

        int damage = Mathf.Max(1, condition.MaxHP * turnCount / 16);
        condition.TakeDamage(damage);

        context.AddLog($"{condition}は {Name}の ダメージを うけた！");
    }

    /// <summary>
    /// 交代時 通常どくに変化
    /// </summary>
    public override NonVolatileStatus OnSwitchOut()
    {
        return new PoisonStatus();
    }
}
