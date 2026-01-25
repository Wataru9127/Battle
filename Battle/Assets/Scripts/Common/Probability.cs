using UnityEngine;

/// <summary>
/// 確率計算
/// </summary>
namespace Common
{
    public static class Probability
    {
        //確率計算 => 引数より値が小さいかを判定する
        public static bool Check(System.Random random, float rate)
        {
            rate = Mathf.Clamp01(rate);
            return random.NextDouble() < rate;
        }
    }
}
