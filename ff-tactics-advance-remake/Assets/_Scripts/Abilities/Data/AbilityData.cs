using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_", menuName = "Game/Abilities/New Ability", order = 0)]
public class AbilityData : ScriptableObject
{
    #region Properties

    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public bool IsMultiTarget { get; private set; }
    [field: SerializeField] public float AbilityPower { get; private set; }
    [field: SerializeField] public float ManaCost { private set; get; }
    [field: SerializeField] public EAbilityType AbilityType { get; private set; }
    [field: SerializeField] public int AbilityRange { get; private set; }

    [SerializeReference] public List<AbilityEffect> AbilityEffects = new List<AbilityEffect>();

    #endregion

    #region Unity Methods

    private void OnValidate()
    {
        if (AbilityRange < 0)
        {
            AbilityRange = 0;
        }else if (AbilityRange > 10)
        {
            AbilityRange = 10;
        }
    }

    #endregion

    #region Methods

    public IEnumerator Execute(Character _caster, Character _target)
    {
        _caster.BattleStatistics.CurrentMP.Value -= ManaCost;
        
        if (IsMultiTarget)
        {
            if (_target.HasNeighbors(out List<Character> _neighbors))
            {
                _neighbors.Add(_target);
                _neighbors.Reverse();
                
            }
            else
            {
                _neighbors.Add(_target);
            }
            
            foreach (var neighbor in _neighbors)
            {
                foreach (var effect in AbilityEffects)
                {
                    yield return effect.Execute(this, _caster, neighbor);
                }
            }
        }
        else
        {
            foreach (var effect in AbilityEffects)
            {
                yield return effect.Execute(this, _caster, _target);
                
            }
        }
    }

    #endregion
    
    #region Effects Add

    [ContextMenu("Add Particle")]
    public void AddParticle()
    {
        AbilityEffects.Add(new ParticleEffect());
    }
    
    [ContextMenu("Add Debug")]
    public void AddDebug()
    {
        AbilityEffects.Add(new DebugEffect());
    }
    
    [ContextMenu("Add Damage")]
    public void AddDamage()
    {
        AbilityEffects.Add(new HPEffect());
    }
    
    #endregion
}

public enum EAbilityType
{
    PHYSICAL,
    MAGICAL
}
