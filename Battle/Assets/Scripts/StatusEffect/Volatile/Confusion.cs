using UnityEngine;
using Battle;

/// <summary>
/// こんらん
/// ・2~5ターン持続
/// ・行動時 1/3 で自傷ダメージ
/// ・ターン開始時に残りターン減少
/// </summary>
public class Confusion : VolatileStatus
{
    //残りターン数
    private int remaining;

    public Confusion()
    {
        //ランダムなターン数を付与
        remaining = Random.Range(2, 6);
    }

    public override string Name => "こんらん";

    /// <summary>
    /// こんらん付与した時に呼び出す
    /// </summary>
    public override void OnApply(BattlePokemon target, BattleContext context)
    {
        context.AddLog($"{target.Name}は こんらんした！");
    }

    /// <summary>
    /// ターン開始時
    /// </summary>
    public override void OnTurnStart(PokemonCondition condition, BattleContext context)
    {
        //ターン数減少
        remaining--;

        //こんらん解除
        if (remaining <= 0)
        {
            context.AddLog($"{condition}は こんらんが とけた！");
        }
    }

    /// <summary>
    /// 行動可否判定
    /// </summary>
    public override bool CanAction(BattlePokemon target, BattleContext context)
    {
        // 1/3 で自傷
        if (Random.value < 1f / 3f)
        {
            int damage = CalculateConfusionDamage(target.Condition);
            target.Condition.TakeDamage(damage);

            context.AddLog($"{target.Name}は こんらんして じぶんを こうげきした！");

            return false;
        }

        return true;
    }

    public override bool Remove(PokemonCondition condition, BattleContext context)
    {
        return remaining <= 0;
    }

    /// <summary>
    /// こんらん自傷ダメージ計算
    /// （威力40の物理技として扱う）
    /// </summary>
    private int CalculateConfusionDamage(PokemonCondition condition)
    {
        //必要な値を取得
        //int level = context.Battler.Level;
        //int attack = context.Battler.Attack;
        //int defense = context.Battler.Defense;

        //int power = 40;

        //int damage = Mathf.FloorToInt(
        //    (((2f * level / 5f + 2f) * power * attack / defense) / 50f) + 2f
        //);

        //return Mathf.Max(1, damage);
        return 1;
    }
}
