using static PokemonType;
using Battle;

/// <summary>
/// ÇÕÇÍ
/// </summary>
namespace Weather
{
    public class Sunny : WeatherBase
    {
        public override string Name => "Ç…ÇŸÇÒÇŒÇÍ";

        public Sunny(int duration = 0) : base(duration) { }

        public override void OnStart(BattleContext context, BattlePokemon owner)
        {
            base.OnStart(context, owner);
            context.AddLog("Ç–Ç¥ÇµÇ™ Ç¬ÇÊÇ≠Ç»Ç¡ÇΩÅI");
        }

        public override float GetDamageRate(BattlePokemon attacker, BattlePokemon defender,
            Waza.WazaBase waza)
        {
            //ÇŸÇÃÇ®ãZ 1.5î{
            if (waza.Type == Fire) return 1.5f;

            //Ç›Ç∏ãZ 0.5î{
            if (waza.Type == Water) return 0.5f;

            //ÇªÇÃëº ìôî{
            return 1;
        }
    }
}
