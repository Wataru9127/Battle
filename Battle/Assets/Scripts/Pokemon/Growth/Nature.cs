using System.Collections.Generic;
using UnityEngine;
using static PokemonStatType;
using static NatureType;

/// <summary>
/// 内部データ構造
/// </summary>
public class NatureData
{
    public string Name;
    public NatureType Type;
    public PokemonStatType IncreaseStat;
    public PokemonStatType DecreaseStat;
    public string IncreaseName;
    public string DecreaseName;

    public NatureData(string name, NatureType type, PokemonStatType inc, PokemonStatType dec, string incName, string decName)
    {
        Name = name;
        Type = type;
        IncreaseStat = inc;
        DecreaseStat = dec;
        IncreaseName = incName;
        DecreaseName = decName;
    }
}

public class Nature
{
    //せいかく名
    public string Name { get; private set; }

    //種類
    public NatureType Type { get; private set; }

    //上昇する能力値
    public PokemonStatType IncreaseAbility { get; private set; }
    public string IncreaseName { get; private set; }

    //下降する能力値
    public PokemonStatType DecreaseAbility { get; private set; }
    public string DecreaseName { get; private set; }

    private Nature(NatureData data)
    {
        Name = data.Name;
        Type = data.Type;
        IncreaseAbility = data.IncreaseStat;
        DecreaseAbility = data.DecreaseStat;
        IncreaseName = data.IncreaseName;
        DecreaseName = data.DecreaseName;
    }

    //Natureの取得
    public static Nature Create(string name)
    {
        if (!natureTable.TryGetValue(name, out var data))
        {
            Debug.LogWarning($"不明な性格名: {name} → がんばりや");
            data = natureTable["がんばりや"];
        }

        return new Nature(data);
    }


    //========== 内部データ構造の定義 ==========
    //Key:日本語名 Value:NatureData
    //NatureDataの順番 => 日本語名、NatureType、上昇能力、下降能力、上昇能力名、下降能力名
    private static readonly Dictionary<string, NatureData> natureTable
    = new Dictionary<string, NatureData>
{
    // ===== こうげき↑ =====
    { "さみしがり", new NatureData("さみしがり", NatureType.Lonely,   PokemonStatType.Attack,         PokemonStatType.Defense,        "こうげき", "ぼうぎょ") },
    { "いじっぱり", new NatureData("いじっぱり", NatureType.Adamant,  PokemonStatType.Attack,         PokemonStatType.SpecialAttack,  "こうげき", "とくこう") },
    { "やんちゃ",   new NatureData("やんちゃ",   NatureType.Naughty,  PokemonStatType.Attack,         PokemonStatType.SpecialDefense, "こうげき", "とくぼう") },
    { "ゆうかん",   new NatureData("ゆうかん",   NatureType.Brave,    PokemonStatType.Attack,         PokemonStatType.Speed,          "こうげき", "すばやさ") },

    // ===== ぼうぎょ↑ =====
    { "ずぶとい",   new NatureData("ずぶとい",   NatureType.Bold,     PokemonStatType.Defense,        PokemonStatType.Attack,         "ぼうぎょ", "こうげき") },
    { "わんぱく",   new NatureData("わんぱく",   NatureType.Impish,   PokemonStatType.Defense,        PokemonStatType.SpecialAttack,  "ぼうぎょ", "とくこう") },
    { "のうてんき", new NatureData("のうてんき", NatureType.Lax,      PokemonStatType.Defense,        PokemonStatType.SpecialDefense, "ぼうぎょ", "とくぼう") },
    { "のんき",     new NatureData("のんき",     NatureType.Relaxed,  PokemonStatType.Defense,        PokemonStatType.Speed,          "ぼうぎょ", "すばやさ") },

    // ===== とくこう↑ =====
    { "ひかえめ",   new NatureData("ひかえめ",   NatureType.Modest,   PokemonStatType.SpecialAttack,  PokemonStatType.Attack,         "とくこう", "こうげき") },
    { "おっとり",   new NatureData("おっとり",   NatureType.Mild,     PokemonStatType.SpecialAttack,  PokemonStatType.Defense,        "とくこう", "ぼうぎょ") },
    { "うっかりや", new NatureData("うっかりや", NatureType.Rash,     PokemonStatType.SpecialAttack,  PokemonStatType.SpecialDefense, "とくこう", "とくぼう") },
    { "れいせい",   new NatureData("れいせい",   NatureType.Quiet,    PokemonStatType.SpecialAttack,  PokemonStatType.Speed,          "とくこう", "すばやさ") },

    // ===== とくぼう↑ =====
    { "おだやか",   new NatureData("おだやか",   NatureType.Calm,     PokemonStatType.SpecialDefense, PokemonStatType.Attack,         "とくぼう", "こうげき") },
    { "おとなしい", new NatureData("おとなしい", NatureType.Gentle,   PokemonStatType.SpecialDefense, PokemonStatType.Defense,        "とくぼう", "ぼうぎょ") },
    { "しんちょう", new NatureData("しんちょう", NatureType.Careful,  PokemonStatType.SpecialDefense, PokemonStatType.SpecialAttack,  "とくぼう", "とくこう") },
    { "なまいき",   new NatureData("なまいき",   NatureType.Sassy,    PokemonStatType.SpecialDefense, PokemonStatType.Speed,          "とくぼう", "すばやさ") },

    // ===== すばやさ↑ =====
    { "おくびょう", new NatureData("おくびょう", NatureType.Timid,    PokemonStatType.Speed,          PokemonStatType.Attack,         "すばやさ", "こうげき") },
    { "せっかち",   new NatureData("せっかち",   NatureType.Hasty,    PokemonStatType.Speed,          PokemonStatType.Defense,        "すばやさ", "ぼうぎょ") },
    { "ようき",     new NatureData("ようき",     NatureType.Jolly,    PokemonStatType.Speed,          PokemonStatType.SpecialAttack,  "すばやさ", "とくこう") },
    { "むじゃき",   new NatureData("むじゃき",   NatureType.Naive,    PokemonStatType.Speed,          PokemonStatType.SpecialDefense, "すばやさ", "とくぼう") },

    // ===== 補正なし =====
    { "がんばりや", new NatureData("がんばりや", NatureType.Hardy,    PokemonStatType.HP,             PokemonStatType.HP,             "", "") },
    { "すなお",     new NatureData("すなお",     NatureType.Docile,   PokemonStatType.HP,             PokemonStatType.HP,             "", "") },
    { "てれや",     new NatureData("てれや",     NatureType.Bashful,  PokemonStatType.HP,             PokemonStatType.HP,             "", "") },
    { "きまぐれ",   new NatureData("きまぐれ",   NatureType.Quirky,   PokemonStatType.HP,             PokemonStatType.HP,             "", "") },
    { "まじめ",     new NatureData("まじめ",     NatureType.Serious,  PokemonStatType.HP,             PokemonStatType.HP,             "", "") },
};

    /// <summary>
    /// 性格補正倍率の取得
    /// </summary></summary>
    public float GetModifier(PokemonStatType stat)
    {
        if (stat == IncreaseAbility) return 1.1f;
        if (stat == DecreaseAbility) return 0.9f;
        return 1.0f;
    }
}
