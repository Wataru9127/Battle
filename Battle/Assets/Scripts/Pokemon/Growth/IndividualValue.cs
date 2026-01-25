using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̒l
/// </summary>
public class IndividualValue
{
    public Dictionary<PokemonStatType, int> individual { get; private set; }

    public IndividualValue(Dictionary<PokemonStatType, int> values)
    {
        this.individual = values;
    }

    /// <summary>
    /// �̒l�̎擾
    /// </summary>
    public int GetIndividual(PokemonStatType ability)
    {
        return Mathf.Clamp(individual[ability], 0, 31);
    }
}
