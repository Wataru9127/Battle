using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 性格の種類
public enum NatureType
{
    Hardy, Lonely, Brave, Adamant, Naughty,
    Bold, Docile, Relaxed, Impish, Lax,
    Timid, Hasty, Serious, Jolly, Naive,
    Modest, Mild, Quiet, Bashful, Rash,
    Calm, Gentle, Sassy, Careful, Quirky
}
public class Nature
{
    //せいかく名
    public string name { get; private set; }

    //種類
    public NatureType Type { get; private set; }

    //上昇する能力値
    public PokemonAbility IncreaseAbility { get; private set; }

    //下降する能力値
    public PokemonAbility DecreaseAbility { get; private set; }

    /// <summary>
    /// 日本語名取得関数
    /// </summary>
    public string JapaneseName
    {
        get
        {
            return typeToJapanese[Type];
        }
    }

    //日本語名と英語名のDictionary
    private static readonly Dictionary<NatureType, string> typeToJapanese = new Dictionary<NatureType, string>
    {
        { NatureType.Hardy, "がんばりや" },
        { NatureType.Lonely, "さみしがり" },
        { NatureType.Brave, "ゆうかん" },
        { NatureType.Adamant, "いじっぱり" },
        { NatureType.Naughty, "やんちゃ" },
        { NatureType.Bold, "ずぶとい" },
        { NatureType.Docile, "すなお" },
        { NatureType.Relaxed, "のんき" },
        { NatureType.Impish, "わんぱく" },
        { NatureType.Lax, "のんき" },
        { NatureType.Timid, "せっかち" },
        { NatureType.Hasty, "ようき" },
        { NatureType.Serious, "まじめ" },
        { NatureType.Jolly, "ようき" },
        { NatureType.Naive, "むじゃき" },
        { NatureType.Modest, "ひかえめ" },
        { NatureType.Mild, "おっとり" },
        { NatureType.Quiet, "れいせい" },
        { NatureType.Bashful, "てれや" },
        { NatureType.Rash, "むてき" },
        { NatureType.Calm, "おだやか" },
        { NatureType.Gentle, "しんちょう" },
        { NatureType.Sassy, "しんちょう" },
        { NatureType.Careful, "ようじゅう" },
        { NatureType.Quirky, "きまぐれ" }
    };
    private static readonly Dictionary<string, NatureType> japaneseToType = new Dictionary<string, NatureType>();

    /// <summary>
    /// 共有データ
    /// クラスが最初に参照されたときに一度だけ呼ばれ
    /// 日本語名 <-> NatureTypeのDictionaryを初期化して使えるようにする
    /// </summary>
    static Nature()
    {
        foreach (var kvp in typeToJapanese)
        {
            japaneseToType[kvp.Value] = kvp.Key;
        }
    }

    /// <summary>
    /// 個々のオブジェクトの状態の初期化
    /// new Nature()で呼ばれ生成される
    /// 生成されたオブジェクトの性格タイプと補正を設定する
    /// </summary>
    /// <param name="type"></param>
    public Nature(NatureType type)
    {
        Type = type;
        SetModifiers(type);
    }

    /// <summary>
    /// 日本語名からせいかくを作成する関数
    /// </summary>
    /// <param name="japaneseName"></param>
    /// <returns></returns>
    public static Nature FromJapaneseName(string japaneseName)
    {
        if (!japaneseToType.ContainsKey(japaneseName))
        {
            Debug.LogWarning($"不明な性格名: {japaneseName} → Hardyに設定");
            return new Nature(NatureType.Hardy); // デフォルト
        }

        return new Nature(japaneseToType[japaneseName]);
    }

    /// <summary>
    /// 性格補正設定関数
    /// </summary>
    /// <param name="type"></param>
    private void SetModifiers(NatureType type)
    {
        switch (type)
        {
            case NatureType.Lonely: IncreaseAbility = PokemonAbility.Attack; DecreaseAbility = PokemonAbility.Defense; break;
            case NatureType.Brave: IncreaseAbility = PokemonAbility.Attack; DecreaseAbility = PokemonAbility.Speed; break;
            case NatureType.Adamant: IncreaseAbility = PokemonAbility.Attack; DecreaseAbility = PokemonAbility.SpecialAttack; break;
            case NatureType.Naughty: IncreaseAbility = PokemonAbility.Attack; DecreaseAbility = PokemonAbility.SpecialDefense; break;
            case NatureType.Bold: IncreaseAbility = PokemonAbility.Defense; DecreaseAbility = PokemonAbility.Attack; break;
            case NatureType.Relaxed: IncreaseAbility = PokemonAbility.Defense; DecreaseAbility = PokemonAbility.Speed; break;
            case NatureType.Impish: IncreaseAbility = PokemonAbility.Defense; DecreaseAbility = PokemonAbility.SpecialAttack; break;
            case NatureType.Lax: IncreaseAbility = PokemonAbility.Defense; DecreaseAbility = PokemonAbility.SpecialDefense; break;
            case NatureType.Timid: IncreaseAbility = PokemonAbility.Speed; DecreaseAbility = PokemonAbility.Attack; break;
            case NatureType.Hasty: IncreaseAbility = PokemonAbility.Speed; DecreaseAbility = PokemonAbility.Defense; break;
            case NatureType.Jolly: IncreaseAbility = PokemonAbility.Speed; DecreaseAbility = PokemonAbility.SpecialAttack; break;
            case NatureType.Naive: IncreaseAbility = PokemonAbility.Speed; DecreaseAbility = PokemonAbility.SpecialDefense; break;
            case NatureType.Modest: IncreaseAbility = PokemonAbility.SpecialAttack; DecreaseAbility = PokemonAbility.Attack; break;
            case NatureType.Mild: IncreaseAbility = PokemonAbility.SpecialAttack; DecreaseAbility = PokemonAbility.Defense; break;
            case NatureType.Quiet: IncreaseAbility = PokemonAbility.SpecialAttack; DecreaseAbility = PokemonAbility.Speed; break;
            case NatureType.Rash: IncreaseAbility = PokemonAbility.SpecialAttack; DecreaseAbility = PokemonAbility.SpecialDefense; break;
            case NatureType.Calm: IncreaseAbility = PokemonAbility.SpecialDefense; DecreaseAbility = PokemonAbility.Attack; break;
            case NatureType.Gentle: IncreaseAbility = PokemonAbility.SpecialDefense; DecreaseAbility = PokemonAbility.Defense; break;
            case NatureType.Sassy: IncreaseAbility = PokemonAbility.SpecialDefense; DecreaseAbility = PokemonAbility.Speed; break;
            case NatureType.Careful: IncreaseAbility = PokemonAbility.SpecialDefense; DecreaseAbility = PokemonAbility.SpecialAttack; break;

            // 補正なし
            case NatureType.Hardy:
            case NatureType.Docile:
            case NatureType.Serious:
            case NatureType.Bashful:
            case NatureType.Quirky:
            default:
                IncreaseAbility = DecreaseAbility = PokemonAbility.HP; // 補正なし
                break;
        }
    }


    /// <summary>
    /// 性格補正倍率の取得
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    public float GetModifier(PokemonAbility ability)
    {
        if (ability == IncreaseAbility) return 1.1f;
        if (ability == DecreaseAbility) return 0.9f;
        return 1.0f;
    }
}
