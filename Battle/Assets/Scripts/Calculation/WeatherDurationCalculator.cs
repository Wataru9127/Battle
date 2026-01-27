using System;
using Battle;


/// <summary>
/// 天候持続ターン数計算
/// </summary>
namespace Calculator
{
    public static class WeatherDurationCalculator
    {
        public static int Calculator(BattleContext context, BattlePokemon owner, int baseTurn)
        {
            int duration = baseTurn;

            // ===== 持ち物補正 =====
            //if (owner.HeldItem != null)
            //{
            //    switch (owner.HeldItem.Id)
            //    {
            //        case ItemId.HeatRock:   // あついいわ
            //        case ItemId.DampRock:   // しめったいわ
            //        case ItemId.SmoothRock: // さらさらいわ
            //        case ItemId.IcyRock:    // つめたいいわ
            //            duration = 8;
            //            break;
            //    }
            //}

            // ===== 特性補正 =====
            //if (owner.PokemonData.Ability != null)
            //{
            //    // 例：
            //    // ・天候延長特性
            //    // ・SV新特性
            //}

            //最低1ターン保証
            return Math.Max(1, duration);
        }
    }
}
