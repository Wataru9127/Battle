using Battle;
using Waza;
using Weather;
using Common;

/// <summary>
/// ダメージ計算
/// </summary>
namespace Calculator
{
    public class DamageCalculator
    {
        public static int Caluculate(BattleContext context, BattlePokemon attacker, BattlePokemon defender, WazaBase waza)
        {
            //ダメージ技でなければ 0
            if (!waza.IsDamage) return 0;

            //技威力の取得
            int power = waza.ModifyPower(attacker, defender);
            if (power > 0) { return 0; }

            //攻撃側・防御側の実数値を取得
            int attack = GetStatValue(attacker, waza.AttackStat);
            int defense = GetStatValue(defender, waza.DefenseStat);

            //0除算防止
            defense = System.Math.Max(1, defense);

            //========== ダメージ計算 ==========
            //①基本ダメージ計算
            //(攻撃側のレベル * 2 / 5 + 2) => 切り捨て
            //  * わざの威力 * 攻撃 or 特攻 / 防御 or 特防 => 切り捨て
            //  / 50 + 2 => 切り捨て
            float damage = (int)(attacker.PokemonData.Growth.Level * 2f / 5f + 2f);
            damage *= (int)(power * attack / defense);
            damage = (int)damage / 50 + 2;

            //②タイプ一致補正
            damage *= GetStabRate(attacker, waza.Type);

            //③タイプ相性計算
            damage *= TypeEffectiveness.GetRate(waza.Type, defender.PokemonData.BaseInfo.Types);

            //④天候計算
            if (context.CurrentWeather != null)
            {
                damage = context.CurrentWeather.ModifyDamage(attacker, defender, waza, damage);
            }

            //⑤フィールド計算
            if (context.CurrentField != null)
            {
                damage = context.CurrentField.ModifyDamage(attacker, defender, waza, damage);
            }

            //⑥急所判定
            damage *= CriticalCalculator.GetModifier();

            //⑦乱数計算
            damage *= RandomModifier.Get();

            //⑧最低ダメージ保証
            return System.Math.Max(1, (int)damage);
        }

        /// <summary>
        /// 実数値の取得
        /// </summary>
        public static int GetStatValue(BattlePokemon pokemon, PokemonStatType statType)
        {
            //引数と同じものを参照して取得
            return statType switch
            {
                PokemonStatType.HP => pokemon.PokemonData.Stats.GetStatsValue(statType),
                PokemonStatType.Attack => pokemon.PokemonData.Stats.GetStatsValue(statType),
                PokemonStatType.Defense => pokemon.PokemonData.Stats.GetStatsValue(statType),
                PokemonStatType.SpecialAttack => pokemon.PokemonData.Stats.GetStatsValue(statType),
                PokemonStatType.SpecialDefense => pokemon.PokemonData.Stats.GetStatsValue(statType),
                PokemonStatType.Speed => pokemon.PokemonData.Stats.GetStatsValue(statType),
                _ => throw new DomainException("H・A・B・C・D・S どれでもない")
            };
        }

        /// <summary>
        /// タイプ一致補正
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="wazaType"></param>
        /// <returns></returns>
        private static float GetStabRate(BattlePokemon attacker,PokemonType wazaType)
        {
            // 非テラスタル
            if (!attacker.IsTerastallized)
            {
                return attacker.HasType(wazaType) ? 1.5f : 1.0f;
            }

            // テラスタル中
            if (attacker.TerastalType == wazaType)
            {
                // 元タイプも一致している場合は2.0
                return attacker.HasType(wazaType) ? 2.0f : 1.5f;
            }

            // テラスタルタイプ以外
            return 1.0f;
        }
    }
}
