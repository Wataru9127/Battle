using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 混乱、ひるみ等重複するもの
/// </summary>
public abstract class VolatileStatus
{
    //表示用の名前
    public abstract string Name { get; }

    /// <summary>
    /// 付与された瞬間
    /// </summary>
    public virtual void OnApply(BattlePokemon target, BattleContext context) { }

    /// <summary>
    /// 行動前チェック
    /// </summary>
    public virtual bool CanAction(BattlePokemon target, BattleContext context) => true;

    /// <summary>
    /// ターン開始時
    /// </summary>
    public virtual void OnTurnStart(PokemonCondition condition, BattleContext context) { }

    /// <summary>
    /// ターン終了時
    /// </summary>
    public virtual void OnTurnEnd(BattlePokemon target, BattleContext context) { }

    /// <summary>
    /// 条件によって解除する
    /// </summary>
    public virtual bool Remove(PokemonCondition condition, BattleContext context)
    {
        return false;
    }

    /// <summary>
    /// ターン数の経過による解除
    /// </summary>
    public virtual bool IsExpired(BattlePokemon target, BattleContext context) => false;

    /// <summary>
    /// 交代可否判定
    /// </summary>
    /// <param name="target"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public virtual bool CanSwitch(BattlePokemon target, BattleContext context) => true;
}
