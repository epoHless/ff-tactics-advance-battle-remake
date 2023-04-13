using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class ParticleEffect : AbilityEffect
{
    #region Properties

    [field: SerializeField] public EParticlePosition ParticlePosition { get; private set; }
    [field: SerializeField] public AbilityParticle Particle { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
    
    #endregion

    public override IEnumerator Execute(AbilityData abilityData, Character _caster, Character _target)
    {
        var particleCopy = GameObject.Instantiate(Particle.gameObject, ParticlePosition == EParticlePosition.CASTER ?
            _caster.transform.position + Offset :
            _target.transform.position + Offset, quaternion.identity).GetComponent<AbilityParticle>();
        particleCopy.IsPlaying = true;
        yield return new WaitUntil(() => !particleCopy.IsPlaying);
    }
}

public enum EParticlePosition
{
    CASTER,
    TARGET
}