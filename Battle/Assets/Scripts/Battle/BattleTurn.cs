using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1ターンの進行制御
/// </summary>
namespace Battle
{
    public class BattleTurn
    {
        private readonly BattleContext context;

        public BattlePhase CurrentPhase { get; private set; }

        public BattleTurn(BattleContext context)
        {
            this.context = context;
            CurrentPhase = BattlePhase.TurnStart;
        }

        /// <summary>
        /// 1ターンの実行
        /// </summary>
        public void ExecuteTurn()
        {
            ExecuteTurnStart();
            ExecuteActionSelect();
            ExecuteActionOrder();
            ExecuteActionExecute();
            ExecuteTurnEnd();
            ExecuteCheckBattleEnd();
        }

        //ターン開始
        private void ExecuteTurnStart()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.TurnStart;

            //ターン数を増やす
            context.TurnCounter.TurnIncrement();

            //天候・フィールドの展開
            context.WeatherManager?.OnTurnStart(context);
            context.FieldManager?.OnTurnStart(context);

            //各ポケモンのターン開始時処理
            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                //ひんしのポケモンは無視
                if (pokemon.IsFainted) continue;

                pokemon.OnTurnStart(context);
            }
        }

        //行動選択フェーズ
        private void ExecuteActionSelect()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.ActionSelect;

            context.ActionQueue.Clear();

            foreach (var pokemon in context.GetAllBattlePokemon())
            {
                //ひんしのポケモンは無視
                if (pokemon.IsFainted) continue;

                var action = context.ActionProvider.DecideAction(pokemon, context);
                context.ActionQueue.Add(action);
            }
        }

        //行動順決定フェーズ
        private void ExecuteActionOrder()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.ActionOrder;

            context.ActionQueue.Sort((a, b) =>
            {
                // 優先度
                int priorityCompare = b.Priority.CompareTo(a.Priority);
                if (priorityCompare != 0)
                    return priorityCompare;

                // 素早さ
                int speedCompare = 1;//(Test)
                //int speedCompare =
                //    b.Actor.Stats.Speed.CompareTo(a.Actor.Stats.Speed);
                if (speedCompare != 0)
                    return speedCompare;

                // 完全同速時はランダム
                return context.Random.Next(0, 2) == 0 ? -1 : 1;
            });
        }

        //行動実行フェーズ
        private void ExecuteActionExecute()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.ActionExecute;

            foreach (var action in context.ActionQueue)
            {
                // 行動前にひんしになっていたらスキップ
                if (action.Actor.IsFainted) continue;

                action.Execute(context);

                // 行動中に勝敗が決まったら即終了
                if (context.IsBattleFinished) break;
            }
        }

        //ターン終了処理
        private void ExecuteTurnEnd()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.TurnEnd;

            //天候・フィールドのターン終了処理
            context.WeatherManager?.OnTurnEnd(context);
            context.FieldManager?.OnTurnEnd(context);

        }

        //勝敗判定フェーズ
        private void ExecuteCheckBattleEnd()
        {
            //フェーズ設定
            CurrentPhase = BattlePhase.CheckBattleEnd;

            if (context.CheckBattleResult(out var result))
            {
                context.Result = result;
                context.IsBattleFinished = true;
            }
        }
    }
}
