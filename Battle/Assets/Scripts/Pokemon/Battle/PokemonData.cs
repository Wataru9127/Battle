using UnityEngine;

/// <summary>
/// 1匹のポケモンの情報をすべて持つ
/// </summary>
public class PokemonData
{
    //この情報を所有するバトルポケモン
    public BattlePokemon Owner { get; private set; }

    //名前・タイプ
    public PokemonBaseInfo BaseInfo { get; }

    //種族値・個体値・努力値・せいかく・レベル
    public PokemonGrowth Growth { get; private set; }

    //とくせい
    public PokemonAbility Ability { get; }

    //実数値
    public PokemonStats Stats { get; private set; }

    public PokemonData(PokemonBaseInfo baseInfo, PokemonGrowth growth, PokemonAbility ability)
    {
        BaseInfo = baseInfo;
        Growth = growth;
        Ability = ability;

        //バトル開始時の実数値の計算
        Stats = PokemonStatsCalculator.Calculate(Growth);
    }

    public void SetOwner(BattlePokemon owner)
    {
        Owner = owner;
    }

    public void SetGrowth(PokemonGrowth growth)
    {
        Growth = growth;
    }

    /// <summary>
    /// フォルムチェンジ・メガシンカ込みの種族値を返す
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public BaseStats GetBaseStats(FormChangeData form)
    {
        return Growth.BaseStats;
        //if (form != null)
        //if (FormStats.TryGetValue(form, out var stats))
        //    return stats;
    }
}
