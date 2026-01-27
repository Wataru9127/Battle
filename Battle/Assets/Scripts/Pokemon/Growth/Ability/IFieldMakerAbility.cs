using Battle;
using Field;

/// <summary>
/// メイカー特性のインターフェース
/// </summary>
public interface IFieldMakerAbility
{
    //各メイカー特性にそれぞれのフィールドを展開させる
    FieldBase CreateField(BattleContext context, BattlePokemon owner);
}