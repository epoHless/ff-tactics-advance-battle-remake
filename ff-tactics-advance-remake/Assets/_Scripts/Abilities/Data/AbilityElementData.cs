using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityElement_", menuName = "Game/Abilities/New Ability Element", order = 0)]
public class AbilityElementData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    
    [field: SerializeField] public List<AbilityElementData> NullAgainstElements { get; private set; }
    [field: SerializeField] public List<AbilityElementData> WeakAgainstElements { get; private set; }
    [field: SerializeField] public List<AbilityElementData> StrongAgainstElements { get; private set; }
}
