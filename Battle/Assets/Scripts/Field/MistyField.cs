using static PokemonType;

/// <summary>
/// ミストフィールド
/// </summary>
namespace Field
{
    public class MistyField : FieldBase
    {
        public override string Name => "ミストフィールド";
        public MistyField(int duration) : base(duration) { }

        //状態異常を付与するかの判定
        public bool CanApplyStatus(BattlePokemon target)
        {
            //地面にいない => 有効
            if (!target.IsOnField) return true;

            //それ以外 => 無効
            return false;
        }

        /// <summary>
        /// ドラゴンタイプの技の威力 / 2
        /// </summary>
        public override float GetDamageRate(BattlePokemon attacker, BattlePokemon defender, Waza.WazaBase waza)
        {
            //フィールドの影響を受けない状態 => 無視
            if (!attacker.IsOnField) return 1;

            //技タイプがドラゴン => 0.5倍
            if (waza.Type == Dragon) return 0.5f;

            //それ以外 => 無視
            return 1;
        }
    }
}
