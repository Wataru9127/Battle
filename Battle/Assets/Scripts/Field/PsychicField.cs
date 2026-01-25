
/// <summary>
/// サイコフィールド
/// </summary>
namespace Field
{
    public class PsychicField : FieldEffect
    {
        public override string Name => "サイコフィールド";
        public PsychicField(int duration) : base(duration) { }

        public override bool CanUseWaza(BattlePokemon attacker, BattlePokemon defender, Waza.Waza waza)
        {
            if (!defender.IsOnField) return true;
            if (!waza.IsWazaPriority) return true;

            return false;
        }
    }
}
