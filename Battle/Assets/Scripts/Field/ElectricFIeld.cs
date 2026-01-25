using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エレキフィールド
/// </summary>
namespace Field
{
    public class ElectricFIeld : FieldEffect
    {
        public override string Name => "エレキフィールド";
        public ElectricFIeld(int duration) : base(duration) { }

        public override float ModifyDamage(BattlePokemon attacker, BattlePokemon defender, Waza.Waza waza, float damage)
        {
            //でんきタイプなら 1.3倍
            if (attacker.IsOnField && waza.Type == PokemonType.Electric)
            {
                return damage * 1.3f;
            }

            return damage;
        }
    }
}
