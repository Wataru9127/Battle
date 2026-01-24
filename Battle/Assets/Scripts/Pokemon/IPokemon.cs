using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// インターフェース
/// </summary>
public interface IPokemon
{
    // ===== 基本情報 =====
    string Name { get; }            //名前
    PokemonType FirstType { get; }  //タイプ1
    PokemonType SecondType { get; } //タイプ2

    // ===== ステータス（実数値）=====
    int CurrentHP { get; }          //HP
    int MaxHP { get; }              //最大HP
    PokemonStats Stats { get; }   //実数値

    int GetRank(PokemonStatType ability);

    // ===== バトル =====
    void TakeDamage(int damage);            //ダメージを受ける
    bool SetStatus(PokemonStats stats);   //実数値の設定
    bool IsFainted();                       //きぜつ判定
}
