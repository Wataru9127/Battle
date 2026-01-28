/// <summary>
/// バトル進行フェーズ
/// </summary>
namespace Battle
{
    public enum BattlePhase
    {
        TurnStart,     // ターン開始（天候・フィールド処理）
        ActionSelect,  // 行動選択（UI or AI）
        ActionOrder,   // 行動順決定（素早さ・優先度）
        ActionExecute, // 技実行
        TurnEnd,       // ターン終了処理（定数ダメ・解除）
        CheckBattleEnd // 勝敗判定
    }
}
