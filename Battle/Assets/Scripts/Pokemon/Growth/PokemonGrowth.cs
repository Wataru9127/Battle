using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 種族値・個体値・努力値・せいかく・レベル
/// 実数値計算に必要なパラメータのみを保持する
/// </summary>
public class PokemonGrowth
{
    //種族値
    public BaseStats BaseStats { get; private set; }

    //個体値
    public IndividualValue IV { get; private set; }

    //努力値
    public EffortValue EV { get; private set; }

    //せいかく
    public Nature Nature { get; private set; }

    //レベル
    public readonly int level = 50;
    public int Level => level;

    public PokemonGrowth(BaseStats baseStats, IndividualValue iv, EffortValue ev, Nature nature)
    {
        BaseStats = baseStats;
        IV = iv;
        EV = ev;
        Nature = nature;
    }

    public void SetNature(Nature nature)
    {
        Nature = nature;
    }

    public void SetIV(IndividualValue iv)
    {
        IV = iv;
    }

    public void SetEV(EffortValue ev)
    {
        EV = ev;
    }

    public void SetBaseStats(BaseStats baseStats)
    {
        BaseStats = baseStats;
    }
}
