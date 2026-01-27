using Battle;
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
        
        //参照する能力 : 攻撃側
        public PokemonStatType AttackStat { get; }

        //参照する能力 : 防御側
        public PokemonStatType DefenseStat { get; }

        //ダメージ技判定
        public bool IsDamage { get; }

        //追加効果の有無
        public virtual bool HasAdditionalEffect => false;

        //追加効果の発動確率
        public virtual int AdditionalEffetRate => 0;

        public WazaBase(int id, string name, PokemonType type, WazaCategory category,
            int power, int accuracy, WazaTarget target, int priority,
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
            Property = property;
            AttackStat = attackStat;
            DefenseStat = defenseStat;
            IsDamage = isDamage;
        }

        /// <summary>
        /// 技の主目的
        /// ・"分類"ではなく、内部用の判断材料
        /// </summary>
        public WazaEffectType EffectType { get; protected set; } = WazaEffectType.None;

        /// <summary>
        /// 威力を返す
        /// </summary>
        public virtual int ModifyPower(BattlePokemon attacker, BattlePokemon defender)
        {
            return Power;
        }

        /// <summary>
        /// 技の追加効果
        /// </summary>
        public virtual void ApplyAdditionalEffect(BattlePokemon attacker, BattlePokemon defender, Battle.BattleContext context) { }

        /// <summary>
        /// 追加効果を試みる
        /// ・追加効果を処理する前に止めるかどうか
        /// </summary>
        public void TryApplyAdditionalEffect(BattleContext context, BattlePokemon attacker, BattlePokemon defender)
        {
            //追加効果がない技なら無視
            if (!HasAdditionalEffect) return;

            //特性・フィールドによる無効化チェック
            if (!context.CanApplyAdditionalEffect(attacker, defender, this)) return;

            //確率判定
            int roll = context.Random.Next(100);
            if (roll >= AdditionalEffetRate) return;

            //追加効果の適用
            ApplyAdditionalEffect(attacker, defender, context);
        }

        /// <summary>
        /// 状態異常が主目的の技のフラグ
        /// (例) おにび、でんじは、あやしいひかり など
        /// ・ミストフィールド状態での無効化判定
        /// ・おうごんのからだでの無効化判定
        /// </summary>
        public bool CausesStatusAilment =>
            EffectType == WazaEffectType.Sleep ||
            EffectType == WazaEffectType.Poison ||
            EffectType == WazaEffectType.Burn ||
            EffectType == WazaEffectType.Paralysis ||
            EffectType == WazaEffectType.Freeze ||
            EffectType == WazaEffectType.Confusion;
    }
}
