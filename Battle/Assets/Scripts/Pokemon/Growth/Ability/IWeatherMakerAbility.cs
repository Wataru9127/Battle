using Battle;
using Weather;

public interface IWeatherMakerAbility
{
    WeatherBase CreateWeather(BattleContext context, BattlePokemon owner);
}