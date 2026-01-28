using static PokemonType;
using Battle;

/// <summary>
/// ‚ ‚ß
/// </summary>
namespace Weather
{
    public class Rain : WeatherBase
    {
        public override string Name => "‚ ‚ß";
        public Rain(int duration) : base(duration) { }

        public override void OnStart(BattleContext context, BattlePokemon owner)
        {
            base.OnStart(context, owner);
            context.AddLog("‚ ‚ß‚ª ‚Ó‚è‚Í‚¶‚ß‚½I");
        }

        //ƒ_ƒ[ƒW”{—¦‚Ìæ“¾
        public override float GetDamageRate(BattlePokemon attacker, BattlePokemon defender,
            Waza.WazaBase waza)
        {
            //‚İ‚¸‹Z 1.5”{
            if (waza.Type == Water) return 1.5f;

            //‚Ù‚Ì‚¨‹Z 0.5”{
            if (waza.Type == Fire) return 0.5f;

            //‚»‚Ì‘¼ “™”{
            return 1;
        }
    }
}
