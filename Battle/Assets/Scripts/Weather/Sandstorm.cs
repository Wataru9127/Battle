using static PokemonType;
using Battle;
using static PokemonStatType;
using System;

/// <summary>
/// すなあらし
/// </summary>
namespace Weather
{
    public class Sandstorm : WeatherBase
    {
        public override string Name => "すなあらし";

        public Sandstorm(int duration = 0) : base(duration) { }

        public override void OnStart(BattleContext context, BattlePokemon owner)
        {
            base.OnStart(context, owner);
            context.AddLog("すなあらしが ふきあれた！");
        }

        /// <summary>
        /// すなあらし下でのとくぼう補正
        /// </summary>
        public override float GetStatRate(BattlePokemon pokemon, PokemonStatType statType)
        {
            //とくぼう 以外は無視
            if (statType != SpecialDefense) return 1f;

            //いわタイプ => 1.5倍
            //その他 => 1.0倍(補正なし)
            return pokemon.HasType(Rock) ? 1.5f : 1.0f;
        }

        public override void OnTurnEnd(BattleContext context)
        {
            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                if (IsNoDamage(pokemon)) continue;

                //最大HPの 1/16 のダメージ
                int damage = pokemon.Condition.MaxHP / 16;

                //最低1ダメージ保証
                damage = Math.Max(1, damage);

                pokemon.Condition.TakeDamage(damage);
            }
        }

        /// <summary>
        /// すなあらしダメージ無効化判定
        /// いわ・じめん・はがね タイプは無効化
        /// </summary>
        private bool IsNoDamage(BattlePokemon pokemon)
        {
            return pokemon.HasType(Rock) ||
                pokemon.HasType(Ground) ||
                pokemon.HasType(Steel);
        }
    }
}
