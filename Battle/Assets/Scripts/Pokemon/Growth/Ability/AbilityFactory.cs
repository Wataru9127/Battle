using Battle;
using System;

/// <summary>
/// とくせい生成用
/// </summary>
public static class AbilityFactory
{
    public static PokemonAbility Create(
        AbilityID abilityId,
        BattleContext context,
        BattlePokemon owner)
    {

        if (abilityId == AbilityID.None)
        {
            return null;
        }


        return abilityId switch
        {
            // ===== フィールド系メイカー =====
            //AbilityID.Erekimeika => new Erekimeika(context, owner),
            //AbilityID.Gurasumeika => new Gurasumeika(context, owner),
            //AbilityID.Misutomeika => new Misutomeika(context, owner),
            //AbilityID.Saikomeika => new Saikomeika(context, owner),

            //// ===== 天候系メイカー =====
            //AbilityID.Amefurashi => new Amefurashi(context, owner),
            //AbilityID.Hideri => new Hideri(context, owner),
            //AbilityID.Sunaokoshi => new Sunaokoshi(context, owner),
            //AbilityID.Yukifurashi => new Yukifurashi(context, owner),

            //// ===== その他 =====
            //AbilityID.Ikaku => new Ikaku(context, owner),
            //AbilityID.Fuyuu => new Fuyuu(context, owner),

            _ => throw new ArgumentOutOfRangeException(
                nameof(abilityId),
                abilityId,
                "未対応のAbilityIdです"
            )
        };
    }
}
