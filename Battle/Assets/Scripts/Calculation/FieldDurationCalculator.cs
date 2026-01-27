using Battle;
using System;

namespace Calculator
{
    /// <summary>
    /// フィールド持続ターン計算
    /// </summary>
    public static class FieldDurationCalculator
    {
        public static int Calculate(BattleContext context, BattlePokemon owner, int baseTurn)
        {
            int duration = baseTurn;

            //========== 持ち物補正 ==========
            //if (owner.HeldItem != null)
            //{
            //    if (owner.HeldItem.ID == ItemID.GrandTerrain)
            //    {
            //        duration = 8;
            //    }
            //}

            // ===== 特性補正（将来用）=====
            if (owner.PokemonData.Ability != null)
            {
                // 例：フィールド延長系特性を作りたくなったらここ
            }

            // 最低 1 ターン保証
            return Math.Max(1, duration);
        }
    }
}
