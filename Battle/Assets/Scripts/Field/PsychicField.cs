using static PokemonType;
using Waza;

/// <summary>
/// サイコフィールド
/// </summary>
namespace Field
{
    public class PsychicField : FieldEffect
    {
        public override string Name => "サイコフィールド";
        public PsychicField(int duration) : base(duration) { }

        /// <summary>
        /// 先制技を受けなくする
        /// </summary>
        public override bool CanUseWaza(BattlePokemon attacker, BattlePokemon defender, Waza.WazaBase waza)
        {
            if (!defender.IsOnField) return true;
            if (!waza.IsWazaPriority) return true;

            return false;
        }

        /// <summary>
        /// エスパータイプの技の威力 * 1.3
        /// </summary>
        public override float ModifyDamage(BattlePokemon attacker, BattlePokemon defender, WazaBase waza, float damage)
        {
            //フィールドの影響を受けない状態 => 無視
            if (!attacker.IsOnField) return damage;

            //技タイプがエスパー => 1.3倍
            if (waza.Type == Esper) return damage * 1.3f;

            //それ以外 => 無視
            return damage;
        }
    }
}
