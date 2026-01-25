/// <summary>
/// フィールド抽象クラス
/// </summary>
namespace Field
{
    public abstract class FieldEffect
    {
        public abstract string Name { get; }
        public int RemainingTurn { get; protected set; }

        protected FieldEffect(int duration)
        {
            RemainingTurn = duration;
        }

        //フィールド発動時
        public virtual void OnStart(Battle.BattleContext context)
        {
            context.AddLog($"{Name}が ひろがった！");
        }

        //毎ターン終了時
        public virtual void OnTurnEnd(Battle.BattleContext context) { }

        //フィールド終了時
        public virtual void OnEnd(Battle.BattleContext context)
        {
            context.AddLog($"{Name}の こうかが きれた！");
        }

        //ダメージ補正
        public virtual float ModifyDamage(BattlePokemon attacker, BattlePokemon defender,
            Waza.Waza waza, float damage)
        {
            return damage;
        }

        //技使用可否 => サイコフィールド用
        public virtual bool CanUseWaza(BattlePokemon attacker, BattlePokemon defender,
            Waza.Waza waza)
        {
            return true;
        }

        public void DecreaseTurn()
        {
            RemainingTurn--;
        }

        //解除
        public bool IsExpired => RemainingTurn <= 0;
    }
}
