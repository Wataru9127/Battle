using static PokemonType;

/// <summary>
/// エレキフィールド
/// </summary>
namespace Field
{
    public class ElectricFIeld : FieldEffect
    {
        public override string Name => "エレキフィールド";
        public ElectricFIeld(int duration) : base(duration) { }

        /// <summary>
        /// でんきタイプの技の威力 * 1.3
        /// </summary>
        public override float GetDamageRate(BattlePokemon attacker, BattlePokemon defender, Waza.WazaBase waza)
        {
            //フィールドの影響を受けない状態 => 無視
            if (!attacker.IsOnField) return 1;

            //技タイプがでんき => 1.3倍
            if (waza.Type == Electric) return 1.3f;

            //それ以外 => 無視
            return 1;
        }
    }
}
