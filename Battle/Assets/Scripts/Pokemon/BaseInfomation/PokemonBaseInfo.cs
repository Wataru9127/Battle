using System.Collections.Generic;

/// <summary>
/// 基本情報
/// </summary>
public class PokemonBaseInfo
{
    //種族ID
    public int SpeciesID { get; }

    //表示用名前
    public string Name { get; }

    //タイプ
    public IReadOnlyList<PokemonType> Types { get; private set; }

    /// <summary>
    /// タイプ変更 => フォルムチェンジ・メガシンカ用
    /// </summary>
    /// <param name="newTypes"></param>
    public void SetTypes(IReadOnlyList<PokemonType> newTypes)
    {
        Types = newTypes;
    }
}
