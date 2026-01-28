using static PokemonType;
using Battle;
using static PokemonStatType;

/// <summary>
/// ゆき
/// </summary>
namespace Weather
{
    public class Snow : WeatherBase
    {
        public override string Name => "ゆき";

        public Snow(int duration = 0) : base(duration) { }

        public override void OnStart(BattleContext context, BattlePokemon owner)
        {
            base.OnStart(context, owner);
            context.AddLog("ゆきが ふりはじめた！");
        }

        public override float GetStatRate(BattlePokemon pokemon, PokemonStatType statType)
        {
            //ぼうぎょ 以外は無視
            if (statType != Defense) return 1f;

            //こおりタイプ => 1.5倍
            //その他 => 1.0倍(補正なし)
            return pokemon.HasType(Ice) ? 1.5f : 1.0f;
        }
    }
}
