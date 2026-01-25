using System.Collections.Generic;

/// <summary>
/// フォルムチェンジ・メガシンカ用データ
/// </summary>
public class FormChangeData
{
    //名前
    public string FormName { get; }

    //タイプ
    public IReadOnlyList<PokemonType> Types { get; }

    //種族値
    public BaseStats BaseStats { get; }

    public FormChangeData(string formName, IReadOnlyList<PokemonType> types, BaseStats baseStats)
    {
        FormName = formName;
        Types = types;
        BaseStats = baseStats;
    }
}
