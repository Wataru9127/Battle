using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ワザ
/// </summary>
namespace Waza
{
    public class Waza
    {
        //ワザID
        public int ID { get; }

        //ワザの名前
        public string Name { get; }

        //ワザタイプ
        public PokemonType Type { get; }

        //ワザカテゴリ => 物理 / 特殊 / 変化
        public WazaCategory Category { get; }

        //威力
        public int Power { get; }

        //命中率
        public int Accuracy { get; }

        //対象
        public WazaTarget Target { get; }

        //優先度
        public int Priority { get; }
        public bool IsWazaPriority => Priority > 0;     //優先度をフラグ化したもの

        public IReadOnlyList<IWazaEffect> Effects { get; }

        public Waza(int id, string name, PokemonType type, WazaCategory category, int power,
            int accuracy, WazaTarget target, int priority, IReadOnlyList<IWazaEffect> effects)
        {
            ID = id;
            Name = name;
            Type = type;
            Category = category;
            Power = power;
            Accuracy = accuracy;
            Target = target;
            Priority = priority;
            Effects = effects;
        }
    }
}
