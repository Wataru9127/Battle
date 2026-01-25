using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミストフィールド
/// </summary>
namespace Field
{
    public class MistyField : FieldEffect
    {
        public override string Name => "ミストフィールド";
        public MistyField(int duration) : base(duration) { }

        //状態異常を付与するかの判定
        public bool CanApplyStatus(BattlePokemon target)
        {
            return !target.IsOnField;
        }
    }
}
