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
    
    //テラスタル判定
    public bool IsTerastallized { get; private set; }

    //テラスタルタイプ
    public PokemonType TerastalType { get; private set; }

    //現在の状態
    public PokemonCondition Condition { get; }

    //地面にいるか => フィールドの影響を受けるか
    public bool IsOnField
    {
        get
        {
            //ひこうタイプの場合
            if (TerastalType == Flying) return false;
            if (PokemonData.BaseInfo.Types.Contains(Flying)) return false;

            /*特性・状態・アイテムでの地面判定の追加*/

            return true;
        }
    }

    //ひんし判定
    public bool IsFainted => Condition.IsFainted;




    //========== その他必要なデータ ==========
    //形態変化データ
    private FormChangeData? currentForm;

    //一時的に上書きされる"形態変化専用タイプ"
    private IReadOnlyList<PokemonType>? formTypes { get; }

    //名前の簡易取得用
    public string Name => PokemonData.BaseInfo.Name;




    public BattlePokemon(PokemonData pokemonData)
    {
        PokemonData = pokemonData;
        PokemonData.SetOwner(this);
    }

    /// <summary>
    /// フォルムチェンジ・メガシンカ操作
    /// ・元に戻るときもこれを使う
    /// </summary>
    public void ApplyFormChange(FormChangeData form)
    {
        //変身データを取得
        currentForm = form
            ?? throw new ArgumentNullException(nameof(form));

        //========== 種族値の設定 ==========
        //=> 現在持っているポケモンのデータの中の種族値を変更する

        //フォルムチェンジ・メガシンカ後の種族値を取得
        var newBaseStats = form.BaseStats;

        //新しい種族値を設定
        PokemonData.Growth.SetBaseStats(newBaseStats);
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
    /// 現在のタイプを返す
    /// </summary>
    public bool HasType(PokemonType type)
    {
        //引数と同じタイプを返す
        return PokemonData.BaseInfo.Types.Contains(type);
    }

    /// <summary>
    /// テラスタル
    /// </summary>
    public void SetTerastal(PokemonType type)
    {
        IsTerastallized = true;
        TerastalType = type;
    }

    /// <summary>
    /// テラスタル解除
    /// ・きぜつ後に復活したときのため
    /// </summary>
    public void ClearTerastal()
    {
        IsTerastallized = false;
    }

}
