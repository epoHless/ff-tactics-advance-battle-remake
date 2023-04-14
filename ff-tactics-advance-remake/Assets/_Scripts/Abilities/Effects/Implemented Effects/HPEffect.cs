using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEffect : AbilityEffect
{
    [SerializeField] private EHPEffect effect;
    [SerializeField] private Vector3 offset;
    
    public override IEnumerator Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        yield return DamageInfo.Instance.ShowInfo(_target.transform.position + offset,  effect == EHPEffect.DAMAGE ? 
            CombatSystem.DealDamage(abilityData, _caster, _target) : CombatSystem.Heal(abilityData, _caster, _target));
    }
}

public enum EHPEffect
{
    DAMAGE,
    HEAL
}