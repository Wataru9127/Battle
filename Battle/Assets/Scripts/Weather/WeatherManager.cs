using Battle;
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

        context.EventDispatcher.Dispatch(
            new BattleEvent(BattleEventType.WeatherEnded, Current));

        Current = weather;
        if (Current == null) return;
        Current.OnStart(context, owner);

        context.EventDispatcher.Dispatch(
                new BattleEvent(BattleEventType.WeatherStarted, Current, owner));
    }

    /// <summary>
    /// 天候を解除する
    /// </summary>
    public void ClearWeather(BattleContext context)
    {
        if (Current == null) return;

        Current.OnEnd(context);

        context.EventDispatcher.Dispatch(
                new BattleEvent(BattleEventType.WeatherEnded, Current));

        Current = null;
    }

    /// <summary>
    /// ターン開始時
    /// </summary>
    /// <param name="context"></param>
    public void OnTurnStart(BattleContext context)
    {
        Current?.OnTurnStart(context);
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
