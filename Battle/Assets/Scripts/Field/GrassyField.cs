using Waza;
using static PokemonType;

/// <summary>
/// グラスフィールド
/// </summary>
namespace Field
{
    public class GrassyField : FieldEffect
    {
        public override string Name => "グラスフィールド";
        public GrassyField(int duration) : base(duration) { }

        //毎ターン終了時回復
        public override void OnTurnEnd(Battle.BattleContext context)
        {
            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                //地面にいないポケモンは無視
                if (!pokemon.IsOnField) continue;

                //最大体力の 1/16 を回復
                int heal = pokemon.Condition.MaxHP / 16;
                pokemon.Condition.Heal(heal);

                context.AddLog($"{pokemon.Name}は グラスフィールドで かいふくした！");
            }
        }

        /// <summary>
        /// くさタイプの技の威力 * 1.3
        /// </summary>
        public override float GetDamageRate(BattlePokemon attacker, BattlePokemon defender, WazaBase waza)
        {
            //フィールドの影響を受けない状態 => 無視
            if (!attacker.IsOnField) return 1;

            //技タイプがくさ => 1.3倍
            if (waza.Type == Grass) return 1.3f;

            //それ以外 => 無視
            return 1;
        }
    }
}
