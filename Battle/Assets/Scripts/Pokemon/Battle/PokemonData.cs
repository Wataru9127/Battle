using System.Collections.Generic;
using static PokemonStatType;

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

    //とくせい(IDのみ)
    public AbilityID Ability { get; }

    //実数値
    public PokemonStats Stats { get; private set; }

    public PokemonData(PokemonBaseInfo baseInfo, PokemonGrowth growth, AbilityID ability)
    {
        BaseInfo = baseInfo;
        Growth = growth;
        Ability = ability;
    }

    public void SetOwner(BattlePokemon owner)
    {
        Owner = owner;
    }

    public void SetBaseStats()
    {
        //追加用Dictionary
        Dictionary<PokemonStatType, int> keyValues = new Dictionary<PokemonStatType, int>();

        keyValues.Add(HP, PokemonStatsCalculator.CalculateBaseStat(Owner, HP));
        keyValues.Add(Attack, PokemonStatsCalculator.CalculateBaseStat(Owner, Attack));
        keyValues.Add(Defense, PokemonStatsCalculator.CalculateBaseStat(Owner, Defense));
        keyValues.Add(SpecialAttack, PokemonStatsCalculator.CalculateBaseStat(Owner, SpecialAttack));
        keyValues.Add(SpecialDefense, PokemonStatsCalculator.CalculateBaseStat(Owner, SpecialDefense));
        keyValues.Add(Speed, PokemonStatsCalculator.CalculateBaseStat(Owner, Speed));

        Stats = new PokemonStats(keyValues);
    }

    /// <summary>
    /// フォルムチェンジ・メガシンカ込みの種族値を返す
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    public BaseStats GetBaseStats(FormChangeData form)
    {
        if (Owner.currentForm != null)
        {
            return Owner.currentForm.BaseStats;
        }

        return Growth.BaseStats;
    }
}
