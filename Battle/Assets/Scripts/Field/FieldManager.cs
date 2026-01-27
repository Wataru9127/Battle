using Battle;
using static PokemonStatType;
using System.Collections.Generic;

namespace Field
{
    public class FieldManager
    {
        public FieldBase Current { get; private set; }

        /// <summary>
        /// フィールドの発動
        /// ・上書きも含む
        /// </summary>
        public void SetField(FieldBase field, BattleContext context)
        {
            //既にフィールドが存在していたら終了
            Current?.OnEnd(context);

            Current = field;
            Current.OnStart(context);
        }

        /// <summary>
        /// フィールドメイカー特性の処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="allowOverride"></param>
        public void TryActivateFieldMakers(BattleContext context, bool allowOverride)
        {
            if (Current != null && !allowOverride) return;

            var makers = new List<(BattlePokemon pokemon, IFieldMakerAbility ability)>();

            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                if (pokemon.IsFainted) continue;

                if (pokemon.PokemonData.Ability is IFieldMakerAbility maker)
                {
                    makers.Add((pokemon, maker));
                }
            }

            if (makers.Count == 0) return;

            var winner = DecidePriority(context, makers);
            var field = winner.ability.CreateField(context, winner.pokemon);

            SetField(field, context);
        }

        /// <summary>
        /// フィールドの解除
        /// </summary>
        public void ClearField(BattleContext context)
        {
            if (Current == null) return;

            Current.OnEnd(context);
            Current = null;
        }

        /// <summary>
        /// 毎ターン終了時の処理
        /// </summary>
        /// <param name="context"></param>
        public void OnTurnEnd(BattleContext context)
        {
            if (Current == null) return;

            if (Current.Tick())
            {
                ClearField(context);
            }
        }

        private (BattlePokemon pokemon, IFieldMakerAbility ability)
            DecidePriority(BattleContext context, List<(BattlePokemon pokemon, IFieldMakerAbility ability)> makers)
        {
            //すばやさ の降順に並べ替える
            makers.Sort((a, b) =>
            {
                int speedA = PokemonStatsCalculator.CalculateStat(a.pokemon, Speed);
                int speedB = PokemonStatsCalculator.CalculateStat(b.pokemon, Speed);

                if (speedA != speedB)
                    return speedB.CompareTo(speedA);

                // 同速ならランダム
                return context.Random.Next(2) == 0 ? -1 : 1;
            });

            return makers[0];
        }
    }
}
