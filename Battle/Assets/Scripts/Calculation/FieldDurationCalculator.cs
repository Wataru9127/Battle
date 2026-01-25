namespace Calculator
{
    /// <summary>
    /// フィールド持続ターン計算
    /// </summary>
    public static class FieldDurationCalculator
    {
        public static int Calculate(BattlePokemon user, int baseTurn)
        {
            int turn = baseTurn;

            //アイテム(グランドコード)で持続ターン増加
            //if (user.HasItem(ItemID.FieldExtender))
            //{
            //    turn += 3;
            //}

            return turn;
        }
    }
}
