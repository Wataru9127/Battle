namespace Battle
{
    /// <summary>
    /// 計算途中に特性・場・天候が介入できるイベント
    /// </summary>
    public abstract class MutableBattleEvent
    {
        /// <summary>
        /// このイベントの処理を中断するか
        /// （無効化・完全防止など）
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        /// このイベントに関係する主体
        /// </summary>
        public BattlePokemon Owner { get; }

        protected MutableBattleEvent(BattlePokemon owner)
        {
            Owner = owner;
        }
    }
}
