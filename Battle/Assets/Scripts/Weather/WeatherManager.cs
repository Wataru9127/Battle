using Battle;
using System;
using Weather;

public class WeatherManager
{
    public WeatherBase Current {  get; private set; }

    /// <summary>
    /// 天候の発動
    /// ・上書きも含む
    /// </summary>
    public void SetWeather(WeatherBase weather, BattleContext context, BattlePokemon owner)
    {
        //既存の天候を終了させる
        Current?.OnEnd(context);

        Current = weather;
        Current.OnStart(context, owner);
    }

    /// <summary>
    /// 天候を解除する
    /// </summary>
    public void ClearWeather(BattleContext context)
    {
        if (Current == null) return;

        Current.OnEnd(context);
        Current = null;
    }

    /// <summary>
    /// 毎ターン終了時の処理
    /// </summary>
    public void OnTurnEnd(BattleContext context)
    {
        if (Current == null) return;

        //毎ターン終了時の各天候の処理
        Current.OnTurnEnd(context);

        if (Current.Tick())
        {
            ClearWeather(context);
        }
    }
}
