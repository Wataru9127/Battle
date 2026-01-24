using System;
using UnityEngine;

/// <summary>
/// やけど
/// ・毎ターン終了時に最大HPの1/16ダメージ
/// ・物理技の攻撃力が1/2
/// </summary>
public class BurnStatus : NonVolatileStatus
{
    public override string Name => "やけど";

    /// <summary>
    /// やけど付与時
    /// </summary>
    public override void OnApply(PokemonCondition condition, BattleContext context)
    {
        context.AddLog($"{condition}は {Name} を おった！");
    }

    /// <summary>
    /// ターン終了時
    /// </summary>
    public override void OnTurnEnd(PokemonCondition condition, BattleContext context)
    {
        int damage = Mathf.Max(1, condition.MaxHP / 16);
        condition.TakeDamage(damage);

        context.AddLog($"{condition}は {Name}の ダメージを うけた！");
    }

    /// <summary>
    /// 物理技の攻撃力補正
    /// </summary>
    public override float GetAttackModifier(WazaCategory category)
    {
        if (category == WazaCategory.Physical)
        {
            return 0.5f;
        }

        return 1f;
    }
}
