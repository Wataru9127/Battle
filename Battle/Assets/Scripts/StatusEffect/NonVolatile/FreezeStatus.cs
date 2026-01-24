using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// こおり
/// ・行動不能
/// ・ターン開始時に20%で解除
/// ・ほのお技を使うと解除
/// ・交代で解除
/// </summary>
public class FreezeStatus : NonVolatileStatus
{
    public override string Name => "こおり";

    //こおり解除フラグ
    private bool thawed = false;

    /// <summary>
    /// 付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        thawed = false;
        context.AddLog($"{condition}は こおってしまった！");
    }

    /// <summary>
    /// ターン開始時：自然解除判定（20%）
    /// </summary>
    public override void OnTurnStart(PokemonCondition condition, BattleContext context)
    {
        if (context.Random.NextFloat() < 0.2f)
        {
            thawed = true;
            context.AddLog($"{condition}の こおりが とけた！");
        }
    }

    /// <summary>
    /// 行動可否
    /// </summary>
    public override bool CanAction(PokemonCondition condition, BattleContext context)
    {
        if (!thawed)
        {
            context.AddLog($"{condition}は こおっていて うごけない！");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 自然解除フラグ
    /// </summary>
    public override bool IsExpired => thawed;

    /// <summary>
    /// 交代時
    /// </summary>
    public override NonVolatileStatus OnSwitchOut()
    {
        return null;
    }

    /// <summary>
    /// ほのお技を使った時
    /// </summary>
    public void OnUseFireMove(PokemonCondition condition, BattleContext context)
    {
        if (!thawed)
        {
            thawed = true;
            context.AddLog($"{condition}の こおりが とけた！");
        }
    }
}
