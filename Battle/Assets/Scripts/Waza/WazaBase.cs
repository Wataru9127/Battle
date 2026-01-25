using System.Collections.Generic;

/// <summary>
/// ワザ
/// </summary>
namespace Waza
{
    public class WazaBase
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

        //性質(接触・音技 など)
        public WazaProperty Property { get; }

        //追加効果
        public IReadOnlyList<IWazaEffect> Effects { get; }

        //参照する能力 : 攻撃側
        public PokemonStatType AttackStat { get; }

        //参照する能力 : 防御側
        public PokemonStatType DefenseStat { get; }

        //ダメージ技判定
        public bool IsDamage { get; }

        public WazaBase(int id, string name, PokemonType type, WazaCategory category, int power,
            int accuracy, WazaTarget target, int priority,IReadOnlyList<IWazaEffect> effects,
            WazaProperty property, PokemonStatType attackStat, PokemonStatType defenseStat,
            bool isDamage)
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
            Property = property;
            AttackStat = attackStat;
            DefenseStat = defenseStat;
            IsDamage = isDamage;
        }

        public virtual int ModifyPower(BattlePokemon attacker, BattlePokemon defender)
        {
            return Power;
        }
    }
}
