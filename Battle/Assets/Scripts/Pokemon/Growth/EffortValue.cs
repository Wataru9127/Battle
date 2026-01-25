using System.Collections.Generic;
using UnityEngine;

public class EffortValue
{
    public Dictionary<PokemonStatType, int> effort { get; private set; }

    public EffortValue(Dictionary<PokemonStatType, int> values)
    {
        this.effort = values;
    }

    /// <summary>
    /// Žw’è”\—Í‚Ì“w—Í’l‚ðŽæ“¾
    /// </summary>
    public int GetEffort(PokemonStatType ability)
    {
        return Mathf.Clamp(effort[ability], 0, 252);
    }
}
