using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability_", menuName = "Game/Abilities/New Ability", order = 0)]
public class AbilityData : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    
    [field: SerializeField] public float AbilityPower { get; private set; }
    [field: SerializeField] public EAbilityType AbilityType { get; private set; }
    
    [field: SerializeField] public int AbilityRange { get; private set; }

    [SerializeReference] public List<AbilityEffect> AbilityEffects = new List<AbilityEffect>();

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
}

public enum EAbilityType
{
    PHYSICAL,
    MAGICAL
}
