using UnityEngine;

[CreateAssetMenu(fileName = "Element_", menuName = "Game/Equipment/New Equipment Element", order = 0)]
public class ElementData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}
