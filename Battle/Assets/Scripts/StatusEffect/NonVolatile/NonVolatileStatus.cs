using Battle;

/// <summary>
/// まひ、やけど等重複しないもの
/// </summary>
public abstract class NonVolatileStatus
{
    //表示用の名前
    public abstract string Name { get; }

    /// <summary>
    /// 付与された瞬間
    /// </summary>
    public virtual void OnApply(PokemonCondition condition, BattleContext context) { }

    /// <summary>
    /// 行動できるか(まひ・ねむり)
    /// </summary>
    public virtual bool CanAction(PokemonCondition condition, BattleContext context)
    {
        return true;
    }

    /// <summary>
    /// ターン開始時
    /// </summary>
    public virtual void OnTurnStart(PokemonCondition condition, BattleContext context) { }

    /// <summary>
    /// ターン終了時(どく・もうどく・やけど)
    /// </summary>
    public virtual void OnTurnEnd(PokemonCondition condition, BattleContext context) { }

    /// <summary>
    /// ダメージ計算補正用
    /// </summary>
    public virtual float GetAttackModifier(WazaCategory category)
    {
        return 1f;
    }

    /// <summary>
    /// 交代時
    /// </summary>
    /// <returns></returns>
    public virtual NonVolatileStatus OnSwitchOut()
    {
        return this;
    }

    /// <summary>
    /// まひ専用 すばやさを半減する
    /// </summary>
    /// <returns></returns>
    public virtual float GetSpeedModifier()
    {
        return 1f;
    }

    /// <summary>
    /// ターン数が経過したフラグ
    /// </summary>
    public virtual bool IsExpired => false;
}
