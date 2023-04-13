using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{
    [SerializeField] private Vector3 offset;
    
    public override IEnumerator Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        yield return DamageInfo.Instance.ShowInfo(_target.transform.position + offset, CombatSystem.DealDamage(abilityData, _caster, _target));
    }
}
