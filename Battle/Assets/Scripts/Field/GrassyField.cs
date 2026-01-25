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
    }
}
