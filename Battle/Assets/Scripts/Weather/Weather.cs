using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象クラス
/// </summary>
namespace Weather
{
    public abstract class Weather
    {
        public abstract string Name { get; }
        public int RemainingTurn { get; protected set; }

        protected Weather(int duration)
        {
            RemainingTurn = duration;
        }

        //フィールド発動時
        public virtual void OnStart(Battle.BattleContext context) { }

        //毎ターン終了時
        public virtual void OnTurnEnd(Battle.BattleContext context) { }

        //フィールド終了時
        public virtual void OnEnd(Battle.BattleContext context) { }

        //技威力補正
        public virtual float ModifyDamage(BattlePokemon attacker, BattlePokemon defender,
            Waza.Waza waza, float damage)
        {
            return damage;
        }

        public void DecreaseTurn()
        {
            RemainingTurn--;
        }

        //解除
        public bool IsExpired => RemainingTurn <= 0;
    }
}
