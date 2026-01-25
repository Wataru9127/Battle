using Battle;
//using Ability;

/// <summary>
/// バトル状況も含めたポケモンの情報
/// </summary>
public class BattlePokemon
{
    //ポケモンの情報
    public PokemonData PokemonData { get; }

    //現在の状態
    public PokemonCondition Condition { get; }

    //場にいるか
    public bool IsOnField { get; private set; }

    //ひんし判定
    public bool IsFainted => Condition.IsFainted;

    //簡易取得用の名前
    public string Name => PokemonData.BaseInfo.Name;

    public void OnSwitchIn(BattleContext context)
    {

    }

    /// <summary>
    /// 交代可否判定
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public bool CanSwitch(BattleContext context)
    {
        foreach (var status in Condition.VolatileStatuses)
        {
            if (!status.CanSwitch(this, context))
            {
                return false;
            }
        }
        return true;
    }

    
}
