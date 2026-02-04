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
        public void SetField(FieldBase field, BattleContext context, BattlePokemon owner)
        {
            //既にフィールドが存在していたら終了
            Current?.OnEnd(context);

            context.EventDispatcher.Dispatch(
                new BattleEvent(BattleEventType.FieldEnded, Current));

            Current = field;
            if (Current == null) return;

            Current.OnStart(context, owner);
            context.EventDispatcher.Dispatch(
                new BattleEvent(BattleEventType.FieldStarted, Current, owner));
        }

        /// <summary>
        /// フィールドメイカー特性の処理
        /// </summary>
        /// <param name="context"></param>
        /// <param name="allowOverride"></param>
        public void TryActivateFieldMakers(BattleContext context, FieldOverridePolicy policy)
        {
            if (Current != null && policy == FieldOverridePolicy.DenyIfExists) return;

            var makers = new List<(BattlePokemon pokemon, IFieldMakerAbility ability)>();

            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                if (pokemon.IsFainted) continue;

                //if (pokemon.PokemonData.Ability is IFieldMakerAbility maker)
                //{
                //    makers.Add((pokemon, maker));
                //}
            }

            if (makers.Count == 0) return;

            var winner = DecidePriority(context, makers);
            var field = winner.ability.CreateField(context, winner.pokemon);

            SetField(field, context, winner.pokemon);
        }

        /// <summary>
        /// フィールドの解除
        /// </summary>
        public void ClearField(BattleContext context)
        {
            if (Current == null) return;

            Current.OnEnd(context);

            context.EventDispatcher.Dispatch(
                new BattleEvent(BattleEventType.FieldEnded, Current));

            Current = null;
        }

        //ターン開始時
        public virtual void OnTurnStart(BattleContext context)
        {
            Current?.OnTurnStart(context);
        }


        /// <summary>
        /// 毎ターン終了時の処理
        /// </summary>
        /// <param name="context"></param>
        public void OnTurnEnd(BattleContext context)
        {
            if (Current == null) return;

            Current.OnTurnEnd(context);

            if (Current.Tick())
            {
                ClearField(context);
            }
        }

        /// <summary>
        /// フィールドメーカー特性の優先順位決定
        /// </summary>
        private (BattlePokemon pokemon, IFieldMakerAbility ability)
            DecidePriority(BattleContext context, List<(BattlePokemon pokemon, IFieldMakerAbility ability)> makers)
        {
            //すばやさ の降順に並べ替える
            makers.Sort((a, b) =>
            {
                int speedA = PokemonStatsCalculator.CalculateStat(context, a.pokemon, Speed);
                int speedB = PokemonStatsCalculator.CalculateStat(context, b.pokemon, Speed);

                if (speedA != speedB)
                    return speedB.CompareTo(speedA);

                // 同速ならランダム
                return context.Random.Next(2) == 0 ? -1 : 1;
            });

            return makers[0];
        }
    }
}
