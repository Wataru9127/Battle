using System;
using System.Collections.Generic;
using Field;
using Waza;
using Weather;

/// <summary>
/// バトル全体の状態を保持する
/// ・フィールド
/// ・天候
/// ・参加ポケモン
/// </summary>
namespace Battle
{
    public class BattleContext
    {
        //乱数源
        public Random Random { get; } = new Random();

        //ログ
        private readonly List<string> log = new();
        public IReadOnlyList<string> Log => log;

        


        // ===== フィールド・天候 =====
        public FieldManager FieldManager { get; }
        public WeatherManager WeatherManager { get; }



        // ===== 参加ポケモン =====
        //将来的に対応するためListで管理
        private readonly List<BattlePokemon> battlePokemons = new List<BattlePokemon>();

        public BattleContext(IEnumerable<BattlePokemon> battlePokemon)
        {
            battlePokemons.AddRange(battlePokemon);
            FieldManager = new FieldManager();
            WeatherManager = new WeatherManager();
        }

        /// <summary>
        /// バトルに参加している全ポケモンを取得
        /// </summary>
        public IReadOnlyList<BattlePokemon> GetAllBattlePokemon()
        {
            // 自分・相手・ダブル対応想定
            return battlePokemons;

            //yield break;
        }

        



        // ===== ログ管理 =====
        public void AddLog(string message)
        {
            log.Add(message);
        }

        public void ClearLog()
        {
            log.Clear();
        }



        // ===== フィールド管理 =====
        /// <summary>
        /// フィールド展開
        /// </summary>
        /// <param name="field"></param>
        public void SetField(FieldBase field)
        {
            FieldManager.SetField(field, this);
        }

        // ===== 天候管理 =====
        public void SetWeather(WeatherBase weather, BattlePokemon owner)
        {
            WeatherManager.SetWeather(weather, this, owner);
        }



        // ===== ターン終了処理 =====
        public void EndTurn()
        {
            //フィールド処理
            FieldManager.OnTurnEnd(this);

            //天候処理
            WeatherManager.OnTurnEnd(this);
        }

        /// <summary>
        /// 技の追加効果を許すかどうかの判断
        /// </summary>
        public bool CanApplyAdditionalEffect(BattlePokemon attacker, BattlePokemon defender, WazaBase wazaBase)
        {
            //対象が場にいない
            if (defender == null || defender.IsFainted) return false;

            //フィールド判定
            CanApplyAdditionalEffectByField(attacker, defender, wazaBase);

            //天候判定
            CanApplyAdditionalEffectByWeather(attacker, defender, wazaBase);

            //特性判定
            CanApplyAdditionalEffectByAbility(attacker, defender, wazaBase);

            //もちもの判定
            CanApplyAdditionalEffectByItem(attacker, defender, wazaBase);

            return true;
        }

        /// <summary>
        /// フィールドでの無効化判定
        /// </summary>
        private bool CanApplyAdditionalEffectByField(BattlePokemon attacker, BattlePokemon defender, WazaBase wazaBase)
        {
            //ミストフィールド => 状態異常技を無視する
            if (FieldManager.Current is Field.MistyField)
            {
                if (defender.IsOnField && wazaBase.CausesStatusAilment) return false;
            }

            /*その他、フィールドでの技判定の無効化*/

            return true;
        }

        /// <summary>
        /// 天候による無効化判定
        /// </summary>
        private bool CanApplyAdditionalEffectByWeather(BattlePokemon attacker, BattlePokemon defender, WazaBase wazaBase)
        {
            /*追加効果を受けない天候の判断*/

            return true;
        }

        /// <summary>
        /// 特性での無効化判定
        /// </summary>
        private bool CanApplyAdditionalEffectByAbility(BattlePokemon attacker, BattlePokemon defender, WazaBase wazaBase)
        {
            /*追加効果を受けない特性の判断
             りんぷん*/

            return true;
        }

        /// <summary>
        /// もちものでの無効化判定
        /// </summary>
        private bool CanApplyAdditionalEffectByItem(BattlePokemon attacker, BattlePokemon defender, WazaBase wazaBase)
        {
            /*追加効果を受けない持ち物の判断
             現状、おんみつマント のみ*/

            return true;
        }
    }
}
