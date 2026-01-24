using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Œ»İ‚Ìó‹µ
/// </summary>
public class BattleContext
{
    public System.Random Random { get; }


    public BattleContext(int seed)
    {
        Random = new System.Random(seed);
    }

    public void AddLog(string log)
    {

    }
}
