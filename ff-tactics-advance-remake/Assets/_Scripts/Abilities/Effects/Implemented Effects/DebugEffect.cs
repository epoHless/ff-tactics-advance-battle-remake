using System.Collections;
using UnityEngine;

public class DebugEffect : AbilityEffect
{
    public override IEnumerator Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        Debug.Log("Ability Has Finished!");
        return base.Execute(abilityData, _caster, _target);
    }
}
