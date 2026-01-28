using Calculator;

/// <summary>
/// 基底クラス
/// </summary>
namespace Weather
{
    public abstract class WeatherBase
    {
        public abstract string Name { get; }
        public int RemainingTurn { get; protected set; }

        protected virtual int BaseDuration => 5;

        protected WeatherBase(int duration)
        {
            RemainingTurn = duration;
        }

        //天候発動時
        public virtual void OnStart(Battle.BattleContext context, BattlePokemon owner)
        {
            RemainingTurn = WeatherDurationCalculator.Calculator(context, owner, BaseDuration);
        }

        //毎ターン終了時
        public virtual void OnTurnEnd(Battle.BattleContext context) { }

        //天候終了時
        public virtual void OnEnd(Battle.BattleContext context) { }

        //ダメージ倍率の取得
        public virtual float GetDamageRate(BattlePokemon attacker, BattlePokemon defender,
            Waza.WazaBase waza)
        {
            return 1;
        }

        //能力補正 => すなあらし・ゆき 専用
        public virtual float GetStatRate(BattlePokemon pokemon, PokemonStatType statType)
        {
            return 1f;
        }

        public bool Tick()
        {
            if (RemainingTurn > 0)
            {
                RemainingTurn--;
            }
            return IsExpired;
        }

        //解除
        public bool IsExpired => RemainingTurn <= 0;
    }
}
