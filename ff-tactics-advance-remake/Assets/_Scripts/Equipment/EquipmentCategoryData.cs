using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentCategory_", menuName = "Game/Equipment/New Equipment Category", order = 0)]
public class EquipmentCategoryData : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
}
