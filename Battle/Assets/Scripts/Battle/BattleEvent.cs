/// <summary>
/// バトル中に発生した「事実」を表すイベント
/// </summary>
namespace Battle
{
    public class BattleEvent
    {
        public BattleEventType Type { get; }
        public object Source { get; }
        public BattlePokemon Owner { get; }

        public BattleEvent(BattleEventType type, object source, BattlePokemon owner = null)
        {
            Type = type;
            Source = source;
            Owner = owner;
        }
    }
}
