using System.Collections.Generic;
using UnityEngine;

public class EffortValue
{
    private Dictionary<PokemonAbility, int> effort;

    public EffortValue(Dictionary<PokemonAbility, int> values)
    {
        this.effort = values;
    }

    /// <summary>
    /// Žw’è”\—Í‚Ì“w—Í’l‚ðŽæ“¾
    /// </summary>
    public int GetEffort(PokemonAbility ability)
    {
        return Mathf.Clamp(effort[ability], 0, 252);
    }
}
