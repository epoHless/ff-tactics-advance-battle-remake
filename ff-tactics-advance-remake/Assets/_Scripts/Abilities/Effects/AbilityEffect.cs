using System.Collections;
using UnityEngine;

[System.Serializable]
public abstract class AbilityEffect
{
    [HideInInspector] public string name;

    public AbilityEffect()
    {
        name = GetType().ToString();
    }
    
    #region Methods

    public virtual IEnumerable Execute(AbilityData abilityData, Character _caster, Character _target) { yield return null; }

    #endregion
}