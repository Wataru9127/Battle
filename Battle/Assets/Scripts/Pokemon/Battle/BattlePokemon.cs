using System;
using System.Collections.Generic;
using System.Linq;
using Battle;
using static PokemonType;
//using Ability;

/// <summary>
/// バトル状況も含めたポケモンの情報
/// </summary>
public class BattlePokemon
{
    //========== 基本情報 ==========
    //ポケモンの情報
    public PokemonData PokemonData { get; }

    //とくせい(実体・ロジック)
    public PokemonAbility Ability { get; }

    //テラスタル判定
    public bool IsTerastallized { get; private set; }

    //テラスタルタイプ
    public PokemonType TerastalType { get; private set; }

    //現在の状態
    public PokemonCondition Condition { get; }

    //地面にいるか
    //・フィールドの影響を受けるか
    //・地面タイプの技を受けるか
    public bool IsOnField
    {
        get
        {
            /*特性・状態・アイテムでの地面判定の追加*/

            /*ひこうタイプの場合*/
            if (HasType(Flying)) return true;

            return true;
        }
    }

    //ひんし判定
    public bool IsFainted => Condition.IsFainted;




    //========== その他必要なデータ ==========
    //形態変化データ
    public FormChangeData? currentForm { get; private set; }

    //名前の簡易取得用
    public string Name
    {
        get
        {
            if (currentForm != null) return currentForm.FormName;

            return PokemonData.BaseInfo.Name;
        }
    }



    public BattlePokemon(PokemonData pokemonData, PokemonType terastal)
    {
        PokemonData = pokemonData;
        PokemonData.SetOwner(this);
        PokemonData.SetBaseStats();
        //Ability = AbilityFactory.Create(data.Ability, this);
        TerastalType = terastal;
        IsTerastallized = false;
    }

    public void OnBattleEvent(BattleEvent battleEvent, BattleContext context)
    {
        //PokemonData.Ability?.OnBattleEvent(battleEvent, context, this);
    }

    public void OnTurnStart(BattleContext context) { }

    /// <summary>
    /// フォルムチェンジ・メガシンカ操作
    /// ・元に戻るときもこれを使う
    /// </summary>
    public void ApplyFormChange(FormChangeData form)
    {
        //変身データを取得
        currentForm = form
            ?? throw new ArgumentNullException(nameof(form));

        //フォルムチェンジ・メガシンカ後の種族値を取得
        var newBaseStats = form.BaseStats;

        //新しい種族値でステータス計算
        //変更後のHP処理
    }

    /// <summary>
    /// 交代可否判定
    /// </summary>
    public bool CanSwitch(BattleContext context)
    {
        foreach (var status in Condition.VolatileStatuses)
        {
            if (!status.CanSwitch(this, context))
            {
                return false;
            }
        }

        /*手持ちがひんし状態のときは交代できなくする*/
        return true;
    }

    /// <summary>
    /// 指定のタイプがあるかどうか
    /// </summary>
    public bool HasType(PokemonType type)
    {
        //テラスタル
        if (IsTerastallized)
        {
            return TerastalType == type;
        }

        //フォルムチェンジ・メガシンカ
        if (currentForm != null)
        {
            return currentForm.Types.Contains(type);
        }

        //通常
        return PokemonData.BaseInfo.Types.Contains(type);
    }

    /// <summary>
    /// 元のタイプ参照
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool HasBaseType(PokemonType type)
    {
        // フォルム・メガ後のタイプを元タイプとして扱う
        if (currentForm != null)
        {
            return currentForm.Types.Contains(type);
        }

        return PokemonData.BaseInfo.Types.Contains(type);
    }

    /// <summary>
    /// テラスタル
    /// </summary>
    public void SetTerastal()
    {
        IsTerastallized = true;
    }

    /// <summary>
    /// テラスタル解除
    /// ・きぜつ後に復活したときのため
    /// </summary>
    public void ClearTerastal()
    {
        IsTerastallized = false;
    }

    /// <summary>
    /// 実数値取得
    /// </summary>
    /// <returns></returns>
    public BaseStats GetBaseStats()
    {
        return PokemonData.GetBaseStats(currentForm);
    }

}
