using System;
using System.Collections.Generic;
using Battle;
using Field;
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
        public FieldEffect CurrentField { get; private set; }
        public Weather.Weather CurrentWeather { get; private set; }


        // ===== 参加ポケモン =====
        //将来的に対応するためListで管理
        private readonly List<BattlePokemon> battlePokemons = new List<BattlePokemon>();

        public BattleContext(IEnumerable<BattlePokemon> battlePokemon)
        {
            battlePokemons.AddRange(battlePokemon);
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
        public void SetField(FieldEffect field)
        {
            //フィールド展開中に同じフィールドが展開されようとしたとき
            //=> なにもしない
            if (CurrentField != null && 
                CurrentField.GetType() == field.GetType())
            {
                return;
            }

            //フィールド展開中に別フィールドが展開されようとしたとき
            //=> 展開中のフィールドを終了して新しいフィールド展開
            if (CurrentField != null)
            {
                CurrentField.OnEnd(this);
            }

            CurrentField = field;

            if (CurrentField != null)
            {
                CurrentField.OnStart(this);
            }
        }

        /// <summary>
        /// メイカー特性処理
        /// </summary>
        public void TryActivateFieldMaker(bool allowOverride)
        {
            /*if (CurrentField != null && !allowOverride) return;

            var makers = new List<(BattlePokemon pokemon, FieldMakerAbility ability)>();

            foreach (var pokemon in GetAllBattlePokemon())
            {
                //きぜつ判定
                if (pokemon.IsFainted) continue;

                //フィールドメイカーを持つポケモンを取得
                if (pokemon.PokemonData.Ability is FieldMakerAbility maker)
                {
                    makers.Add((pokemon, maker));
                }
            }

            //フィールドメイカーを持つポケモンがいなかったら終了
            if (makers.Count == 0) return;

            makers.Sort((a, b) =>
            {
                int diff = b.pokemon.PokemonData.Stats.GetStats(PokemonStatType.Speed).CompareTo(a.pokemon.PokemonData.Stats.GetStats(PokemonStatType.Speed));

                if (diff != 0) return diff;

                //同速ならランダム
                return Random.Next(2) == 0 ? -1 : 1;
            });

            var winner = makers[0];
            winner.ability.Activate(this, winner.pokemon);*/
        }

        // ===== 天候管理 =====
        public void SetWeather(Weather.Weather weather)
        {
            if (CurrentWeather != null)
            {
                CurrentWeather.OnEnd(this);
            }

            CurrentWeather = weather;

            if (CurrentWeather != null)
            {
                CurrentWeather.OnStart(this);
            }
        }



        // ===== ターン終了処理 =====
        public void EndTurn()
        {
            //フィールド処理
            if (CurrentField != null)
            {
                CurrentField.OnTurnEnd(this);

                if (CurrentField.Tick())
                {
                    var expiredField = CurrentField;

                    CurrentField = null;
                    expiredField.OnEnd(this);
                    

                    //フィールドメイカーのチェック
                    TryActivateFieldMaker(allowOverride: false);
                }
            }

            //天候処理
            if (CurrentWeather != null)
            {
                CurrentWeather.OnTurnEnd(this);

                if (CurrentWeather.Tick())
                {
                    CurrentWeather.OnEnd(this);
                    CurrentWeather = null;
                }
            }
        }
    }
}
