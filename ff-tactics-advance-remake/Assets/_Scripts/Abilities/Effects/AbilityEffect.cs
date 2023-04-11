using System.Collections;

public abstract class AbilityEffect
{
    #region Methods

    public virtual IEnumerable Execute(AbilityData abilityData, Character _caster, Character _target) { yield return null; }

    #endregion
}