namespace Waza
{
    /// <summary>
    /// 技が持つ主効果の種別
    /// フィールド・特性・アイテム判定用
    /// </summary>
    public enum WazaEffectType
    {
        None,

        // ===== 状態異常 =====
        Sleep,
        Poison,
        Burn,
        Paralysis,
        Freeze,

        // ===== 能力変化 =====
        StatUp,
        StatDown,

        // ===== その他 =====
        Flinch,     //ひるみ => ねこだまし(いわなだれ ×)
        Confusion,  //こんらん => あやしいひかり
        Heal,       //回復 => ギガドレイン
        Recoil,     //反動系 => とっしん、もろはのずつき
    }
}
