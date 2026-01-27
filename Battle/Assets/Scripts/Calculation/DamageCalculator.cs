using Battle;
using Waza;
using Weather;
using Common;
using System.Collections.Generic;

/// <summary>
/// ダメージ計算
/// </summary>
namespace Calculator
{
    public class DamageCalculator
    {
        /// <summary>
        /// ダメージ計算
        /// </summary>
        public static int Caluculate(BattleContext context, BattlePokemon attacker, BattlePokemon defender, WazaBase waza)
        {
            //ダメージ技でなければ 0
            if (!waza.IsDamage) return 0;

            //技威力の取得
            int power = waza.ModifyPower(attacker, defender);
            if (power <= 0) { return 0; }

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
            int baseDamage = (attacker.PokemonData.Growth.Level * 2) / 5 + 2;
            baseDamage *= (power * attack / defense);
            baseDamage = baseDamage / 50 + 2;
            float damage = baseDamage;

            //②タイプ一致補正
            damage *= GetStabRate(attacker, waza.Type);

            //③タイプ相性計算
            IReadOnlyList<PokemonType> type;

            //テラスタルしている => テラスタイプで計算
            if (defender.IsTerastallized)
            {
                type = new List<PokemonType>
                {
                    defender.TerastalType
                };
            }
            //フォルムチェンジ・メガシンカしている => 変身後のタイプで計算
            else if (defender.currentForm != null)
            {
                type = defender.currentForm.Types;
            }
            //なにも変化していない => 元のタイプで計算
            else
            {
                type = defender.PokemonData.BaseInfo.Types;
            }

            float typeRate = TypeEffectiveness.GetRate(waza.Type, type);
            if (typeRate == 0) return 0;    //相性無効は即return
            damage *= typeRate;

            //④天候計算
            damage *= context.WeatherManager.Current.GetDamageRate(attacker, defender, waza);

            //⑤フィールド計算
            damage *= context.FieldManager.Current.GetDamageRate(attacker, defender, waza);

            //⑥急所判定
            damage *= CriticalCalculator.GetModifier();

            //⑦乱数計算
            damage *= RandomModifier.Get();

            if (damage <= 0) return 0;

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
