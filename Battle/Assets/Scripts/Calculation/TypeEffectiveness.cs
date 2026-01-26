using System.Collections.Generic;
using static PokemonType;

/// <summary>
/// タイプ相性計算
/// </summary>
namespace Calculator
{
    public static class TypeEffectiveness
    {
        /// <summary>
        /// 有効相性表
        /// => Dictionary<攻撃タイプ, Dictionary<防御タイプ, 倍率>>
        /// </summary>
        private static readonly Dictionary<PokemonType, Dictionary<PokemonType, float>> chart
            = new()
            {
                //ノーマル
                {
                    Normal, new()
                    {
                        { Rock, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //ほのお
                {
                    Fire, new()
                    {
                        { Grass, 2f },
                        { Ice, 2f },
                        { Bug, 2f },
                        { Steel, 2f },

                        { Fire, 0.5f },
                        { Water, 0.5f },
                        { Rock, 0.5f },
                        { Dragon, 0.5f },
                    }
                },

                //みず
                {
                    Water, new()
                    {
                        { Fire, 2f },
                        { Ground, 2f },
                        { Rock, 2f },

                        { Water, 0.5f },
                        { Grass, 0.5f },
                        { Dragon, 0.5f },
                    }
                },

                //でんき
                {
                    Electric, new()
                    {
                        { Water, 2f },
                        { Flying, 2f },

                        { Electric, 0.5f },
                        { Grass, 0.5f },
                        { Dragon, 0.5f },
                    }
                },

                //くさ
                {
                    Grass, new()
                    {
                        { Water, 2f },
                        { Ground, 2f },
                        { Rock, 2f },

                        { Fire, 0.5f },
                        { Grass, 0.5f },
                        { Poison, 0.5f },
                        { Flying, 0.5f },
                        { Bug, 0.5f },
                        { Dragon, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //こおり
                {
                    Ice, new()
                    {
                        { Grass, 2f },
                        { Ground, 2f },
                        { Flying, 2f },
                        { Dragon, 2f },

                        { Fire, 0.5f },
                        { Water, 0.5f },
                        { Ice, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //かくとう
                {
                    Fighting, new()
                    {
                        { Normal, 2f },
                        { Ice, 2f },
                        { Rock, 2f },
                        { Dark, 2f },
                        { Steel, 2f },

                        { Poison, 0.5f },
                        { Flying, 0.5f },
                        { Esper, 0.5f },
                        { Bug, 0.5f },
                        { Fairy, 0.5f },
                    }
                },

                //どく
                {
                    Poison, new()
                    {
                        { Grass, 2f },
                        { Fairy, 2f },

                        { Poison, 0.5f },
                        { Ground, 0.5f },
                        { Rock, 0.5f },
                        { Ghost, 0.5f },
                    }
                },

                //じめん
                {
                    Ground, new()
                    {
                        { Fire, 2f },
                        { Electric, 2f },
                        { Poison, 2f },
                        { Rock, 2f },
                        { Steel, 2f },

                        { Grass, 0.5f },
                        { Bug, 0.5f },
                    }
                },

                //ひこう
                {
                    Flying, new()
                    {
                        { Grass, 2f },
                        { Fighting, 2f },
                        { Bug, 2f },

                        { Electric, 0.5f },
                        { Rock, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //エスパー
                {
                    Esper, new()
                    {
                        { Fighting, 2f },
                        { Poison, 2f },

                        { Esper, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //むし
                {
                    Bug, new()
                    {
                        { Grass, 2f },
                        { Esper, 2f },
                        { Dark, 2f },

                        { Fire, 0.5f },
                        { Fighting, 0.5f },
                        { Poison, 0.5f },
                        { Flying, 0.5f },
                        { Ghost, 0.5f },
                        { Steel, 0.5f },
                        { Fairy, 0.5f },
                    }
                },

                //いわ
                {
                    Rock, new()
                    {
                        { Fire, 2f },
                        { Ice, 2f },
                        { Flying, 2f },
                        { Bug, 2f },

                        { Fighting, 0.5f },
                        { Ground, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //ゴースト
                {
                    Ghost, new()
                    {
                        { Esper, 2f },
                        { Ghost, 2f },

                        { Dark, 0.5f },
                    }
                },

                //ドラゴン
                {
                    Dragon, new()
                    {
                        { Dragon, 2f },

                        { Steel, 0.5f },
                    }
                },

                //あく
                {
                    Dark, new()
                    {
                        { Esper, 2f },
                        { Ghost, 2f },

                        { Fighting, 0.5f },
                        { Dark, 0.5f },
                        { Fairy, 0.5f },
                    }
                },

                //はがね
                {
                    Steel, new()
                    {
                        { Ice, 2f },
                        { Rock, 2f },
                        { Fairy, 2f },

                        { Fire, 0.5f },
                        { Water, 0.5f },
                        { Electric, 0.5f },
                        { Steel, 0.5f },
                    }
                },

                //フェアリー
                {
                    Fairy, new()
                    {
                        { Fighting, 2f },
                        { Dragon, 2f },
                        { Dark, 2f },

                        { Fire, 0.5f },
                        { Poison, 0.5f },
                        { Steel, 0.5f },
                    }
                },
            };

        /// <summary>
        /// 無効タイプ相性
        /// </summary>
        private static readonly Dictionary<PokemonType, HashSet<PokemonType>> invalidChart
            = new()
            {
                { PokemonType.Normal,   new() { PokemonType.Ghost } },
                { PokemonType.Fighting, new() { PokemonType.Ghost } },
                { PokemonType.Ghost,    new() { PokemonType.Normal } },
                { PokemonType.Electric, new() { PokemonType.Ground } },
                { PokemonType.Poison,   new() { PokemonType.Steel } },
                { PokemonType.Ground,   new() { PokemonType.Flying } },
                { PokemonType.Esper,    new() { PokemonType.Dark } },
                { PokemonType.Dragon,   new() { PokemonType.Fairy } },
            };

        /// <summary>
        /// 無効判定
        /// </summary>
        public static bool IsInvalid(PokemonType wazaType, PokemonType defenderType)
        {
            return invalidChart.TryGetValue(wazaType, out var defenders)
                && defenders.Contains(defenderType);
        }

        /// <summary>
        /// タイプ相性倍率を取得
        /// </summary>
        public static float GetRate(PokemonType wazaType, IReadOnlyList<PokemonType> types)
        {
            float rate = 1f;

            foreach (var defenderType in types)
            {
                // 無効が1つでもあれば0でreturn
                if (IsInvalid(wazaType, defenderType))
                {
                    return 0f;
                }

                //倍率をかける
                rate *= GetSingleRate(wazaType, defenderType);
            }

            return rate;
        }

        /// <summary>
        /// 単一タイプに対する倍率
        /// </summary>
        private static float GetSingleRate(PokemonType wazaType, PokemonType defenderType)
        {
            if (chart.TryGetValue(wazaType, out var table) &&
                table.TryGetValue(defenderType, out var rate))
            {
                return rate;
            }

            return 1;
        }
    }
}
