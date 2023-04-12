using System.Collections;
using UnityEngine;

[System.Serializable]
public class ParticleEffect : AbilityEffect
{
    #region Properties

    [field: SerializeField] public EParticlePosition ParticlePosition { get; private set; }
    [field: SerializeField] public AbilityParticle Particle { get; private set; }
    
    #endregion

    public override IEnumerable Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        var particleCopy = GameObject.Instantiate(Particle.gameObject).GetComponent<AbilityParticle>();
        yield return particleCopy.PlayParticles(ParticlePosition == EParticlePosition.CASTER ? _caster.transform.position : _target.transform.position);
    }
}

public enum EParticlePosition
{
    CASTER,
    TARGET
}