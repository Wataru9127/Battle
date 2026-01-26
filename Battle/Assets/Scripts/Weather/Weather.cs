/// <summary>
/// 基底クラス
/// </summary>
namespace Weather
{
    public abstract class Weather
    {
        public abstract string Name { get; }
        public int RemainingTurn { get; protected set; }

        protected Weather(int duration)
        {
            RemainingTurn = duration;
        }

        //フィールド発動時
        public virtual void OnStart(Battle.BattleContext context) { }

        //毎ターン終了時
        public virtual void OnTurnEnd(Battle.BattleContext context) { }

        //フィールド終了時
        public virtual void OnEnd(Battle.BattleContext context) { }

        //ダメージ倍率の取得
        public virtual float GetDamageRate(BattlePokemon attacker, BattlePokemon defender,
            Waza.WazaBase waza)
        {
            return 1;
        }

        public bool Tick()
        {
            RemainingTurn--;
            return IsExpired;
        }

        //解除
        public bool IsExpired => RemainingTurn <= 0;
    }
}
