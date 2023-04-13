using UnityEngine;

[CreateAssetMenu(fileName = "Character_", menuName = "Characters/New Character", order = 0)]
public class CharacterData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public RaceData RaceData { get; private set; }
    [field: SerializeField] public GameObject Model { get; private set; }
    [field: SerializeField] public Material CharacterMaterial { get; private set; }
}
