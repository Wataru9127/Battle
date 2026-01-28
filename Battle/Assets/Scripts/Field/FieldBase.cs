using Battle;

/// <summary>
/// フィールド基底クラス
/// </summary>
namespace Field
{
    public abstract class FieldBase
    {
        public abstract string Name { get; }
        public int RemainingTurn { get; protected set; }

        protected FieldBase(int duration)
        {
            RemainingTurn = duration;
        }

        //フィールド発動時
        public virtual void OnStart(BattleContext context, BattlePokemon owner)
        {
            context.AddLog($"{Name}が ひろがった！");
        }

        //ターン開始時
        public virtual void OnTurnStart(BattleContext context) { }

        //毎ターン終了時
        public virtual void OnTurnEnd(BattleContext context) { }

        //フィールド終了時
        public virtual void OnEnd(Battle.BattleContext context)
        {
            context.AddLog($"{Name}の こうかが きれた！");
        }

        //ダメージ倍率の取得
        public virtual float GetDamageRate(BattlePokemon attacker,
            BattlePokemon defender, Waza.WazaBase waza)
        {
            return 1;
        }

        //技使用可否 => サイコフィールド用
        public virtual bool CanUseWaza(BattlePokemon attacker, BattlePokemon defender,
            Waza.WazaBase waza)
        {
            return true;
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
