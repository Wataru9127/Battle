using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// インターフェース
/// </summary>
public interface IPokemon
{
    //名前
    string Name { get; }

    //レベル
    int Level { get; }

    //タイプ1
    PokemonType FirstType { get; }

    //タイプ2
    PokemonType SecondType { get; }

    //状態異常
    PokemonStatus Status { get; }

    //せいかく
    Nature Nature { get; }

    //現在の実数値を取得
    int GetActual(PokemonAbility ability);

    //ダメージを受ける
    void TakeDamage(int damage);

    //状態異常を受ける
    void TakeStatus(PokemonStatus status);

    //きぜつ判定
    bool IsFainted();
}
