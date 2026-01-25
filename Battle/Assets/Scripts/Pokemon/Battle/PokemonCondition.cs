using System.Collections.Generic;
using UnityEngine;
using Battle;

/// <summary>
/// HP・状態異常・能力ランク
/// </summary>
public class PokemonCondition
{
    //この情報を所有しているポケモン
    public PokemonData Owner { get; }

    //MaxHP
    public int MaxHP { get; }

    //現在のHP
    public int CurrentHP { get; private set; }

    //状態異常 まひ・やけど など
    public NonVolatileStatus NonVolatile { get; private set; }

    //状態異常 こんらん など
    public List<VolatileStatus> VolatileStatuses { get; }

    //現在の能力ランク(H・A・B・C・D・S)
    private Dictionary<PokemonStatType, int> rank { get; }

    //気絶判定
    public bool IsFainted => CurrentHP <= 0;

    /// <summary>
    /// 初期化
    /// </summary>
    public PokemonCondition(int maxHP, PokemonData owner)
    {
        //HP
        MaxHP = maxHP;
        CurrentHP = maxHP;

        //状態異常
        VolatileStatuses = new List<VolatileStatus>();

        //能力ランク
        rank = new Dictionary<PokemonStatType, int>();
        foreach (PokemonStatType stat in System.Enum.GetValues(typeof(PokemonStatType)))
        {
            rank[stat] = 0;
        }

        //所有ポケモン
        Owner = owner;
    }

    /// <summary>
    /// NonVolatileの解除
    /// </summary>
    public void RemoveNonVolatileStatus()
    {
        NonVolatile = null;
    }


    // --------------------
    // HP 操作
    // --------------------

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        CurrentHP = Mathf.Clamp(CurrentHP - damage, 0, MaxHP);
    }

    /// <summary>
    /// 回復する
    /// </summary>
    /// <param name="heal"></param>
    public void Heal(int heal)
    {
        CurrentHP = Mathf.Clamp(CurrentHP + heal, 0, MaxHP);
    }


    // --------------------
    // 状態異常付与
    // --------------------

    /// <summary>
    /// 状態異常を付与
    /// どく・やけど・まひ など
    /// </summary>
    public bool ApplyNonVolatileStatus(NonVolatileStatus status, BattleContext context)
    {
        //既にあれば付与しない
        if (NonVolatile != null) return false;

        //付与
        NonVolatile = status;

        //NonVolatileStatusに通知
        status.OnApply(this, context);

        return true;
    }

    /// <summary>
    /// 一時的な状態異常を付与
    /// こんらん、バインド など
    /// </summary>
    /// <param name="status"></param>
    /// <param name="context"></param>
    public bool AddVolatileStatus(VolatileStatus status, BattleContext context)
    {
        //同じ状態異常は付与しない
        foreach (var v in VolatileStatuses)
        {
            if (v.GetType() == status.GetType())
            {
                return false;
            }
        }

        //付与
        VolatileStatuses.Add(status);

        //VolatileStatusに通知
        status.OnApply(Owner.Owner, context);

        return true;
    }



    // --------------------
    // 能力ランク
    // --------------------

    /// <summary>
    /// 能力ランクの取得
    /// </summary>
    public int GetRank(PokemonStatType stat)
    {
        return rank.TryGetValue(stat, out var value) ? value : 0;
    }

    /// <summary>
    /// 能力ランクの増減
    /// </summary>
    public int AddRank(PokemonStatType stat, int value)
    {
        int before = rank[stat];
        rank[stat] = Mathf.Clamp(before + value, -6, 6);
        return rank[stat] - before;
    }

    /// <summary>
    /// 能力ランクのリセット
    /// </summary>
    private void ResetRank()
    {
        foreach (var key in rank.Keys)
        {
            rank[key] = 0;
        }
    }



    // --------------------
    // ターン処理
    // --------------------

    /// <summary>
    /// ターン開始時
    /// </summary>
    public void OnTurnStart(BattleContext context)
    {
        //ねむり・まひ・こおり などの処理
        NonVolatile?.OnTurnStart(this, context);

        if (NonVolatile != null && NonVolatile.IsExpired)
        {
            NonVolatile = null;
        }
    }

    /// <summary>
    /// ターン終了時
    /// </summary>
    public void OnTurnEnd(BattleContext context)
    {
        /*1.どく・やけど などのダメージ*/
        NonVolatile?.OnTurnEnd(this, context);

        /*2.バインド・ちょうはつ の解除など*/
        //逆順に処理する => 正順だと削除してから次を読み込むため、すべて見れなくなるため
        for (int i = VolatileStatuses.Count - 1; i >= 0; i--)
        {
            //ターンカウンター減少、のろいダメージ、ちょうはつ解除などの処理
            var status = VolatileStatuses[i];
            status.OnTurnEnd(Owner.Owner, context);

            if (status.IsExpired(Owner.Owner, context))
            {
                VolatileStatuses.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 行動可否判定
    /// </summary>
    public bool CanAction(BattleContext context)
    {
        //ねむり・まひ・こおり などで行動不能
        if (NonVolatile != null &&
            !NonVolatile.CanAction(this, context))
        {
            return false;
        }

        //こんらん・メロメロ・ちょうはつ などの行動制限
        foreach (var v in VolatileStatuses)
        {
            if (!v.CanAction(Owner.Owner, context))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 交代時に呼ぶ関数
    /// </summary>
    public void SwitchOut()
    {
        //能力ランクのリセット
        ResetRank();

        //こんらん・のろい・メロメロ などの解除
        VolatileStatuses.Clear();

        if (NonVolatile != null)
        {
            NonVolatile = NonVolatile.OnSwitchOut();
        }
    }
}
